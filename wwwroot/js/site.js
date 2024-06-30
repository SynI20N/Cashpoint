var html = $("#nominals")[0].innerHTML;
var html2 = $("#nominalsCount")[0].innerHTML;
var html3 = $("#statuses")[0].innerHTML;
var model = {
    TapeCount: 5,
    TapeNominals: [100],
    NominalsCount: [10],
    Cash: 100
};
updateTapes();
function updateTapes() {
    $("#nominals")[0].innerHTML = "";
    $("#nominalsCount")[0].innerHTML = "";
    $("#statuses")[0].innerHTML = "";
    var number = $("#Count")[0].valueAsNumber;
    for (let i = 0; i < number; i++) {
        $("#nominals")[0].innerHTML += html;
        $("#nominalsCount")[0].innerHTML += html2;
        $("#statuses")[0].innerHTML += html3;
    }
}
function updateModel() {
    model.TapeNominals = [];
    model.NominalsCount = [];
    model.TapeCount = $("#Count")[0].valueAsNumber;
    var k = 0;
    var i = 0;
    while(k < model.TapeCount) {
        if ($(".Status")[k].checked === false) {
            k++;
            continue;
        }
        model.TapeNominals[i] = $(".NominalValue")[k].valueAsNumber;
        model.NominalsCount[i] = $(".NominalCount")[k].valueAsNumber;
        k++;
        i++;
    }
    model.TapeCount = i;
    model.Cash = $("#Cash")[0].valueAsNumber;
}
function checkGive(element) {
    updateModel();
    var ajaxTime = Date.now();
    $.ajax({
        type: "POST",
        url: "/Home/IsCashGiveable/",
        data: JSON.stringify(model),
        traditional: true,
        contentType: "application/json",
        success: function (resultData) {
            var totalTime = Date.now() - ajaxTime;
            element.innerHTML = resultData + totalTime + " миллисекунд";
            $("#Tick").css({ 'color': 'green' });
        }
    });
}