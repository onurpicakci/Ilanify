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
