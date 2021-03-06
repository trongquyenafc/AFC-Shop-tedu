﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />


(function (app) {
    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';

        $scope.search = search;
        

        $scope.selectAll = selectAll;
        $scope.isAll = false;

        $scope.deleteMultiple = deleteMultiple;
        $scope.deleteProduct = deleteProduct;
        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);

            });

            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }

            apiService.del('api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/product/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })

            });
        }

        function selectAll() {
            if($scope.isAll===false){
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });

                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }





        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);











        function search(){
            getProducts();
        }
        function getProducts(page){
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize:7
                }
            }

            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }

                $scope.products =  result.data.Items,
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

            }, function () {

                console.log('Load product failed.');
            });

        }


        $scope.getProducts();
    }

})(angular.module('tedushop.products'));