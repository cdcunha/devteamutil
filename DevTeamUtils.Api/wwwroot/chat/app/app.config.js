angular.module('chatDevTeam').config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

angular.module('chatDevTeam')
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider
                .when('/users', {
                    controller: 'UserListCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/chat/app/templates/User/index.html'
                })
                .when('/users/create', {
                    controller: 'UserCreateCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/chat/app/templates/User/create.html'
                })
                .when('/users/edit/:id', {
                    controller: 'UserEditCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/chat/app/templates/User/edit.html'
                })
                .when('/users/remove/:id', {
                    controller: 'UserRemoveCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/chat/app/templates/User/remove.html'
                })
                .when('/login', {
                    controller: 'UserLoginCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/chat/app/templates/User/login.html'
                })
                .when('/chat/:apelido', {
                    controller: 'ChatCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/chat/app/templates/Chat/chat.html'
                });
        }
    ]);
