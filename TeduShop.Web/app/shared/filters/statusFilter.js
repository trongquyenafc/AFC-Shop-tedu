(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if(input==true){
                return 'kich hoat';
            } else {

                return 'Khoa';
            }
        }

    });

})(angular.module('tedushop.common'));