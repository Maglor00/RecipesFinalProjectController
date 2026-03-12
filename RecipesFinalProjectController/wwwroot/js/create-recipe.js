document.addEventListener("DOMContentLoaded", function () {
    setupCreateRecipeImagePreview();
    setupCreateRecipeIngredientTable();
});

function setupCreateRecipeImagePreview() {
    const imageInput = document.getElementById("imageInput");
    const imagePreview = document.getElementById("imagePreview");

    if (!imageInput || !imagePreview) return;

    imageInput.addEventListener("change", function () {
        const file = this.files && this.files[0];
        if (!file) {
            imagePreview.src = "";
            return;
        }

        const reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
        };
        reader.readAsDataURL(file);
    });
}

function setupCreateRecipeIngredientTable() {
    const table = document.getElementById("ingredientsTable");
    if (!table) return;

    window.addIngredientRow = function () {
        const tbody = table.querySelector("tbody");
        if (!tbody || tbody.rows.length === 0) return;

        const firstRow = tbody.rows[0];
        const newRow = firstRow.cloneNode(true);

        newRow.querySelectorAll("input").forEach(input => {
            input.value = "";
        });

        newRow.querySelectorAll("select").forEach(select => {
            select.selectedIndex = 0;
        });

        tbody.appendChild(newRow);
    };

    window.removeRow = function (button) {
        const tbody = table.querySelector("tbody");
        if (!tbody) return;

        if (tbody.rows.length === 1) {
            tbody.rows[0].querySelectorAll("input").forEach(input => {
                input.value = "";
            });

            tbody.rows[0].querySelectorAll("select").forEach(select => {
                select.selectedIndex = 0;
            });

            return;
        }

        const row = button.closest("tr");
        if (row) row.remove();
    };
}