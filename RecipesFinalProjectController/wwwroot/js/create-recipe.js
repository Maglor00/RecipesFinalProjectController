document.addEventListener("DOMContentLoaded", function () {
    setupImagePreview();
    setupCategoryLock();
    setupDifficultyLock();
    setupIngredientRowLocks();
});

function setupImagePreview() {
    const imageInput = document.getElementById("imageInput");
    const imagePreview = document.getElementById("imagePreview");
    const imagePreviewContainer = document.getElementById("imagePreviewContainer");

    if (!imageInput || !imagePreview || !imagePreviewContainer) return;

    imageInput.addEventListener("change", function () {
        const file = this.files && this.files[0];

        if (!file) {
            imagePreview.src = "";
            imagePreviewContainer.style.display = "none";
            return;
        }

        const reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
            imagePreviewContainer.style.display = "block";
        };
        reader.readAsDataURL(file);
    });
}

function setupCategoryLock() {
    const newInput = document.querySelector('input[name="NewCategoryName"]');
    const select = document.querySelector('select[name="CategoryId"]');

    if (!newInput || !select) return;

    const syncState = () => {
        const hasNewText = newInput.value.trim().length > 0;
        const hasSelectedValue = select.value !== "";

        if (hasNewText) {
            select.disabled = true;
            newInput.disabled = false;
            select.selectedIndex = 0;
            return;
        }

        if (hasSelectedValue) {
            newInput.disabled = true;
            select.disabled = false;
            return;
        }

        newInput.disabled = false;
        select.disabled = false;
    };

    newInput.addEventListener("input", syncState);
    newInput.addEventListener("change", syncState);
    select.addEventListener("change", syncState);

    syncState();
}

function setupDifficultyLock() {
    const newInput = document.querySelector('input[name="NewDifficultyName"]');
    const select = document.querySelector('select[name="DifficultyId"]');

    if (!newInput || !select) return;

    const syncState = () => {
        const hasNewText = newInput.value.trim().length > 0;
        const hasSelectedValue = select.value !== "";

        if (hasNewText) {
            select.disabled = true;
            newInput.disabled = false;
            select.selectedIndex = 0;
            return;
        }

        if (hasSelectedValue) {
            newInput.disabled = true;
            select.disabled = false;
            return;
        }

        newInput.disabled = false;
        select.disabled = false;
    };

    newInput.addEventListener("input", syncState);
    newInput.addEventListener("change", syncState);
    select.addEventListener("change", syncState);

    syncState();
}

function setupIngredientRowLocks() {
    const rows = document.querySelectorAll("#ingredientsTable tbody tr");

    rows.forEach(row => {
        const newInput = row.querySelector('input[name="NewIngredientNames"]');
        const select = row.querySelector('select[name="IngredientIds"]');

        if (!newInput || !select) return;

        const syncState = () => {
            const hasNewText = newInput.value.trim().length > 0;
            const hasSelectedValue = select.value !== "";

            if (hasNewText) {
                select.disabled = true;
                newInput.disabled = false;
                select.selectedIndex = 0;
                return;
            }

            if (hasSelectedValue) {
                newInput.disabled = true;
                select.disabled = false;
                return;
            }

            newInput.disabled = false;
            select.disabled = false;
        };

        newInput.addEventListener("input", syncState);
        newInput.addEventListener("change", syncState);
        select.addEventListener("change", syncState);

        syncState();
    });
}

function addIngredientRow() {
    const tableBody = document.querySelector("#ingredientsTable tbody");
    if (!tableBody) return;

    const firstRow = tableBody.querySelector("tr");
    if (!firstRow) return;

    const newRow = firstRow.cloneNode(true);

    newRow.querySelectorAll("input").forEach(input => {
        input.value = "";
        input.disabled = false;
    });

    newRow.querySelectorAll("select").forEach(select => {
        select.selectedIndex = 0;
        select.disabled = false;
    });

    tableBody.appendChild(newRow);
    setupIngredientRowLocks();
}

function removeRow(button) {
    const tableBody = document.querySelector("#ingredientsTable tbody");
    if (!tableBody) return;

    const rows = tableBody.querySelectorAll("tr");

    if (rows.length === 1) {
        const row = rows[0];

        row.querySelectorAll("input").forEach(input => {
            input.value = "";
            input.disabled = false;
        });

        row.querySelectorAll("select").forEach(select => {
            select.selectedIndex = 0;
            select.disabled = false;
        });

        return;
    }

    const row = button.closest("tr");
    if (row) {
        row.remove();
    }
}