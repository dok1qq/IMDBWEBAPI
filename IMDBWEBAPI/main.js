'use strict';

var uri = 'api/films';

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            var film = document.getElementById("films");
            $.each(data, function (key, item) {
                // Add a list item for the product. 
                var el = document.createElement("li");
                console.log(key, item);
                el.innerHTML = item.Director + ":" + item.Title;
                film.appendChild(el);
            });
        });
});

function find() {
    var id = $('#filmId').val();
    $.getJSON(uri + '/' + id.toString())
        .done(function (data) {
            var film = document.getElementById("film");
            film.innerHTML = data.Title;
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#film').text('Error: ' + err);
        });
}