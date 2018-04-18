/// <reference path="../typings/jqueryui/jqueryui.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
$(function () {
    var locations = [];
    $.ajax({
        type: "POST",
        url: "Request.aspx/GetLocations",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            locations = JSON.parse(data.d);
            var locationNames = $.map(locations, function (location, index) { return location.Name; });
            $("#txtLocationAutoCompleteBox").autocomplete({
                source: locationNames,
            });
        }
    });
});
//# sourceMappingURL=request.aspx.js.map