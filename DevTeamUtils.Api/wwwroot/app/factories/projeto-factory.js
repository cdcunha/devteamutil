(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Projeto' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Projeto' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/Projeto' };
    
    angular.module('devTeamUtil').factory('ProjetoFactory', ProjetoFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        };
    });

    //ProjetoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function ProjetoFactory($http, $rootScope, SETTINGS) {
    function ProjetoFactory($http, $rootScope) {
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

        function post(projeto) {
            return $http.post(SETTINGS.SERVICE_URL, projeto, $rootScope.header);
        }

        function put(projeto) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + projeto.id, projeto, $rootScope.header);
        }

        function remove(projeto) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + projeto.id, $rootScope.header);
        }

        function download(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/download/' + id, $rootScope.header);
        }
    }
})();