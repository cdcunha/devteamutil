(function () {
    'use strict';
    angular.module('devTeamUtil').controller('TabelaListCtrl', TabelaListCtrl);

    TabelaListCtrl.$inject = ['$routeParams', '$location', 'TabelaFactory'];
    
    function TabelaListCtrl($routeParams, $location, TabelaFactory) {
        var vm = this;
        vm.tabelas = [];
        vm.projetoId = $routeParams.projetoId;

        vm.backToProject = backToProject;
        
        activate();
        
        function activate() {
            getTabelas();            
        }

        function getTabelas() {
            TabelaFactory.get(vm.projetoId)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.tabelas = response;
                /*vm.tabelas.forEach(function (tabela) {
                    if (tabela.dataNascimento != null) {
                        var arDate = tabela.dataNascimento.substring(0, 10).split('-');
                        tabela.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
                    }
                });*/
            }

            function fail(error) {
                if (error.status === 401)
                    toastr.error("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.statusText !== '')
                        toastr.error(error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                    else {
                        if (error.data === null)
                            toastr["error"]("Erro indeterminado<br/><button type='button' class='btn clear'>Ok</button>", 'Erro indeterminado');
                        else {
                            var erros = error.data;
                            for (var i = 0; i < erros.length; ++i) {
                                toastr.error(erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                            }
                        }
                    }
                }
            }
        }

        function backToProject() {
            $location.path('/projetos/edit/' + vm.projetoId);
        }
    }
})();