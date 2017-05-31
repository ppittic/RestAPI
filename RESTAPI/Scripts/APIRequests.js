var uri = 'api/tickets';

function formatItem(item) {
    return item.Reporter + ': "' + item.Message + '"';
}

function showAll() {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                // Add a list item for the product.
                $("#tickets").append('<li class="list-group-item">' + '<span class="badge pull-left">' + item.Id + '</span>' + formatItem(item) + '</li>');

                $('#AllTickets').show();
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            if (jqXHR.status >= 400 && jqXHR.status < 500)
                alert("Client Error");
            else
                if (jqXHR.status > 500)
                    alert("Server Error");
                else
                    alert('getJSON request failed! ' + "err:" + err);
        });
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
            $('#ticket').text('Item with ID '+ id + ' deleted');
        },
        fail: function (jqXHR, textStatus, err) {
            alert('Error: ' + err + "; Text status" + textStatus);
        }
    });
}
//POST used to create
function postTicket() {
    var id = $('#ticketId').val();
    var reporter = $('#Reporter').val();
    var message = $('#Message').val();
    $.ajax({
        url: uri + '/' + id + '/' + reporter + '/' + message,
        type: 'POST',
        success: function (result) {
            $('#ticket').text('Item with reporter: ' + reporter + ' and message: ' + message + '  POSTED  ' + '  ID  ' + result.Id);
        },
        fail: function (jqXHR, textStatus, err) {
            alert('Error: ' + err + "; Text status" + textStatus);
        }
    });
}
//PUT is idempotent (PUTting an object twice has no effect)
//Used to create a resource, or overwrite it
function putTicket() {
    var id = $('#ticketId').val();
    var reporter = $('#Reporter').val();
    var message = $('#Message').val();
    $.ajax({
        url: uri + '/' + id + '/' + reporter + '/' + message,
        type: 'PUT',
        success: function (result) {
            $('#ticket').text('Item with reporter: ' + reporter + ' and message: ' + message + '  PUT  ');
        },
        fail: function (jqXHR, textStatus, err) {
            alert('Error: ' + err + "; Text status" + textStatus);
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
            $('#ticket').text('Item with ID: '+ id + ' patched. New message: ' + message);
        },
        fail: function (jqXHR, textStatus, err) {
            alert('Error: ' + err + "; Text status" + textStatus);
        }
    });
}

function clearAllTickets()
{
    $("#tickets").empty();
    $('#AllTickets').hide();
}
