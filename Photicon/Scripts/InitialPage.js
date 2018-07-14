window.onload = function () {
    $(".picContainer").hover(
        function (event) {
            $(this).children(".picBanner").slideDown("slow");
        },
        function (event) {
            $(this).children(".picBanner").slideUp("slow");
        }
    );
    
};

function readfile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageShowcase').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
