
    $(document).ready(function() {
        $(".header-menu .nice-select .list").on("click", function (e) {
            e.stopPropagation();
        })
            $(".header-menu_mobile .nice-select .list").on("click", function(e) {
        e.stopPropagation();
            })

    $(window).on('resize', function() {
                var win = $(this);
                if (win.width() > 767) {
        $('.ltn__header-options').removeClass("ltn__header-fixed")
                    $('body').removeClass("scrollHide")
    $('.search-btn').removeClass('sreachPosition')
                }
            });
    $(document).on("click", function(e) {
                var win = $(this);
    // console.log(!$(e.target).is($(".header-menu_mobile .nice-select")) || !$(e.target).is($(".header-menu_mobile .nice-select .current")));
    if (!$(e.target).is($(".header-menu_mobile .nice-select")) || !$(e.target).is($(".header-menu_mobile .nice-select .current"))) {
        setTimeout(() => {
            if (!$(".header-menu_mobile .nice-select").hasClass("open")) {
                $(".header-menu_mobile .nice-select .current .fas").removeClass("fa-xmark");
                $(".header-menu_mobile .nice-select .current .fas").addClass("fa-search");
                $('.ltn__header-options').removeClass("ltn__header-fixed");
                $('body').removeClass("scrollHide")
                $('.search-btn').removeClass('sreachPosition')


            } else {
                $(".header-menu_mobile .nice-select .current .fas").removeClass("fa-search");
                $(".header-menu_mobile .nice-select .current .fas").addClass("fa-xmark");
                if (win.width() <= 767) {
                    $('.ltn__header-options').addClass("ltn__header-fixed")
                    $('body').addClass("scrollHide")
                    $('.search-btn').addClass('sreachPosition')
                } else {
                    $('.ltn__header-options').removeClass("ltn__header-fixed");
                    $('body').removeClass("scrollHide")
                    $('.search-btn').removeClass('sreachPosition')
                }
            }
        }, 10);
                }
            })
        })

    //   Login script strat
    function openLoginNav() {
        document.getElementById("logNav").style.height = "100%";
        }

    function closeNav() {
        document.getElementById("logNav").style.height = "0%";
        }
