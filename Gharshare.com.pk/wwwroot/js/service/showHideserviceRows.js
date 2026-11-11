$(document).ready(function () {
    $('#ddlservicetype').on('change', function () {
        var serviceId = $(this).find(':selected').val();
       // alert(serviceId);
        //ajax call
        if (serviceId.trim() > 0) {
            $.ajax({
                url: '/admin/service/selectservice?id=' + serviceId.trim(),
                type: 'GET',
                success: function (data) {
                    //alert(data.servicetype);
                    switch (data.servicetype) {
                        case 1:
                            $('#dvListingsize').show();
                            $('#sendmessagehidden').empty();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').hide();
                            $('#dv30day').hide();
                            $('#sendmessagehidden').empty();
                            $('#seemobilehidden').empty();
                            $('#30dayhidden').empty();
                            break;
                        case 2:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').show();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').hide();
                            $('#dv30day').hide();
                            $('#sendmessagehidden').empty();
                            $('#seemobilehidden').empty();
                            $('#30dayhidden').empty();
                            break;
                        case 3:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').show();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').hide();
                            $('#dv30day').hide();
                            $('#sendmessagehidden').empty();
                            $('#seemobilehidden').empty();
                            $('#30dayhidden').empty();
                            break;
                        case 4:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').show();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').hide();
                            $('#dv30day').hide();
                            $('#sendmessagehidden').empty();
                            $('#seemobilehidden').empty();
                            $('#30dayhidden').empty();
                            break;
                        case 5:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').show();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').hide();
                            $('#dv30day').hide();
                            $('#sendmessagehidden').empty();
                            $('#seemobilehidden').empty();
                            $('#30dayhidden').empty();
                            break;
                        case 6:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').show();
                            var html = '<input type="hidden" name="planservice.sendreceivemessage" value="false" />'
                            $('#sendmessagehidden').append(html);
                            $('#seemobilehidden').empty();
                            $('#30dayhidden').empty();                             
                            $('#dvSeemobile').hide();
                            $('#dv30day').hide();
                            break;
                        case 7:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').show();
                            var html = '<input type="hidden" name="planservice.seemobilenumber" value="false" />';
                            $('#seemobilehidden').append(html);
                            $('#30dayhidden').empty();                            
                            $('#sendmessagehidden').empty();
                            $('#dv30day').hide();
                            break;
                        case 8:
                            $('#dvListingsize').hide();
                            $('#dvSearchranking').hide();
                            $('#dvTopsearch').hide();
                            $('#dvPropertylisting').hide();
                            $('#dvPersonlisting').hide();
                            $('#dvSendmessage').hide();
                            $('#dvSeemobile').hide();
                            $('#dv30day').show();
                            var html = '<input type="hidden" name="planservice.thirtydayguarantee" value="false" />';
                            $('#30dayhidden').append(html);
                            $('#seemobilehidden').empty();
                            $('#sendmessagehidden').empty();                            
                            break;
                        default:
                            // Default case if servicetype doesn't match any known type
                            break;
                    }
                },
                error: function (xhr, status, error) {
                    // Handle error
                    //debugger;
                    //console.log(xhr);
                }
            }); //end of ajax call
        }
        else {
            $('#dvListingsize').hide();
            $('#dvSearchranking').hide();
            $('#dvTopsearch').hide();
            $('#dvPropertylisting').hide();
            $('#dvPersonlisting').hide();
            $('#dvSendmessage').hide();
            $('#dvSeemobile').hide();
            $('#dv30day').hide();  
        }        
    });

});
