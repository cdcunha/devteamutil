(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Script' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Script' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/Script' };
    
    angular.module('devTeamUtil').factory('ScriptFactory', ScriptFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        };
    });

    //ScriptFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function ScriptFactory($http, $rootScope, SETTINGS) {
    function ScriptFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            post: post,
            put: put,
            remove: remove
        };

        function get(passoId) {
            return $http.get(SETTINGS.SERVICE_URL + '/idPasso/' + passoId, $rootScope.header);
        }

        function getById(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id, $rootScope.header);
        }

        function post(script) {
            return $http.post(SETTINGS.SERVICE_URL, script, $rootScope.header);
        }

        function put(script) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + script.id, script, $rootScope.header);
        }

        function remove(script) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + script.id, $rootScope.header);
        }
    }
})();