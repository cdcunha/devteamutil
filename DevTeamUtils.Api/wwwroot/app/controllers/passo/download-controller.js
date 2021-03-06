﻿(function () {
    'use strict';
    angular.module('devTeamUtil').controller('PassoDownloadCtrl', PassoDownloadCtrl);

    PassoDownloadCtrl.$inject = ['$routeParams', '$filter', '$location', 'PassoFactory'];

    function PassoDownloadCtrl($routeParams, $filter, $location, PassoFactory) {
        var vm = this;
        var id = $routeParams.id;
        var nomeServidor = $routeParams.nomeServidor;
        vm.passo = {};

        activate();
        vm.download = download;
        vm.cancel = cancel;

        vm.download();

        function activate() {
            getPasso();
        }

        function getPasso() {
            PassoFactory.getById(id)
                 .success(success)
                 .catch(fail);

            function success(response) {
                vm.passo = response;
                //var arDate = response.dataNascimento.substring(0, 10).split('-');
                //vm.passo.dataNascimento = new Date(arDate[1] + '/' + arDate[2] + '/' + arDate[0]);
            }

            function fail(error) {
                if (error.data === '') {
                    toastr["error"](error.status + "<br/><button type='button' class='btn clear'>Ok</button>", error.statusText);
                }
                else {
                    var erros = error.data;
                    for (var i = 0; i < erros.length; ++i) {
                        toastr["error"](erros[i].value + "<br/><button type='button' class='btn clear'>Ok</button>", 'Falha na Requisição');
                    }
                }
            }
        }

        function download() {
            PassoFactory.download(id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr["success"]("Download concluído com sucesso<br/><button type='button' class='btn clear'>Ok</button>", 'Download realizado');
                saveTextAsFile(response, nomeServidor);
                $location.path('/passos');
            }

            function fail(error) {
                if (error.status === 401)
                    toastr["error"]("Você não tem permissão para ver esta página<br/><button type='button' class='btn clear'>Ok</button>", 'Requisição não autorizada');
                else {
                    if (error.statusText !== '')
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

            function destroyClickedElement(event) {
                document.body.removeChild(event.target);
            }

            function saveTextAsFile(iniText, nomeServidor) {
                var blob = new Blob([iniText], {
                    type: "text/plain;"
                });
                var downloadLink = document.createElement('a');
                downloadLink.setAttribute('download', 'sishosp_' + nomeServidor + '.ini');
                downloadLink.setAttribute('href', window.URL.createObjectURL(blob));
                downloadLink.onclick = destroyClickedElement;
                downloadLink.click();
            }
        }

        function cancel() {
            $location.path('/passos');
        }
    }
})();