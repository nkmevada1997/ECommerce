$(document).ready(function () {
    var stateId = $("#getStateId").val();
    GetAllStates(stateId);
    Change()
    InputChange()
})

function GetAllStates(id) {
    $.ajax({
        type: 'Get',
        dataType: 'JSON',
        url: '/States/GetStates',
        success:
            function (response) {

                var states = "<option value=''>Select State </option>";
                for (var i = 0; i < response.data.length; i++) {
                    states += "<option value=" + response.data[i].id + ">" + response.data[i].stateName + "</option>";
                }
                $("#stateDropdown").html(states)
                $("#stateDropdown").val(id)

                if (!id) {
                    $("#cityDiv").hide();
                    $("#submitButton").prop("disabled", true)
                }

            },
        error:
            function (response) {
                console.log(response)
            }
    })
}

function Change() {
    $("#stateDropdown").change(function () {
        var selectedValue = $(this).val()
        if (!selectedValue) {
            $("#cityDiv").hide();
            $("#submitButton").prop("disabled", true)
        } else if (selectedValue) {
            $("#cityDiv").show();
        }
    })
}

function InputChange() {
    $("#citiInput").change(function () {
        alert();

        var input = $(this).val()

        if (input.length > 0) {
            $("#submitButton").prop("disabled", false)
        }
        else {
            $("#submitButton").prop("disabled", true)
        }
    })
}