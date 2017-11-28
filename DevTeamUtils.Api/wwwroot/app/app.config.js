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
                    templateUrl: 'app/templates/Conexao/index.html'
                })
                .when('/conexoes/create', {
                    controller: 'ConexaoCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Conexao/create.html'
                })
                .when('/conexoes/edit/:id', {
                    controller: 'ConexaoEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Conexao/edit.html'
                })
                .when('/conexoes/remove/:id', {
                    controller: 'ConexaoRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Conexao/remove.html'
                })
                .when('/conexoes/download/:id/:nomeServidor', {
                    controller: 'ConexaoDownloadCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Conexao/download.html'
                })
                /****************************************
                * Contatos
                *****************************************/
                .when('/contatos', {
                    controller: 'ContatoListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Contato/index.html'
                })
                .when('/contatos/create', {
                    controller: 'ContatoCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Contato/create.html'
                })
                .when('/contatos/edit/:id', {
                    controller: 'ContatoEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Contato/edit.html'
                })
                .when('/contatos/remove/:id', {
                    controller: 'ContatoRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Contato/remove.html'
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ]);
