(function () {
    'use strict';
    angular.module('chatDevTeam').controller('UserListCtrl', UserListCtrl);

    UserListCtrl.$inject = ['UserFactory'];

    function UserListCtrl(UserFactory) {
        var vm = this;
        vm.users = [];
        
        activate();
        
        function activate() {
            getUsers();
        }

        function getUsers() {
            UserFactory.get()
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.users = response;
                /*vm.users.forEach(function (user) {
                    if (user.dataNascimento != null) {
                        var arDate = user.dataNascimento.substring(0, 10).split('-');
                        user.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
                    }
                });*/
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
    }
})();