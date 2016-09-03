/// <reference path="../plugins/angular/angular.js" />
var myapp = angular.module('mymodel', []);
myapp.controller('schoolcontroller', schoolcontroller);
myapp.controller('studentmycontroller', studentmycontroller);
myapp.controller('techmycontroller', techmycontroller);


function schoolcontroller($scope) {

    $scope.message = "annomount from school.";
}


function studentmycontroller($scope) {
      $scope.message = "this is message angular from student" //neu khong co message thi no an theo message shool
}


function techmycontroller($scope) {
    $scope.message = "this is message angular from teacher"//neu khong co message thi no an theo message shool
}



//function studentmycontroller($rootScope,$scope) {
//    $rootScope.message = "this is message angular from student"
//    //bien rootScope la bien toan cuc neu co rootSocpe thi cac bien cung nhau no se hien thi ra giong nhau
//}