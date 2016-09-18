﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    'use strict';

    app.controller('applicationUserAddController', applicationUserAddController);

    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];


    function applicationUserAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.addAccount = addAccount;
        $scope.account = {
            Groups: []
        }




        function addAccount() {
            apiService.post('/api/applicationUser/add', $scope.account, addSuccessed, addFailed);
        }
        function addSuccessed() {
            notificationService.displaySuccess($scope.account.UserName + ' đã được thêm mới.');

            $location.url('application_users');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });

        }

        loadGroups();
    }

   


})(angular.module('tedushop.application_users'));
