app.service('translationService', function($resource) {  
    this.getTranslation = function($scope, language) {
        var languageFilePath = 'app/translations/t/translation_' + language + '.json';
        $resource(languageFilePath).get(function (data) {
            $scope.translation = data;        
        });
    };
});