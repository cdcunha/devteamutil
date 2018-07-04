(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ScriptCreateCtrl', ScriptCreateCtrl);
        /*.directive('ngModel', function attributeNgModelDirective() {
            return {
                require: 'ngModel',
                link: function (scope, el, attrs, ctrl) {
                    ctrl.$attributes = attrs;
                }
            };
    });*/
    
    ScriptCreateCtrl.$inject = ['$routeParams', '$location', 'ScriptFactory'];

    function ScriptCreateCtrl($routeParams, $location, ScriptFactory) {
        var vm = this;
        vm.script = {
            passoId : $routeParams.passoId
        };
        vm.tipos = ['Create', 'Insert', 'Update', 'Alter', 'Other'];

        vm.save = save;
        vm.cancel = cancel;

        /*var today = new Date();
        today.setYear(today.getFullYear() - 16);
        vm.minBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        today.setYear(today.getFullYear() - 90);
        vm.maxBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        */
        
        function save() {
            ScriptFactory.post(vm.script)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Script <strong>" + response.nomeScript + "</strong> cadastrada com sucesso<br/><br/><button type='button' class='btn clear'>Yes</button>", "Script Cadastrada");
                $location.path('/scripts/' + vm.script.passoId);
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
                    vm.script.image = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#file')).on('change', handleFileSelect);
        */
        function cancel() {
            //clearScript();
            $location.path('/scripts/' + vm.script.passoId);
        }

        function clearScript() {
            vm.script = {
                id: 0,
                nomeScript: '',
                descricaoScript: '',
                tipoScript: '',
                mnemonico: '',
                txtScript: '',
                validado: '',
                campos: [],
                status: '',
                dataStatus: '',
                dataCriacao: '',
                dataAlteracao: ''
            };
        }
    }
})();