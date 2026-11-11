// This function is used to load different fields of property based on the view passed.
function loadProperty(viewname) {

    var propertyId = $('#txtPropertyId').val();

    $.ajax({
        url: '/listing/loadpropertyinpopup?propertyid=' + propertyId + '&&viewname=' + viewname,
        type: 'GET',
        success: function (data) {
            console.log(data);
            $('#accountSettingsContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}