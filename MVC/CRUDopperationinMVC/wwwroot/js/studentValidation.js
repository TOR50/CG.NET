// Basic JavaScript validation for Student form

function validateStudentForm() {
    var name = document.getElementById('Name').value.trim();
    var age = document.getElementById('Age').value;
    var marks = document.getElementById('Marks').value;

    // Validate Name
    if (name === "") {
        alert("Name is required!");
        return false;
    }

    if (name.length > 100) {
        alert("Name cannot be longer than 100 characters!");
        return false;
    }

    // Validate Age
    if (age === "" || age < 5 || age > 100) {
        alert("Age must be between 5 and 100!");
        return false;
    }

    // Validate Marks
    if (marks === "" || marks < 0 || marks > 100) {
        alert("Marks must be between 0 and 100!");
        return false;
    }

    return true;  // All validations passed
}

// Test function - Open browser console (F12) and run: testValidation()
function testValidation() {
    console.log("=== Validation Tests ===");
    
    // Test 1: Empty name
    console.log("Test 1: Empty name - Should fail");
    var test1 = "" === "" ? "PASS: Caught empty name" : "FAIL";
    console.log(test1);
    
    // Test 2: Age out of range
    console.log("Test 2: Age = 150 - Should fail");
    var test2 = 150 > 100 ? "PASS: Caught invalid age" : "FAIL";
    console.log(test2);
    
    // Test 3: Marks out of range
    console.log("Test 3: Marks = 105 - Should fail");
    var test3 = 105 > 100 ? "PASS: Caught invalid marks" : "FAIL";
    console.log(test3);
    
    // Test 4: Valid data
    console.log("Test 4: Valid data - Should pass");
    var name = "John";
    var age = 20;
    var marks = 85;
    var test4 = (name !== "" && age >= 5 && age <= 100 && marks >= 0 && marks <= 100) 
        ? "PASS: Valid data accepted" 
        : "FAIL";
    console.log(test4);
    
    console.log("=== Tests Complete ===");
}
