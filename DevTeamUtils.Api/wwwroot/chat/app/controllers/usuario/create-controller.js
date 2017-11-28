(function () {
    'use strict';
    angular.module('chatDevTeam').controller('UserCreateCtrl', UserCreateCtrl);
        /*.directive('ngModel', function attributeNgModelDirective() {
            return {
                require: 'ngModel',
                link: function (scope, el, attrs, ctrl) {
                    ctrl.$attributes = attrs;
                }
            };
    });*/
    
    UserCreateCtrl.$inject = ['$scope', '$location', 'UserFactory'];

    function UserCreateCtrl($scope, $location, UserFactory) {
        var vm = this;
        vm.users = [];
        vm.user = {
            id: 0,
            nome: '',
            apelido: '',
            senha: '',
            email: '',
            online: false,
            connectionId: '',
            dataCriacao: '',
            dataAlteracao: ''
        };
        
        vm.save = save;
        vm.cancel = cancel;

        /*var today = new Date();
        today.setYear(today.getFullYear() - 16);
        vm.minBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        today.setYear(today.getFullYear() - 90);
        vm.maxBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        */
        
        function save() {
            UserFactory.post(vm.user)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Usuário <strong>" + response.apelido + "</strong> cadastrado com sucesso<br/><br/><button type='button' class='btn clear'>Yes</button>", "Usuário Cadastrado");
                $location.path('/login');
            }

            function fail(error){
                if (error.status === 401)
                    toastr.error("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.statusText != '')
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

        /*var handleFileSelect = function (evt) {
            var file = evt.currentTarget.files[0];
            var reader = new FileReader();
            reader.onload = function (evt) {
                $scope.$apply(function ($scope) {
                    vm.user.image = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#file')).on('change', handleFileSelect);
        */
        function cancel() {
            //clearUser();
            $location.path('/login');
        }

        function clearUser() {
            vm.user = {
                id: 0,
                nome: '',
                apelido: '',
                senha: '',
                email: '',
                online: false,
                connectionId: '',
                dataCriacao: '',
                dataAlteracao: ''
            };
        }
    };
})();