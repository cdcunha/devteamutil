(function () {
    'use strict';
    angular.module('devTeamUtil').controller('CampoListCtrl', CampoListCtrl);

    CampoListCtrl.$inject = ['$routeParams', '$location', 'CampoFactory'];
    
    function CampoListCtrl($routeParams, $location, CampoFactory) {
        var vm = this;
        vm.campos = [];
        vm.tabelaId = $routeParams.tabelaId;
        vm.backToTable = backToTable;
        
        activate();
        
        function activate() {
            getCampos();            
        }

        function getCampos() {
            CampoFactory.get(vm.tabelaId)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.campos = response;
                /*vm.campos.forEach(function (campo) {
                    if (campo.dataNascimento != null) {
                        var arDate = campo.dataNascimento.substring(0, 10).split('-');
                        campo.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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

        function backToTable() {
            $location.path('/tabelas/edit/' + vm.tabelaId);
        }
    }
})();