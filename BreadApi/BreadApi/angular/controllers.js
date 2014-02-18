'use strict';

/* Controllers */

angular.module('breadApp.controllers', []).
  controller('breadController', function ($scope) {

    $scope.alert = function (text) {
      alert(text);
    };

  });