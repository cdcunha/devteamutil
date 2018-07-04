(function () {
    'use strict';
    angular.module('devTeamUtil').controller('PassoEditCtrl', PassoEditCtrl);

    PassoEditCtrl.$inject = ['$routeParams', '$filter', '$location', 'PassoFactory'];

    function PassoEditCtrl($routeParams, $filter, $location, PassoFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.passo = {};

        activate();
        vm.save = save;
        vm.cancel = cancel;
        vm.seeTables = seeTables;
        
        function activate() {
            getPasso();
        }

        function getPasso() {
            PassoFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.passo = response;                
                /*var arDate = response.dataNascimento.substring(0, 10).split('-');
                vm.passo.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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
            PassoFactory.put(vm.passo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Passo <strong>" + response.nome + "</strong> cadastrado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", "Passo Cadastrada");
                $location.path('/passos');
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
            $location.path('/passos');
        }

        function seeTables() {
            $location.path('/scripts/' + vm.passo.id);
        }
    }
})();