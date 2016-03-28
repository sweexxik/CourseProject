'use strict';
app.factory('adminService', ['$http',
 function ($http) {

  var adminServiceFactory = {};  

    var _getAllUsers = function () {    
        return $http.get(serviceBase + 'api/admin/users')
            .then(function (results) { return results; });
    };

     var _getAllMedals = function () {    
        return $http.get(serviceBase + 'api/admin/medals')
            .then(function (results) { return results; });
    };

    var _saveUserData = function(data){
        return $http.post(serviceBase + 'api/admin/save', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };   

    var _deleteUser = function(data){
        return $http.post(serviceBase + 'api/admin/delete', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };

    var _resetPasword = function(data){
        return $http.post(serviceBase + 'api/admin/reset', JSON.stringify(data), {
            headers:{contentType:'application/json; charset=utf-8', dataType:"json"}
        }).then(function(results){
            return results;
        });
    };

    var _registerNewUser = function (registration) {     

        return $http.post(serviceBase + 'api/admin/register', registration).then(function (response) {
            return response;
        });

    };
       
    adminServiceFactory.getAllUsers = _getAllUsers;
    adminServiceFactory.getAllMedals = _getAllMedals;
    adminServiceFactory.saveUserData = _saveUserData;
    adminServiceFactory.deleteUser = _deleteUser;
    adminServiceFactory.resetPassword = _resetPasword;
     adminServiceFactory.registerNewUser  = _registerNewUser

    return adminServiceFactory;    
    
}]);