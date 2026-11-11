function createPropertyList(properties, loggedIn, userId) {
    //debugger
    var html = '';
    for (let item of properties) {
        html += `<div class="col-lg-4 col-sm-6 col-12">
                                <div class="ltn__blog-item ltn__blog-item-3">
                                    <div class="ltn__blog-img">
                                        <a href="/property/details?listingid=${item.uId}">
                                            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel" data-interval="false">
                                                <ol class="carousel-indicators">
                                                    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active">
                                                    </li>                                                     
                                                </ol>
                                                <div class="carousel-inner">
                                                    <div class="carousel-item active">
                                                        <img src="${item.coverImage}" class="d-block w-100" alt="..." style="min-height:360px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </a>`
        if (loggedIn && userId != "" && userId != null)
            html += `<a class="shortlistButton"
                                               onclick="shortPlacelisting('${item.id}','${userId}')">
                                                <i class="fa-regular fa-star fa-xl"
                                                   style="color: #ffffff;"></i>
                                            </a>`
        else {
            html += `<a class="shortlistButton" href="/login">
                                                <i class="fa-regular fa-star fa-xl" style="color: #ffffff;"></i>
                                            </a>`
        }
        html +=`<div class="sendMessage ${item.owner.freetoMessage ? '' : ' d-none'}">
            <span>Free to Message</span>
        </div>
                                    </div >
            <div class="ltn__blog-brief">
                <div class="ltn__blog-meta">
                    <ul>
                        <li class="ltn__blog-author">
                            <h6><span>${GetRent(item)}</span></h6>
                        </li>
                    </ul>
                    <div class="ltn__blog-meta">
                        <div class="row">
                            <div class="col-md-5">
                                <i class="fa fa-bath" style="color: #5c727d;"> <span>${item.bathroom}</span></i>
                                <i class="fa fa-bed" style="color: #5c727d;"> <span>${item.bed}</span></i>
                                <i class="fa fa-user" style="color: #5c727d;"> <span>${item.totalflatmates}</span></i>
                            </div>
                            <div class="col-md-7">
                                <ul>
                                    <li>
                                        <p>${GetPropertyType(item.accommodationtype)}</p>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <p>
                        <a href="/property/details?listingid=${item.uid}">
                        </a></p><p><a href="/property/details?listingid=${item.uid}">
                            ${getShortDescription(item.description, 30)}</a></p>
                    <a href="/property/details?listingid=${item.uid}">
                    </a>
                    <p></p>
                    <div class="ltn__blog-btn">
                        ${getPropertyAvailability(item)}
                    </div>

                </div>
            </div>
                                </div >
                            </div > `;
    }
    $('#divPropertyListing').append(html);
}
function getPropertyAvailability(placelisting) {
    //debugger;
    //console.log('inside getAvailability')
    var currentDate = new Date();
    var availableNow = false;
    var availableFrom = null;

    if (placelisting.accommodationtype === 2) {
        if (new Date(placelisting.availableFrom) <= currentDate) {
            availableNow = true;
        } else {
            availableFrom = formatDateToDDMMYY(placelisting.availableFrom);
        }
    } else {
        if (placelisting.rooms.length > 0) {
            if (new Date(placelisting.rooms[0].availableFrom) <= currentDate) {
                availableNow = true;
            } else {
                availableFrom = formatDateToDDMMYY(placelisting.rooms[0].availableFrom);
            }
        }
    }

    if (availableNow) {
        return "Available now";
    } else if (availableFrom != null || availableFrom != "") {
        return "Available from " + availableFrom;
    } else {
        return "Not available";
    }
}
function GetPropertyType(accommodationType) {
    switch (parseInt(accommodationType)) {
        case 1:
            return "Room in sharehouse";
        case 2:
            return "Whole property for rent";
        case 3:
            return "Student accommodation";
        case 4:
            return "Homestay";
        case 5:
            return "Flat";
        case 6:
            return "Room in Flatshare";
        default:
            return "Unknown";
    }
}
function GetRent(placelisting) {
    if (placelisting.accommodationtype === 2) { // whole property case
        if (placelisting.rent != null && placelisting.rent > 0)
            return "Rs. " + formatCurrency(placelisting.rent) + "/month";
        else
            return "NA";
    } else {
        if (placelisting.rooms.length > 0) {
            var minRent = Math.min(...placelisting.rooms.map(room => room.rent));
            var maxRent = Math.max(...placelisting.rooms.map(room => room.rent));

            if (minRent === maxRent) {
                if (minRent != null && minRent > 0)
                    return "Rs. " + minRent + "/month";
                else
                    return "NA";
            } else if (minRent > 0 && maxRent > 0) {
                return "Rs. " + formatCurrency(minRent) + "-" + formatCurrency(maxRent) + "/month";
            }
            else
                return "NA";
        }
    }
}