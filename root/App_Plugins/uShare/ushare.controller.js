angular.module("umbraco").controller("Governor.uShareController", function ($scope, $routeParams, uShareResource) {
    $scope.model.value = {
        FacebookName: 'Facebook',
        LinkedInName: 'LinkedIn',
        TwitterName: 'Twitter',
        enableFacebook: $scope.model.config.enableFacebook == '1',
        enableLinkedIn: $scope.model.config.enableLinkedIn == '1',
        enableTwitter: $scope.model.config.enableTwitter == '1',
        selectedFacebook: $scope.model.config.enableFacebook == '1' && typeof $scope.model.value.selectedFacebook == 'undefined' ? $scope.model.config.activateFacebook == '1' : $scope.model.value.selectedFacebook,
        selectedLinkedIn: $scope.model.config.enableLinkedIn == '1' && typeof $scope.model.value.selectedLinkedIn == 'undefined' ? $scope.model.config.activateLinkedIn == '1' : $scope.model.value.selectedLinkedIn,
        selectedTwitter: $scope.model.config.enableTwitter == '1' && typeof $scope.model.value.selectedTwitter == 'undefined' ? $scope.model.config.activateTwitter == '1' : $scope.model.value.selectedTwitter
    };
    $scope.share = function() {
        uShareResource.share($routeParams.id, $scope.model.value.selectedFacebook, $scope.model.value.selectedLinkedIn, $scope.model.value.selectedTwitter).then(function(result) {
            angular.forEach(angular.fromJson(angular.fromJson(result.data)), function(value) {
                UmbClientMgr.mainWindow().UmbSpeechBubble.ShowMessage(value.Icon, value.Header, value.Body);
            });
        });
    };
});