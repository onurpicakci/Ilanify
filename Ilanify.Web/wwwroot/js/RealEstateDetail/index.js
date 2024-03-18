$(document).ready(function () {
    var currentImageIndex = 0;
    var images = $('.detail-thumbnail-image');
    var thumbnailsWrapper = $('.thumbnails-wrapper');

    images.on('click', function () {
        changeMainImage($(this).index());
    });

    $('.left-arrow').on('click', function () {
        navigateMainImage(-1);
    });

    $('.right-arrow').on('click', function () {
        navigateMainImage(1);
    });

    function changeMainImage(index) {
        currentImageIndex = index;
        var mainImage = $('#main-image');
        var imageUrl = images.eq(currentImageIndex).attr('data-image-url');
        mainImage.attr('src', imageUrl);
    }

    function navigateMainImage(step) {
        var newIndex = (currentImageIndex + step + images.length) % images.length;
        currentImageIndex = newIndex;
        changeMainImage(currentImageIndex);
    }

    function navigateThumbnail(step) {
        var scrollAmount = thumbnailsWrapper.scrollLeft() + step * 100; // Adjust as necessary
        thumbnailsWrapper.animate({scrollLeft: scrollAmount}, 'slow');
    }

    $('.thumbnail-nav.left-arrow').on('click', function () {
        navigateThumbnail(-1);
    });

    $('.thumbnail-nav.right-arrow').on('click', function () {
        navigateThumbnail(1);
    });
});

function showPhoneNumberAndEnableCall(element) {
    var phoneNumber = element.getAttribute('data-phone-number');
    if (phoneNumber) {
        element.innerHTML = phoneNumber;
        element.classList.add('phone-number-visible');
        element.setAttribute('href', '#');
        element.removeEventListener('click', showPhoneNumberAndEnableCall);
        element.addEventListener('click', makeCall);
    } else {
        console.error('Telefon numarası bulunamadı.');
    }
}

function makeCall(event) {
    var phoneNumber = this.getAttribute('data-phone-number');
    if (phoneNumber) {
        window.location.href = 'tel:' + phoneNumber;
    } else {
        console.error('Telefon numarası bulunamadı.');
    }
    event.preventDefault();
}




