angular.module('chatDevTeam', ['ngRoute', 'ngMessages']).directive('myLink', function () {
    return {
        restrict: 'A',
        scope: {
            enabled: '=myLink'
        },
        link: function (scope, element, attrs) {
            element.bind('click', function (event) {
                if (!scope.enabled) {
                    event.preventDefault();
                }
            });
        }
    };
});

angular.element(document).ready(function () {
    angular.bootstrap(document, ['chatDevTeam']);
});
