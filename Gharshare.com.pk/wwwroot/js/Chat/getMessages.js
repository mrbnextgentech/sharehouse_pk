$(document).ready(function () {
    var connection1 = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    var fromUserID1 = "";
    var toUserID1 = "";
    var groupName="";

    $("a.userMessage").click(function (event) {
        event.preventDefault(); // prevent the default action of the link
        var lid = $(this).data("lid"); 
        var tenantPic = $(this).data("tenantpic");
        var landlordPic = $(this).data("landlordpic");
        var currentUserId = $(this).data("landlordid");
        var tenantUserId = $(this).data("tenantuserid");
        $('#tenantInput').val(tenantUserId);
        toUserID1 = tenantUserId;

        var tenantName ="Chat with " + $(this).data("tenantname");
        $("#tenantName").html(tenantName);
       // console.log(lid+"----"+tenantPic + "----" + landlordPic + "----" + currentUserId);
        //debugger;
        $.ajax({
            url: '/Property/LoadMessages?lid=' + lid,
            success: function (result) {
                console.log(result);
                $('#DZ_W_Contacts_Body3').empty();
                $.each(result, function (i, message) {
                    var html = '';
                    var sentOnDate = new Date(message.sentOn);
                    var formattedDate = sentOnDate.toLocaleDateString('en-GB'); // dd/mm/yyyy format
                    if (currentUserId == message.fromUserID) {
                        html = `
                    <div class="d-flex justify-content-end mb-4">
                        <div class="img_cont_msg">
                            <img src="landlordPic" class="rounded-circle user_img_msg" alt="" />
                        </div>
                        <div class="msg_cotainer">
                            ${message.text}
                            <span class="msg_time">${formattedDate}</span>
                        </div>
                    </div>
                `;
                    } else {
                        html = `
                    <div class="d-flex justify-content-start mb-4">
                        <div class="msg_cotainer_send">
                            ${message.text}
                            <span class="msg_time_send">${formattedDate}</span>
                        </div>
                        <div class="img_cont_msg">
                            <img src="tenantPic" class="rounded-circle user_img_msg" alt="" />
                        </div>
                    </div>
                `;
                    }
                    $('#DZ_W_Contacts_Body3').append(html);
                });
                // Call the chat function here after the ajax call completes
                initializeChatFunction();
            }
        });

    });

    ///----------------------------------------------------

    // This fucntion is used to start signalR hub connection and create a group
    //    between Landlord and selected tenant. 
    function initializeChatFunction() {

        "use strict";

        //connection1 = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

        //Disable the send button until connection is established.
        //document.getElementById("sendButton1").disabled = true;

        const tenantId = document.getElementById("tenantInput").value;
        const landlordId = document.getElementById("landlordInput").value;
        const sentBy = document.getElementById("sendBy").value;


        if (sentBy == "tenant") {
            fromUserID1 = tenantId;
            toUserID1 = landlordId;
        }

        else if (sentBy == "landlord") {
            fromUserID1 = landlordId;
            toUserID1 = tenantId;
        }
        groupName = `Landlord_${landlordId}-Tenant_${tenantId}`;

        if (connection1.state === signalR.HubConnectionState.Connected) {
            // If it's already started, invoke the AddToGroup method
            connection1.invoke("AddToGroup", groupName).catch(function (err) {
                return console.error(err.toString());
            });
        } else {
            // If the connection is not started, start it first and then invoke the AddToGroup method
            connection1.start().then(function () {
                connection1.invoke("AddToGroup", groupName).catch(function (err) {
                    return console.error(err.toString());
                });
            }).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }

     // click of sendbutton1 starts
    document.getElementById("sendButton1").addEventListener("click", function (event) {
        var user = document.getElementById("userInput1").value;
        const message = document.getElementById("messageInput1").value;
        if (message !== null && message !== '') {
            var currentDate = new Date();
            var options = {
                year: "numeric",
                month: "2-digit",
                day: "2-digit",
                hour: "2-digit",
                minute: "2-digit",
                second: "2-digit",
                hour12: true
            };
            var formattedDate = currentDate.toLocaleString("en-US", options);
            //var formattedDate = currentDate.toLocaleDateString('en-GB');
            console.log(groupName);
            console.log(user);
            console.log(message);
            console.log(fromUserID1);
            console.log(toUserID1);
            console.log(formattedDate);
            connection1.invoke("SendMessage", groupName, user, message, fromUserID1, toUserID1, formattedDate).catch(function (err) {
                return console.error(err.toString());
            });
        }
        event.preventDefault();
    });
    //Click of sendbutton1 ends

    //Keyup of messageInput1
    // Get the message input element
    const messageInput = document.getElementById("messageInput1");

    // Add an event listener for the "keyup" event on the message input box
    messageInput.addEventListener("keyup", function (event) {
        // Check if the "Enter" key was pressed
        if (event.keyCode === 13) {
            // Trigger the same function as the "click" event of the "sendButton1"
            var user = document.getElementById("userInput1").value;
            const message = document.getElementById("messageInput1").value;
            if (message !== null && message !== '') {
                var currentDate = new Date();
                var options = {
                    year: "numeric",
                    month: "2-digit",
                    day: "2-digit",
                    hour: "2-digit",
                    minute: "2-digit",
                    second: "2-digit",
                    hour12: true
                };
                var formattedDate = currentDate.toLocaleString("en-US", options);
                //var formattedDate = currentDate.toLocaleDateString('en-GB');
                console.log(groupName);
                console.log(user);
                console.log(message);
                console.log(fromUserID1);
                console.log(toUserID1);
                console.log(formattedDate);
                connection1.invoke("SendMessage", groupName, user, message, fromUserID1, toUserID1, formattedDate).catch(function (err) {
                    return console.error(err.toString());
                });
            }
            event.preventDefault();
        }
    });
//Keyup of messageInput1 ends

    //ReceiveMessage Receive Starts

    connection1.on("ReceiveMessage", function (user, message, sentOn, fromUserID1, msgID) {

        var currentUserId1 = document.getElementById("currentUserId1").value;
        console.log(user);
        //debugger;
        var html;
        if (currentUserId1 == fromUserID1) {
            html = `
                    <div class="d-flex justify-content-end mb-4">
                        <div class="img_cont_msg">
                            <img src="landlordPic" class="rounded-circle user_img_msg" alt="" />
                        </div>
                        <div class="msg_cotainer">
                            ${message}
                            <span class="msg_time">${sentOn}</span>
                        </div>
                    </div>
                `;

        }
        else {
            html = `
                    <div class="d-flex justify-content-start mb-4">
                        <div class="msg_cotainer_send">
                            ${message}
                            <span class="msg_time_send">${sentOn}</span>
                        </div>
                        <div class="img_cont_msg">
                            <img src="tenantPic" class="rounded-circle user_img_msg" alt="" />
                        </div>
                    </div>
                `;
            const audio = new Audio('https://localhost:7163/assets/audio/notification.mp3');
            audio.play();
        }
        $('#DZ_W_Contacts_Body3').append(html);         
        document.getElementById("messageInput1").value = "";
    });
    
    //ReceiveMessage Receive ends

    //MessageUpdated starts
    connection1.on("MessageUpdated", function (isRead) {
        // Update the sender UI to show the second tick (received)
        if (isRead) {
            // Add code to show the received tick
            var messagesList = document.getElementById("messagesList");
            var lastMessage = messagesList.querySelector("li:last-child");
            //Find and remove span with class 'Tick-sent'
            var sentTick = lastMessage.querySelector(".tick-sent");
            lastMessage.removeChild(sentTick);
            // Add a span that contains class with two ticks
            var tickSent = document.createElement("span");
            tickSent.classList.add("tick-seen");
            lastMessage.appendChild(tickSent);
        }
    });

    //MessageUpdated ends
});