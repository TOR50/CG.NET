// Name Validation - Only letters and spaces
$(document).ready(function () {
    var nameInputs = 'input[id*="FullName"], input[id*="FatherName"], input[id*="MotherName"]';
    
    // Block invalid characters on keypress
    $(nameInputs).on('keypress', function (e) {
        var char = String.fromCharCode(e.which);
        if (!/^[a-zA-Z\s]$/.test(char)) {
            e.preventDefault();
            return false;
        }
    });
    
    // Clean pasted content
    $(nameInputs).on('paste', function () {
        var input = $(this);
        setTimeout(function () {
            var cleaned = input.val().replace(/[^a-zA-Z\s]/g, '');
            if (input.val() !== cleaned) input.val(cleaned);
        }, 10);
    });
    
    // Visual validation on input
    $(nameInputs).on('input blur', function () {
        var input = $(this);
        var val = input.val().trim();
        input.removeClass('is-valid is-invalid');
        if (val && /^[a-zA-Z\s]+$/.test(val)) {
            input.addClass('is-valid');
        } else if (val) {
            input.addClass('is-invalid');
        }
    });
});
