// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//use 'strict';
var week = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
$("#today").click(function () {
    $.ajax({
        url: "https://localhost:44377/api/WeatherForecast/TodaysWeather",
        type: "GET",
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
        contentType: "application/json",
        crossDomain: true
    })
        .done(function (response) {
            var today = new Date().getDay();
            $('#table').bootstrapTable({
                data: response
            });
            $('#modalText').text("Today is " + week[today] + " and weather is going to be " + response[0].summary);
            $('#myModal').modal('show');
        })
        .fail(function (jqXHR, exception) {
            if (jqXHR.status === 403) {
                $('#modalText').text("You are not authorized to view today's weather. It requires to be a Common user");
            }
            else {
                $('#modalText').text(exception);
            }
            $('#myModal').modal('show');
        })
});

$("#forecast").click(function () {
    $.ajax({
        url: "https://localhost:44377/api/WeatherForecast/Forecast",
        type: "GET",
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
        contentType: "application/json",
        crossDomain: true
    })
        .done(function (data) {
            $('#table').bootstrapTable({
                data: data
            });
        })
        .fail(function (jqXHR, exception) {
            if (jqXHR.status === 403) {
                $('#modalText').text("You are not authorized to view weather forecast. It requires to be a Admin user");
            }
            else {
                $('#modalText').text(exception);
            }
            $('#myModal').modal('show');
        })
});

if (localStorage.getItem("accessToken") === "" || localStorage.getItem('accessToken') === null) {
    localStorage.setItem('accessToken', accessToken);
}

$("#logout").click(function () {
    localStorage.removeItem('accessToken');
})