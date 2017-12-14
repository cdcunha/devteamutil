(function () {
    'use strict';
    angular.module('devTeamUtil').controller('ConexaoListCtrl', ConexaoListCtrl);

    ConexaoListCtrl.$inject = ['ConexaoFactory'];
    
    function ConexaoListCtrl(ConexaoFactory) {
        var vm = this;
        vm.conexoes = [];
        
        activate();
        
        function activate() {
            getConexaos();
            createSocketConnection();
        }

        function createSocketConnection() {
            let transportType = signalR.TransportType.WebSockets;
            let http = new signalR.HttpConnection('http://' + document.location.hostname + ':52854/statusDB', { transport: transportType });
            var connection = new signalR.HubConnection(http);

            connection.on('StatusDBResponse', (id, statusDB, dateStatus) => {
                vm.conexoes.forEach(function (conexao) {
                    if (conexao.id === id) {
                        vm.conexao.status = statusDB;
                        vm.conexao.dataStatus = dateStatus;
                    }
                });
            });

            connection.start();

            vm.conexoes.forEach(function (conexao) {
                if (conexao.id !== null) {
                    connection.invoke('GetStatusDB', conexao.id);
                }
            })
            //Call the server side method for every 10 minutes
            var minutes = 10; //Interval in minutes
            var interval = minutes * 60 * 1000; //Convert Minutes to miliseconds
            setInterval(function () {
                vm.conexoes.forEach(function (conexao) {
                    if (conexao.id !== null) {
                        connection.invoke('GetStatusDB', conexao.id);
                    }
                });
            }, interval); 
        }

        function getConexaos() {
            ConexaoFactory.get()
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.conexoes = response;
                /*vm.conexoes.forEach(function (conexao) {
                    if (conexao.dataNascimento != null) {
                        var arDate = conexao.dataNascimento.substring(0, 10).split('-');
                        conexao.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
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