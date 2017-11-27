(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Conexao' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Conexao' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/User' };
    
    angular.module('chatDevTeam').factory('ChatFactory', ChatFactory);

    angular.module('chatDevTeam').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        }
    });

    //ConexaoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function ConexaoFactory($http, $rootScope, SETTINGS) {
    function ChatFactory($http, $rootScope) {
        return {
            onlineUsers: onlineUsers,
            logout: logout
        }

        function onlineUsers() {
            return $http.put(SETTINGS.SERVICE_URL, $rootScope.header);
        }

        function logout() {
            return $http.get(SETTINGS.SERVICE_URL, $rootScope.header);
        }
    }
})();