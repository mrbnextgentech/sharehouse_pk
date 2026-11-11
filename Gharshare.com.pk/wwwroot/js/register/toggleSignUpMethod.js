document.querySelectorAll('input[name="ltype"]').forEach(radio => {
    radio.addEventListener('click', function () {
        toggleSignUpMethod(this.value);
    });
});

function toggleSignUpMethod(value) {
    //debugger;
    const emailDiv = document.getElementById("divSignUpByEmail");
    const mobileDiv = document.getElementById("divSignUpByMobile");

    if (value === "email") {
        emailDiv.classList.remove("d-none");
        mobileDiv.classList.add("d-none");
        document.getElementById("txtmobile").value = "";
    } else if (value === "phone") {
        emailDiv.classList.add("d-none");
        mobileDiv.classList.remove("d-none");
        document.getElementById("txtusername").value = "";
    }
}
document.getElementById("txtmobile").addEventListener("input", function () {
    this.value = this.value.replace(/[^0-9]/g, ""); // Remove non-numeric characters
});