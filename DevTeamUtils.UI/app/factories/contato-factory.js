(function () {
    'use strict';

    var SETTINGS = { 'SERVICE_URL': 'http://localhost:56914/api/Contato' };

    angular.module('devTeamUtil').factory('ContatoFactory', ContatoFactory);

    angular.module('devTeamUtil').filter('filterYesNo', function () {
        return function (input) {
            return input ? 'Sim' : 'Não';
        }
    });

    //ContatoFactory.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function ContatoFactory($http, $rootScope) {
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

        function post(contato) {
            console.log(contato);
            return $http.post(SETTINGS.SERVICE_URL, contato, $rootScope.header);
        }

        function put(contato) {
            return $http.put(SETTINGS.SERVICE_URL + '/' + contato.id, contato, $rootScope.header);
        }

        function remove(contato) {
            return $http.delete(SETTINGS.SERVICE_URL + '/' + contato.id, $rootScope.header);
        }
    }
})();