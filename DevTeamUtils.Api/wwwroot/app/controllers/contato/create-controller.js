(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ContatoCreateCtrl', ContatoCreateCtrl);

    ContatoCreateCtrl.$inject = ['$scope', '$location', 'ContatoFactory'];

    function ContatoCreateCtrl($scope, $location, ContatoFactory) {
        var vm = this;
        vm.contatos = [];
        vm.contato = {
            id: '',
            name: '',
            telefone: '',
            cargo: '',
            local: '',
            observacao: '',
            dataCriacao: '',
            dataAlteracao: ''
        };
        vm.save = save;
        vm.cancel = cancel;

        /*activate();

        function activate() {
            getTechnologies();
        }

        function getTechnologies() {
            TechnologyFactory.get()
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.technologies = response;
            }

            function fail(error) {
                if (error.status == 401)
                    toastr.error("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else
                    toastr.error("Sua requisição não pode ser processada<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição na Requisição');
            }
        }*/

        function save() {
            ContatoFactory.post(vm.contato)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Contato <strong>" + response.nome + "</strong> cadastrado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Contato Cadastrado");
                $location.path('/contatos');
            }

            function fail(error){
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

        /*var handleFileSelect = function (evt) {
            var file = evt.currentTarget.files[0];
            var reader = new FileReader();
            reader.onload = function (evt) {
                $scope.$apply(function ($scope) {
                    vm.contato.image = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#file')).on('change', handleFileSelect);
        */

        function cancel() {
            //clearContato();
            $location.path('/contatos');
        }

        function clearContato() {
            vm.contato = {
                id: '',
                name: '',
                telefone: '',
                cargo: '',
                local: '',
                observacao: '',
                dataCriacao: '',
                dataAlteracao: ''
            };
        }
    }
})();