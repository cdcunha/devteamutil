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

        clearScript($routeParams.passoId);

        vm.save = save;
        vm.cancel = cancel;

        /*var today = new Date();
        today.setYear(today.getFullYear() - 16);
        vm.minBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        today.setYear(today.getFullYear() - 90);
        vm.maxBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        */
        
        function save() {
            vm.script.tipoScript = vm.selectedTipoScript.id;
            vm.script.tipoObjeto = vm.selectedTipoObjeto.id;

            ScriptFactory.post(vm.script)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Script <strong>" + response.nomeScript + "</strong> cadastrada com sucesso<br/><br/><button type='button' class='btn clear'>Ok</button>", "Script Cadastrada");
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

        function clearScript(passoId) {
            vm.script = {
                id: '{00000000-0000-0000-0000-000000000000}',
                passoId: passoId,
                nomeScript: '',
                descricaoScript: '',
                tipoScript: '',
                tipoObjeto: '',
                mnemonico: '',
                nomeTabelaPai: '',
                txtScript: '',
                validado: 'false',
                status: '',
                dataStatus: '',
                dataCriacao: '',
                dataAlteracao: ''
            };

            vm.selectedTipoScript = { 'id': 0, 'description': 'Create' };
            vm.selectedTipoObjeto = { 'id': 0, 'description': 'Tabela de Cadastro' };
            vm.TipoScripts = [
                { 'id': 0, 'description': 'Create' },
                { 'id': 1, 'description': 'Insert' },
                { 'id': 2, 'description': 'Update' },
                { 'id': 3, 'description': 'Alter' },
                { 'id': 4, 'description': 'Outros' }
            ];
            vm.TipoObjetos = [
                { 'id': 0, 'description': 'Tabela de Cadastro' },
                { 'id': 1, 'description': 'Tabela de Detalhe' },
                { 'id': 2, 'description': 'Tabela de Movimentação' },
                { 'id': 3, 'description': 'Tabela de Estáticas/Domínio' },
                { 'id': 4, 'description': 'Tabela de Logs de Operação' },
                { 'id': 5, 'description': 'Tabela Temporária' },
                { 'id': 6, 'description': 'View' },
                { 'id': 7, 'description': 'Sequence' },
                { 'id': 8, 'description': 'Function' },
                { 'id': 9, 'description': 'Stored Procedure' },
                { 'id': 10, 'description': 'Trigger Insert After' },
                { 'id': 11, 'description': 'Trigger Insert Before' },
                { 'id': 12, 'description': 'Trigger Update After' },
                { 'id': 13, 'description': 'Trigger Update Before' },
                { 'id': 14, 'description': 'Trigger Delete After' },
                { 'id': 15, 'description': 'Trigger Delete Before' },
                { 'id': 16, 'description': 'Package' },
                { 'id': 17, 'description': 'Job' }
            ];
        }
    }
})();