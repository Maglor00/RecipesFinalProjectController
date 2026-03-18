document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("togglePasswordFormBtn");
    const container = document.getElementById("changePasswordContainer");

    if (!toggleBtn || !container) return;

    toggleBtn.addEventListener("click", function () {
        const isHidden = container.style.display === "none" || container.style.display === "";

        if (isHidden) {
            container.style.display = "block";
            toggleBtn.textContent = "Hide Change Password";
        } else {
            container.style.display = "none";
            toggleBtn.textContent = "Change Password";
        }
    });
});