// Date of Birth Validation - Age 18-60
$(document).ready(function () {
    var dobInput = 'input[type="date"][id*="DateOfBirth"], input[id*="dateOfBirth"]';
    
    $(dobInput).on('change blur', function () {
        var input = $(this);
        var dobVal = input.val();
        if (!dobVal) return;
        
        var dob = new Date(dobVal);
        var today = new Date();
        today.setHours(0, 0, 0, 0);
        
        var errorSpan = input.siblings('.text-danger');
        input.parent().find('.age-display').remove();
        
        // Check future date
        if (dob > today) {
            input.removeClass('is-valid').addClass('is-invalid');
            errorSpan.text('Date cannot be in the future.');
            return;
        }
        
        // Calculate age
        var age = today.getFullYear() - dob.getFullYear();
        var m = today.getMonth() - dob.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < dob.getDate())) age--;
        
        // Show age
        input.parent().append('<small class="form-text text-info age-display">Age: ' + age + ' years</small>');
        
        // Validate age range
        if (age < 18) {
            input.removeClass('is-valid').addClass('is-invalid');
            errorSpan.text('Must be at least 18 years old.');
        } else if (age > 60) {
            input.removeClass('is-valid').addClass('is-invalid');
            errorSpan.text('Age cannot exceed 60 years.');
        } else {
            input.removeClass('is-invalid').addClass('is-valid');
            errorSpan.text('');
        }
    });
});
