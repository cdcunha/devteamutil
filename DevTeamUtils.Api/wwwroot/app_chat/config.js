(function () {
    'use strict';

    //SETTINGS = { 'SERVICE_URL': 'http://localhost:18066/' };
    //SETTINGS = { 'SERVICE_URL': 'http://AMLNOTPR398HT3:51640/' };
    SETTINGS = { 'SERVICE_URL': window.location.protocol + '//' + window.location.host + '/' };

    angular.module('chatDevTeam').constant('SETTINGS', SETTINGS);
    
})