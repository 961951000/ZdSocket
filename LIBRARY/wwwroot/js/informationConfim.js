$(function () {
    //表格导入
    $("#m4_submit").click(function () {
        $(this).attr("type", "button");
        if ($("#m4_path").val() != "") {
            $(this).attr("type", "submit");
            $(this).click();
        }
    });
    pageM3();
    initM3();
    //信息确认
    $("#m3_submit").click(function () {
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
            url = $("#m3_start").attr("href");
            params = { "id": arr.toString(), requestMethod: "requestInformationConfim" };
            if (confirm("是否继续？")) {
                $.post(url, params, function (datas) {
                    if (datas > 0) {
                        alert("操作成功!");
                        $('#m3_current').click();
                    }
                });
            }
        }
    });
    //信息确认
    $("#m3_cancel").click(function () {
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
            url = $("#m3_start").attr("href");           
            params = { "id": arr.toString(), requestMethod: "requestInformationConfimNo" };
            if (confirm("是否继续？")) {
                $.post(url, params, function (datas) {
                    if (datas > 0) {
                        alert("操作成功!");
                        $('#m3_current').click();
                    }
                });
            }
        }
    });
});

function initM3() {
    //用户信息管理
    $("#li3").click(function () {
        $('#m3_start').click();
        $("#m3").show();
    });
    //搜索
    $("#m3_submit").unbind("click").click(function () {
        $('#m3_start').click();
    });
}
function pageM3() {
    //修改
    $("#m3_modifySubmit").click(function () {
        url = $("#m3_start").attr("href");
        params = $("#m3_modify").serializeJson();
        params.requestMethod = "requestModifyContacts";
        $.post(url, params, function (datas) {
            $("#m3_modifyCancel").click();
            if (datas > 0) {
                alert("操作成功");
            }
        })
    });
    //首页           
    $('#m3_start').click(function () {
        url = $("#m3_start").attr("href");
        showDataM3(url);
        return false;
    });
    //上一页
    $('#m3_before').click(function () {
        url = $("#m3_before").attr("href");
        showDataM3(url);
        return false;
    });
    //当前页
    $("#m3_current").click(function () {
        url = $("#m3_current").attr("href");
        showDataM3(url);
        return false;
    });
    //下一页
    $('#m3_after').click(function () {
        url = $("#m3_after").attr("href");
        showDataM3(url);
        return false;
    });
    //尾页
    $('#m3_end').click(function () {
        url = $("#m3_end").attr("href");
        showDataM3(url);
        return false;
    });
}
//显示数据
function showDataM3(url) {
    params = { requestMethod: "requestGetPersonTemporary" };
    $.post(url, params, function (jsonObject) {
        //删除已有表格
        $("table tbody").empty();
        json = $.parseJSON(jsonObject);
        if (json.pc == 1) {
            $("#m3_before").hide();
        } else {
            $("#m3_before").show();
        }
        if (json.tp > json.pc) {
            $("#m3_after").show()
        } else {
            $("#m3_after").hide();
        }
        if (json.pc > json.tp) {
            $("#m3_pageCurrent").html(1);
            alert("未查询出任何数据！！！");
        } else {
            //设置分页链接
            $("#m3_pageCurrent").html(json.pc);
            $("#m3_totalPage").html(json.tp);
            $("#m3_totalRecord").html(json.tr);
            $("#m3_before").attr("href", "contactsAjax.aspx?pc=" + (json.pc - 1));
            $("#m3_current").attr("href", "contactsAjax.aspx?pc=" + json.pc);
            $("#m3_after").attr("href", "contactsAjax.aspx?pc=" + (json.pc + 1));
            $("#m3_end").attr("href", "contactsAjax.aspx?pc=" + json.tp);
            //遍历集合,显示数据          
            $.each(json.datas, function (n, value) {
                //alert(n + ' ' + value.phoneShow);
                var id = $("<td>");
                id.text((json.pc - 1) * json.ps + n + 1);
                var name = $("<td>");
                name.text(value.name);
                var clientName = $("<td>");
                clientName.text(value.clientName);
                var clientBusiness = $("<td>");
                clientBusiness.text(value.clientBusiness);
                var position = $("<td>");
                position.text(value.position); 
                var email = $("<td>");
                email.text(value.email);
                var operateTime = $("<td>");
                operateTime.text(value.operateTime);
                var select = "<td><input type='checkbox' name='id' class='selected_node' value='" + value.id + "'/>";
                var tr = $("<tr>");
                tr.append(id).append(name).append(clientName).append(clientBusiness).append(position).append(email).append(operateTime).append(select);
                $("#m3_infoTab").append(tr);
            });
        }
    });
}
