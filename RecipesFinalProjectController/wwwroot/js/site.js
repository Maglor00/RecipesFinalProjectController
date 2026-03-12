// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    setupDismissibleAlerts();
});

function setupDismissibleAlerts() {
    const alerts = document.querySelectorAll(".alert[data-auto-hide='true']");
    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.transition = "opacity 0.4s ease";
            alert.style.opacity = "0";
            setTimeout(() => alert.remove(), 400);
        }, 3000);
    });
}
