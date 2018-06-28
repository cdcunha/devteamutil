(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ProjetoEditCtrl', ProjetoEditCtrl);

    ProjetoEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'ProjetoFactory'];

    function ProjetoEditCtrl($routeParams, $filter, $location, ProjetoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.projeto = {};

        vm.tabelaShow = false;
        vm.campoShow = false;

        activate();
        vm.save = save;
        vm.cancel = cancel;
        vm.addTabela = addTabela;        
        vm.addCampo = addCampo;
        vm.cancelaTabela = cancelaTabela;
        vm.cancelaCampo = cancelaCampo;

        function activate() {
            getProjeto();
        }

        function getProjeto() {
            ProjetoFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.projeto = response;
                vm.projeto.tabela = {};
                vm.projeto.tabela.tipos = ['Primary Key', 'Foreign Key', 'Código', 'Número', 'Data/Hora', 'Descrição', 'Nome', 'Valor',
                    'Tipo', 'Sim/Não', 'Sigla', 'Imagem/Arquivo', 'Texto', 'Quantidade', 'Situação/Status', 'Indicação'];
                vm.projeto.campo = {};
                vm.projeto.campo.tipoCampos = ['Create', 'Insert', 'Update', 'Alter', 'Other'];
                vm.projeto.campo.atributos = ['Number(ORA) ou Integer(IFX)', 'Varchar2(ORA) ou Varchar(IFX)', 'Date(ORA) ou Datetime Year to Second(IFX)',
                    'Number(ORA) ou Decimal(IFX)', 'Long Row(ORA) ou Byte(IFX)', 'Long(ORA) ou Text(IFX)'];
                /*var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.projeto.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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
            ProjetoFactory.put(vm.projeto)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Projeto <strong>" + response.nome + "</strong> cadastrado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Projeto Cadastrada");
                $location.path('/projetos');
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
            $location.path('/projetos');
        }

        function addTabela() {
            vm.tabelaShow = !vm.tabelaShow;
        }

        function addCampo() {
            vm.campoShow = !vm.campoShow;
        }

        function cancelaTabela() {
            vm.tabelaShow = !vm.tabelaShow;
        }

        function cancelaCampo() {
            vm.campoShow = !vm.campoShow;
        }
    }
})();