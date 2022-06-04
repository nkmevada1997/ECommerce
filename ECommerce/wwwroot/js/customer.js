$(document).ready(function () {

    $("#stateDropdownDiv").hide();
    $("#cityDropdownDiv").hide();
    GetAllCountries();
    $("#countriesDropdown").change(function () {
        var selectedValue = $(this).val();
        GetStatesByCountry(selectedValue)
    })

    $("#stateDropdown").change(function () {
        var selectedValue = $(this).val();
        GetCitiesByState(selectedValue)
    })
})

function GetAllCountries() {
    $.ajax({
        type: 'GET',
        dataType: 'JSON',
        url: '/Countries/GetCountries',
        success:
            function (response) {
                var countryId = $("#getCountryId").val();
                var countires = "<option value =''> Select Country </option>";
                for (var i = 0; i < response.data.length; i++) {
                    countires += "<option value=" + response.data[i].id + ">" + response.data[i].countryName + "</option>";
                }
                $("#countriesDropdown").html(countires)
                $("#countriesDropdown").val(countryId);

                if (countryId) {
                    GetStatesByCountry(countryId)
                }
            },
        error:
            function (response) {
                console.log(response)
            }
    })
}

function GetStatesByCountry(id) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/States/GetStateByCountry',
        data: { countryId: id },
        success:
            function (response) {

                if (id || response.data.length > 0) {
                    $("#stateDropdownDiv").show();
                }
                var stateId = $("#getStateId").val();
                var states = "<option value=''>Select State </option>";
                for (var i = 0; i < response.data.length; i++) {
                    states += "<option value=" + response.data[i].id + ">" + response.data[i].stateName + "</option>";
                }
                $("#stateDropdown").html(states)
                $("#stateDropdown").val(stateId)

                if (stateId) {
                    GetCitiesByState(stateId)
                }
            },
        error:
            function (response) {
                console.log(response)
            }
    })
}

function GetCitiesByState(id) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Cities/GetCityByState',
        data: { stateId: id },
        success:
            function (response) {

                if (id || response.data.length > 0) {
                    $("#cityDropdownDiv").show();
                }
                var cityId = $("#getCityId").val();
                var cities = "<option value=''>Select City</option>";
                for (var i = 0; i < response.data.length; i++) {
                    cities += "<option value=" + response.data[i].id + ">" + response.data[i].cityName + "</option>";
                }
                $("#cityDropdown").html(cities)
                $("#cityDropdown").val(cityId)
            },
        error:
            function (response) {
                console.log(response)
            }
    })
}