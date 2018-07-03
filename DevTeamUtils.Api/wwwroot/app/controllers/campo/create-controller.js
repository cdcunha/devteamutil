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

        clearCampo($routeParams.tabelaId);

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
                toastr.success("Campo <strong>" + response.nome + "</strong> cadastrada com sucesso<br/><br/><button type='button' class='btn clear'>Yes</button>", "Campo Cadastrado");
                $location.path('/campos/' + vm.campo.tabelaId);
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
            $location.path('/campos/' + vm.campo.tabelaId);
        }

        function clearCampo(tabelaId) {
            vm.campo = {
                tabelaId: tabelaId
            };

            vm.selectedTipo = { 'id': 1, 'description': 'Number(ORA) ou Integer(IFX)' };
            vm.selectedAtributo = { 'id': 1, 'description': 'Primary Key' };

            vm.Tipos = [
                { 'id': 1, 'description': 'Number(ORA) ou Integer(IFX)' },
                { 'id': 2, 'description': 'Varchar2(ORA) ou Varchar(IFX)' },
                { 'id': 3, 'description': 'Date(ORA) ou Datetime Year to Second(IFX)' },
                { 'id': 4, 'description': 'Number(ORA) ou Decimal(IFX)' },
                { 'id': 5, 'description': 'Long Row(ORA) ou Byte(IFX)' },
                { 'id': 6, 'description': 'Long(ORA) ou Text(IFX)' }
            ];

            vm.Atributos = [
                { 'id': 1, 'description': 'Primary Key' },
                { 'id': 2, 'description': 'Foreign Key' },
                { 'id': 3, 'description': 'Código' },
                { 'id': 4, 'description': 'Número' },
                { 'id': 5, 'description': 'Data/Hora' },
                { 'id': 6, 'description': 'Descrição' },
                { 'id': 7, 'description': 'Nome' },
                { 'id': 8, 'description': 'Valor' },
                { 'id': 9, 'description': 'Tipo' },
                { 'id': 10, 'description': 'Sim/Não' },
                { 'id': 11, 'description': 'Sigla' },
                { 'id': 12, 'description': 'Imagem/Arquivo' },
                { 'id': 13, 'description': 'Texto' },
                { 'id': 14, 'description': 'Quantidade' },
                { 'id': 15, 'description': 'Situação/Status' },
                { 'id': 16, 'description': 'Indicação' }
            ];
        }
    }
})();