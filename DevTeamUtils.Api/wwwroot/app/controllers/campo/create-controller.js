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
    
    CampoCreateCtrl.$inject = ['$scope', '$location', 'CampoFactory'];

    function CampoCreateCtrl($scope, $location, CampoFactory) {
        var vm = this;
        vm.campo = {};

        vm.atributos = ['Number(ORA) ou Integer(IFX)', 'Varchar2(ORA) ou Varchar(IFX)', 'Date(ORA) ou Datetime Year to Second(IFX)',
            'Number(ORA) ou Decimal(IFX)', 'Long Row(ORA) ou Byte(IFX)', 'Long(ORA) ou Text(IFX)'];
        vm.tipoCampos = ['Primary Key', 'Foreign Key', 'Código', 'Número', 'Data/Hora', 'Descrição', 'Nome', 'Valor',
            'Tipo', 'Sim/Não', 'Sigla', 'Imagem/Arquivo', 'Texto', 'Quantidade', 'Situação/Status', 'Indicação'];

        vm.save = save;
        vm.cancel = cancel;

        /*var today = new Date();
        today.setYear(today.getFullYear() - 16);
        vm.minBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        today.setYear(today.getFullYear() - 90);
        vm.maxBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        */
        
        function save() {
            CampoFactory.post(vm.campo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Campo <strong>" + response.nome + "</strong> cadastrada com sucesso<br/><br/><button type='button' class='btn clear'>Yes</button>", "Campo Cadastrado");
                $location.path('/tabelas');
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
            $location.path('/tabelas');
        }

        function clearCampo() {
            vm.campo = {};
            vm.campo.atributos = ['Number(ORA) ou Integer(IFX)', 'Varchar2(ORA) ou Varchar(IFX)', 'Date(ORA) ou Datetime Year to Second(IFX)',
                'Number(ORA) ou Decimal(IFX)', 'Long Row(ORA) ou Byte(IFX)', 'Long(ORA) ou Text(IFX)'];
            vm.campo.tipoCampos = ['Primary Key', 'Foreign Key', 'Código', 'Número', 'Data/Hora', 'Descrição', 'Nome', 'Valor',
                'Tipo', 'Sim/Não', 'Sigla', 'Imagem/Arquivo', 'Texto', 'Quantidade', 'Situação/Status', 'Indicação'];
        }
    }
})();