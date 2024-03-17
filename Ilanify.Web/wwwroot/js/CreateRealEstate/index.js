$(document).ready(function () {
    function loadCities() {
        $.ajax({
            url: 'http://api.geonames.org/searchJSON?country=TR&featureClass=P&maxRows=81&username=onurpicakci',
            type: 'GET',
            success: function (data) {
                var citySelect = $('#citySelect');
                citySelect.empty();
                citySelect.append('<option value="">Şehir Seçiniz</option>');
                $.each(data.geonames, function (index, city) {
                    citySelect.append('<option value="' + city.name + '" data-code="' + city.adminCodes1.ISO3166_2 + '">' + city.name + '</option>');
                });
            }
        });
    }

    window.loadDistricts = function() {
        var selectedCityCode = $("#citySelect option:selected").attr('data-code');
        if (selectedCityCode) {
            $.ajax({
                url: `http://api.geonames.org/searchJSON?country=TR&featureClass=A&maxRows=500&adminCode1=${selectedCityCode}&username=onurpicakci`,
                type: 'GET',
                success: function (data) {
                    var districtSelect = $('#districtSelect');
                    districtSelect.empty();
                    districtSelect.append('<option value="">İlçe Seçiniz</option>');
                    $.each(data.geonames, function (index, district) {
                        districtSelect.append('<option value="' + district.name + '">' + district.name + '</option>');
                    });
                }
            });
        }
    }

    function loadNeighborhoods() {
        var selectedCity = $('#citySelect').val();
        var selectedDistrict = $('#districtSelect').val();
        if (selectedCity && selectedDistrict) {
            $.ajax({
                url: 'http://api.geonames.org/searchJSON?country=TR&featureClass=L&maxRows=500&adminCode1=' + selectedCity + '&adminCode2=' + selectedDistrict + '&username=onurpicakci',
                type: 'GET',
                success: function (data) {
                    var neighborhoodSelect = $('#neighborhoodSelect');
                    neighborhoodSelect.empty();
                    neighborhoodSelect.append('<option value="">Mahalle Seçiniz</option>');
                    $.each(data.geonames, function (index, neighborhood) {
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
                    $.each(data, function (index, attr) {
                        var attributeHtml = '<div class="form-group">';
                        attributeHtml += '<label>' + attr.name + '</label>';

                        switch(attr.dataType) {
                            case 1: // Text
                                attributeHtml += '<input type="text" name="attributeValues[' + index + '].Value" class="form-control" />';
                                break;
                            case 2: // Number
                                if(attr.name === "Oda Sayısı") {
                                    attributeHtml += '<select name="attributeValues[' + index + '].Value" class="form-control">';
                                    attributeHtml += '<option value="">Seçiniz</option>'; // Varsayılan seçenek
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
                                attributeHtml += '<input type="checkbox" name="attributeValues[' + index + '].Value" class="form-check-input" />';
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

document.addEventListener("DOMContentLoaded", function() {
    document.querySelector("form").addEventListener("submit", function() {
        document.querySelectorAll('input[type="checkbox"]').forEach(function(checkbox) {
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