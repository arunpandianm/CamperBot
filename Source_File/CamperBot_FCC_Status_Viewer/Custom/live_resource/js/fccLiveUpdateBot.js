var key = "ae28f23f134c4364ad45e7b7355cfa91c92038bb";
var arr = [];
var points = 0;
var html = '<table class="table" id="data"><thead><tr><th class="text-center">Avatar</th><th class="text-center">Name</th><th class="text-center">User Name</th><th class="text-center">Points</th></tr></thead><tbody>';
var sum = 0;

$(document).ready(function() {
    var url = 'https://api.gitter.im/v1/rooms?access_token=' + key;
    var roomId = "";
    var noOfUsers = 0;
    $.ajax({
        type: 'GET', url: url,
        //data:data,
        async: false,
        dataType: 'json',
        success: function(data) {
            //Do stuff with the JSON data
            for (var i = 0; i < data.length; i++) { //data.length
                if (data[i]["name"] == 'kgisl/campsite') {
                    roomId = data[i]["id"];
                    noOfUsers = data[i]["userCount"];
                    break;
                }

            }
        },
        error: function(xhr, textStatus, errorThrown) {
            points = 0;
        }
    });
    var jsonData = [];

    for (var i = 0; i < noOfUsers; i += 100) { //30){//noOfUsers
        $.ajax({
            type: 'GET',
            url: 'https://api.gitter.im/v1/rooms/' + roomId + '/users?access_token=' + key + '&skip=' + i + '&limit=100',
            //data:data,
            async: false,
            dataType: 'json',
            success: function(data) {
                $.merge(jsonData, data);
                //alert(jsonData);
            },
            error: function(xhr, textStatus, errorThrown) {
                points = 0;
            }
        });
    }
    getData(jsonData);

});

$(document).ajaxStop(function() {
    console.log('triggered');
    sortTable();
});

function getData(jsonData) {
    //alert(json["array"].length
    var len = jsonData.length;

    for (var i = 0; i < len; i++) { //len
        if (jsonData[i]["username"] !== 'QuincyLarson') {
            browniePointsFetcher(jsonData[i]["username"]);

            arr.push({avatar: jsonData[i]["avatarUrlSmall"], avatar2: jsonData[i]["avatarUrlMedium"], name: jsonData[i]["displayName"], uname: jsonData[i]["username"], points: points});

        }
    }
    var j = 0;
    html += arr.map(function(a) {
        j++;
        return '<tr>' + dataFormatter(a.avatar, a.name, a.uname, a.points, a.avatar2) + '</tr>';
    }).join('');
    html += '</tbody></table>';

    $("#data").html(html);

    var a = $("#data").html();
    $("#campers").html('<h2><span class="label label-info btn-success">Total Campers:- ' + j + '</span></h2>');
    $("#totalProblems").html('<h2><span class="label label-info btn-success">Total Problems:- ' + sum + '</span></h2>');

}

function browniePointsFetcher(uname) {
    var points = 0;
    var url = 'https://www.freecodecamp.com/api/users/about?username=' + uname.toLowerCase();
    $.ajax({
        type: 'GET', url: url,
        //data:data,
        async: true,
        dataType: 'json',
        success: function(data) {
            //Do stuff with the JSON data
            points = data["about"]["browniePoints"];
            $('#' + uname).html('<h2 class="pts">' + points + '</h2>');
            sum += points;
            $("#totalProblems").html('<h2><span class="label label-info">Total Problems:- ' + sum + '</span></h2>');

        },
        error: function(xhr, textStatus, errorThrown) {
            points = 0;
            $('#' + uname).html('<h2 class="pts">0</h2>');
        }
    });
}

function dataFormatter(image, name, uname, points, urlmedium) {
    //alert(urlmedium);
    var temp_html = '<td>';
    temp_html += '<img src=' + image + ' class="img-thumbnail" width="100px" ></img></td>';
    temp_html += '<td>';
    temp_html += '<h3>' + name + '</h3></td>';
    temp_html += '<td>';
    temp_html += '<h3><a href="http://freecodecamp.com/' + uname + '" target="_blank">' + uname + '</a></h3></td>';
    temp_html += '<td id=' + uname + ' class="points"></td>';
    return temp_html;
}

function sortTable() {
    var rows = $('table tbody tr').get();

    rows.sort(function(a, b) {

        var x = parseInt($(a).children('td').eq(3).children().text());
        var y = parseInt($(b).children('td').eq(3).children().text());

        if (x < y)
            return 1;
        if (x > y)
            return -1;
        return 0;
    });
    $.each(rows, function(index, row) {
        $('table').children('tbody').append(row);
    });
}
