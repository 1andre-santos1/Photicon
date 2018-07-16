$(function () {
    debugger;
    adqInterf();
    usersStage();
});
var interf = {};
function adqInterf() {
    interf.usersContainer = document.querySelector('#usersContainer');
}
function usersStage() {
    return getUsers()
        .then(function (users) {
            mostraUsers(users);
        })
        .catch(function (erro) {
            console.error(erro);
        });
}
function mostraUsers(users) {
    debugger;
    for (var i = 0; i < users.length; i++) {
        var header = document.createElement('label');
        header.textContent = users[i].UserName;
        interf.usersContainer.append(header);
    }
}
