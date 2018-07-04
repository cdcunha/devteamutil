(function () {
    'use strict';
    angular.module('devTeamUtil').controller('CampoCreateCtrl', CampoCreateCtrl);
        /*.directive('ngModel', function attributeNgModelDirective() {
            return {
                require: 'ngModel',
                link: function (scope, el, attrs, ctrl) {
                    ctrl.$attributes = attrs;
                }
            };
    });*/
    
    CampoCreateCtrl.$inject = ['$routeParams', '$location', 'CampoFactory'];

    function CampoCreateCtrl($routeParams, $location, CampoFactory) {
        var vm = this;

        clearCampo($routeParams.scriptId);

        vm.save = save;
        vm.cancel = cancel;

        /*var today = new Date();
        today.setYear(today.getFullYear() - 16);
        vm.minBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        today.setYear(today.getFullYear() - 90);
        vm.maxBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        */
        
        function save() {
            vm.campo.tipoCampo = vm.selectedTipo.id;
            vm.campo.atributo = vm.selectedAtributo.id;

            CampoFactory.post(vm.campo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Campo <strong>" + response.nome + "</strong> cadastrada com sucesso<br/><br/><button type='button' class='btn clear'>OK</button>", "Campo Cadastrado");
                $location.path('/campos/' + vm.campo.scriptId);
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
                    vm.campo.image = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#file')).on('change', handleFileSelect);
        */
        function cancel() {
            //clearCampo();
            $location.path('/campos/' + vm.campo.scriptId);
        }

        function clearCampo(scriptId) {
            vm.campo = {
                id: '{00000000-0000-0000-0000-000000000000}',
                scriptId: scriptId,
                descricaoCampo: '',
                atributo: '',
                tipoCampo: '',
                tamanhoCampo: '',
                valorCampo: '',
                notNull: '',
                mnemonicoRefFk: '',
                fieldRefFk: ''
            };

            vm.selectedTipo = { 'id': 0, 'description': 'Number(ORA) ou Integer(IFX)' };
            vm.selectedAtributo = { 'id': 0, 'description': 'Primary Key' };

            vm.Tipos = [
                { 'id': 0, 'description': 'Number(ORA) ou Integer(IFX)' },
                { 'id': 1, 'description': 'Varchar2(ORA) ou Varchar(IFX)' },
                { 'id': 2, 'description': 'Date(ORA) ou Datetime Year to Second(IFX)' },
                { 'id': 3, 'description': 'Number(ORA) ou Decimal(IFX)' },
                { 'id': 4, 'description': 'Long Row(ORA) ou Byte(IFX)' },
                { 'id': 5, 'description': 'Long(ORA) ou Text(IFX)' }
            ];

            vm.Atributos = [
                { 'id': 0, 'description': 'Primary Key' },
                { 'id': 1, 'description': 'Foreign Key' },
                { 'id': 2, 'description': 'Código' },
                { 'id': 3, 'description': 'Número' },
                { 'id': 4, 'description': 'Data/Hora' },
                { 'id': 5, 'description': 'Descrição' },
                { 'id': 6, 'description': 'Nome' },
                { 'id': 7, 'description': 'Valor' },
                { 'id': 8, 'description': 'Tipo' },
                { 'id': 9, 'description': 'Sim/Não' },
                { 'id': 10, 'description': 'Sigla' },
                { 'id': 11, 'description': 'Imagem/Arquivo' },
                { 'id': 12, 'description': 'Texto' },
                { 'id': 13, 'description': 'Quantidade' },
                { 'id': 14, 'description': 'Situação/Status' },
                { 'id': 15, 'description': 'Indicação' }
            ];
        }
    }
})();