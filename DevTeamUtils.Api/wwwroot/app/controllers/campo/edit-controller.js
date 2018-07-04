(function () {
    'use strict';
    angular.module('devTeamUtil').controller('CampoEditCtrl', CampoEditCtrl);

    CampoEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'CampoFactory'];

    function CampoEditCtrl($routeParams, $filter, $location, CampoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.campo = {};

        vm.selectedTipo = { 'id': 0, 'description': 'Number(ORA) ou Integer(IFX)' };
        vm.selectedAtributo = { 'id': 0, 'description': 'Primary Key' };

        vm.Tipos = [
            { 'id': 0, 'description': 'Number(ORA) ou Integer(IFX)' },
            { 'id': 1, 'description': 'Varchar2(ORA) ou Varchar(IFX)' },
            { 'id': 2, 'description': 'Date(ORA) ou Datetime Year to Second(IFX)' },
            { 'id': 3, 'description': 'Number(ORA) ou Decimal(IFX)' },
            { 'id': 4, 'description': 'Long Row(ORA) ou Byte(IFX)' },
            { 'id': 5, 'description': 'Long(ORA) ou Text(IFX)' }
        ];

        vm.Atributos = [
            { 'id': 0, 'description': 'Primary Key' },
            { 'id': 1, 'description': 'Foreign Key' },
            { 'id': 2, 'description': 'Código' },
            { 'id': 3, 'description': 'Número' },
            { 'id': 4, 'description': 'Data/Hora' },
            { 'id': 5, 'description': 'Descrição' },
            { 'id': 6, 'description': 'Nome' },
            { 'id': 7, 'description': 'Valor' },
            { 'id': 8, 'description': 'Tipo' },
            { 'id': 9, 'description': 'Sim/Não' },
            { 'id': 10, 'description': 'Sigla' },
            { 'id': 11, 'description': 'Imagem/Arquivo' },
            { 'id': 12, 'description': 'Texto' },
            { 'id': 13, 'description': 'Quantidade' },
            { 'id': 14, 'description': 'Situação/Status' },
            { 'id': 15, 'description': 'Indicação' }
        ];

        activate();
        vm.save = save;
        vm.cancel = cancel;

        function activate() {
            getCampo();
        }

        function getCampo() {
            CampoFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.campo = response;
                /*var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.campo.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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
            vm.campo.tipoCampo = vm.selectedTipo.id;
            vm.campo.atributo = vm.selectedAtributo.id;

            CampoFactory.put(vm.campo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Campo <strong>" + response.nome + "</strong> cadastrado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Campo Cadastrada");
                $location.path('/campos/' + vm.campo.scriptId);
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
            $location.path('/campos/' + vm.campo.scriptId);
        }
    }
})();