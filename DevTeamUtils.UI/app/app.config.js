angular.module('devTeamUtil').config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

angular.module('devTeamUtil')
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider
            /****************************************
            * Conexao
            *****************************************/
            .when('/conexoes', {
                controller: 'ConexaoListCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/conexao/index.html'
            })
            .when('/conexoes/create', {
                controller: 'ConexaoCreateCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/conexao/create.html'
            })
            .when('/conexoes/edit/:id', {
                controller: 'ConexaoEditCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/conexao/edit.html'
            })
            .when('/conexoes/remove/:id', {
                controller: 'ConexaoRemoveCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/conexao/remove.html'
            })
            /****************************************
            * Contatos
            *****************************************/
            .when('/contatos', {
                controller: 'ContatoListCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/contato/index.html'
            })
            .when('/contatos/create', {
                controller: 'ContatoCreateCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/contato/create.html'
            })
            .when('/contatos/edit/:id', {
                controller: 'ContatoEditCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/contato/edit.html'
            })
            .when('/contatos/remove/:id', {
                controller: 'ContatoRemoveCtrl',
                controllerAs: 'vm',
                templateUrl: 'app/templates/contato/edit.html'
            })
            .otherwise({
                redirectTo: '/'
            });
        }
    ]);
