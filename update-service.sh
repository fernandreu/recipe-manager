#!/usr/bin/env bash

# Variables needed for this:
BUILD_ID=$1
ref="$BUILD_ID"
folder="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"
serviceName=recipe-manager-api
refPath="${folder}/${ref}"
newLine="ExecStart=dotnet ${refPath}/WebApi.dll"
echo New line is: ${newLine}
if [[ -z "$newLine" ]]; then
    echo "Wrong newLine; aborting"
    exit 1
fi

downloadArtifact () {
    artifactName=$1
    destination=$2
    apiUrl="https://dev.azure.com/fernandreu-public/RecipeManager/_apis/build/builds/${ref}/artifacts?artifactName=${artifactName}&api-version=4.1"
    downloadUrl=$(curl -s ${apiUrl} | grep -Pom 1 '"downloadUrl": *"\K[^"]*')
    downloadPath="/tmp/artifact-${artifactName}.zip"
    wget -q -O ${downloadPath} ${downloadUrl}
    artifactPath="/tmp/${artifactName}"
    rm -rf ${artifactPath}
    unzip -qq ${downloadPath} -d "/tmp"
    cp -rf "${artifactPath}/." ${destination}
    
    # Remove downloaded / temporarily extracted files
    rm -rf ${artifactPath} 
    rm -rf ${downloadPath}
}

# Re-run database
downloadArtifact "DockerCompose" ${folder}
composerPath="${folder}/docker-compose.yml"
composerExternalPath="${folder}/docker-compose.External.yml"
docker-compose -f ${composerPath} -f ${composerExternalPath} down
docker-compose -f ${composerPath} -f ${composerExternalPath} up -d

# Download binaries
mkdir ${refPath}
downloadArtifact "WebAPI" ${refPath}

# Get the current deployment path from the service
serviceFile="/etc/systemd/system/${serviceName}.service"
line=$(sed -n -e '/^ExecStart=/p' ${serviceFile})
previousPath=$(echo "$line" | awk '{print $(NF) }')
previousPath=$(dirname "${previousPath}")
echo Service previously executing from: ${previousPath}

# Replace that path with the new one
echo New line is: ${newLine}
escaped=$(sed -e 's/[\/&]/\\&/g' <<< ${newLine})
echo Escaped: ${escaped}
sudo sed -i "/ExecStart=/c\\${escaped}" "${serviceFile}"
escaped=$(sed -e 's/[\/&]/\\&/g' <<< "WorkingDirectory=${refPath}")
sudo sed -i "/WorkingDirectory=/c\\${escaped}" "${serviceFile}"

# Restart service
echo Restarting service
sudo systemctl daemon-reload
sudo systemctl restart ${serviceName}

# Delete previous source code
echo Deleting previous binaries in: ${previousPath}
sudo rm -rf ${previousPath}
