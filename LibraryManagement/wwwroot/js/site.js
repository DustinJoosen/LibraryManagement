$(document).ready(function () {
    $(".book").on("click", function () {
        var guid = $(this).attr("value");
        document.location.href = "/" + guid
    })
})

function SetRole(role) {
    document.location.href = "/account/setrole?role=" + role;
}