angular.module("umbraco.resources").factory("uShareResource", function ($http) {
    return {
        share: function (nodeId, shareFacebook, shareLinkedIn, shareTwitter) {
            var url = Umbraco.Sys.ServerVariables["uShare"]["uShareBaseUrl"] + "GetShareResult";
            if (typeof shareFacebook === 'undefined') shareFacebook = false;
            if (typeof shareLinkedIn === 'undefined') shareLinkedIn = false;
            if (typeof shareTwitter === 'undefined') shareTwitter = false;
            return $http.get(url + "?nodeId=" + nodeId + "&shareFacebook=" + shareFacebook + "&shareLinkedIn=" + shareLinkedIn + "&shareTwitter=" + shareTwitter);
        }
    };
});