
controllers = angular.module('controllers')
controllers.controller("UsersController", [ '$scope', '$routeParams', '$location', '$resource',
  ($scope,$routeParams,$location,$resource)->

    $scope.fName = '';
    $scope.lname = '';
    $scope.passw1 = '';
    $scope.passw2 = '';
    $scope.users = [{id:1, fName:'Small',  lName:"Pege" },
      {id:2, fName:'Kim',   lName:"Pim"},
      {id:3, fName:'Sal',   lName:"Smith" },
      {id:4, fName:'Jack',  lName:"Jones" },
      {id:5, fName:'John',  lName:"Doe" },
      {id:6, fName:'Peter', lName:"Pan" }];
    $scope.edit = true;
    $scope.error = false;
    $scope.incomplete = false;

    $scope.editUser = (id) ->  if (id == 'new')
      $scope.edit = true;
      $scope.incomplete = true;
      $scope.fName = '';
      $scope.lName = '';
    else
      $scope.edit  = false;
      $scope.fName = $scope.users[id-1].fName;
      $scope.lName = $scope.users[id-1].lName;

    $scope.$watch('passw1', () -> $scope.test())
    $scope.$watch('passw2', () -> $scope.test())
    $scope.$watch('name' , () -> $scope.test())
    $scope.$watch('lname' , () -> $scope.test())

    $scope.test = () -> (

      if ($scope.passw1 != $scope.passw2)
        $scope.error = true;
      else
        $scope.error = false;

      $scope.incomplete = false;

      if ($scope.edit && (!$scope.fName.length ||
        !$scope.lName.length ||
        !$scope.passw1.length||
        !$scope.passw2.length)
      )
        $scope.incomplete = true
    )

])


