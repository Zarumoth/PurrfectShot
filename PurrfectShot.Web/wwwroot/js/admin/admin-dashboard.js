function updateUserRole(selectElement) {
    const userId = selectElement.getAttribute('data-user-id');
    const roleName = selectElement.value;
    const spinner = document.getElementById(`spinner-${userId}`);

    spinner.classList.remove('d-none');
    selectElement.style.opacity = "0.5";

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    fetch('/Admin/Dashboard/AssignRole', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'RequestVerificationToken': token
        },
        body: `userId=${userId}&roleName=${roleName}`
    })
        .then(response => {
            if (!response.ok) {
                alert("Грешка при смяна на ролята.");
                location.reload();
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert("Възникна системна грешка.");
        })
        .finally(() => {
            spinner.classList.add('d-none');
            selectElement.style.opacity = "1";
        });
}