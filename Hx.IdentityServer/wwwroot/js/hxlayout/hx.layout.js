$(function () {
    $('.hx-aside-menu').slimscroll({
        width:'100%',
        height: 'auto',
        position: 'right',
        railOpacity: 1,
        size: "5px",
        opacity: .4,
        color: '#d6d7e1',
        wheelStep: 5,
        touchScrollStep: 50
    });
    $("body").on('click', '[data-stopPropagation]', function (e) {
        e.stopPropagation();
    });
    $('.hx-aside .hx-aside-toggle').on('click', function (e, r) {
        var $parent = $(this).parent(".hx-aside-menu-item"),
            subMenu = $(this).next('.hx-aside-submenu');;
        if (!subMenu.hasClass('show')) {
            var showItem = $parent.siblings(".hx-aside-menu-item.show")
            //展开未展开
            subMenu.addClass('show').slideDown(200);
            $parent.addClass('show');
            showItem.removeClass('show').children('.hx-aside-submenu').removeClass('show').slideUp(200);
        } else {
            //收缩已展开
            subMenu.removeClass('show').slideUp(200);
            $parent.removeClass('show')
        }
    })

    $('.hx-brand button').on('click', function () {
        var $this = $(this),
            miniClass = 'hx-aside-mini',
            $parent = $this.parent(),
            $aside = $('.hx-aside'),
            $body = $('.hx-body');
        if ($aside.hasClass(miniClass)) {
            $aside.removeClass(miniClass);
            $parent.removeClass(miniClass);
            $body.removeClass(miniClass);
        } else {
            $parent.addClass(miniClass);
            $aside.addClass(miniClass);
            $body.addClass(miniClass);
        }
    });

    $(document).on('mouseover', '.hx-aside-mini.hx-aside', function () {
        var $aside = $('.hx-aside');
        $aside.addClass('hx-aside-mini-hover')
    });

    $(document).on('mouseleave', '.hx-aside-mini.hx-aside', function () {
        var $aside = $('.hx-aside'),
            showItem = $aside.find('.hx-aside-menu-item.show');
        showItem.removeClass('show').children('.hx-aside-submenu.show').removeClass('show')
        $aside.removeClass('hx-aside-mini-hover')
    });
});
