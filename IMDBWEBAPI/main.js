'use strict';

var uri = 'api/films';

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                console.log(key, item);
                // Add a list item for the product.
                $('<li>', { text: formatItem(item) }).appendTo($('#films'));
            });
        });
});

function formatItem(item) {
    return item.Name + ': $' + item.Price;
}

function find() {
    var id = $('#filmId').val();
    $.getJSON(uri + '/' + id.toString())
        .done(function (data) {
            console.log(data);
            var film = document.getElementById("film");
            film.innerHTML = data.Title;
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#film').text('Error: ' + err);
        });
}