// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//hack fix
/*document.write('<style type="text/css">body{display:none}</style>');
jQuery(function ($) {
    $('body').css('display', 'block');
});*/

//smooth refresh
/*$(function () {
    $("body").css("display", "none");
    $("body").fadeIn(100);
});

//persist body scroll location
$(function () {
    let body = document.querySelector("body");
    let top = sessionStorage.getItem("body-scroll");
    if (top !== null) {
        sidebar.scrollTop = parseInt(top, 10);
    }
    window.addEventListener("beforeunload", () => {
        $("body").fadeOut(100);
        sessionStorage.setItem("body-scroll", body.scrollTop);
    });
});*/

const months = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];

function getReadableDate() {
    const date = new Date();
    const day = date.getDate();
    const month = months[date.getMonth()];
    const year = date.getFullYear();
    return month + " " + day + ordinal(day) + ", " + year;
}

function ordinal(date) {
    if (date > 20 || date < 10) {
        switch (date % 10) {
            case 1:
                return "st";
            case 2:
                return "nd";
            case 3:
                return "rd";
        }
    }
    return "th";
}

function getGreeting() {
    const date = new Date();
    if (date.getHours() < 12) {
        return "Good morning!";
    } else if (date.getHours() < 18) {
        return "Good afternoon!";
    } else {
        return "Good evening!";
    }
}