$(function () {
    adqInterf();
    usersStage();
});
var interf = {};
function adqInterf() {
    interf.usersContainer = document.querySelector('#usersContainer');
    interf.userContainer = document.querySelector('#userContainer');
    interf.mainButton = document.querySelector('#mainButton');
    interf.mainButton.addEventListener('click', function () {
        $(interf.usersContainer).hide();
        $(interf.usersContainer).empty();
        $(interf.userContainer).hide();
        $(interf.userContainer).empty();
        setTimeout(function (e) {
            usersStage();
        }, 500);
    });
}
function usersStage() {
    return getUsers()
        .then(function (users) {
            showUsers(users);
        })
        .catch(function (erro) {
            console.error(erro);
        });
}
function userStage(id) {
    debugger;
    return getUser(id)
        .then(function (user) {
            showUser(user);
        })
        .catch(function (erro) {
            console.error(erro);
        });
}
function showUsers(users) {
    $('#mainTitle').text("Logins");
    $('#subTitle').text("Login Information");
    showLoader();
    for (var i = 0; i < users.length; i++) {

        var userDiv = document.createElement('div');
        $(userDiv).addClass('userContainerGrid col-lg-3 col-md-3 col-sm-3 col-xs-12');

        var picContainer = document.createElement('div');
        $(picContainer).addClass('picContainer');
        $(userDiv).append(picContainer);
        let id = users[i].Id;
        picContainer.addEventListener('click', function () {
            $(interf.usersContainer).hide();
            $(interf.usersContainer).empty();
            setTimeout(function (e) {
                userStage(id);
            }, 500);
        });

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
    $('#usersContainer').fadeIn("slow");
    setTimeout(hideLoader, 1000);
}
function showUser(user) {
    $('#mainTitle').text("Logins");
    $('#subTitle').text("User Information");
    showLoader();

    var userDiv = document.createElement('div');
    $(userDiv).addClass('containerLeft');

    var picContainer = document.createElement('div');
    $(picContainer).addClass('picContainerUser');
    $(userDiv).append(picContainer);

    var picBanner = document.createElement('div');
    $(picBanner).addClass('picBannerInfo');
    $(picContainer).append(picBanner);

    var userNameText = document.createElement('span');
    $(userNameText).addClass('userNameText');
    userNameText.textContent = user.UserName;
    $(picBanner).append(userNameText);

    var pleft = document.createElement('p');
    pleft.align = "left";
    $(pleft).addClass('profilePictureContainer');
    $(picContainer).append(pleft);

    var img = document.createElement('img');
    img.src = user.ProfilePhoto;
    $(img).addClass('profilePictureInfo');
    $(img).attr('id',"imageShowcase");
    $(pleft).append(img);

    interf.userContainer.append(userDiv);


    var infoLabel = document.createElement('p');
    $(infoLabel).addClass('infoLabel');
    $(infoLabel).text("Information");
    interf.userContainer.append(infoLabel);

    var infoContainer = document.createElement('p');
    $(infoContainer).addClass('infoContainer');
    interf.userContainer.append(infoContainer);

    var picsCommunityLabel = document.createElement('span');
    $(picsCommunityLabel).addClass('picsCommunityLabel');
    $(picsCommunityLabel).text("Pictures Shared With The Community");
    $(infoContainer).append(picsCommunityLabel);

    var break2 = document.createElement('br');
    $(infoContainer).append(break2);
    var break3 = document.createElement('br');
    $(infoContainer).append(break3);
    var publicPicsLabel = document.createElement('span');
    $(publicPicsLabel).addClass('picsCommunityLabel');
    publicPicsLabel.textContent = ""+user.Pictures;
    $(infoContainer).append(publicPicsLabel);

    var break4 = document.createElement('br');
    $(infoContainer).append(break4);
    var break5 = document.createElement('br');
    $(infoContainer).append(break5);

    var picsCommunityLabel = document.createElement('span');
    $(picsCommunityLabel).addClass('picsCommunityLabel');
    $(picsCommunityLabel).text("Likes Given");
    $(infoContainer).append(picsCommunityLabel);

    var break7 = document.createElement('br');
    $(infoContainer).append(break7);
    var break8 = document.createElement('br');
    $(infoContainer).append(break8);

    var likesLabel = document.createElement('span');
    likesLabel.textContent = "" + user.Likes;
    $(likesLabel).addClass('picsCommunityLabel');
    $(infoContainer).append(likesLabel);

    var break6 = document.createElement('br');
    $(infoContainer).append(break6);

    $('#userContainer').fadeIn("slow");
    setTimeout(hideLoader, 500);
}
function showLoader(){
    $('#loader').show();
}
function hideLoader(){
    $('#loader').hide();
}

