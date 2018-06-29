(function () {
    'use strict';
    
    //var SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/api/Tabela' };
    //var SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/api/Tabela' };
    var SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/api/Tabela' };
    
    angular.module('devTeamUtil').factory('TabelaFactory', TabelaFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        };
    });

    //TabelaFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    //function TabelaFactory($http, $rootScope, SETTINGS) {
    function TabelaFactory($http, $rootScope) {
        return {
            get: get,
            getById: getById,
            post: post,
            put: put,
            remove: remove
        };

        function get() {
            return $http.get(SETTINGS.SERVICE_URL, $rootScope.header);
        }

        function getById(id, index) {
            return $http.get(SETTINGS.SERVICE_URL + '/' + id + '/' + index, $rootScope.header);
        }

        function post(tabela) {
            return $http.post(SETTINGS.SERVICE_URL, tabela, $rootScope.header);
        }

        function put(tabela) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + tabela.id, tabela, $rootScope.header);
        }

        function remove(tabela) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + tabela.id, $rootScope.header);
        }
    }
})();