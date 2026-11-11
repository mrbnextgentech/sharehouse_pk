 
function initializeCKEditor(hiddenfield) {
        ClassicEditor
            .create(document.querySelector('#editor'), {
                // CKEditor configuration options
            })
            .then(editor => {
                const content = document.querySelector('#' + hiddenfield).value;
                editor.setData(content);  /// get value from DB & populate RTE

                //Get value from RTE & save in hidden field to post back to server
                editor.model.document.on('change:data', () => {
                    document.querySelector('#'+hiddenfield).value = editor.getData();
                });
            })
            .catch(error => {
                console.error(error);
            });
    }