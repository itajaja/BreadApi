'use strict';

/* Services */
angular.module('breadApp.services', []).
value('hostUrl', 'http://localhost:55181/').
service('breadApi', function ($http, hostUrl) {
  this.invoke = function (bread, breadClass, method) {
    return $http.post(hostUrl + bread + '/' + breadClass + '/' + method);
  };
  this.params = function (bread, breadClass, method) {
    return $http.post(hostUrl + bread + '/' + breadClass + '/' + method + '/params');
  };
  this.breads = function (bread) {
    return $http.post(hostUrl + bread);
  };
  this.methods = function (bread, breadClass) {
    return $http.post(hostUrl + bread + '/' + breadClass);
  };
});
