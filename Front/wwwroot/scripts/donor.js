function onSuccess() {

}

function onError(){

}

function always(){

}


function btnRegister_Click() {
    var obj = {};

    obj.userId = 18;
    obj.conditions = [];
    if ($('#tattooQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#tattooQuestionYes").val(), months: $("#tattooCondition").val() });
    }
    if ($('#tattooQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#tattooQuestionNo").val(), months: $("#tattooCondition").val() });
    }
    if ($('#bloodQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#bloodQuestionYes").val(), months: $("#bloodCondition").val() });
    }
    if ($('#bloodQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#bloodQuestionNo").val(), months: $("#bloodCondition").val() });
    }
    if ($('#piercingQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#piercingQuestionYes").val(), months: $("#piercingCondition").val() });
    }
    if ($('#piercingQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#piercingQuestionNo").val(), months: $("#piercingCondition").val() });
    }
    if ($('#transplanteQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#transplanteQuestionYes").val(), months: -1 });
    }
    if ($('#transplanteQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#transplanteQuestionNo").val(), months: -1 });
    }
    if ($('#drugQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#drugQuestionYes").val(), months: -1 });
    }
    if ($('#drugQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#drugQuestionNo").val(), months: -1 });
    }
    if ($('#pregnanQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#pregnanQuestionYes").val(), months: -1 });
    }
    if ($('#pregnanQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#pregnanQuestionNo").val(), months: -1 });
    }
    if ($('#transfusionQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#transfusionQuestionYes").val(), months: $("#transfusionCondition").val() });
    }
    if ($('#transfusionQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#transfusionQuestionNo").val(), months: $("#transfusionCondition").val() });
    }
    if ($('#odonQuestionYes').is(':checked')) {
        obj.conditions.push({ conditionId: $("#odonQuestionYes").val(), months: 1 });
    }
    if ($('#odonQuestionNo').is(':checked')) {
        obj.conditions.push({ conditionId: $("#odonQuestionNo").val(), months: 1 });
    }
    obj.conditions.push({ conditionId: $("#enfermedadesList").val(), months: -1 });

    $.ajax({
        url: 'https://localhost:44393/api/waitlist',
        method: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).done(onSuccess)
        .fail(onError)
        .always(always);
}