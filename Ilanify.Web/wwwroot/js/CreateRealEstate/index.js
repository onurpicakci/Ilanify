$(document).ready(function () {
    function loadCities() {
        $.ajax({
            url: 'https://turkiyeapi.dev/api/v1/provinces',
            type: 'GET',
            success: function (response) {
                var citySelect = $('#citySelect');
                citySelect.empty();
                citySelect.append('<option value="">Şehir Seçiniz</option>');
                $.each(response.data, function (index, city) {
                    citySelect.append('<option value="' + city.name + '">' + city.name + '</option>');
                });
            }
        });
    }

    window.loadDistricts = function () {
        var cityName = $("#citySelect").val();
        if (cityName) {
            $.ajax({
                url: `https://turkiyeapi.dev/api/v1/provinces?name=${cityName}`,
                type: 'GET',
                success: function (response) {
                    var districtSelect = $('#districtSelect');
                    districtSelect.empty();
                    districtSelect.append('<option value="">İlçe Seçiniz</option>');
                    $.each(response.data[0].districts, function (index, district) {
                        districtSelect.append('<option value="' + district.name + '" data-id="'+ district.id +'">' + district.name + '</option>');
                    });
                }
            });
        }
    }


    function loadNeighborhoods() {
        var districtId = $('#districtSelect option:selected').data('id');
        if (districtId) {
            $.ajax({
                url: `https://turkiyeapi.dev/api/v1/districts/${districtId}`,
                type: 'GET',
                success: function (response) {
                    var neighborhoodSelect = $('#neighborhoodSelect');
                    neighborhoodSelect.empty();
                    neighborhoodSelect.append('<option value="">Mahalle Seçiniz</option>');
                    $.each(response.data.neighborhoods, function (index, neighborhood) {
                        neighborhoodSelect.append('<option value="' + neighborhood.name + '">' + neighborhood.name + '</option>');
                    });
                }
            });
        }
    }
    function loadCategoryAttributes() {
        var categoryId = $('#categorySelect').val();
        if (categoryId) {
            $.ajax({
                url: '/GetCategoryAttributes/' + categoryId,
                type: 'GET',
                success: function (data) {
                    var attributesContainer = $('#categoryAttributes');
                    attributesContainer.empty();
                    $.each(data.$values, function (index, attr) {
                        var attributeHtml = '<div class="form-group">';
                        attributeHtml += '<label>' + attr.name + '</label>';
                        switch (attr.dataType) {
                            case 1: // Text
                                attributeHtml += '<input type="text" name="attributeValues[' + index + '].Value" class="form-control" />';
                                break;
                            case 2: // Number
                                if (attr.name === "Oda Sayısı") {
                                    attributeHtml += '<select name="attributeValues[' + index + '].Value" class="form-control">';
                                    attributeHtml += '<option value="">Seçiniz</option>';
                                    attributeHtml += '<option value="1+0">1+0</option>';
                                    attributeHtml += '<option value="1+1">1+1</option>';
                                    attributeHtml += '<option value="2+1">2+1</option>';
                                    attributeHtml += '<option value="3+1">3+1</option>';
                                    attributeHtml += '<option value="4+1">4+1</option>';
                                    attributeHtml += '<option value="5+1">5+1</option>';
                                    attributeHtml += '<option value="6+1">6+1</option>';
                                    attributeHtml += '<option value="7+1">7+1</option>';
                                    attributeHtml += '</select>';
                                } else {
                                    attributeHtml += '<input type="number" name="attributeValues[' + index + '].Value" class="form-control" />';
                                }
                                break;
                            case 3: // Boolean
                                attributeHtml += '<div class="btn-group btn-group-toggle" data-toggle="buttons">';
                                attributeHtml += '<label class="btn btn-danger">';
                                attributeHtml += '<input type="radio" name="attributeValues[' + index + '].Value" value="Evet"> Evet';
                                attributeHtml += '</label>';
                                attributeHtml += '<label class="btn btn-dark">';
                                attributeHtml += '<input type="radio" name="attributeValues[' + index + '].Value" value="Hayır"> Hayır';
                                attributeHtml += '</label>';
                                attributeHtml += '</div>';
                                break;
                            default:
                                attributeHtml += '<input type="text" name="attributeValues[' + index + '].Value" class="form-control" />';
                        }

                        attributeHtml += '<input type="hidden" name="attributeValues[' + index + '].AttributeId" value="' + attr.id + '" />';
                        attributeHtml += '<input type="hidden" name="attributeValues[' + index + '].CategoryAttributeId" value="' + attr.id + '" />';
                        attributeHtml += '</div>';
                        attributesContainer.append(attributeHtml);
                    });
                },
                error: function (xhr, status, error) {
                    console.error('Error loading category attributes:', error);
                }
            });
        } else {
            $('#categoryAttributes').empty();
        }
    }

    $('#citySelect').change(function () {
        loadDistricts();
    });

    $('#districtSelect').change(function () {
        loadNeighborhoods();
    });

    $('#categorySelect').change(function () {
        loadCategoryAttributes();
    });

    loadCities();
});

document.addEventListener("DOMContentLoaded", function () {
    document.querySelector("form").addEventListener("submit", function () {
        document.querySelectorAll('input[type="checkbox"]').forEach(function (checkbox) {
            if (checkbox.checked) {
                checkbox.value = "true";
            } else {
                var input = document.createElement("input");
                input.type = "hidden";
                input.name = checkbox.name;
                input.value = "false";
                checkbox.form.appendChild(input);
            }
        });
    });
});