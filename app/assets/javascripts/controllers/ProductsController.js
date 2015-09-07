//length/width/height/weight #dimensions: 48.l X 14w X 12h (@ 42.lbs
var app;

app = angular.module('controllers', ['smart-table']);

app.factory('Resource', ['$q', '$filter', '$timeout', function ($q, $filter, $timeout) {

    //this would be the service to call your server, a standard bridge between your model an $http

    // the database (normally on your server)
    var randomsItems = createRandomItems();

    //fake call to the server, normally this service would serialize table state to send it to the server (with query parameters for example) and parse the response
    //in our case, it actually performs the logic which would happened in the server
    function getPage(start, number, params) {

        var deferred = $q.defer();

        var filtered = params.search.predicateObject ? $filter('filter')(randomsItems, params.search.predicateObject) : randomsItems;

        if (params.sort.predicate) {
            filtered = $filter('orderBy')(filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = filtered.slice(start, start + number);

        $timeout(function () {
            //note, the server passes the information about the data set size
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil(1000 / number)
            });
        }, 1500);


        return deferred.promise;
    }

    return {
        getPage: getPage
    };

}]);

function createRandomItems() {
    var rowCollection = [];
    var id = 1;
    for (id; id < 35; id++) {
        rowCollection.push(generateRandomItem());
    }
    return rowCollection
}

function generateRandomItem() {
    var names = ['Golf-1', 'G-supper', 'Meduim-Golf', 'Max-Golf'];
    var lengths = ['10.5', '15.5', '22.4', '33.6'];
    var widths = ['10.5', '15.5', '22.4', '33.6'];
    var id = 1;
    var name = names[Math.floor(Math.random() * 4)];
    var length = Math.floor(Math.random() * 2000);
    var width = Math.floor(Math.random() * 20);
    var height = Math.floor(Math.random() * 200);
    var weight = Math.floor(Math.random() * 200);

    return {

        name: name,
        length: length,
        width: width,
        height: height,
        weight: weight
    }
}

//$http.get('./service_products.json').success(function(data) {
//    return $scope.rowCollection = data;
//});
//http://coffeescript.org/
//http://js2.coffee/
//$http.get('./products.json').success(function(data) {
//    return $scope.rowCollection = data;
//});

app.factory('Resource', ['$q', '$filter', '$timeout', function ($q, $filter, $timeout) {

    //this would be the service to call your server, a standard bridge between your model an $http

    // the database (normally on your server)
    var randomsItems = createRandomItems();

    //fake call to the server, normally this service would serialize table state to send it to the server (with query parameters for example) and parse the response
    //in our case, it actually performs the logic which would happened in the server
    function getPage(start, number, params) {

        var deferred = $q.defer();

        var filtered = params.search.predicateObject ? $filter('filter')(randomsItems, params.search.predicateObject) : randomsItems;

        if (params.sort.predicate) {
            filtered = $filter('orderBy')(filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = filtered.slice(start, start + number);

        $timeout(function () {
            //note, the server passes the information about the data set size
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil(1000 / number)
            });
        }, 1500);


        return deferred.promise;
    }

    return {
        getPage: getPage
    };

}]);


//$http.get('./products.json').
//    success(function(data)
//{
//
//     return $scope.rowCollection = data;
//})



app.factory('Products', ['$resource', function ($resource) {
    //http://blog.joshsoftware.com/2014/07/17/building-web-apps-with-rails4-and-angularjs-in-15-minutes/
    return $resource('./products.json', {}, {
        query: {method: 'GET', isArray: true},
        create: {method: 'POST'}
    })
}]);



app.factory('Product', ['$resource', function ($resource) {
    return $resource('./product/:id.json', {}, {
        show: {method: 'GET'},
        update: {method: 'PUT', params: {id: '@id'}},
        delete: {method: 'DELETE', params: {id: '@id'}}
    });
}]);

//https://ng-rest-client.herokuapp.com/products.json

//http://getbootstrap.com/components/
//http://fdietz.github.io/recipes-with-angular-js/common-user-interface-patterns/displaying-a-modal-dialog.html
app.controller("ProductsController", [
    '$scope', '$routeParams', '$resource', '$location', 'flash', '$filter', '$http', 'Resource', 'Products', 'Product',
    function ($scope, $routeParams, $resource, $location, flash, $filter, $http, service, Products, Product) {

        $scope.displayedCollection = [];

        $scope.rowCollection = Products.query();
        console.log($scope.rowCollection);

        $scope.itemsByPage = 6;
        $scope.predicates = ['name', 'length', 'width', 'height', 'weight'];
        $scope.selectedPredicate = $scope.predicates[0];
        $scope.isLoading = true;
        $scope.isLoading = false;

        $scope.save = function (length) {
            var row;

            row = {
                name:   $scope.name,
                length: $scope.length,
                width:  $scope.width,
                height: $scope.height,
                weight: $scope.weight
            };
            if ($scope.edit) {//every thing works
                if (confirm("Are you sure that you want to create a new Product? " + $scope.name) == true) {
                    $scope.rowCollection.push(row);
                    Products.create({product: row}, function () {
                        $location.path('/');
                    }, function (error) {
                        console.log(error)
                    });
                }

            } else { //every thing works
                var msg = confirm("Are you sure that you want to save the changes of " + $scope.name + " ?")
                if (msg == true) {

                    var r   = $scope.rowCollection[$scope.current_index];
                    r.name  = $scope.name,
                    r.length= $scope.length,
                    r.width = $scope.width,
                    r.height= $scope.height,
                    r.weight= $scope.weight

                    Product.update({id: $scope.id}, {product: row}, function () {
                        $location.path('/');
                    }, function (error) {
                        alert(error)
                        console.log(error)
                    });
                }
            }
        }

        $scope.removeItem = function (row) {//data not working
            var index, r;
            r = true
            r = confirm("Are you sure to delete this item " + row.name + " ?");
            if (r === true) {
                index = $scope.rowCollection.indexOf(row);
                if (index !== -1) {
                    $scope.rowCollection.splice(index, 1);
                    Product.delete({id: row.id}, function () {
                        //$scope.rowCollection = Products.query()
                        //$location.path('/');
                    });
                }
            }
        };

        $scope.editProduct = function ( mode, row) {
            if (mode === 'new') {
                $scope.edit = true;
                $scope.incomplete = true;
                $scope.name = '';
                $scope.length = '';
                $scope.width = '';
                $scope.height = '';
                $scope.weight = '';
            } else {
                var id = row.id
                id = $scope.rowCollection.indexOf(row);
                $scope.edit = false;
                $scope.current_index = id
                $scope.id       = $scope.rowCollection[id].id;
                $scope.name     = $scope.rowCollection[id].name;
                $scope.length   = $scope.rowCollection[id].length;
                $scope.width    = $scope.rowCollection[id].width;
                $scope.height   = $scope.rowCollection[id].height;
                $scope.weight   = $scope.rowCollection[id].weight;
            }
        };


        var Product;
        $scope.name = '';
        $scope.length = '';
        $scope.width = '';
        $scope.height = '';
        $scope.weight = '';

        $scope.edit = true;
        $scope.error = false;
        $scope.incomplete = false;


        $scope.$watch('name', function () {
            return $scope.test();
        });
        $scope.$watch('length', function () {
            return $scope.test();
        });
        $scope.$watch('width', function () {
            return $scope.test();
        });
        $scope.$watch('height', function () {
            return $scope.test();
        });
        $scope.$watch('weight', function () {
            return $scope.test();
        });
        $scope.test = function () {
            if ($scope.edit && (!$scope.name.length
                || !$scope.length.length
                || !$scope.width.length
                || !$scope.height.length
                || !$scope.weight.length)) {
                return $scope.incomplete = true;
            } else {
                return $scope.incomplete = false;
            }
        };

        if ($routeParams.productId) {
            Product.get({
                productId: $routeParams.productId
            }, (function (product) {
                return $scope.product = product;
            }), (function (httpResponse) {
                $scope.product = null;
                return flash.error = "There is no product with ID " + $routeParams.productId;
            }));
        } else {
            $scope.product = {};
        }
        $scope.back = function () {
            return $location.path("/");
        };
        $scope.edit = function () {
            return $location.path("/products/" + $scope.product.id + "/edit");
        };
        $scope.cancel = function () {
            if ($scope.product.id) {
                return $location.path("/products/" + $scope.product.id);
            } else {
                return $location.path("/");
            }
        };


    }
]);