(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Campo' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Campo' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/Campo' };
    
    angular.module('devTeamUtil').factory('CampoFactory', CampoFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        };
    });

    //CampoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function CampoFactory($http, $rootScope, SETTINGS) {
    function CampoFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            post: post,
            put: put,
            remove: remove
        };

        function get(tabelaId) {
            return $http.get(SETTINGS.SERVICE_URL + '/byTable/' + tabelaId, $rootScope.header);
        }

        function getById(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id, $rootScope.header);
        }

        function post(campo) {
            return $http.post(SETTINGS.SERVICE_URL, campo, $rootScope.header);
        }

        function put(campo) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + campo.id, campo, $rootScope.header);
        }

        function remove(campo) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + campo.id, $rootScope.header);
        }
    }
})();