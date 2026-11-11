$(function () {
    $('#availability #inspection').on('click', '.delete', function () {
        $(this).closest('.inspection').remove();
        updateIdsAndNames();
    });
    //let inspectionCount = 0;
    $('#addinspection').click(function () { 
        const $inspectionRows = $('#availability .inspection');
        let inspectionCount = $inspectionRows.length;
        // Increment the inspectionCount to use as an index for IDs and names
        //inspectionCount++;
        // Create the dynamic HTML structure using a template literal
        const dynamicHTML = `
          <div class="row mb-3 inspection">
            <div class="col-md-6">
              <label class="text-label form-label">Inspection date?</label>
              <div class="input-group">
                <input type="text" class="datepicker form-control" data-val="true" data-val-required="The InspectionDate field is required." id="Inspections_${inspectionCount}__InspectionDate" name="Inspections[${inspectionCount}].InspectionDate" value="">
                <div class="input-group-text fa fa-calendar"></div>
                <span class="text-danger field-validation-valid" data-valmsg-for="Inspections[${inspectionCount}].InspectionDate" data-valmsg-replace="true"></span>
              </div>
            </div>
            <div class="col-md-3">
              <label class="text-label form-label">From</label>
              <select class="nice-select wide w-100 mb-3" id="Inspections_${inspectionCount}__From" name="Inspections[${inspectionCount}].From">
                <option value="">--Pick time--</option>
                <option value="09:00 am">09:30 am</option>
                <option value="09:30 am">10:00 am</option>
                <option value="10:00 am">10:30 am</option>
                <option value="10:30 am">11:00 am</option>
                <option value="11:30 pm">12:30 pm</option>
                <option value="12:00 pm">12:30 pm</option>
                <option value="12:30 pm">01:00 pm</option>
                <option value="01:30 pm">02:00 pm</option>
                <option value="02:30 pm">03:00 pm</option>
                <option value="03:00 pm">03:30 pm</option>
                <option value="04:00 pm">04:30 pm</option>
                <option value="04:30 am">05:00 pm</option>
              </select>
            </div>
            <div class="col-md-3">
              <label class="text-label form-label">To</label>
              <select class="nice-select wide w-100 mb-3" id="Inspections_${inspectionCount}__To" name="Inspections[${inspectionCount}].To">
                <option value="">--Pick time--</option>
                <option value="09:00 am">09:30 am</option>
                <option value="09:30 am">10:00 am</option>
                <option value="10:00 am">10:30 am</option>
                <option value="10:30 am">11:00 am</option>
                <option value="11:30 pm">12:30 pm</option>
                <option value="12:00 pm">12:30 pm</option>
                <option value="12:30 pm">01:00 pm</option>
                <option value="01:30 pm">02:00 pm</option>
                <option value="02:30 pm">03:00 pm</option>
                <option value="03:00 pm">03:30 pm</option>
                <option value="04:00 pm">04:30 pm</option>
                <option value="04:30 am">05:00 pm</option>        
              </select>
            </div>
            <a href="#" class="text-end delete">Remove Inspection Time</a>
          </div>
        `;
        // Append the dynamicHTML to the div with id "inspection"
        $('#availability #inspection').append(dynamicHTML);
        bindDatetimepicker();
    });
    function updateIdsAndNames() {
        // Get all inspection rows inside the "availability" div
        const $inspectionRows = $('#availability .inspection');

        // Loop through each inspection row and update IDs and names
        $inspectionRows.each(function (index) {
            const newIndex = index;
            const oldId = $(this).attr('id');
            const newId = `inspection-${newIndex}`;
            $(this).attr('id', newId);

            // Update IDs and names of input elements inside the current inspection row
            $(this).find('input, select').each(function () {
                const oldName = $(this).attr('name');
                const newName = oldName.replace(/\[\d+\]/, `[${newIndex}]`);
                $(this).attr('name', newName);
                $(this).attr('id', newName.replace(/\./g, '_'));
            });
        });

        // Update the inspectionCount
        inspectionCount = $inspectionRows.length;
    }

    function bindDatetimepicker() {
        // Select all inputs of type text with class .datepicker inside .inspection within #availability
        const datepickerInputs = $('#availability #inspection .inspection input[type="text"].datepicker');
        //debugger;
        // Iterate through each input and bind the DateTimePicker plugin
        if (typeof jQuery !== 'undefined') {
            datepickerInputs.each(function () {
                $(this).datetimepicker({
                    // Add your desired options here
                    format: 'd/m/Y', // Adjust this according to your needs
                    date: new Date(),
                    minDate: new Date(),
                    timepicker: false,
                });
                // alert('hell');
            });
        }
    }
});