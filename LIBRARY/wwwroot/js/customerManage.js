$(function () {
    initM3();
    pageM3();
});

function initM3() {

    //用户账户管理
    $("#li3").click(function () {
        $('#m3_start').click();
        $("#m3").show();
    });
}
function pageM3() {
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
    params = { requestMethod: "requestGetCustomer" };
    $.post(url, params, function (jsonObject) {
        //alert(jsonObject);
        var json = $.parseJSON(jsonObject);
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
            alert("未查询出任何数据！！！");
        } else {
            //删除已有表格
            $("table tbody").empty();
            //设置分页链接
            $("#m3_pageCurrent").html(json.pc);
            $("#m3_totalPage").html(json.tp);
            $("#m3_totalRecord").html(json.tr);
            $("#m3_before").attr("href", "systemAjax.aspx?pc=" + (json.pc - 1));
            $("#m3_current").attr("href", "systemAjax.aspx?pc=" + json.pc);
            $("#m3_after").attr("href", "systemAjax.aspx?pc=" + (json.pc + 1));
            $("#m3_end").attr("href", "systemAjax.aspx?pc=" + json.tp);
            
            //遍历集合,显示数据          
            $.each(json.datas, function (n, value) {
                //alert(n + ' ' + value.username);
                var id = $("<td>");
                id.text((json.pc - 1) * json.ps + n + 1);
                var name = $("<td>");
                name.text(value.name);
                var customerCode = $("<td>");
                customerCode.text(value.customerCode);
                var address = $("<td>");
                address.text(value.address);
                var contacts = $("<td>");
                contacts.text(value.contacts);
                var openDate = $("<td>");
                openDate.text(value.openDate);
                var tr = $("<tr>");
                tr.append(id).append(name).append(customerCode).append(address).append(contacts).append(openDate);
                $("#m3_infoTab").append(tr);
            });
        }
    });
}
