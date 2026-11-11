document.getElementById("Custrfr").addEventListener("click", function () {
    let intValue;
    while (true) {
        let userInput = prompt("Enter an integer value:");

        if (userInput === null) return; // If user cancels, exit the function

        intValue = parseInt(userInput, 10);

        if (!isNaN(intValue) && intValue > 6) break; // Validate integer and ensure it's > 6

        alert("Invalid input! Please enter an integer greater than 6.");
    }

    this.value = intValue;
    document.getElementById("lblCustom").textContent = intValue;
});
//// Enter your own number for bed
document.getElementById("custbed").addEventListener("click", function () {
    let intValue;
    while (true) {
        let userInput = prompt("Enter an integer value:");

        if (userInput === null) return; // If user cancels, exit the function

        intValue = parseInt(userInput, 10);

        if (!isNaN(intValue) && intValue > 6) break; // Validate integer and ensure it's > 6

        alert("Invalid input! Please enter an integer greater than 6.");
    }

    this.value = intValue;
    document.getElementById("lblcustbed").textContent = intValue;
});
//// Enter custom bath number
document.getElementById("Custbath").addEventListener("click", function () {
    let intValue;
    while (true) {
        let userInput = prompt("Enter an integer value:");

        if (userInput === null) return; // If user cancels, exit the function

        intValue = parseInt(userInput, 10);

        if (!isNaN(intValue) && intValue > 6) break; // Validate integer and ensure it's > 6

        alert("Invalid input! Please enter an integer greater than 6.");
    }

    this.value = intValue;
    document.getElementById("lblcustbath").textContent = intValue;
});

//// Enter custom
document.getElementById("custflatmat").addEventListener("click", function () {
    let intValue;
    while (true) {
        let userInput = prompt("Enter an integer value:");

        if (userInput === null) return; // If user cancels, exit the function

        intValue = parseInt(userInput, 10);

        if (!isNaN(intValue) && intValue > 6) break; // Validate integer and ensure it's > 6

        alert("Invalid input! Please enter an integer greater than 6.");
    }
    this.value = intValue;
    document.getElementById("lblcustflatmat").textContent = intValue;
});