(function () {
    'use strict';
    angular.module('devTeamUtil').controller('IfxerroSearchCtrl', IfxerroSearchCtrl);

    IfxerroSearchCtrl.$inject = ['IfxerroFactory'];
    
    function IfxerroSearchCtrl(IfxerroFactory) {
        var vm = this;
        vm.ifxerro = { "code" : "" };
        
        vm.search = search;
        vm.cancel = cancel;

        function search() {
            IfxerroFactory.getByCode(vm.ifxerro.code)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.ifxerro = response;
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
            $location.path('/ifxerros');
        }
    }
})();