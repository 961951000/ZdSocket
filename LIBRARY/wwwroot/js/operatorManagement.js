$(function () {
    initM3();
    pageM3();
    addOperator();
});

function initM3() {   
    //用户账户管理
    $("#li3").click(function () {
        $('#m3_start').click();
        $("#m3").show();
    });
    $("#m3_cancelModify").click(function () {
        $("#m3_err").html("");
    });
    //修改
    $("#m3_submitModify").click(function () {
        url = $("#m3_start").attr("href");
        params = $("#m3_modifyForm").serializeJson();
        params.requestMethod = "requestIfAdminName";
        $.post(url, params, function (datas) {
            if (datas > 0) {
                $("#m3_err").html("用户名已存在");
            } else if (datas == 0) {
                params.requestMethod = "requestModifyAdmin";
                $.post(url, params, function (datas) {
                    if (datas > 0) {
                        $("#li3").click();
                        $("#m3_cancelModify").click();
                        alert("操作成功");
                    }
                });
            }
        });
    });
}
function pageM3() {
    //首页           
    $('#m3_start').click(function () {
        url = $("#m3_start").attr("href");
        showDataM3(url);
        return false;
    });
    $('#m3_start').click();
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
    params = { requestMethod: "requestGetAdminAll" };
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
                var username = $("<td>");
                username.text(value.username);
                var role = $("<td>");
                role.text(value.role);
                if (value.role == "系统管理员") {
                    var modify_select = "<td>修改</td>";
                    var reset_select = "<td>密码重置</td>";
                } else {
                    var modify_select = "<td><input type='hidden' id='hidden_id' value='" + value.id + "'/><input type='hidden' id='hidden_name' value='" + value.username + "'/><a class='modifyBtn' href='#'>修改</a></td>";
                    var reset_select = "<td><input type='hidden' value='" + value.id + "'/><a class='resetBtn' href='#'>密码重置</a></td>";
                }
                var tr = $("<tr>");
                tr.append(id).append(username).append(role).append(modify_select).append(reset_select);
                $("#m3_infoTab").append(tr);
                //修改
                $(".modifyBtn").unbind("click").click(function () {
                    $("#m3_id").val($(this).parent().children("#hidden_id").val());
                    $("#m3_name").attr("placeholder", $(this).parent().children("#hidden_name").val());
                    $("#m3_name").val("");
                    $("#m3_modifyBtn").click();
                });

                //密码重置
                $(".resetBtn").unbind("click").click(function () {
                    if (confirm("是否继续")) {
                        url = $("#m3_start").attr("href");
                        params = { id: $(this).parent().children("input").val() }
                        params.requestMethod = "requestResetPassword";
                        $.post(url, params, function (datas) {
                            $("#m3_current").click();
                            if (datas > 0) {
                                alert("操作成功");
                            }
                        });
                    }
                });
            });
        }
    });
}
function addOperator() {
    $("#m3_username").blur(function () {
        username = $("#m3_username").val().trim();
        if (username == "") {
            $("#m3_nameErr").html("用户名不能为空");
        } else {
            url = $("#m3_start").attr("href");
            params = { username: username };
            params.requestMethod = "requestIfAdminName";
            $.post(url, params, function (datas) {
                if (datas > 0) {
                    $("#m3_nameErr").html("用户名已存在");
                } else if (datas == 0) {
                    $("#m3_nameErr").html("");
                }
            });
        }
    });
    $("#m3_submitAdd").click(function () {
        $("#m3_username").blur();
        if ($("#m3_nameErr").text() == "") {
            url = $("#m3_start").attr("href");
            params = { username: $("#m3_username").val().trim(), requestMethod: "requestAddOperator" };
            $.post(url, params, function (datas) {
                if (datas > 0) {
                    $("#m3_cancelAdd").click();
                    $("#m3_username").val("");
                    alert("操作成功");
                    $("#m3_current").click();
                } else if (datas == 0) {
                }
            });
        }
    });
}