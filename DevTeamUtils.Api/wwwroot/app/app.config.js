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
                * Passos
                *****************************************/
                .when('/passos', {
                    controller: 'PassoListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Passo/index.html'
                })
                .when('/passos/create', {
                    controller: 'PassoCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Passo/create.html'
                })
                .when('/passos/edit/:id', {
                    controller: 'PassoEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Passo/edit.html'
                })
                .when('/passos/remove/:id', {
                    controller: 'PassoRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Passo/remove.html'
                })
                .when('/passos/download/:id/:nomeServidor', {
                    controller: 'PassoDownloadCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Passo/download.html'
                })
                /****************************************
                * Scripts
                *****************************************/
                .when('/scripts/:passoId', {
                    controller: 'ScriptListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Script/index.html'
                })
                .when('/scripts/create/:passoId', {
                    controller: 'ScriptCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Script/create.html'
                })
                .when('/scripts/edit/:id', {
                    controller: 'ScriptEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Script/edit.html'
                })
                .when('/scripts/remove/:id', {
                    controller: 'ScriptRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Script/remove.html'
                })
                /****************************************
                * Campos
                *****************************************/
                .when('/campos/:scriptId', {
                    controller: 'CampoListCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/index.html'
                })
                .when('/campos/create/:scriptId', {
                    controller: 'CampoCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/create.html'
                })
                .when('/campos/edit/:id', {
                    controller: 'CampoEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/edit.html'
                })
                .when('/campos/remove/:id', {
                    controller: 'CampoRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Campo/remove.html'
                })
                /****************************************
                * Ifxerros
                *****************************************/
                .when('/ifxerros', {
                    controller: 'IfxerroSearchCtrl',
                    controllerAs: 'vm',
                    templateUrl: 'app/templates/Ifxerro/index.html'
                });
        }
    ]);
