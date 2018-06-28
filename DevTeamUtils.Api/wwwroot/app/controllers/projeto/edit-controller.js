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
            tabelas: [],
            tabela: {
                campos: []
            },
            campo: {}
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
            vm.projeto.tabela = {
                campos: []
            };
            vm.projeto.tabela.tipos = ['Create', 'Insert', 'Update', 'Alter', 'Other'];
            clearCampo();
        }

        function clearCampo() {
            vm.projeto.indCampo = 0;
            vm.projeto.campo = {};
            vm.projeto.campo.atributos = ['Number(ORA) ou Integer(IFX)', 'Varchar2(ORA) ou Varchar(IFX)', 'Date(ORA) ou Datetime Year to Second(IFX)',
                'Number(ORA) ou Decimal(IFX)', 'Long Row(ORA) ou Byte(IFX)', 'Long(ORA) ou Text(IFX)'];
            vm.projeto.campo.tipoCampos = ['Primary Key', 'Foreign Key', 'Código', 'Número', 'Data/Hora', 'Descrição', 'Nome', 'Valor',
                'Tipo', 'Sim/Não', 'Sigla', 'Imagem/Arquivo', 'Texto', 'Quantidade', 'Situação/Status', 'Indicação'];
        }

        function getTabela(aIndex) {
            hideAddTable();
            //Testar Table: http://jsfiddle.net/Pixic/6Texj/
            //              http://jsfiddle.net/Pixic/sd5318kL/
            vm.projeto.tabelas = [];
            vm.projeto.tabela.nomeTabela = 'NOME TABELA 1';
            vm.projeto.tabela.descricaoTabela = 'DESC NOME TABELA 1';
            vm.projeto.tabelas.push(vm.projeto.tabela);
            vm.projeto.tabela.nomeTabela = 'NOME TABELA 2';
            vm.projeto.tabela.descricaoTabela = 'DESC NOME TABELA 2';
            vm.projeto.tabelas.push(vm.projeto.tabela);

            vm.projeto.tabelas[0].campos = [];
            vm.projeto.campo.nomeCampo = 'NOME CAMPO 1';
            vm.projeto.campo.descricaoCampo = 'DESC NOME CAMPO 1';
            vm.projeto.tabelas[0].campos.push(vm.projeto.campo);
            vm.projeto.campo.nomeCampo = 'NOME CAMPO 2';
            vm.projeto.campo.descricaoCampo = 'DESC NOME CAMPO 2';
            vm.projeto.tabelas[0].campos.push(vm.projeto.campo);

            if (aIndex = -1 || vm.projeto.tabelas == null) {
                return;
            }

            vm.projeto.indTabela = aIndex;
            vm.projeto.tabela = vm.projeto.tabelas[aIndex];
        }

        function getCampo(aIndex) {
            hideAddCampo();

            if (aIndex = -1 || vm.projeto.tabelas == null) {
                return;
            }
            if (vm.projeto.tabelas[vm.projeto.indTabela].campos == null) {
                return;
            }
            vm.projeto.indCampo = aIndex;
            vm.projeto.campo = vm.projeto.tabelas[vm.projeto.indTabela].campos[aIndCamp];
        }

        function addTabela() {
            if (vm.projeto.tabelas === null) {
                vm.projeto.tabelas = [];
            }
            vm.projeto.tabelas.push(vm.projeto.tabela);
            hideAddTable();
        }

        function addCampo() {
            if (vm.projeto.tabelas === null) {
                vm.projeto.tabelas = [];
                vm.projeto.tabelas.push(vm.projeto.tabela);
            }
            vm.projeto.indTabela = vm.projeto.tabelas.length -1;
            vm.projeto.tabelas[vm.projeto.indTabela].campos.push(vm.projeto.campo);
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