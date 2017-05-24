$(function () {
    $("#li2").click(function () {
        showDataM2();
        $("#m2").show();
    });
    //栏目设置
    $("#m2_submit").click(function () {
        selecteds = $(".selected_node");
        arr = new Array();
        for (i = 0; i < selecteds.length; i++) {
            if (selecteds[i].checked) {
                arr.push($(selecteds[i]).val());
            }
        }
        if (arr.length == 0) {
            alert("请选中！");
        } else {
            url = "systemAjax.aspx";
            params = { "id": arr.toString(), requestMethod: "requestModifyColumn" };
            if (confirm("是否继续？")) {
                $.post(url, params, function (datas) {
                    if (datas > 0) {
                        alert("操作成功!");
                        $("#li2").click();
                    }
                });
            }
        }
    });
    //显示数据
    function showDataM2() {
        url = "systemAjax.aspx";
        params = { requestMethod: "requestGetColumn" };
        $.post(url, params, function (jsonObject) {
            //删除已有表格
            $("table tbody").empty();
            json = $.parseJSON(jsonObject);
            //遍历集合,显示数据      
            var tr = '<tr>';
            $.each(json, function (n, value) {
                //alert(n + ' ' + value.is_select);                        
                if (value.is_select) {
                    var checkbox = '<input type="checkbox" name="column" class="selected_node" checked="checked" value="' + value.id + '"/>';
                } else {
                    var checkbox = '<input type="checkbox" name="column" class="selected_node" value="' + value.id + '"/>';
                }
                var td = '<td>' + checkbox + value.name + '</td>';
                if (n % 5 === 0 && n > 0) {
                    tr += '</tr><tr>' + td;
                } else {
                    tr += td;
                }
                if (n === json.length - 1) {
                    $("#m2_infoTab").append(tr);
                }
            });
        });
    }
});