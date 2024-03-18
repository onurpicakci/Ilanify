var currentImageIndex = 0;
var images = [];

document.addEventListener('DOMContentLoaded', function () {
    images = document.querySelectorAll('.detail-thumbnail-image');
    for (var i = 0; i < images.length; i++) {
        images[i].addEventListener('click', function (event) {
            var index = Array.from(event.target.parentNode.children).indexOf(event.target);
            changeMainImage(index);
        });
    }
});

function changeMainImage(index) {
    currentImageIndex = index;
    var mainImage = document.getElementById('main-image');
    var imageUrl = images[index].getAttribute('data-image-url');
    mainImage.src = imageUrl;
}

function navigateMainImage(step) {
    var newIndex = (currentImageIndex + step + images.length) % images.length;
    changeMainImage(newIndex);
}

function navigateThumbnail(step) {
    var thumbnailsWrapper = document.querySelector('.thumbnails-wrapper');
    thumbnailsWrapper.scrollLeft += step * 100;
}

document.querySelector('.left-arrow').addEventListener('click', function () {
    navigateMainImage(-1);
});
document.querySelector('.right-arrow').addEventListener('click', function () {
    navigateMainImage(1);
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




