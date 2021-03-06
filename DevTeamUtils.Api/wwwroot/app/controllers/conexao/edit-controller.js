﻿(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ConexaoEditCtrl', ConexaoEditCtrl);

    ConexaoEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'ConexaoFactory'];

    function ConexaoEditCtrl($routeParams, $filter, $location, ConexaoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.conexao = {};

        activate();
        vm.save = save;
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
                vm.conexao.sistemas = ["Sishosp"];
                vm.conexao.bancosDeDados = ["Informix", "Oracle"];
                /*var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.conexao.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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
            ConexaoFactory.put(vm.conexao)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Conexão <strong>" + response.nome + "</strong> cadastrada com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Conexão Cadastrado");
                $location.path('/conexoes');
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
            $location.path('/conexoes');
        }
    }
})();