//$.get("/api/Movies", function (movies) {
//    for (var movie in movies)
//    {
//        console.log(movies[movie])
//    }
//}); 

//function GetData() {
//    $.ajax({
//        url: 'api/Movies',
//        type: 'GET',
//        success: function (data) {
//            console.log(data);
//        }
//    })
//}

function FillJumbotron(data) {
    var tab = document.querySelector(".tab");
    for (var object in data) {
       // console.log(object)
        var movie = data[object];
        // tab.innerHTML += "<tr>";
        var tr = document.createElement("tr");
        for (var key in movie) {
            //   tab.innerHTML += "<td>" + movie[key] + "</td> kkk";
            var td = document.createElement("td");
            td.innerHTML = movie[key];
            tr.appendChild(td);
            console.log(key)
        }
        //td.innerHTML =  "<a href="">Delete</a>";
        tab.appendChild(tr);
    }
}


function GetData() {
    $.ajax({
        url: 'api/Movies',
        type: 'GET',
        success: function (data) {
            FillJumbotron(data);
        }
    })
}



//function AddData() {
//    $.ajax({
//        url: 'api/Movies',
//        type: 'POST',
//        data: {
//            Title: "Harry Potter",
//            Price: 300,
//            Year: 2006,
//            Genre: "Fantasy",
//            DirectorId: 1
//        },
//        success: function (data) {
//            console.log(data);
//        }
//    })
//}

function AddData() {
    $.ajax({
        url: 'api/Movies',
        type: 'POST',
        data: {
            Title: $('#title').val(),
            Price: $('#price').val(),
            Year: $('#year').val(),
            Genre: $('#genre').val(),
            DirectorId: $('#directorId').val()
        },
        success: function (data) { console.log(data) }
    });
};

//AddData();
GetData();

$('#submit').on('click', AddData);


