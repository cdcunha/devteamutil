(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Conexao' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Conexao' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/User' };
    
    angular.module('chatDevTeam').factory('UserFactory', UserFactory);

    angular.module('chatDevTeam').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        }
    });

    //ConexaoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function UserFactory($http, $rootScope, SETTINGS) {
    function UserFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            post: post,
            put: put,
            remove: remove,
            login: login
        }

        function get() {
            return $http.get(SETTINGS.SERVICE_URL, $rootScope.header);
        }

        function getById(id) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id, $rootScope.header);
        }

        function post(user) {
            return $http.post(SETTINGS.SERVICE_URL, user, $rootScope.header);
        }

        function put(user) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + user.id, user, $rootScope.header);
        }

        function remove(user) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + user.id, $rootScope.header);
        }

        function login(login) {
            return $http.post(SETTINGS.SERVICE_URL + '/' + login, $rootScope.header);
        }
    }
})();