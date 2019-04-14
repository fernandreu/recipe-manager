# Recipe Manager
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

To check the latest build, visit the following link:

https://recipemanager.azurewebsites.net

To test the REST API of the backend server, visit the following link:

https://recipemanager.azurewebsites.net/api

If you have issues getting JSON responses back, try with standalone API testing tools such as [Postman](https://www.getpostman.com/).

Bear in mind this project is in a **WIP pre-alpha** status and has the following limitations:

- The final design of the frontend has not yet been decided
- Recipes cannot be added to the database by users yet
- Only a few recipes created for testing purposes are available (try searching for `eggs` or `sugar`)
- There are no authentication / authorization services

## API search queries

Searching recipes that fulfills some criteria is done in the `GET /api/recipes` endpoint, using the `search` query parameter.
For example, to find a recipe whose title contains the word `Fajitas`, try the following (`co` is shorthand for *contains*):

```
GET /api/recipes?search=title co fajitas
```

On the other hand, if you just want a recipe whose title is exactly `Fajitas`, try the `eq` (*equals*) operator instead:

```
GET /api/recipes?search=title eq fajitas
```

The most interesting property to query for is `ingredients`, namely with the `co` operator. On its simplest form, it will
list all recipes containing the particular ingredient you specified:

```
GET /api/recipes?search=ingredients co eggs
```

In addition, after specifying the ingredient name, you can also specify the quantity to search for. For example, to find
recipes needing less than 5 eggs:

```
GET /api/recipes?search=ingredients co eggs lt 5
```

The available operators are `lt`, `le`, `eq` `ge` and `gt`, which correspond to *less than*, *less than or equal to*, *equal
to*, *greater than or equal to* and *greater than* respectively.

When querying ingredients with mass / volume units, these can be appended after the quantity. For example, to find recipes
with more than 100g sugar:

```
GET /api/recipes?search=ingredients co sugar gt 100g
```

Several different units are available, such as `g`, `kg`, `ml`, `oz`, `tbsp`, etc.

The `search` query parameter can also be repeated multiple times to specify more than one condition that has to be fulfilled.
For example, to find recipes that contain more than 5 eggs but less than 500g sugar:

```
GET /api/recipes?search=ingredients co eggs gt 5&search=ingredients co sugar lt 500g
```

Other considerations:

- All text searches are case insensitive except when specifying units (e.g. `kg`, `tbsp`).

# Build Status

[![Build Status](https://dev.azure.com/fernandreu-public/RecipeManager/_apis/build/status/fernandreu.recipe-manager?branchName=master)](https://dev.azure.com/fernandreu-public/RecipeManager/_build/latest?definitionId=2&branchName=master)
