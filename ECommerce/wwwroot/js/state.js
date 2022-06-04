$(document).ready(function () {
    GetAllCountries();
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