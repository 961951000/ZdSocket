$(function () {
    pageM1();
    initM1();
});

function initM1() {

    $("#li3").click(function () {
        $("#m3").show();
    });
    //用户信息管理
    $("#li1").click(function () {
        $('#m1_start').click();
        $("#m1").show();
    });
    //搜索
    $("#m1_submit").unbind("click").click(function () {
        $('#m1_start').click();
    });
}
function pageM1() {
    //修改
    $("#m1_modifySubmit").click(function () {
        url = $("#m1_start").attr("href");
        params = $("#m1_modify").serializeJson();
        params.requestMethod = "requestModifyContacts";
        $.post(url, params, function (datas) {
            $("#m1_modifyCancel").click();
            if (datas > 0) {
                alert("操作成功");
               $("#m1_current").click();
            }
        })
    });

    //删除方法
    $("#delSure").click(function () {
        url = $("#m1_start").attr("href");
        params = $("#m1_delSure").serializeJson();
        params.requestMethod = "requestDelSureContacts";
        $.post(url, params, function (datas) {
            if (datas > 0) {
                alert("操作成功");
                $("#m1_current").click();
            }
        })
    });

    //首页           
    $('#m1_start').click(function () {
        url = $("#m1_start").attr("href");
        showDataM1(url);
        return false;
    });
    //上一页
    $('#m1_before').click(function () {
        url = $("#m1_before").attr("href");
        showDataM1(url);
        return false;
    });
    //当前页
    $("#m1_current").click(function () {
        url = $("#m1_current").attr("href");
        showDataM1(url);
        return false;
    });
    //下一页
    $('#m1_after').click(function () {
        url = $("#m1_after").attr("href");
        showDataM1(url);
        return false;
    });
    //尾页
    $('#m1_end').click(function () {
        url = $("#m1_end").attr("href");
        showDataM1(url);
        return false;
    });
}
//显示数据
function showDataM1(url) {
    params = $("#m1_form").serializeJson();
    params.requestMethod = "requestGetContacts";
    $.post(url, params, function (jsonObject) {
        //删除已有表格
        $("table tbody").empty();
        json = $.parseJSON(jsonObject);
        if (json.pc == 1) {
            $("#m1_before").hide();
        } else {
            $("#m1_before").show();
        }
        if (json.tp > json.pc) {
            $("#m1_after").show()
        } else {
            $("#m1_after").hide();
        }
        if (json.pc > json.tp) {
            $("#m1_pageCurrent").html(1);
            alert("未查询出任何数据！！！");
        } else {
            //设置分页链接
            $("#m1_pageCurrent").html(json.pc);
            $("#m1_totalPage").html(json.tp);
            $("#m1_totalRecord").html(json.tr);
            $("#m1_before").attr("href", "contactsAjax.aspx?pc=" + (json.pc - 1));
            $("#m1_current").attr("href", "contactsAjax.aspx?pc=" + json.pc);
            $("#m1_after").attr("href", "contactsAjax.aspx?pc=" + (json.pc + 1));
            $("#m1_end").attr("href", "contactsAjax.aspx?pc=" + json.tp);
            //遍历集合,显示数据          
            $.each(json.datas, function (n, value) {
                //alert(n + ' ' + value.phoneShow);
                var id = $("<td>");
                id.text((json.pc - 1) * json.ps + n + 1);
                var name = $("<td>");
                name.text(value.name);
                var position = $("<td>");
                position.text(value.position);
                var phone = $("<td>");
                phone.text(value.phone);
                var clientName = $("<td>");
                clientName.text(value.clientName);
                var qq = $("<td>");
                qq.text(value.qq);
                var fax = $("<td>");
                fax.text(value.fax);
                var email = $("<td>");
                email.text(value.email);
                var modify = "<td><input type='hidden' id='hidden_id' value='" + value.id + "'/><input type='hidden' id='hidden_name' value='" + value.name + "'/><input type='hidden' id='hidden_birthday' value='" + value.birthday + "'/><input type='hidden' id='hidden_position' value='" + value.position + "'/><input type='hidden' id='hidden_zip' value='" + value.zip + "'/><input type='hidden' id='hidden_phone' value='" + value.phone + "'/><input type='hidden' id='hidden_email' value='" + value.email + "'/><input type='hidden' id='hidden_qq' value='" + value.qq + "'/><input type='hidden' id='hidden_fax' value='" + value.fax + "'/><input type='hidden' id='hidden_img' value='" + value.img + "'/><input type='hidden' id='hidden_operateTime' value='" + value.operateTime + "'/><input type='hidden' id='hidden_clientName' value='" + value.clientName + "'/><input type='hidden' id='hidden_clientAddress' value='" + value.clientAddress + "'/><input type='hidden' id='hidden_clientPhone' value='" + value.clientPhone + "'/><input type='hidden' id='hidden_clientBusiness' value='" + value.clientBusiness + "'/><input type='hidden' id='hidden_clientUrl' value='" + value.clientUrl + "'/><input type='hidden' id='hidden_nature' value='" + value.nature + "'/><input type='hidden' id='hidden_classify' value='" + value.classify + "'/><input type='hidden' id='hidden_legalPerson' value='" + value.legalPerson + "'/><input type='hidden' id='hidden_phoneShow' value='" + value.phoneShow + "'/><input type='hidden' id='hidden_positionShow' value='" + value.positionShow + "'/><a class='m1_modifyBtn' href='#'>修改</a>&nbsp;&nbsp;&nbsp;<a class='m1_delSureBtn' href='#'>删除</a></td>";
                var tr = $("<tr>");
                tr.append(id).append(name).append(position).append(phone).append(clientName).append(qq).append(fax).append(email).append(modify);
                $("#m1_infoTab").append(tr);
                //修改
                $(".m1_modifyBtn").unbind("click").click(function () {
                    $("#m1_modifyId").val($(this).parent().children("#hidden_id").val());
                    $("#m1_modifyName").attr("placeholder", $(this).parent().children("#hidden_name").val());
                    $("#m1_modifyBirthday").attr("placeholder", $(this).parent().children("#hidden_birthday").val());
                    $("#m1_modifyPosition").attr("placeholder", $(this).parent().children("#hidden_position").val());
                    $("#m1_modifyZip").attr("placeholder", $(this).parent().children("#hidden_zip").val());
                    $("#m1_modifyPhone").attr("placeholder", $(this).parent().children("#hidden_phone").val());
                    $("#m1_modifyEmail").attr("placeholder", $(this).parent().children("#hidden_email").val());
                    $("#m1_modifyQQ").attr("placeholder", $(this).parent().children("#hidden_qq").val());
                    $("#m1_modifyFax").attr("placeholder", $(this).parent().children("#hidden_fax").val());
                    $("#m1_modifyImg").html('<img id="m1_modifyImgBtn" width="150" height="200" class="img" src="' + $(this).parent().children("#hidden_img").val() + '"/>');
                    $("#m1_modifyOperateTime").attr("placeholder", $(this).parent().children("#hidden_operateTime").val());
                    $("#m1_modifyClientName").attr("placeholder", $(this).parent().children("#hidden_clientName").val());
                    $("#m1_modifyClientAddress").attr("placeholder", $(this).parent().children("#hidden_clientAddress").val());
                    $("#m1_modifyClientPhone").attr("placeholder", $(this).parent().children("#hidden_clientPhone").val());
                    $("#m1_modifyClientBusiness").attr("placeholder", $(this).parent().children("#hidden_clientBusiness").val());
                    $("#m1_modifyClientUrl").attr("placeholder", $(this).parent().children("#hidden_clientUrl").val());
                    $("#m1_modifyNature").attr("placeholder", $(this).parent().children("#hidden_nature").val());
                    $("#m1_modifyClassify").attr("placeholder", $(this).parent().children("#hidden_classify").val());
                    $("#m1_modifyLegalPerson").attr("placeholder", $(this).parent().children("#hidden_legalPerson").val());
                    if ($(this).parent().children("#hidden_phoneShow").val() == "True") {
                        $("#m1_modifyPhoneOpen").attr("checked", "checked");
                    } else {
                        $("#m1_modifyPhoneClose").attr("checked", "checked");
                    }
                    if ($(this).parent().children("#hidden_positionShow").val() == "True") {
                        $("#m1_modifyPositionOpen").attr("checked", "checked");
                    } else {
                        $("#m1_modifyPositionClose").attr("checked", "checked");
                    }
                    $("#m1_modifyImgBtn").unbind("click").click(function () {
                        // 上传方法
                        $.upload({
                            url: 'contactsAjax.aspx',
                            dataType: 'string',
                            fileName: 'filedata',
                            params: {
                                id: $("#m1_modifyId").val(),
                                requestMethod: "requestModifyImg"
                            },
                            onSend: function () {
                                return true;
                            },
                            onComplate: function (data) {
                                if (data == "formatErr") {
                                    alert("图片限于bmp,png,gif,jpeg,jpg格式");
                                } else if (data == "sizeErr") {
                                    alert("图片大小不能超过2M");
                                } else if (data != "") {
                                    $("#m1_modifyImgBtn").attr("src", data);
                                }
                            }
                        });
                    });
                    $("#m1_modyfyNone").click();
                });

                //删除
                $(".m1_delSureBtn").unbind("click").click(function () {
                    $("#m1_delSureId").val($(this).parent().children("#hidden_id").val());
                    $('#myModal').modal('show');
                });
            });
        }

    });
}
