'use strict';

/* Services */
angular.module('breadApp.services', []).
value('hostUrl', 'http://localhost:55181/').
value('breadApiUrl', 'http://localhost:55181/breads/').
service('breadApi', function ($http, breadApiUrl) {
  this.invoke = function (bread, breadClass, method) {
    return $http.post(breadApiUrl + bread + '/' + breadClass + '/' + method);
  };
  this.params = function (bread, breadClass, method) {
    return $http.post(breadApiUrl + bread + '/' + breadClass + '/' + method + '/params');
  };
  this.breads = function (bread) {
    return $http.post(breadApiUrl + bread);
  };
  this.methods = function (bread, breadClass) {
    return $http.post(breadApiUrl + bread + '/' + breadClass);
  };
});
