(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ContatoRemoveCtrl', ContatoRemoveCtrl);

    ContatoRemoveCtrl.$inject = ['$routeParams', '$filter', '$location', 'ContatoFactory'];

    function ContatoRemoveCtrl($routeParams, $filter, $location, ContatoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.contato = {};

        activate();
        vm.remove = remove;
        vm.cancel = cancel;

        function activate() {
            getContato();
        }

        function getContato() {
            ContatoFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.contato = response;
            }

            function fail(error) {
                if (error.data === '') {
                    toastr["error"](error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                }
                else {
                    if (error.statusText !== null)
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

        function remove() {
            ContatoFactory.remove(vm.contato)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Contato <strong>" + response.nome + "</strong> removido com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Contato Removido');
                $location.path('/contatos');
            }

            function fail(error) {
                if (error.status === 401)
                    toastr["error"]("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.statusText !== '')
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
            $location.path('/contatos');
        }
    }
})();