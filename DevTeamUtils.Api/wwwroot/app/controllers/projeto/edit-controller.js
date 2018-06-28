(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ProjetoEditCtrl', ProjetoEditCtrl);

    ProjetoEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'ProjetoFactory'];

    function ProjetoEditCtrl($routeParams, $filter, $location, ProjetoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.projeto = {
            indTabela: 0,
            indCampo: 0,
            tabelas: []
        };

        vm.tabelaShow = false;
        vm.campoShow = false;

        activate();
        vm.save = save;
        vm.cancel = cancel;
        vm.hideAddTable = hideAddTable;
        vm.hideAddCampo = hideAddCampo;
        vm.getTabela = getTabela;
        vm.getCampo = getCampo;
        vm.addTabela = addTabela;
        vm.addCampo = addCampo;
        vm.editTabela = editTabela;
        vm.editCampo = editCampo;
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
                clearTabela();
                clearCampo();
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

        function hideAddTable() {
            vm.tabelaShow = !vm.tabelaShow;
        }

        function hideAddCampo() {
            vm.campoShow = !vm.campoShow;
        }

        function clearTabela() {
            vm.projeto.indTabela = 0;
            vm.projeto.tabela = {};
            vm.projeto.tabela.tipos = ['Primary Key', 'Foreign Key', 'Código', 'Número', 'Data/Hora', 'Descrição', 'Nome', 'Valor',
                'Tipo', 'Sim/Não', 'Sigla', 'Imagem/Arquivo', 'Texto', 'Quantidade', 'Situação/Status', 'Indicação'];
            cancelaCampo();
        }

        function clearCampo() {
            vm.projeto.indCampo = 0;
            vm.projeto.campo = {};
            vm.projeto.campo.atributos = ['Number(ORA) ou Integer(IFX)', 'Varchar2(ORA) ou Varchar(IFX)', 'Date(ORA) ou Datetime Year to Second(IFX)',
                'Number(ORA) ou Decimal(IFX)', 'Long Row(ORA) ou Byte(IFX)', 'Long(ORA) ou Text(IFX)'];
            vm.projeto.campo.tipoCampos = ['Create', 'Insert', 'Update', 'Alter', 'Other'];
        }

        function getTabela(aIndex) {
            hideAddTable();
            vm.projeto.indTabela = aIndex;
            vm.projeto.tabela = vm.projeto.tabelas[aIndex];
        }

        function getCampo(aIndex) {
            hideAddCampo();
            vm.projeto.indCampo = aIndex;
            vm.projeto.campo = vm.projeto.tabelas[vm.projeto.indTabela].campos[aIndCamp];
        }

        function addTabela() {
            vm.projeto.tabelas.push(vm.projeto.tabela);
            hideAddTable();
        }

        function addCampo() {
            vm.projeto.tabelas.campos.push(vm.projeto.campo);
            hideAddCampo();
        }

        function editTabela() {
            vm.projeto.tabelas[vm.projeto.indTabela] = vm.projeto.tabela;
            hideAddTable();
        }

        function editCampo() {
            vm.projeto.tabelas[vm.projeto.indTabela].campos[vm.projeto.indCampo] = vm.projeto.campo;
            hideAddCampo();
        }

        function cancelaTabela() {
            clearTabela();
            hideAddTable();
        }

        function cancelaCampo() {
            clearCampo();
            hideAddCampo();
        }
    }
})();