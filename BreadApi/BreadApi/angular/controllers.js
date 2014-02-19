'use strict';

/* Controllers */

angular.module('breadApp.controllers', []).
controller('breadController', function ($scope, breadApi) {

  $scope.loading = true;

  //todo POM should be a parameter
  $scope.getBreads = function (bread) {
    $scope.bread = bread;
    breadApi.breads($scope.bread).success(function (data) {
      $scope.breads = data;
      $scope.loading = false;
    });
  };

  $scope.getMethods = function (breadClass) {
    $scope.loading = true;
    $scope.methods = [];
    $scope.breadFormTitle = '';
    $scope.methodName = '';
    $scope.breadClass = breadClass;
    breadApi.methods($scope.bread, breadClass.Name).success(function (data) {
      $scope.methods = data;
      $scope.loading = false;
    });
  };
    
  $scope.getMethod = function (methodName) {
    $scope.loading = true;
    $scope.methodName = methodName;
    breadApi.params($scope.bread, $scope.breadClass.Name, methodName).success(function (data) {
      $scope.loading = false;
      //TODO: this DOM manipulation should into a custom directive...
      $scope.breadFormTitle = $scope.breadClass.Name + '.' + $scope.methodName + ':';
      $('#bread-form').html(''); //resets the form
      $('#bread-form').jsonForm({
        schema: data,
        onSubmit: function (errors, values) {
          if (errors) {
            $('#result').val('errors are present, unable to submite request:\n' + errors);
          } else {
            console.log(values);
            breadApi.invoke($scope.bread, $scope.breadClass.Name, methodName, values).success(function (data) {
              $('#result').html('suceed:<br/><pre><code>' + JSON.stringify(data, undefined, 2) + '</code></pre>');
            });
          }
        }
      });
    });
  };

  $scope.isSelectedBread = function (bread) {
    return bread == $scope.breadClass;
  };

  $scope.isSelectedMethod = function (methodName) {
    return methodName == $scope.methodName;
  };
});
