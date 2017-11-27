(function () {
    'use strict';
    angular.module('chatDevTeam').controller('ChatCtrl', UserLoginCtrl);

    ChatCtrl.$inject = ['$routeParams', '$filter', '$location', 'ChatFactory'];

    function ChatCtrl($routeParams, $filter, $location, ChatFactory) {
        var vm = this;
        var id = $routeParams.id;

        vm.onlineUsers = [];

        vm.login = {
            apelido: '',
            senha: ''
        };

        vm.logout = logout;
        vm.cancel = cancel;

        activate();

        function activate() {
            getOnlineUsers();
        }

        function getOnlineUsers() {
            ChatFactory.onlineUsers()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.conexoes = response;
            }

            function fail(error) {
                if (error.status === 401)
                    toastr.error("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
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

        function logout() {
            ChatFactory.login(vm.login)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Usuário <strong>" + response.apelido + "</strong> deslogado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Usuário Logado');
                $location.path('/chat');
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