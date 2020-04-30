# Recipe Manager

[![Build Status](https://dev.azure.com/fernandreu-public/RecipeManager/_apis/build/status/fernandreu.recipe-manager?branchName=master)](https://dev.azure.com/fernandreu-public/RecipeManager/_build/latest?definitionId=2&branchName=master)

This **WIP pre-alpha** project aims to serve as a recipe database and ingredient management system for the least experienced
cooks.

Very often after cooking a recipe, you can find yourself with many leftover ingredients and without a clear idea of what to
do with them before they expire. This is especially true for those ingredients that come in any sort of fixed-size container
in the supermarket (e.g. a bottle of milk or a pack of 12 eggs).

Recipe Manager's search engine allows you to find recipes whose ingredients meet certain criteria. This can be used to solve
situations like the one above (e.g. find me a recipe for which I need 300ml of milk and 5 eggs at most).

As part of ingredient management, future versions of the project will allow you to schedule all recipes you plan to do
throughout a period of time (e.g. a week), with a useful summary of the ingredients you will need (in case you are planning to
buy them all at once), or useful reminders when a recipe date approaches (in case you forgot to buy the ingredients).

# Try it live

The client-side blazor app is live on GitHub Pages:

https://fernando.andreu.info/recipe-manager/

To test the REST API of the backend server, visit the following link:

https://fernandreu.ddns.net:5001

Check the [wiki](https://github.com/fernandreu/recipe-manager/wiki/API-Documentation) to know how to interrogate the API.

# Other considerations

If you have issues getting JSON responses back, try with standalone API testing tools such as [Postman](https://www.getpostman.com/).

Bear in mind this project is in a **WIP pre-alpha** status and has the following limitations:

- The final design of the frontend has not yet been decided
- Recipes cannot be added to the database by users yet
- Only a few recipes created for testing purposes are available (try searching for `eggs` or `sugar`)
- There are no authentication / authorization services
