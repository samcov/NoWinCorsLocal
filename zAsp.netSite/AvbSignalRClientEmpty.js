/*
* 	IVC SignalR Control
* 	Depends on SignalR
*
*	Copyright (c) 2012 IVC Business Systems (http://IvcBusinessSystems.com)
*
*
*/

var chat;
function initSignalR()
{
    // Process Hub
    // Proxy created on the fly, using the Javascript convention of small letter first
	$.connection.hub.url = "http://localhost:8888/signalr";
    chat = $.connection.myHub;
	
            // Create a function that the hub can call to broadcast messages.
            chat.client.addMessage = function (name, message) {
                // Html encode display name and message.
                var encodedName = $('<div />').text(name).html();
                var encodedMsg = $('<div />').text(message).html();
                // Add the message to the page.
                $('#discussion').append('<li><strong>' + encodedName
                    + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
            };
            // Get the user name and store it to prepend to messages.
            //$('#displayname').val(prompt('Enter your name:', ''));
            $('#displayname').val('User');
            // Set initial focus to message input box.
            $('#message').focus();
//	$.connection.hub.url = 'http://76.90.50.83/signalr'; // use the Hub on another server... Fantastic!!!
	// Start the connection
            $.connection.hub.start({ transport: ['webSockets', 'longPolling'] }).done(function () {
	    
                $('#sendmessage').click(function () {
                    // Call the Send method on the hub.
                    chat.server.send($('#displayname').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
		
            });
}
	