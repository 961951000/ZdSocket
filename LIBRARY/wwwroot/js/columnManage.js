$(function () {   
    if ($("#role").val() == "系统管理员") {
        $(".admin").css("display", "");
    }
    $("#li1").click(function () {
        showDataM1();
        $("#m1").show();
    });
});
//显示数据
function showDataM1() {
    url = "systemAjax.aspx";
    params = { requestMethod: "requestGetColumnIsSelect" };
    $.post(url, params, function (jsonObject) {
        //删除已有表格
        $("table tbody").empty();
        json = $.parseJSON(jsonObject);
        //遍历集合,显示数据      
        var tr = '<tr>';
        $.each(json, function (n, value) {
            //alert(n + ' ' + value.phoneShow);
            var td = '<td>' + value.name + '</td>';
            if (n % 5 === 0 && n > 0) {
                tr += '</tr><tr>' + td;
            } else {
                tr += td;
            }
            if (n === json.length - 1) {
                $("#m1_infoTab").append(tr);
            }
        });
    });
}