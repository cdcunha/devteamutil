(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ConexaoRemoveCtrl', ConexaoRemoveCtrl);

    ConexaoRemoveCtrl.$inject = ['$routeParams', '$filter', '$location', 'ConexaoFactory'];

    function ConexaoRemoveCtrl($routeParams, $filter, $location, ConexaoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.conexao = {};

        activate();
        vm.remove = remove;
        vm.cancel = cancel;

        function activate() {
            getConexao();
        }

        function getConexao() {
            ConexaoFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.conexao = response;
                var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.conexao.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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
            ConexaoFactory.remove(vm.conexao)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Conexão <strong>" + response.nome + "</strong> removido com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Conexão Removida');
                $location.path('/conexoes');
            }

            function fail(error) {
                if (error.status === 401)
                    toastr["error"]("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
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
        }

        function cancel() {
            $location.path('/conexoes');
        }
    };
})();