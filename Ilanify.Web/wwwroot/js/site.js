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

function onFileSelected(event) {
    var selectedFile = event.target.files[0];
    var reader = new FileReader();

    var imgtag = document.querySelector(".profile-photo-container img");
    imgtag.title = selectedFile.name;

    reader.onload = function (event) {
        imgtag.src = event.target.result;
    };

    reader.readAsDataURL(selectedFile);
}

$(document).ready(function () {
    $('#filtersForm').submit(function (e) {
        e.preventDefault();

        var formData = {
            realEstateType: parseInt($('#realEstateType').val()) || null,
            city: $('#city').val() || null,
            district: $('#district').val() || null,
            neighborhood: $('#neighborhood').val() || null,
            categoryId: parseInt($('#categoryId').val()) || null,
            minPrice: parseFloat($('#minPrice').val()) || null,
            maxPrice: parseFloat($('#maxPrice').val()) || null,
            roomCount: $('#roomCount').val() || null,
            minSquareMeters: parseFloat($('#minSquareMeters').val()) || null,
            maxSquareMeters: parseFloat($('#maxSquareMeters').val()) || null,
        };

        function createFilterTags() {
            $('.selected-filter').empty();

            $('#filtersForm label').each(function () {
                var filterName = $(this).text().trim();
                var filterId = $(this).attr('for');
                var filterValue = $('#' + filterId).val();

                if (filterValue) {
                    var filterItem = $(`<span class="badge filter-badge">${filterName}: ${filterValue}</span>`);
                    var removeButton = $(`<button type="button" class="close-filter" aria-label="Close"><i class="fa-solid fa-xmark"></i></button>`);

                    removeButton.on('click', function () {
                        $('#' + filterId).val('');
                        $(this).parent().remove();
                        $('#filtersForm').submit();
                    });

                    filterItem.append(removeButton).appendTo('.selected-filter');
                }
            });
        }


        $.ajax({
            url: '/api/RealEstateFilter',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(formData),
            success: function (data) {
                var city = $('#city').val();
                var estateType = $('#realEstateType').val();
                var infoData;
                if (city == null && estateType == null || city == "" && !estateType)
                    infoData = "Toplam";
                else if (city != null || estateType != null) {
                    infoData = estateType == 1 ? city + " Satılık": estateType == 2 ? city + " Kiralık" : city;
                }
                var count = data.length;

                $('#realEstateContainer').empty().append(
                    `<div class="card" id="infoCard">
                        <b class="infoContainer">${infoData}</b>
                        <p class="container-count">için ${count} ilan bulundu</p>
                        <hr/>
                     <div class="selected-filter"></div>
                    </div>`
                );
                createFilterTags();

                data.forEach(function (realEstate) {
                    var imageUrl = realEstate.images && realEstate.images.length > 0
                        ? `/images/real-estate-images/${realEstate.images[0].imageUrl}`
                        : '/images/placeholder-image.jpg';

                    var attributesHtml = '';
                    if (realEstate.category && realEstate.category.categoryAttributes) {
                        realEstate.category.categoryAttributes.forEach(function (attr) {
                            realEstate.attributeValues.forEach(function (attrValue) {
                                    if (attr.id === attrValue.categoryAttributeId) {
                                        if (attr.name === "Oda Sayısı")
                                            attributesHtml += "| " + attrValue.value
                                        if (attr.name === "Kat Sayısı")
                                            attributesHtml += " | " + attrValue.value + ". Kat ";
                                        if (attr.name === "Bina Yaşı")
                                            attributesHtml += " | " + (new Date().getFullYear() - attrValue.value) + " Yıllık";
                                    }
                                }
                            );

                        });
                    }

                    var realEstateHtml = `
                        <a href="/RealEstate/Details/?realEstateId=${realEstate.id}" class="text-decoration-none">
                            <div class="card mb-3">
                                <div class="listing-date position-absolute">
                                    <small class="text-muted">${new Date(realEstate.listingDate).toLocaleDateString()}</small>
                                </div>
                                <div class="row no-gutters m-1">
                                    <div class="col-md-4 mt-3 mb-3">
                                        <img src="${imageUrl}" class="card-img" style="width:100%; height:auto;">
                                    </div>
                                    <div class="col-md-8">
                                        <div class="card-body">
                                            <h5 class="estate-price">${realEstate.price.toLocaleString('tr-TR', {
                        minimumFractionDigits: 1,
                        maximumFractionDigits: 1
                    })} TL</h5>
                                            <p class="estate-title">${realEstate.title}</p>
                                            <p class="card-text">
                                                <small class="estate-property">${realEstate.type === 1 ? "Satılık" : "Kiralık"} ${realEstate.category.name} | ${realEstate.squareMeters} m² ${attributesHtml}</small>
                                            </p>
                                            <p class="card-text">
                                                <i class="fa-regular fa-location-dot"></i>
                                                <small class="text-muted location-info"> ${realEstate.location.district}, ${realEstate.location.neighborhood}</small>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>`;

                    $('#realEstateContainer').append(realEstateHtml);
                });
            },
            error: function (xhr, status, error) {
                console.log("Bir hata oluştu:", error);
            }
        });
    });
});
