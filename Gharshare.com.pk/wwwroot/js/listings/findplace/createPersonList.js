function createPersonList(listings, loggedIn, userId) {
    var html = '';
    for (let item of listings) {
        html += ` <div class="col-lg-4 col-sm-6 col-12">
                                <div class="ltn__List-item ltn__blog-item-3">
                                    <div class="ltn__blog-img">
                                        <a href="/person/details?listingId=${item.uId}">
                                            <div id="carouselExampleIndicators" class="carousel slide"
                                            data-ride="carousel" data-interval="false">                                                 
                                                <div class="carousel-inner">
                                                    <div class="carousel-item active">
                                                        <img src="${item.user.profilePicture || '/img/default-user.jpg'}" 
                                                        class="d-block w-100" alt="..." style="min-height:360px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </a>`
        if (loggedIn && userId != "" && userId != null) {
            html += `<a class="shortlistButton"
                                               onclick="shortlistPerson('${item.id}','${userId}')">
                                                <i class="fa-regular fa-star fa-xl"
                                                   style="color: #ffffff;"></i>
                                            </a>`}
        else {
            html += `<a class="shortlistButton" href="/login">
                                                <i class="fa-regular fa-star fa-xl" style="color: #ffffff;"></i>
                                            </a>`}
        html += `<div class="sendMessage" ${item.user.freetoMessage ? '' : ' d-none'}>
                                                <span>Free to Message</span>
                                            </div>
                                    </div>
                                    <div class="ltn__blog-brief">
                                        <div class="ltn__blog-meta">
                                            <ul>
                                                <li class="ltn__blog-author">
                                                    <h6>${item.user.name}</h6>
                                                </li>
                                            </ul>
                                            <ul>
                                                <li>
                                                    <p>
                                                        <span>Rs.</span>${item.monthlyBudget}/month
                                                    </p>
                                                </li>
                                                <li>
                                                    <p>
                                                        <span> • </span>42 year old
                                                        ${GetGender(item.user.gender)}
                                                    </p>
                                                </li>
                                            </ul>
                                        </div>
                                        <p>
                                            <a href="/findplace/findplacepreview?listingId=${item.uId}">
                                            </a>
                                        </p>
                                            <div class="ltn__blog-btn">
                                                ${getAvailability(item)}
                                            </div>
                                    </div>
                                </div>
                            </div>`;
    }
    $('#divPersonListing').append(html);
}
function getAvailability(listing) {
    var currentDate = new Date();
    var availableNow = false;
    var availableFrom = null;

    if (new Date(listing.moveDate) <= currentDate) {
        availableNow = true;
    } else {
        availableFrom = formatDateToDDMMYY(listing.moveDate);
    }

    if (availableNow) {
        return "Available now";
    } else if (availableFrom !=null) {
        return "Available from " + availableFrom;
    } else {
        return "Not available";
    }
}
function GetGender(type) {
    switch (parseInt(type)) {
        case 1:
            return "Male";
        case 2:
            return "Female";
        case 3:
            return "Transgender";         
        default:
            return "Undisclosed";
    }
}