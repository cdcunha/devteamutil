(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ScriptRemoveCtrl', ScriptRemoveCtrl);

    ScriptRemoveCtrl.$inject = ['$routeParams', '$filter', '$location', 'ScriptFactory'];

    function ScriptRemoveCtrl($routeParams, $filter, $location, ScriptFactory) {
        var vm = this;
        var id = $routeParams.id;
        vm.script = {};

        activate();
        vm.remove = remove;
        vm.cancel = cancel;

        function activate() {
            getScript();
        }

        function getScript() {
            ScriptFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.script = response;
                //var arDate = response.dataNascimento.substring(0, 10).split('-');
                //vm.script.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
            }

            function fail(error) {
                if (error.data === '') {
                    toastr["error"](error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                }
                else {
                    var erros = error.data;
                    for (var i = 0; i < erros.length; ++i) {
                        toastr["error"](erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                    }
                }
            }
        }

        function remove() {
            ScriptFactory.remove(vm.script)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Script <strong>" + response.nome + "</strong> removida com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Script Removido');
                $location.path('/scripts/' + vm.script.passoId);
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
            $location.path('/scripts/' + vm.script.passoId);
        }
    }
})();