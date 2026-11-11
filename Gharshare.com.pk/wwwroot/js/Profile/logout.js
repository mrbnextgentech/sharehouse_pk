     const logoutButton = document.getElementById('logout');
    logoutButton.addEventListener('click', function(event) {
        event.preventDefault();
    document.getElementById('logoutForm').submit();
        });