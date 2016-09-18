/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    'use strict';
    app.controller('applicationRoleListController', applicationRoleListController);
    applicationRoleListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    
    function applicationRoleListController($scope, apiService, notificationService, $ngBootbox, $filte) {
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.loading = true;
        $scope.checkedall = checkedall;
        $scope.deleteItem  =   deleteItem;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        function checkedall() {
            if ($scope.isAll === false) {
                angular.forEach($scope.data, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.data, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("data", function (n, o) {
            var checked = $filte("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $("#btnDelete").removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        },true);



        function deleteMultiple() {

            var listid = [];
            $.each($scope.selected, function (i, item) {

                listid.push(item.Id);
            });

            var config = {
                params: {
                    checkedList: JSON.stringify(listid)
                }
            }
            apiService.del('api/applicationRole/deletemulti', config, function (result) {

                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });

        }


        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/applicationRole/Delete', config, function () {
                    $scope.loading = true;
                    notificationService.displaySuccess('Đã xóa thành công.');
                    search();
                },
                function () {
                    notificationService.displayError('Xóa không thành công.');
                });
            });

        }

        function search(page) {
            page = page || 0;
            $scope.loading = true;
       
            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.keyword
                }
            }

            apiService.get('api/applicationRole/getlistpaging', config, dataLoadCompleted, dataLoadFailed);

        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;
            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' được tìm thấy');
            }


        }

        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }

        $scope.search();
    }


})(angular.module('tedushop.application_roles'));