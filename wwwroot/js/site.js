/* Mathilda Eriksson, DT191G, VT24 */

document.addEventListener('DOMContentLoaded', function() {
    // Toggle mobile menu
    const btn = document.querySelector('[aria-controls="mobile-menu"]');
    const menu = document.getElementById('mobile-menu');
    const openIcon = document.getElementById('menuOpenIcon'); 
    const closeIcon = document.getElementById('menuCloseIcon'); 
    
    if (btn && menu) {
        btn.addEventListener('click', () => {
            const expanded = btn.getAttribute('aria-expanded') === 'true';
            btn.setAttribute('aria-expanded', !expanded);
            menu.classList.toggle('hidden');
    
            openIcon.classList.toggle('hidden');
            closeIcon.classList.toggle('hidden');
        });
    }
   
    // Admin dropdown
    const adminLink = document.getElementById('navbarDropdownMenuLink');
    const adminDropdown = document.getElementById('adminDropdown');
    
    adminLink.addEventListener('click', () => adminDropdown.classList.toggle('hidden'));

    // Profile dropdown
    const userMenuButton = document.querySelector('#user-menu-button');
    const userMenuDropdown = document.querySelector('[role="menu"]');

    userMenuButton.addEventListener('click', () => {
        const isExpanded = userMenuButton.getAttribute('aria-expanded') === 'true';
        userMenuButton.setAttribute('aria-expanded', !isExpanded);
        userMenuDropdown.classList.toggle('hidden');
    });

    // Close dropdowns if clicked outside
    document.addEventListener('click', (e) => {
        if (!adminLink.contains(e.target) && !adminDropdown.contains(e.target)) {
            adminDropdown.classList.add('hidden');
        }

        if (!userMenuButton.contains(e.target) && !userMenuDropdown.contains(e.target)) {
            userMenuDropdown.classList.add('hidden');
            userMenuButton.setAttribute('aria-expanded', false);
        }
    });
});