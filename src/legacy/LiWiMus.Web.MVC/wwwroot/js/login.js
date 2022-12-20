successResetPassword = function (xhr) {
    alert(xhr); // добавить модальное окно, например, тут https://getbootstrap.com/docs/5.1/components/alerts/
};

failureResetPassword = function (xhr) {
    alert(`Status code ${xhr.status}, message ${xhr.responseText}`)
}