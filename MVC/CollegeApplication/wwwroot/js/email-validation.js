// Email Validation
$(document).ready(function () {
    var emailInput = 'input[type="email"], input[id*="Email"]';
    var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    
    // Visual validation
    $(emailInput).on('input blur', function () {
        var input = $(this);
        var val = input.val().trim();
        input.removeClass('is-valid is-invalid');
        
        if (val && emailRegex.test(val)) {
            input.addClass('is-valid');
        } else if (val) {
            input.addClass('is-invalid');
        }
    });
    
    // Auto lowercase
    $(emailInput).on('input', function () {
        var input = $(this);
        var pos = this.selectionStart;
        var val = input.val().toLowerCase();
        if (input.val() !== val) {
            input.val(val);
            this.setSelectionRange(pos, pos);
        }
    });
    
    // Prevent spaces
    $(emailInput).on('keypress', function (e) {
        if (e.which === 32) {
            e.preventDefault();
            return false;
        }
    });
});
