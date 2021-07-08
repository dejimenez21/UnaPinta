function onSuccess(){

}

function onError(){

}

function always(){

}


function btnRegister_Click() {
    //alert("");
    var obj = {};

    obj.userId = 1;
    obj.conditions = [];

    obj.conditions.push({conditionId: $("#tattooQuestion").val(), months: $("#tattooCondition").val()});
    obj.conditions.push({conditionId: $("#bloodQuestion").val(), months: $("#bloodCondition").val()});
    obj.conditions.push({conditionId: $("#piercingQuestion").val(), months: $("#piercingCondition").val()});
    obj.conditions.push({conditionId: $("#transplanteQuestion").val(), months: -1});
    obj.conditions.push({conditionId: $("#drugQuestion").val(), months: $("#drugCondition").val()});
    obj.conditions.push({conditionId: $("#pregnanQuestion").val(), months: $("#pregnanCondition").val()});
    obj.conditions.push({conditionId: $("#transfusionQuestion").val(), months: $("#transfusionCondition").val()});
    obj.conditions.push({conditionId: $("#odonQuestion").val(), months: 1});
    obj.conditions.push({conditionId: $("#enfermedadesList").val(), months: -1});
    // $.ajax({
    //     type: "POST",
    //     url: 'https://localhost:44393/api/waitlist',
    //     data: JSON.stringify(obj),
    //     dataType: "json",
    //     contentType: "application/json; charset=utf-8",
    //     success: function (data) {
    //         alert("Data has been added successfully." + data);
    //     },
    //     error: function (request, error) {
    //         alert(JSON.stringify(request));
    //     }
    // });

    $.ajax({
        url: 'https://localhost:44393/api/waitlist',
        method: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        //contentType: "application/json; charset=utf-8",
      }).done(onSuccess)
        .fail(onError)
        .always(always);
}