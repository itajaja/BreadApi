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
    $scope.selectedBread = breadClass;
    breadApi.methods($scope.bread, breadClass.Name).success(function (data) {
      $scope.methods = data;
      $scope.loading = false;
    });
  };
    
  $scope.isSelectedBread = function (bread) {
    return bread == $scope.selectedBread;
  };
});
