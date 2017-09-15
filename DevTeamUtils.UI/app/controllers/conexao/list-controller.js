(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ConexaoListCtrl', ConexaoListCtrl);

    ConexaoListCtrl.$inject = ['ConexaoFactory'];

    function ConexaoListCtrl(ConexaoFactory) {
        var vm = this;
        vm.conexoes = [];
        
        activate();
        
        function activate() {
            getConexaos();
        }

        function getConexaos() {
            ConexaoFactory.get()
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.conexoes = response;
                vm.conexoes.forEach(function (conexao) {
                    if (conexao.dataNascimento != null) {
                        var arDate = conexao.dataNascimento.substring(0, 10).split('-');
                        conexao.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
                    }
                });
            }

            function fail(error) {
                if (error.status === 401)
                    toastr.error("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.data === '') {
                        toastr.error(error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                    }
                    else
                    {
                        var erros = error.data;
                        for (var i = 0; i < erros.length; ++i) {
                            toastr.error(erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                        }
                    }
                }
            }
        }
    }
})();