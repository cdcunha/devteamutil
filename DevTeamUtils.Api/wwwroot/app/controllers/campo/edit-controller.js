(function () {
    'use strict';
    angular.module('devTeamUtil').controller('CampoEditCtrl', CampoEditCtrl);

    CampoEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'CampoFactory'];

    function CampoEditCtrl($routeParams, $filter, $location, CampoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.campo = {};

        vm.atributos = ['Number(ORA) ou Integer(IFX)', 'Varchar2(ORA) ou Varchar(IFX)', 'Date(ORA) ou Datetime Year to Second(IFX)',
            'Number(ORA) ou Decimal(IFX)', 'Long Row(ORA) ou Byte(IFX)', 'Long(ORA) ou Text(IFX)'];
        vm.tipoCampos = ['Primary Key', 'Foreign Key', 'Código', 'Número', 'Data/Hora', 'Descrição', 'Nome', 'Valor',
            'Tipo', 'Sim/Não', 'Sigla', 'Imagem/Arquivo', 'Texto', 'Quantidade', 'Situação/Status', 'Indicação'];

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
            CampoFactory.put(vm.campo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Campo <strong>" + response.nome + "</strong> cadastrado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Campo Cadastrada");
                $location.path('/campos/' + vm.campo.tabelaId);
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
            $location.path('/campos/' + vm.campo.tabelaId);
        }
    }
})();