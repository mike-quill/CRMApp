// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//persist expanded sidebar dropdowns while changing pages
$(function () {
    $(".sidebar-submenu").each(function () {
        if (sessionStorage.getItem(this.id) == "active") {
            $(this).parent().addClass("active");
            $(this).show();
        }
    });

    //persist sidebar scroll location
    let sidebar = document.querySelector(".sidebar-content");
    let top = sessionStorage.getItem("sidebar-scroll");
    if (top !== null) {
        sidebar.scrollTop = parseInt(top, 10);
    }

    window.addEventListener("beforeunload", () => {
        sessionStorage.setItem("sidebar-scroll", sidebar.scrollTop);
    });
});

$(function () {
    //sidebar dropdown functionality
    $(".sidebar-dropdown > a").click(function () {
        //$(".sidebar-submenu").slideUp(200, function () { sessionStorage.removeItem(this.id, "active") });
        if ($(this).parent().hasClass("active"))
        {
            $(".sidebar-submenu").slideUp(200, function () { sessionStorage.removeItem(this.id, "active") });
            $(".sidebar-dropdown").removeClass("active");
            $(this).parent().removeClass("active");
        }
        else
        {
            //$(".sidebar-dropdown").removeClass("active");
            $(this).next(".sidebar-submenu").slideDown(200, function () { sessionStorage.setItem(this.id, "active") });
            $(this).parent().addClass("active");
        }
    });

    // Toggle sidebar using the same button
    $("#show-sidebar").click(function () {
        $(".page-wrapper").toggleClass("toggled");

        // Remove the hidden class every time, should only be there initially
        $(".page-wrapper").removeClass("hidden-md");
    });
});


