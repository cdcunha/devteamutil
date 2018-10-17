(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Ifxerro' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Ifxerro' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/Ifxerror' };
    
    angular.module('devTeamUtil').factory('IfxerroFactory', IfxerroFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        };
    });

    //IfxerroFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function IfxerroFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            getByCode: getByCode,
            post: post,
            put: put,
            remove: remove
        };

        function get() {
            return $http.get(SETTINGS.SERVICE_URL, $rootScope.header);
        }

        function getById(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id, $rootScope.header);
        }

        function getByCode(code) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + code, $rootScope.header);
        }

        function post(ifxerro) {
            return $http.post(SETTINGS.SERVICE_URL, ifxerro, $rootScope.header);
        }

        function put(ifxerro) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + ifxerro.id, ifxerro, $rootScope.header);
        }

        function remove(ifxerro) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + ifxerro.id, $rootScope.header);
        }
    }
})();