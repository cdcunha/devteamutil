angular.module('devTeamUtil').config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

angular.module('devTeamUtil')
    .config([
        '$compileProvider',
        function ($compileProvider) {
            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension|javascript):/);
            // Angular before v1.2 uses $compileProvider.urlSanitizationWhitelist(...)
        }
    ]);

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
                })
                /****************************************
                * Projetos
                *****************************************/
                .when('/projetos', {
                    controller: 'ProjetoListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Projeto/index.html'
                })
                .when('/projetos/create', {
                    controller: 'ProjetoCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Projeto/create.html'
                })
                .when('/projetos/edit/:id', {
                    controller: 'ProjetoEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Projeto/edit.html'
                })
                .when('/projetos/remove/:id', {
                    controller: 'ProjetoRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Projeto/remove.html'
                })
                .when('/projetos/download/:id/:nomeServidor', {
                    controller: 'ProjetoDownloadCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Projeto/download.html'
                })
                /****************************************
                * Tabelas
                *****************************************/
                .when('/tabelas', {
                    controller: 'TabelaListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Tabela/index.html'
                })
                .when('/tabelas/create', {
                    controller: 'TabelaCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Tabela/create.html'
                })
                .when('/tabelas/edit/:id/:index', {
                    controller: 'TabelaEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Tabela/edit.html'
                })
                .when('/tabelas/remove/:id/:index', {
                    controller: 'TabelaRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Tabela/remove.html'
                })
                /****************************************
                * Campos
                *****************************************/
                .when('/campos', {
                    controller: 'CampoListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/index.html'
                })
                .when('/campos/create', {
                    controller: 'CampoCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/create.html'
                })
                .when('/campos/edit/:id/:index', {
                    controller: 'CampoEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/edit.html'
                })
                .when('/campos/remove/:id/:index', {
                    controller: 'CampoRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/remove.html'
                });
        }
    ]);
