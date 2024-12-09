$(document).ready(function () {
    if (window.location.pathname === "/Privacy") {
        $("#privacy-icon").html('<use xlink:href="#privacy-filled"/>');
    } else {
        $("#privacy-icon").html('<use xlink:href="#privacy-empty"/>');
    }

    if (window.location.pathname === "/" || window.location.pathname === "/Index") {
        $("#home-icon").html('<use xlink:href="#home-filled"/>');
    } else {
        $("#home-icon").html('<use xlink:href="#home-empty"/>');
    }
});