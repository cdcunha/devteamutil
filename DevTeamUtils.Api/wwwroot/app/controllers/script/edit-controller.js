(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ScriptEditCtrl', ScriptEditCtrl);

    ScriptEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'ScriptFactory'];

    function ScriptEditCtrl($routeParams, $filter, $location, ScriptFactory) {
        var vm = this;
        var id = $routeParams.id;

        vm.selectedTipoScript = { 'id': 0, 'description': 'Create' };
        vm.selectedTipoObjeto = { 'id': 0, 'description': 'Tabela de Cadastro' };
        vm.TipoScripts = [
            { 'id': 0, 'description': 'Create' },
            { 'id': 1, 'description': 'Insert' },
            { 'id': 2, 'description': 'Update' },
            { 'id': 3, 'description': 'Alter' },
            { 'id': 4, 'description': 'Outros' }
        ];
        vm.TipoObjetos = [
            { 'id': 0, 'description': 'Tabela de Cadastro' },
            { 'id': 1, 'description': 'Tabela de Detalhe' },
            { 'id': 2, 'description': 'Tabela de Movimentação' },
            { 'id': 3, 'description': 'Tabela de Estáticas/Domínio' },
            { 'id': 4, 'description': 'Tabela de Logs de Operação' },
            { 'id': 5, 'description': 'Tabela Temporária' },
            { 'id': 6, 'description': 'View' },
            { 'id': 7, 'description': 'Sequence' },
            { 'id': 8, 'description': 'Function' },
            { 'id': 9, 'description': 'Stored Procedure' },
            { 'id': 10, 'description': 'Trigger Insert After' },
            { 'id': 11, 'description': 'Trigger Insert Before' },
            { 'id': 12, 'description': 'Trigger Update After' },
            { 'id': 13, 'description': 'Trigger Update Before' },
            { 'id': 14, 'description': 'Trigger Delete After' },
            { 'id': 15, 'description': 'Trigger Delete Before' },
            { 'id': 16, 'description': 'Package' },
            { 'id': 17, 'description': 'Job' }
        ];
        
        activate();
        vm.save = save;
        vm.cancel = cancel;
        vm.seeFields = seeFields;

        function activate() {
            getScript();
        }

        function getScript() {
            ScriptFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.script = response;
                /*var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.script.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
                */
            }

            function fail(error) {
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

        function save() {
            vm.script.tipoScript = vm.selectedTipoScript.id;
            vm.script.tipoObjeto = vm.selectedTipoObjeto.id;

            ScriptFactory.put(vm.script)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Script <strong>" + response.nome + "</strong> cadastrado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Script Cadastrada");
                $location.path('/scripts/' + vm.script.passoId);
            }

            function fail(error) {
                if (error.status === 401) {
                    toastr.error("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                } else {
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

        function cancel() {
            $location.path('/scripts/' + vm.script.passoId);
        }

        function seeFields() {
            $location.path('/campos/' + vm.script.id);
        }
    }
})();