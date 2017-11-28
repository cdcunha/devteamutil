(function () {
    'use strict';
    
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
            return $http.get(SETTINGS.SERVICE_URL + '/online', $rootScope.header);
        }

        function logout(login) {
            return $http.get(SETTINGS.SERVICE_URL + '/Logout', login, $rootScope.header);
        }
    }
})();