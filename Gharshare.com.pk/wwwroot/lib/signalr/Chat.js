"use strict";
$(document).ready(function () {

var connection1 = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
//document.getElementById("sendButton1").disabled = true;

connection1.on("ReceiveMessage", function (user, message, sentOn, fromUserID1,msgID) {
    var currentUserId1 = document.getElementById("currentUserId1").value;
    console.log(user);
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    if (currentUserId1 == fromUserID1)
        user = "You";
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    if (user == "You")
        li.innerHTML = `<strong>${user} say ${message}</strong><br>${sentOn}<span class="tick-sent"></span>`;
    else
    {
        li.innerHTML = `${user} says ${message}<br>${sentOn}<span class="tick-seen"></span>`;
        connection1.invoke("UpdateMessage", true, fromUserID1, msgID);
    } 
    const audio = new Audio('https://localhost:7163/assets/audio/notification.mp3');
    audio.play();
    document.getElementById("messageInput1").value = "";
});
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
 
    const tenantId = document.getElementById("tenantInput").value;
    const landlordId = document.getElementById("landlordInput").value;
    const sentBy = document.getElementById("sendBy").value;

var fromUserID1 = "";
var toUserID1="";
if (sentBy == "tenant")
{
    fromUserID1 = tenantId;
    toUserID1 = landlordId;
}
    
else if (sentBy == "landlord")
{
    fromUserID1 = landlordId;
    toUserID1 = tenantId;
}

    const groupName = `Landlord_${landlordId}-Tenant_${tenantId}`;    
    connection1.start().then(function () {
        connection1.invoke("AddToGroup", groupName).catch(function (err) {
            return console.error(err.toString());
        });
    }).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendButton1").disabled = false;

    document.getElementById("sendButton1").addEventListener("click", function (event) {
    var user = document.getElementById("userInput1").value;
        const message = document.getElementById("messageInput1").value;
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
window.addEventListener("unload", function (event) {
    connection1.invoke("RemoveFromGroup", groupName).catch(function (err) {
        return console.error(err.toString());
    });
});

});