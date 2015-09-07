#\app\assets\javascripts\app.coffee

receta = angular.module('receta', [
  'templates',
  'ngRoute',
  'ngResource',
  'controllers',
  'angular-flash.service',
  'angular-flash.flash-alert-directive'
  'smart-table'
])

receta.config(['$routeProvider', 'flashProvider',
  ($routeProvider, flashProvider)->
    flashProvider.errorClassnames.push("alert-danger")
    flashProvider.warnClassnames.push("alert-warning")
    flashProvider.infoClassnames.push("alert-info")
    flashProvider.successClassnames.push("alert-success")


    $routeProvider
    .when('/',
      templateUrl: "products.html"
      controller: 'ProductsController'
    ).when('/recipes/new',
      templateUrl: "form.html"
      controller: 'RecipeController'
    ).when('/recipes/:recipeId',
      templateUrl: "show.html"
      controller: 'RecipeController'
    ).when('/recipes/:recipeId/edit',
      templateUrl: "form.html"
      controller: 'RecipeController'
    ).when('/users/:index3',
      templateUrl: "index3.html"
      controller: 'UsersController'
    )
])

controllers = angular.module('controllers', [])
