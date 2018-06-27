﻿(function () {
    'use strict';
    angular.module('devTeamUtil').controller('TabelaRemoveCtrl', TabelaRemoveCtrl);

    TabelaRemoveCtrl.$inject = ['$routeParams', '$filter', '$location', 'TabelaFactory'];

    function TabelaRemoveCtrl($routeParams, $filter, $location, TabelaFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.tabela = {};

        activate();
        vm.remove = remove;
        vm.cancel = cancel;

        function activate() {
            getTabela();
        }

        function getTabela() {
            TabelaFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.tabela = response;
                //var arDate = response.dataNascimento.substring(0, 10).split('-');
                //vm.tabela.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
            }

            function fail(error) {
                if (error.data === '') {
                    toastr["error"](error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                }
                else {
                    var erros = error.data;
                    for (var i = 0; i < erros.length; ++i) {
                        toastr["error"](erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                    }
                }
            }
        }

        function remove() {
            TabelaFactory.remove(vm.tabela)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Tabela <strong>" + response.nome + "</strong> removida com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Tabela Removido');
                $location.path('/projetos');
            }

            function fail(error) {
                if (error.status === 401)
                    toastr["error"]("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.statusText != '')
                        toastr.error(error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                    else {
                        if (error.data === null)
                            toastr["error"]("Erro indeterminado<br/><button type='button' class='btn clear'>Ok</button>", 'Erro indeterminado');
                        else {
                            var erros = error.data;
                            for (var i = 0; i < erros.length; ++i) {
                                toastr["error"](erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                            }
                        }
                    }
                }
            }
        }

        function cancel() {
            $location.path('/projetos');
        }
    };
})();