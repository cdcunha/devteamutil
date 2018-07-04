(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Passo' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Passo' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/Passo' };
    
    angular.module('devTeamUtil').factory('PassoFactory', PassoFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        };
    });

    //PassoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function PassoFactory($http, $rootScope, SETTINGS) {
    function PassoFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            post: post,
            put: put,
            remove: remove,
            download: download
        };

        function get() {
            return $http.get(SETTINGS.SERVICE_URL, $rootScope.header);
        }

        function getById(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id, $rootScope.header);
        }

        function post(passo) {
            return $http.post(SETTINGS.SERVICE_URL, passo, $rootScope.header);
        }

        function put(passo) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + passo.id, passo, $rootScope.header);
        }

        function remove(passo) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + passo.id, $rootScope.header);
        }

        function download(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/download/' + id, $rootScope.header);
        }
    }
})();