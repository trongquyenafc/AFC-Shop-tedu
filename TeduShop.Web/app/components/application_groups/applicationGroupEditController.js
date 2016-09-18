﻿(function (app) {
    'use strict';

    app.controller('applicationGroupEditController', applicationGroupEditController);

    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];


    function applicationGroupEditController($scope, apiService, notificationService, $location, $stateParams) {
          
        $scope.group ={

        }

        $scope.updateApplicationGroup = updateApplicationGroup;

        function updateApplicationGroup() {
            apiService.put('/api/applicationGroup/update', $scope.group, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notificationService.displaySuccess($scope.group.Name + ' đã được cập nhật thành công.');

            $location.url('application_groups');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

        function loadeital() {
            apiService.get('/api/applicationGroup/detail/' + $stateParams.id, null,

           function (result) {
               $scope.group = result.data;
           }, function (error) {
               notificationService.displayError(result.data);
           });
        }

        function loadRoles() {
            apiService.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    $scope.roles = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách quyền.');
                });

        }

        loadeital();
        loadRoles();
    }



})(angular.module('tedushop.application_groups'));