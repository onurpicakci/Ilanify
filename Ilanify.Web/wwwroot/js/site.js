let lastScrollTop = 0;

window.addEventListener("scroll", function () {
    var currentScroll = window.pageYOffset || document.documentElement.scrollTop;
    if (currentScroll > lastScrollTop) {
        document.querySelector('.navbar').style.top = '-80px';
    } else {
        document.querySelector('.navbar').style.top = '0';
    }
    lastScrollTop = currentScroll <= 0 ? 0 : currentScroll;
}, false);
