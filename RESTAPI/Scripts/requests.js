var uri = 'api/tickets';

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                // Add a list item for the product.
                $('<li>', { text: formatItem(item) }).appendTo($('#tickets'));
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#AllTickets').text('Error: ' + err);

            if (err == "Critical Exception")
                alert("Critical");
            else
                alert('getJSON request failed! ' + textStatus + "err:" + err);
        });
});

function formatItem(item) {
    return item.id + " Reporter: " + item.Reporter + ' MSG: ' + item.Message;
}

function find() {
    var id = $('#ticketId').val();
    /*$.ajaxSetup({
        headers: {
            'Accept-Encoding': 'gzip'
       }
     });
    */
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#ticket').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#ticket').text('Error: ' + err + "text status" + textStatus);

            if (jqXHR.status == 404) {
                alert("404 Not Found");
            } else {
                alert("Other non-handled error type");
            }
        });
}

function deleteTicket() {
    var id = $('#ticketId').val();
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            $('#ticket').text(id + 'deleted');
        },
        fail: function (jqXHR, textStatus, err) {
            $('#ticket').text('Error: ' + err + "text status" + textStatus);
        }
    });
}
//
function postTicket() {
    var id = $('#ticketId').val();
    var reporter = $('#Reporter').val();
    var message = $('#Message').val();
    $.ajax({
        url: uri + '/' + id + '/' + reporter + '/' + message,
        type: 'POST',
        success: function (result) {
            $('#ticket').text(id + reporter + message + 'Inserted');
        },
        fail: function (jqXHR, textStatus, err) {
            $('#ticket').text('Error: ' + err + "text status" + textStatus);
        }
    });
}
function putTicket() {
    var id = $('#ticketId').val();
    var reporter = $('#Reporter').val();
    var message = $('#Message').val();
    $.ajax({
        url: uri + '/' + id + '/' + reporter + '/' + message,
        type: 'PUT',
        success: function (result) {
            $('#ticket').text(id + reporter + message + 'PUT');
        },
        fail: function (jqXHR, textStatus, err) {
            $('#ticket').text('Error: ' + err + "text status" + textStatus);
        }
    });
}
//
function patchTicket() {
    var id = $('#ticketId').val();
    var message = $('#Message').val();
    $.ajax({
        url: uri + '/' + id + '/' + message,
        type: 'PATCH',
        success: function (result) {
            $('#ticket').text(id + 'patched' + message);
        },
        fail: function (jqXHR, textStatus, err) {
            $('#ticket').text('Error: ' + err + "text status" + textStatus);
        }
    });
}