(function () {
    'use strict';
    angular.module('devTeamUtil').controller('PassoListCtrl', PassoListCtrl);

    PassoListCtrl.$inject = ['PassoFactory'];
    
    function PassoListCtrl(PassoFactory) {
        var vm = this;
        vm.passos = [];
        
        activate();

        function activate() {
            getPassos();            
        }

        function getPassos() {
            PassoFactory.get()
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.passos = response;
                /*vm.passos.forEach(function (passo) {
                    if (passo.dataNascimento != null) {
                        var arDate = passo.dataNascimento.substring(0, 10).split('-');
                        passo.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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