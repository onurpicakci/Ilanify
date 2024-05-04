$(document).ready(function () {
    var currentImageIndex = 0;
    var images = $('.detail-thumbnail-image');
    var thumbnailsWrapper = $('.thumbnails-wrapper');

    images.on('click', function () {
        changeMainImage($(this).index());
    });

    $('.image-nav.left-arrow').on('click', function () {
        navigateMainImage(-1);
    });

    $('.image-nav.right-arrow').on('click', function () {
        navigateMainImage(1);
    });

    $('.thumbnail-nav.left-arrow').on('click', function () {
        navigateThumbnail(-1);
    });

    $('.thumbnail-nav.right-arrow').on('click', function () {
        navigateThumbnail(1);
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
        var scrollAmount = thumbnailsWrapper.scrollLeft() + step * 100; 
        thumbnailsWrapper.animate({scrollLeft: scrollAmount}, 'slow');
    }
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

$(document).ready(function () {
    var mapElement = $('#map');
    var city = mapElement.data('city');
    var district = mapElement.data('district');
    var neighborhood = mapElement.data('neighborhood');

    var query = `${neighborhood}, ${district}, ${city}`;

    $.get(`https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(query)}`, function (data) {
        if (data.length > 0) {
            var lat = parseFloat(data[0].lat);
            var lon = parseFloat(data[0].lon);

            var map = L.map('map').setView([lat, lon], 13);

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors'
            }).addTo(map);

            L.marker([lat, lon]).addTo(map)
                .bindPopup(query)
                .openPopup();
        } else {
            console.error('Konum bulunamadı.');
        }
    });
});


