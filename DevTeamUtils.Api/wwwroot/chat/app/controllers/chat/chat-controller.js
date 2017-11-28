(function () {
    'use strict';
    angular.module('chatDevTeam').controller('ChatCtrl', ChatCtrl);

    ChatCtrl.$inject = ['$routeParams', '$filter', '$location', 'ChatFactory'];

    function ChatCtrl($routeParams, $filter, $location, ChatFactory) {
        var vm = this;
        var apelido = $routeParams.apelido;

        vm.onlineUsers = [];

        vm.login = {
            apelido: $routeParams.apelido,
            senha: '',
            connectionId: ''
        };

        vm.message = '';
        vm.connection;

        vm.logout = logout;
        vm.send = send;

        activate();

        function activate() {
            let transportType = signalR.TransportType.WebSockets;
            let http = new signalR.HttpConnection('http://' + document.location.host + '/messenger', { transport: transportType });
            vm.connection = new signalR.HubConnection(http);
            vm.connection.start();

            vm.connection.on('SendAll', (nick, message) => {
                if (ready) {
                    $("#msgs").append("<br/>" + nick + " says: " + message + "");
                }
            });

            vm.connection.on('SendTo', (nick, message) => {
                if (ready)
                    $("#msgs").append("<br/>" + message + "");
            });

            vm.connection.on('UsersJoined', users => {
                users.forEach(user => {
                    appendLine('User ' + user.name + ' joined the chat');
                    addUserOnline(user);
                });
            });

            vm.connection.on('UsersLeft', users => {
                users.forEach(user => {
                    appendLine('User ' + user.name + ' left the chat');
                    document.getElementById(user.connectionId).outerHTML = '';
                });
            });

            vm.connection.on("message2Me", function (nick, response) {
                if (ready) {
                    $("#msgs").append("<br/>" + who + " says: " + msg + "");
                }
            });

            vm.connection.on("update", function (response) {
                if (ready)
                    $("#msgs").append("<br/>" + response + "");
            })

            getOnlineUsers();
        }

        function getOnlineUsers() {
            ChatFactory.onlineUsers()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.onlineUsers = response;
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

        function logout() {
            ChatFactory.logout(vm.login)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Usuário <strong>" + response.apelido + "</strong> deslogado com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Usuário Logado');
                $location.path('/login');
            }

            function fail(error) {
                if (error.status === 401)
                    toastr["error"]("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.statusText != '')
                        toastr.error(error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                    else {
                        if (error.data === null)
                            toastr["error"]("Erro indeterminado<br/><button type='button' class='btn clear'>Ok</button>", 'Erro indeterminado');
                        else {
                            var erros = error.data;
                            for (var i = 0; i < erros.length; ++i) {
                                toastr["error"](erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                            }
                        }
                    }
                }
            }
        }

        function send() {
            if (isConnected()) {
                var message = $("#msg").val();
                if (message != "") {
                    var nick = $("#name").val();
                    //socketio.emit("sendAll", msg, $("#name").val());
                    connection.invoke('SendAll', nick, message);
                    $("#msg").val("");
                }
            }
        }
    };
})();