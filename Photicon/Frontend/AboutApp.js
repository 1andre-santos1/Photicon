$(function () {
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
    for (var i = 0; i < users.length; i++) {

        var userDiv = document.createElement('div');
        $(userDiv).addClass('userContainerGrid col-lg-3 col-md-3 col-sm-3 col-xs-12');

        var picContainer = document.createElement('div');
        $(picContainer).addClass('picContainer');
        $(userDiv).append(picContainer);

        var picBanner = document.createElement('div');
        $(picBanner).addClass('picBanner');
        $(picContainer).append(picBanner);

        var loginTextLabel = document.createElement('span');
        $(loginTextLabel).addClass('LoginTextLabel');
        $(loginTextLabel).text("Login: ");
        $(picBanner).append(loginTextLabel);

        var loginTextData = document.createElement('span');
        $(loginTextData).addClass('LoginTextData');
        $(loginTextData).text(users[i].UserName);
        $(picBanner).append(loginTextData);

        var break1 = document.createElement('br');
        $(picBanner).append(break1);

        var passTextLabel = document.createElement('span');
        $(passTextLabel).addClass('PasswordTextLabel');
        $(passTextLabel).text("Password: ");
        $(picBanner).append(passTextLabel);

        var passTextData = document.createElement('span');
        $(passTextData).addClass('PasswordTextData');

        var password = "";
        switch (users[i].UserName) {
            case "Barbara":
                password = "Ba1234!";
                break;
            case "william":
                password = "#1William1#";
                break;
            case "yasmin":
                password = "Yas1234!";
                break;
            case "Rui":
                password = "Rui123!";
                break;
            case "Xavi":
                password = "!1Xavi1!";
                break;
            case "Cris":
                password = "Cris!1234";
                break;
            case "louise":
                password = "Louise1!";
                break;
            case "pete1000":
                password = "#Peter1000#";
                break;
            case "Elsa":
                password = "Elsinha#1";
                break;
            case "Sam":
                password = "Samuel9#";
                break;
        }

        $(passTextData).text(password);
        $(picBanner).append(passTextData);

        var break2 = document.createElement('br');
        $(picBanner).append(break2);
        var break3 = document.createElement('br');
        $(picBanner).append(break3);

        var img = document.createElement('img');
        $(img).addClass('picture');
        $(img).attr('src', users[i].ProfilePhoto);
        $(picContainer).append(img);

        interf.usersContainer.append(userDiv);
    }
}
