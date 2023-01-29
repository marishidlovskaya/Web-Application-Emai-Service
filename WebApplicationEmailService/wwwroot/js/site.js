function update_content() {
    $.ajax({
        dataType: "json",
        method: "GET",
        url: "/Email/GetAllMessagesOfCurrentUser",
        success: function (data) {
            $("#table-inbox-mess tbody").empty();

            data.sort(compare);

            for (var item in data)
            {
                var title = "";
                var text = "";
                if (data[item].title != null) { title = data[item].title }
                if (data[item].text != null) { text = data[item].text }
             
               // console.log(item);
                $("#table-inbox-mess tbody").append("<tr>" +
                    "<td>" + data[item].sender + "</td>" +
                    "<td>" + title + "</td > " +
                    "<td style=' white-space: nowrap; max-width: 80px; max-height: 20px; overflow: hidden; text-overflow: ellipsis;'>" + text + "</td>" +
                    "<td>" + new Date(data[item].date).toLocaleString('en-GB', { timeZone: 'UTC' }) + "</td>" +
                    "</tr>");
            }
        },
    });
}

function compare(a, b) {
    if (a.date < b.date) {
        return 1;
    }
    if (a.date > b.date) {
        return -1;
    }
    return 0;
}

    $('#send_email').click(function (event) {
        var recipient = $('#to').val();
        var title = $('#cc').val();
        var text = $('#message').val();
        $.ajax({
            dataType: "json",
            method: "POST",
            url: "/Email/SendMessage",
            data: { receiverId: recipient, title: title, textbody: text},
            success: function (result) {
                $('#to').val("");
                $('#cc').val("");
                $('#message').val("");
                 
                $('#errorMessage').hide();
                $('#successMessage').show();

            },
            error: function (result) {
                
                $('#successMessage').hide();
                $('#errorMessage').show();
            },            
        });
    });

