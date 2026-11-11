"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/AdminchatHub").build();

//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true; 
connection.on("ReceiveMessage", function (user, usertype, message, sentOn, fromUserID, msgID) {
    var currentUserId = document.getElementById("currentUserId").value;
    const audio = new Audio('https://localhost:7163/assets/audio/notification.mp3');

    var messageDiv = document.getElementById("messagediv");
        var newMessageDiv = document.createElement("div");        
        if (currentUserId == fromUserID)
            {
            newMessageDiv.classList.add("messages__item");
            newMessageDiv.classList.add("messages__item--operator");            
            newMessageDiv.innerHTML = `<strong>You say ${message}</strong><br>${sentOn}<span class="tick-sent"></span>`;
            messageDiv.insertBefore(newMessageDiv, messageDiv.firstChild)
        }             
        else {            
            newMessageDiv.classList.add("messages__item");
            newMessageDiv.classList.add("messages__item--visitor");
            newMessageDiv.innerHTML = `${user} says ${message}<br>${sentOn}<span class="tick-seen"></span>`;
            messageDiv.insertBefore(newMessageDiv, messageDiv.firstChild)
            connection.invoke("UpdateMessage", true, fromUserID, msgID);

            // Show the chat window
            const chatButton = document.querySelector('.chatbox__button');
            const chatContent = document.querySelector('.chatbox__support');
            const icons = {
                isClicked: '<img src="https://localhost:7163/images/icons/chatbox-icon.svg" />',
                isNotClicked: '<img src="https://localhost:7163/images/icons/chatbox-icon.svg" />'
            }
            const chatbox = new InteractiveChatbox(chatButton, chatContent, icons);
            chatbox.state = true;
            chatbox.display();
            chatbox.toggleIcon(false, chatButton);
            chatbox.showOrHideChatBox(chatContent, chatButton);
            audio.play();
        // chat window ends           
    }
    
    document.getElementById("messageInput").value = "";
    if (usertype == "Tenant" || usertype == "Landlord") {
        console.log(usertype);
        document.getElementById('senderId').value = fromUserID;         
    }    
});

connection.on("MessageUpdated", function (isRead) {
    // Update the sender UI to show the second tick (received)
    if (isRead) {
        console.log("Only sender UI will be updated");
        // Add code to show the received tick
        var messagesList = document.getElementById("messagediv"); 
        var lastMessage = messagesList.querySelector("div:first-child");
        //Find and remove span with class 'Tick-sent'
        var sentTick = lastMessage.querySelector(".tick-sent");
        if(sentTick !=null)
        lastMessage.removeChild(sentTick);        
        // Add a span that contains class with two ticks        
        var tickSent = document.createElement("span");        
        tickSent.classList.add("tick-seen");        
        lastMessage.appendChild(tickSent);
    }
});
    
var fromUserID = "";
var toUserID = "";
var defaultAdminName = "";
var defaultAdminId = "";
var chatRoom = ""; 

connection.start().then(function () {
    //console.log("Connected to the Admin chat hub!");
    var currentUserId = document.getElementById("currentUserId").value;
    var userType = document.getElementById("userType").value;  

    if (userType == "Admin")
    {
        defaultAdminId = currentUserId;
        connection.invoke("GetUserIds", userType)
            .then(function (userIds) {
                for (var i = 0; i < userIds.length; i++) {
                    var userId = userIds[i];
                    chatRoom = `G_Admin_${userId}`;
                    connection.invoke("AddToGroup", chatRoom).catch(function (err) {
                        return console.error(err.toString());
                    });
                }
            })
            .catch(function (err) {
                return console.error(err.toString());
            });
    }
    else if (userType == "Landlord" || userType == "Tenant") {
        chatRoom = `G_Admin_${currentUserId}`;
        connection.invoke("AddToGroup", chatRoom).catch(function (err) {
            return console.error(err.toString());
        }); 

        /*Get Default Admin id and name*/
        connection.invoke("GetDefaultAdmin")
            .then(function (defaultAdmin)
            {
                defaultAdminId = defaultAdmin["Id"];
                defaultAdminName = defaultAdmin["Name"];
                //console.log(defaultAdminId);
                //console.log(defaultAdminName);
            })
            .catch(function (err) {
                return console.error(err.toString());
            });
        /* End Get Default Admin id and name*/
    }
    }).catch(function (err) {
        return console.error(err.toString());
    });
    //document.getElementById("sendButton").disabled = false;
    document.getElementById("sendButton").addEventListener("click", function (event) {    
    var user = document.getElementById("userInput").value;    
    var usertype = document.getElementById("userType").value; 
    var currentUserId = document.getElementById("currentUserId").value; 
        var senderId = "";
        if (usertype == "Landlord" || usertype == "Tenant")
        {
            chatRoom = `G_Admin_${currentUserId}`;
            fromUserID = currentUserId;
            toUserID = defaultAdminId;
            //console.log("L/T: from User ID=" + fromUserID);
            //console.log("L/T: To User ID=" + toUserID);
        }            
        else {             
            senderId = document.getElementById("senderId").value;             
            chatRoom = `G_Admin_${senderId}`;
            fromUserID = defaultAdminId;
            toUserID = senderId;
            //console.log("Admin: from User ID=" + fromUserID);
            //console.log("Admin: To User ID=" + toUserID);
        }          
        const message = document.getElementById("messageInput").value;
        if (message !== null && message !== '')
        {  
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
           // formattedDate = new Date(formattedDate).toUTCString();
            //var formattedDate = currentDate.toString();
            fromUserID = document.getElementById("currentUserId").value;            
            connection.invoke("SendMessage", chatRoom, user, usertype, message, fromUserID, toUserID, formattedDate).catch(function (err) {
        return console.error(err.toString());
    });
        }
    event.preventDefault();
});
window.addEventListener("unload", function (event) {
    connection.invoke("RemoveFromGroup", chatRoom).catch(function (err) {
        return console.error(err.toString());
    });
});
$(document).ready(function () {
    $("#messageInput").keydown(function (event) {
        if (event.keyCode === 13) {
            $("#sendButton").click();
        }
    });
});