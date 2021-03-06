﻿function getUsers() {
    var url = "https://localhost:44330/Api/About/";

    return fetch(url, { headers: { Accept: 'application/json' } })
        .then(function (response) {
            if (response.status === 200) {
                return response.json();
            } else {
                return Promise.reject(new Error("Users Aquiring Error"));
            }
        });
}
function getUser(id) {
    var url = `https://localhost:44330/Api/About/${id}`;

    return fetch(url, { headers: { Accept: 'application/json' } })
        .then(function (response) {
            if (response.status === 200) {
                return response.json();
            } else {
                return Promise.reject(new Error("User Aquiring Error"));
            }
        });
}