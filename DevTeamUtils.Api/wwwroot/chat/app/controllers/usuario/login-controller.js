(function () {
    'use strict';
    angular.module('chatDevTeam').controller('UserLoginCtrl', UserLoginCtrl);

    UserLoginCtrl.$inject = ['$filter', '$location', 'UserFactory'];

    function UserLoginCtrl($filter, $location, UserFactory) {
        var vm = this;
        vm.loginData = {
            apelido: '',
            senha: ''
        };

        vm.login = login;
        vm.cancel = cancel;
        
        function login() {
            UserFactory.login(vm.loginData)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Usuário <strong>" + response.apelido + "</strong> logado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Usuário Logado');
                $location.path('/chat/' + vm.loginData.apelido);
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
            $location.path('/login');
        }
    };
})();