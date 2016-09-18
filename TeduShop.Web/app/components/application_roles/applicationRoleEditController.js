(function (app) {
    'use strict';

    app.controller('applicationRoleEditController', applicationRoleEditController);

    applicationRoleEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

    function applicationRoleEditController($scope, apiService, notificationService, $location, $stateParams) {
        $scope.role = {}



        $scope.updateAppRole = updateAppRole;

        function updateAppRole() {
            apiService.put('/api/applicationRole/update', $scope.role, updatesuccesd, addFailed)
        }

        function updatesuccesd() {
            notificationService.displaySuccess($scope.role.Name + ' đã được cập nhật thành công.');

            $location.url('application_roles');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

        function loadDetail() {
            apiService.get('/api/applicationRole/detail/' + $stateParams.id, null,
            function (result) {
                $scope.role = result.data;
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }

   
        loadDetail();
    }
})(angular.module('tedushop.application_roles'));