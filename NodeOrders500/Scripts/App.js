// url for performing database api functions
var uri = 'api/Orders';

$(document).ready(function () {
    // creates drop down list for salespeople and stores on load
    GetPeopleData();
    GetCityData();
});

function GetPeopleData() {
    // Send an AJAX request
    $.getJSON(uri + '/GetSalesPeople')
        .done(function (data) {
            // appends sales people to dropdown list
            $('<select>', { text: "FirstName LastName" }).appendTo($('#spID'));
            $.each(data, function (key, item) {
                // adds option for each salesperson
                $('<option>', { text: formatItem(item) }).appendTo($('#spID'));
            });
        });
}

function GetCityData() {
    // Send an AJAX request
    $.getJSON(uri + '/GetStores')
        .done(function (data) {
            // appends cities people to dropdown list
            $('<select>', { text: "City" }).appendTo($('#sID'));
            $.each(data, function (key, item) {
                // adds option for each store
                $('<option>', { text: item.City }).appendTo($('#sID'));
            });
        });
}

function GetPersonDetails() {
    // sets selected salesperson id from drop down as parameter
    var personID = $('#spID').val()
    // Send an AJAX request with selected id parameter
    $.getJSON(uri + '/GetEmpSales/' + personID)
        .done(function (data) {
            // returns details about the salesperson
            $('#persondetails').text("This employee has sold a total of $" + data + " for the year");
        });
}

function GetStoreDetails() {
    // sets selected store id from drop down as parameter
    var storeID = $('#sID').val()
    // Send an AJAX request with selected id parameter
    $.getJSON(uri + '/GetStoreSales/' + storeID)
        .done(function (data) {
            // returns details about the store
            $('#storedetails').text("This store has sold a total of $" + data + " for the year");
        });
}

function GetMostSales() {
    // Send an AJAX request
    $.getJSON(uri + '/GetHighestSales')
        // Send an AJAX request to fill the table with data
        .done(function (data) {
            $('#table').text("City | Sales Count");
            $.each(data, function (key, item) {
                // appends each store with their recorded sales to a list in descending order
                $('<li>', { text: formatItem2(item) }).appendTo($('#mostsales'));
            });
        });
}

function formatItem(item) {
    // formats salesperson data to display first and last name
    return item.FirstName + ' ' + item.LastName;
}

function formatItem2(item) {
    // formats list data to display store name and their recorded sales
    return item.m_Item3 + ' | ' + item.m_Item2;
}