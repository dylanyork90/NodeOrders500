var uri = 'api/Orders';

$(document).ready(function () {
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
                // Add a list item for the product.
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
                $('<option>', { text: item.City }).appendTo($('#sID'));
            });
        });
}

function GetPersonDetails() {
    // Send an AJAX request
    var personID = $('#spID').val()
    $.getJSON(uri + '/GetEmpSales/' + personID)
        .done(function (data) {
            $('#persondetails').text(data);
        });
}

function GetStoreDetails() {
    // Send an AJAX request
    var storeID = $('#sID').val()
    $.getJSON(uri + '/GetStoreSales/' + storeID)
        .done(function (data) {
            $('#storedetails').text(data);
        });
}

function GetMostSales() {
    // Send an AJAX request
    $.getJSON(uri + '/GetHighestSales')
        .done(function (data) {
            $('<li>', { text: "City: SalesCount" }).appendTo($('#mostsales'));
            $.each(data, function (key, item) {
                console.log(item)
                $('<li>', { text: formatItem2(item) }).appendTo($('#mostsales'));
            });
        });
}

function formatItem(item) {
    return item.FirstName + ' ' + item.LastName;
}

function formatItem2(item) {
    return item.m_Item3 + ' ' + item.m_Item2;
}