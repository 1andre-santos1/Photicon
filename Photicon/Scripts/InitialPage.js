window.onload = function () {
    $(".picContainer").hover(
        function (event) {
            $(this).children(".picBanner").slideDown(1200,"swing");
        },
        function (event) {
            $(this).children(".picBanner").slideUp(1200,"swing");
        }
    );
    
};

function readfile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageShowcase').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
