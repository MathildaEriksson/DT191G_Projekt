/* Mathilda Eriksson, DT191G, VT24 */

document.addEventListener('DOMContentLoaded', () => {
    const filterDialogBackdrop = document.querySelector('.bg-black.bg-opacity-25');
    const filterDialog = document.querySelector('.relative.z-40[aria-modal="true"]');
    const closeFilterButton = filterDialog.querySelector('button');
    const filterToggleButton = document.getElementById('filterToggle');

    // Function to toggle mobile filter dialog visibility
    const toggleFilterDialog = (isVisible) => {
        filterDialogBackdrop.style.display = isVisible ? 'block' : 'none';
        filterDialog.style.display = isVisible ? 'flex' : 'none';
    };

    // Event listener to open filter dialog
    filterToggleButton.addEventListener('click', () => toggleFilterDialog(true));

    // Event listener to close filter dialog
    closeFilterButton.addEventListener('click', () => toggleFilterDialog(false));

    // Handling the backdrop click to close the dialog
    filterDialogBackdrop.addEventListener('click', () => toggleFilterDialog(false));
});
