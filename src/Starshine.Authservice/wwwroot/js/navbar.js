$(function () {
    $('.navbar-toggler').click(function () {
        $('.hx-aside').toggleClass('hx-nav-collapse');
    })
    var hxsidebar = document.querySelector('.hx-aside'),
        activeEl = hxsidebar.querySelector('[data-active@(string.IsNullOrEmpty(ViewBag.Active)?"":"="+ ViewBag.Active)]');
    if (activeEl) {
        if (activeEl.classList) {
            activeEl.classList.add('active');
        } else {
            var className = (element.className || '').trim();
            element.className = "".concat(className, " ").concat('active');
        }
    }
})