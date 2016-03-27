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
        }).then(function(results){return results;})
    };   
       
    adminServiceFactory.getAllUsers = _getAllUsers;
    adminServiceFactory.getAllMedals = _getAllMedals;
    adminServiceFactory.saveUserData = _saveUserData;

    return adminServiceFactory;
    
}]);