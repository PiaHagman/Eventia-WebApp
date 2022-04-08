
const resetBtn = document.getElementById("reset-btn");
const addEventBtn = document.getElementById("registerSubmit");

resetBtn.addEventListener("click",
    () => {
        alert("Database restored!");
    });

addEventBtn.addEventListener("click", () => {
    alert("Your event has been added!");
})