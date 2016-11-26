'use strict';

var uri = 'api/films';

window.onload = function() {
    var xhr = new XMLHttpRequest();

    xhr.open('GET', uri, true);
    xhr.send();
    xhr.onreadystatechange = function() {
        if (xhr.readyState != 4) return;
        if (xhr.status != 200) {
            console.log(xhr.statusText);
        } else {
            var film = document.getElementById("films");
           
            var data = JSON.parse(xhr.responseText);
            data.forEach(function (item, i, arr) {
                var el = document.createElement("li");
                el.innerHTML = item.Director + ":" + item.Title;
                film.appendChild(el);
            });
        }
    }
};

function find() {
    var xhr = new XMLHttpRequest();
    var id = document.getElementById('filmId').value;
    var film = document.getElementById("film");
    
    xhr.open('GET', (uri + '/' + id.toString()), true);
    xhr.send();
    xhr.onreadystatechange = function () {
        if (xhr.readyState != 4) return;

        if (xhr.status != 200) {
            console.log(xhr.statusText);
        } else {
            var data = JSON.parse(xhr.responseText);
            film.innerHTML = data.Title;
        }

    }
}

function search() {
    var xhr = new XMLHttpRequest();
    var search = document.getElementById("searchFilm");
    var str = document.getElementById('search').value;

    xhr.open('POST', uri, true);
    xhr.setRequestHeader('Content-Type', 'application/json;odata=verbose');
    xhr.send(str);

    xhr.onreadystatechange = function () {
        if (xhr.readyState != 4) return;

        if (xhr.status != 200) {
            console.log(xhr.statusText);
        } else {
            var data = JSON.parse(xhr.responseText);
            console.log(data);
            search.innerHTML = data.Title;
        }

    }
}


