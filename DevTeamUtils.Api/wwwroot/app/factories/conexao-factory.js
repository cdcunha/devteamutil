(function () {
    'use strict';
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:51640/api/Conexao' };
    var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Conexao' };

    angular.module('devTeamUtil').factory('ConexaoFactory', ConexaoFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        }
    });

    //ConexaoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function ConexaoFactory($http, $rootScope, SETTINGS) {
    function ConexaoFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            post: post,
            put: put,
            remove: remove
        }

        function get() {
            return $http.get(SETTINGS.SERVICE_URL, $rootScope.header);
        }

        function getById(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id, $rootScope.header);
        }

        function post(conexao) {
            return $http.post(SETTINGS.SERVICE_URL, conexao, $rootScope.header);
        }

        function put(conexao) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + conexao.id, conexao, $rootScope.header);
        }

        function remove(conexao) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + conexao.id, $rootScope.header);
        }
    }
})();