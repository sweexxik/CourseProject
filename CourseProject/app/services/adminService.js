'use strict';
app.factory('adminService', ['$http',
 function ($http) {

  var adminServiceFactory = {};  

   var _getAllUsers = function () {
    
        return $http.get(serviceBase + 'api/admin/users')
            .then(function (results) { return results; });
    };
       
    adminServiceFactory.getAllUsers = _getAllUsers;

    return adminServiceFactory;
}]);