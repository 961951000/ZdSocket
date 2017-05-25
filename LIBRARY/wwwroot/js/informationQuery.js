$(function () {
    pageM2();
    initM2();
});

function initM2() {
    //用户信息查询
    $("#li2").click(function () {
        $('#m2_start').click();
        $("#m2").show();
    });
}
function pageM2() {
    //导出联系人数据
    $("#m2_derived").click(function () {
        $("#m2_derivedForm [name=elxStr]").val(encodeURIComponent("<table>" + $("#m2_infoTab thead").html() + $("#m2_infoTab tbody").html() + "</table>"));
        $("#m2_derivedForm").submit();
    });
    //搜索
    $("#m2_submit").unbind("click").click(function () {
        $('#m2_cancel1').click();
        $('#m2_start').click();
    });

    //首页           
    $('#m2_start').click(function () {
        url = $("#m2_start").attr("href");
        showDataM2(url);
        return false;
    });
    //上一页
    $('#m2_before').click(function () {
        url = $("#m2_before").attr("href");
        showDataM2(url);
        return false;
    });
    //当前页
    $("#m2_current").click(function () {
        url = $("#m2_current").attr("href");
        showDataM2(url);
        return false;
    });
    //下一页
    $('#m2_after').click(function () {
        url = $("#m2_after").attr("href");
        showDataM2(url);
        return false;
    });
    //尾页
    $('#m2_end').click(function () {
        url = $("#m2_end").attr("href");
        showDataM2(url);
        return false;
    });
}
//显示数据
function showDataM2(url) {
    params = $("#m2_form").serializeJson();
    params.requestMethod = "requestGetContacts";
    $.post(url, params, function (jsonObject) {
        //删除已有表格
        $("table tbody").empty();
        json = $.parseJSON(jsonObject);
        if (json.pc === 1) {
            $("#m2_before").hide();
        } else {
            $("#m2_before").show();
        }
        if (json.tp > json.pc) {
            $("#m2_after").show()
        } else {
            $("#m2_after").hide();
        }
        if (json.pc > json.tp) {
            $("#m2_pageCurrent").html(1);
            alert("未查询出任何数据！！！");
        } else {
            //设置分页链接
            $("#m2_pageCurrent").html(json.pc);
            $("#m2_totalPage").html(json.tp);
            $("#m2_totalRecord").html(json.tr);
            $("#m2_before").attr("href", "contactsAjax.aspx?pc=" + (json.pc - 1));
            $("#m2_current").attr("href", "contactsAjax.aspx?pc=" + json.pc);
            $("#m2_after").attr("href", "contactsAjax.aspx?pc=" + (json.pc + 1));
            $("#m2_end").attr("href", "contactsAjax.aspx?pc=" + json.tp);            
            //遍历集合,显示数据          
            $.each(json.datas, function (n, value) {
                //alert(n + ' ' + value.name);
                var id = $("<td>");
                id.text((json.pc - 1) * json.ps + n + 1);
                var name = $("<td>");
                name.text(value.name);
                var position = $("<td>");
                position.text(value.position);
                var phone = $("<td>");
				phone.text(value.phone);
				var classify = $("<td>");
				classify.text(value.classify);
                var clientName = $("<td>");
				clientName.text(value.clientName);
                var qq = $("<td>");
                qq.text(value.qq);
                var fax = $("<td>");
                fax.text(value.fax);
                var email = $("<td>");
                email.text(value.email);
                var select = "<td><input type='hidden' id='hidden_name' value='" + value.name + "'/><input type='hidden' id='hidden_birthday' value='" + value.birthday + "'/><input type='hidden' id='hidden_position' value='" + value.position + "'/><input type='hidden' id='hidden_zip' value='" + value.zip + "'/><input type='hidden' id='hidden_phone' value='" + value.phone + "'/><input type='hidden' id='hidden_email' value='" + value.email + "'/><input type='hidden' id='hidden_qq' value='" + value.qq + "'/><input type='hidden' id='hidden_fax' value='" + value.fax + "'/><input type='hidden' id='hidden_img' value='" + value.img + "'/><input type='hidden' id='hidden_operateTime' value='" + value.operateTime + "'/><input type='hidden' id='hidden_clientName' value='" + value.clientName + "'/><input type='hidden' id='hidden_clientAddress' value='" + value.clientAddress + "'/><input type='hidden' id='hidden_clientPhone' value='" + value.clientPhone + "'/><input type='hidden' id='hidden_clientBusiness' value='" + value.clientBusiness + "'/><input type='hidden' id='hidden_clientUrl' value='" + value.clientUrl + "'/><input type='hidden' id='hidden_nature' value='" + value.nature + "'/><input type='hidden' id='hidden_classify' value='" + value.classify + "'/><input type='hidden' id='hidden_legalPerson' value='" + value.legalPerson + "'/><input type='hidden' id='hidden_phoneShow' value='" + value.phoneShow + "'/><input type='hidden' id='hidden_positionShow' value='" + value.positionShow + "'/><a class='m2_queryAllBtn' href='#'>详细</a></td>";
                var tr = $("<tr>");
				tr.append(id).append(name).append(position).append(phone).append(classify).append(clientName).append(qq).append(fax).append(email).append(select);
                $("#m2_infoTab").append(tr);
                //详细
                $(".m2_queryAllBtn").unbind("click").click(function () {
                    $("#m2_name").text($(this).parent().children("#hidden_name").val());
                    $("#m2_birthday").text($(this).parent().children("#hidden_birthday").val());
                    $("#m2_position").text($(this).parent().children("#hidden_position").val());
                    $("#m2_zip").text($(this).parent().children("#hidden_zip").val());
                    $("#m2_phone").text($(this).parent().children("#hidden_phone").val());
                    $("#m2_email").text($(this).parent().children("#hidden_email").val());
                    $("#m2_qq").text($(this).parent().children("#hidden_qq").val());
                    $("#m2_fax").text($(this).parent().children("#hidden_fax").val());
                    $("#m2_img").html('<img width="150" height="200" src="' + $(this).parent().children("#hidden_img").val() + '"/>');
                    $("#m2_operateTime").text($(this).parent().children("#hidden_operateTime").val());
                    $("#m2_clientName").text($(this).parent().children("#hidden_clientName").val());
                    $("#m2_clientAddress").text($(this).parent().children("#hidden_clientAddress").val());
                    $("#m2_clientPhone").text($(this).parent().children("#hidden_clientPhone").val());
                    $("#m2_clientBusiness").text($(this).parent().children("#hidden_clientBusiness").val());
                    $("#m2_clientUrl").text($(this).parent().children("#hidden_clientUrl").val());
                    $("#m2_nature").text($(this).parent().children("#hidden_nature").val());
                    $("#m2_classify").text($(this).parent().children("#hidden_classify").val());
                    $("#m2_legalPerson").text($(this).parent().children("#hidden_legalPerson").val());
                    $("#m2_queryAllNone").click();
                });
            });
        }
    });
}
