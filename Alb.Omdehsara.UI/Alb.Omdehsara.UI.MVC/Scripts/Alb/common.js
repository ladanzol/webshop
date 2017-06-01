
$(document).ready(function () {
    tools.showHintMessage();
    $('.dropdown.keep-open').on({
        "shown.bs.dropdown": function () { this.closable = false; },
        "click": function () { this.closable = false; },
        "hide.bs.dropdown": function () { this.closable = false; return this.closable; }
    });
    $('.login-tabs').on('click', '', function () {
        $(this).closest('.dropdown').addClass('dontClose');
    });
    $('.login-dropdown').on('hide.bs.dropdown', function (e) {
        if ($(this).hasClass('dontClose')) {
            e.preventDefault();
        }
        $(this).removeClass('dontClose');
    });
    //for omdehsara    $(document).ready(function () {
        $('.divMenu>ul>li').each(function (index) {
            $(this).find('.divSubMenu').css('left', $('.divCenterFrame').offset().left - $(this).offset().left + 50);
        });
    });
    setTimeout(function () {
        $(document).ready(function () {
            console.log(parseInt($(".divCenterFrameContainer").height()) - 40);
            $(".divFooter").css("top", parseInt($(".divCenterFrameContainer").height()) - 40);
        })
    }, 1000);

    $(window).load(function () {

        var $flippers = $(".divContainer");
        if ($flippers) {
            var qtFlippers = $flippers.length;
            if (qtFlippers) {
                setInterval(function () {
                    $flippers.eq(Math.floor(Math.random() * qtFlippers)).toggleClass('hover');
                }, 8000);
            }
        }
    });

  
    $('[title]').tooltip();
   
    function submitPolling(Id) {
        alert($("input[name=options" + Id + "]:checked").val());
        $("#poling-" + Id).html("با تشکر. نظر شما ثبت گردید.");
    };
});