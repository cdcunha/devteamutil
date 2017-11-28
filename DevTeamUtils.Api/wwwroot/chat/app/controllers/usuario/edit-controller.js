(function () {
    'use strict';
    angular.module('chatDevTeam').controller('UserEditCtrl', UserEditCtrl);

    UserEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'UserFactory'];

    function UserEditCtrl($routeParams, $filter, $location, UserFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.user = {};

        activate();
        vm.save = save;
        vm.cancel = cancel;

        function activate() {
            getUser();
        }

        function getUser() {
            UserFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.user = response;
                /*var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.user.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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
            UserFactory.put(vm.user)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("User <strong>" + response.apelido + "</strong> cadastrada com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Usuário Cadastrado");
                $location.path('/login');
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
            $location.path('/login');
        }
    };
})();