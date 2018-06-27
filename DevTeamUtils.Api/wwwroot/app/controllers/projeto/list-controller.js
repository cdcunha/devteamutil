(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ProjetoListCtrl', ProjetoListCtrl);

    ProjetoListCtrl.$inject = ['ProjetoFactory'];
    
    function ProjetoListCtrl(ProjetoFactory) {
        var vm = this;
        vm.projetos = [];
        
        activate();
        
        function activate() {
            getProjetos();            
        }

        function getProjetos() {
            ProjetoFactory.get()
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.projetos = response;
                /*vm.projetos.forEach(function (projeto) {
                    if (projeto.dataNascimento != null) {
                        var arDate = projeto.dataNascimento.substring(0, 10).split('-');
                        projeto.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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