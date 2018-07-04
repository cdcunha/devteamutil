(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ConexaoCreateCtrl', ConexaoCreateCtrl);
        /*.directive('ngModel', function attributeNgModelDirective() {
            return {
                require: 'ngModel',
                link: function (scope, el, attrs, ctrl) {
                    ctrl.$attributes = attrs;
                }
            };
    });*/
    
    ConexaoCreateCtrl.$inject = ['$scope', '$location', 'ConexaoFactory'];

    function ConexaoCreateCtrl($scope, $location, ConexaoFactory) {
        var vm = this;
        vm.conexoes = [];
        vm.conexao = {
            id: 0,
            sistema: '',
            sistemas: ["Sishosp"],
            bancoDados: '',
            bancosDeDados: ["Informix", "Oracle"],
            nomeServidor: '',
            ip: '',
            porta: '',
            usuario: '',
            senha: '',
            status: '',
            dataStatus: '',
            dataCriacao: '',
            dataAlteracao: ''
        };
        vm.save = save;
        vm.cancel = cancel;

        /*var today = new Date();
        today.setYear(today.getFullYear() - 16);
        vm.minBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        today.setYear(today.getFullYear() - 90);
        vm.maxBirthDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
        */
        
        function save() {
            ConexaoFactory.post(vm.conexao)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Conexão <strong>" + response.nome + "</strong> cadastrada com sucesso<br/><br/><button type='button' class='btn clear'>Ok</button>", "Conexão Cadastrada");
                $location.path('/conexoes');
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
                    vm.conexao.image = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#file')).on('change', handleFileSelect);
        */
        function cancel() {
            //clearConexao();
            $location.path('/conexoes');
        }

        function clearConexao() {
            vm.conexao = {
                id: 0,
                sistema: '',
                sistemas: ["SISHOSP"],
                bancoDados: '',
                bancosDeDados: ["Informix", "Oracle"],
                nomeServidor: '',
                ip: '',
                porta: '',
                usuario: '',
                senha: '',
                status: '',
                dataStatus: '',
                dataCriacao: '',
                dataAlteracao: ''
            };
        }
    }
})();