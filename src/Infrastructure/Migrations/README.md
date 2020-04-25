# How to work with migrations

## Using `dotnet ef`

This option seems better than the NuGet Package Manager console because it can be run from
any command prompt. This needs to be installed first (no need for admin rights for this):

```
dotnet tool install --global dotnet-ef
```

More info here:
https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

## Adding a migration

Command:

```
dotnet ef migrations add <name> --startup-project tests\IntegrationTests --project src\Infrastructure
```

The `--project` argument must point to the project containing the `AppDbContext` class. A `Migrations` 
folder will be created as part of this project.

The command actually needs a project which can be executed on its own; however, the `Infrastructure`
project is a .NET Standard one. This is why we pass the `--startup-project` argument. In particular,
the `IntegrationTests` project contains a reference to a design NuGet package which is also required
for the command to work.

## Removing the previous migration

Command:

```
dotnet ef migrations remove --startup-project tests\IntegrationTests --project src\Infrastructure
```
