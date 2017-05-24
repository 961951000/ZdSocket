$(function () {
    initM1();
    pageM1();
    showDataM1(url);
    showDataM2(url);
    addfw();


});

function myPrint(obj) {
    //打开一个新窗口newWindow
    var newWindow = window.open("打印窗口", "_blank");
    //要打印的div的内容
    var docStr = obj.innerHTML;
    //打印内容写入newWindow文档
    newWindow.document.write(docStr);
    //关闭文档
    newWindow.document.close();
    //调用打印机
    newWindow.print();
    //关闭newWindow页面
    newWindow.close();
}

function initM1() {
    //通讯录打印
    $("#li3").click(function () {
        $("#m5").show();
    })

    //签名文件打印
    $("#li2").click(function () {
        showDataM2(url);
        $("#m22").show();
    })

    //点击取消按钮返回发文内容列表界面
    $("#cancel1").click(function () {
        $("#m222").hide();
        $("#m22").show();
    });

    //编辑的时候取消编辑返回发文内容列表
    $("#cancelUpdate").click(function () {
        $("#m22222").hide();
        $("#m22").show();
    });

    //点击题目查看发文内容后再点击返回按钮返回到当前的发文内容列表
    $("#back").click(function () {
        $("#m22").show();
        $("#m2222").hide();
    });
        
    //点击新增模板按钮 隐藏当前发文内容列表，打开编辑页面
    $("#addfw").click(function () {
        $("#m22").hide();
        $("#titlefw").val('');
        var editor = UM.getEditor('myEditor1');
        $("#m222").show();
        editor.setContent('');
    });

    //点击信封显示页面的返回  back2
    $("#back2").click(function () {
        $("#m202").hide();
        $("#m1").show();
    });

    //点击发文内容显示页面的返回  back3
    $("#back3").click(function () {
        $("#m203").hide();
        $("#m1").show();
    });

    //导出联系人数据
    $("#m2_derived").click(function () {
        $("#m2_derivedForm [name=elxStr]").val(encodeURIComponent("<table>" + $("#m2_infoTab thead").html() + $("#m2_infoTab tbody").html() + "</table>"));
        $("#m2_derivedForm").submit();
    });

    //二维码打印
    $("#li4").click(function () {
        $("#m4").show();
    });

    //打印信封
    $("#li1").click(function () {
        $("#m1").show();
    });

    //打开联系人
    $("#addPerson").click(function () {
        $("#copyPersonList").hide();
        $("#contactPerson").show();
    });

    //打开抄送人
    $("#copyPerson").click(function () {
        $("#contactPerson").hide();
        $("#copyPersonList").show();
    });

    $("#left div div").click(function () {
        $(".hidden").remove();
        $(".result").hide();
        $(".cause").show();
    });

    //搜索
    $("#m1_cause input[type=button]").click(function () {
        key = $(this).parent().children(".m1_value").attr("name");
        url = $("#m1_start").attr("href");
        if (key == undefined) {
            $(".cause").hide();
            showDataM1(url);
        } else if ($(this).parent().children(".m1_value").val().trim() != "") {
            $("#m1_cause div").append('<input type="hidden" class="hidden" id="m1_hidden" name="' + key + '" value="' + $(this).parent().children(".m1_value").val().trim() + '" />');
            $(".cause").hide();
            showDataM1(url);
        }
    });

    //导出联系人数据
    //全选按钮  
    $("#checkAll3").click(function () {
        $('input[name="tableCheck1"]').prop("checked", this.checked);
    });

    $("#outCheck").click(function () {
        if ($("input[name='tableCheck1']:checked").length == 0) {
            alert("请选择要导出的人员信息！");
            return false;
        }
        var showLetter = "";
        for (var i = 0; i < $("input[name='tableCheck1']:checked").length; i++) {
            letter = $("input[name='tableCheck1']:checked")[i].value;
            alert(letter);
            str = letter.split(",");
            var arr = "";
            arr0 = "<span>" + str[0] + "</span><br><br>";
            arr1 = "<span>" + str[1] + "</span><br><br>";
            arr2 = "<span>" + str[2] + "</span><br><br>";
            arr3 = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span>" + str[3] + "</span><br><br>";
            arr = arr0 + arr1 + arr2 + arr3;
            showLetter += "<div style='float:left; margin-left:70px;'>" + arr + "</div>";
        }
        $("#m1").hide();
        $("#letter").html(showLetter);
        $("#m202").show();
    });
}

function pageM1() {
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
    params = getParamsM1();
    params.requestMethod = "requestGetContacts";
    $.post(url, params, function (jsonObject) {
        //删除已有表格
        $("#m1_infoTab tbody").empty();
        var json = jQuery.parseJSON(jsonObject);
        //设置分页链接
        $("#m1_pageCurrent").html(json.pc);
        $("#m1_totalPage").html(json.tp);
        $("#m1_totalRecord").html(json.tr);
        $("#m1_before").attr("href", "ValueAjax.aspx?pc=" + (json.pc - 1));
        $("#m1_current").attr("href", "ValueAjax.aspx?pc=" + json.pc);
        $("#m1_after").attr("href", "ValueAjax.aspx?pc=" + (json.pc + 1));
        $("#m1_end").attr("href", "ValueAjax.aspx?pc=" + json.tp);
        if (json.pc == 1) {
            $("#m1_before").hide();
        } else {
            $("#m1_before").show();
        }
        if (json.pc < json.tp) {
            $("#m1_after").show();
        } else {
            $("#m1_after").hide();
        }
       
        //遍历集合,显示数据          
        var check;
        $.each(json.datas, function (n, value) {
            var id = $("<td>");
            id.text((json.pc - 1) * json.ps + n + 1);
            var name = $("<td>");
            name.text(value.name);
            var clientName = $("<td>");
            clientName.text(value.clientName);
            var clientAddress = $("<td>");
            clientAddress.text(value.clientAddress);
            var zip = $("<td>");
            zip.text(value.zip);
            check = "<td><input type='checkbox' name='tableCheck1' style='width:20px;height:20px;'  clientAddress='" + value.clientAddress + "' value='" + value.zip + "," + value.clientName + "," + value.clientAddress + "," + value.name + "'></td>";
            var tr = $("<tr>");
            tr.append(check).append(id).append(name).append(clientName).append(clientAddress).append(zip);
            $("#m1_infoTab").append(tr);
        });
        $(".result").show();
    });
}

//保存分页条件
function getParamsM1() {
    key = $("#m1_hidden").attr("name");
    value = $("#m1_hidden").val();
    url = $("#m1_start").attr("href");
    if (key == "name") {
        params = { name: value };  
    } else if (key == "unit") {
        params = { unit: value };
    } else if (key == undefined) {
        params = {};
    }
    return params;
}

//发文内容的初始数据显示
function showDataM2(url) {
    url = "ValueAjax.aspx";
    params = {}
    params.requestMethod = "requestQueryAllMessage";
    $.post(url, params, function (jsonObject) {
        $("#m3_infoTab tbody").empty();
        var json = jQuery.parseJSON(jsonObject);
        $.each(json, function (n, value) {
            $("#m3_infoTab").append("<tbody><tr><td style='border:0px; text-align:left; cursor:pointer;'><input name='selectBoard' style='width:20px;height:20px;' type='radio' value='" + value.content + "' /></td><td class='showContent' style='border:0px; text-align:left; cursor:pointer;'><span class='glyphicon glyphicon-hand-right' id='del' style='font-size:8px; margin-right:20px;'>" +
                "</span>" + value.title + "</td><td class='editor' style='border:0px; text-align:left;'><span class='glyphicon glyphicon-pencil'  style='font-size:8px; margin-left:400px; cursor:pointer;'>编辑</span></td>" +
                "<td class='del' style='border:0px;  text-align:left;'><span class='glyphicon glyphicon-remove-sign' style='font-size:8px; margin-left:20px;  cursor:pointer;'>删除</span></td></tr></tbody>");

            //点击查看当前的发文内容
            $('.showContent').click(function () {
                var rowIdx = $(this).parent()[0].rowIndex + 1;
                if ((n + 1) == rowIdx) {
                    $("#contentShow").html(value.content);
                    $("#m22").hide();
                    $("#m2222").show();
                }
            });

            //删除发文内容
            $(".del").click(function () {
                var rowIdx = $(this).parent()[0].rowIndex + 1;
                if ((n + 1) == rowIdx) {
                    if (confirm("确定删除吗？")) {
                        var id = value.id;
                        url = "ValueAjax.aspx";
                        params = { id: id }
                        params.requestMethod = "requestDel";
                         $.post(url, params, function (datas) {
                            if (datas != null) {
                                showDataM2(url);
                                alert("删除成功！");
                            } else {
                                alert("删除失败！");
                            }
                        });
                    } else {
                        return false;
                    }
                }
            });

            //修改发文内容
            $('.editor').click(function () {
                var rowIdx = $(this).parent()[0].rowIndex + 1;
                if ((n + 1) == rowIdx) {
                    $("#m22").hide();
                    var editor = UM.getEditor('myEditor2');
                    $("#titleUpdate").val(value.title);
                    $("#myEditor2").html(value.content);
                    $("#m22222").show();
                    $("#saveUpdate").unbind("click").click(function () {
                        var id = value.id;
                        var title = $("#titleUpdate").val();
                        var content = UM.getEditor('myEditor2').getContent();
                        url = "ValueAjax.aspx";
                        params = { id: id, title: title, content: content };
                        params.requestMethod = "requestUpdate";                       
                        $.post(url, params, function (datas) {
                            if (datas != null) {
                                $("#m22222").hide();
                                alert("修改成功！");
                                showDataM2(url);
                                $('#m22').show();
                            } else {
                                alert("修改失败！");
                            }
                        });
                    });
                }
            });
           
            //关联打印发文内容
            $("#printFileContent").unbind("click").click(function () {
                var item = $(":radio:checked");
                var len = item.length;
                if (len > 0) {
                    if ($("input[name='tableCheck1']:checked").length == 0) {
                        alert("请选择要打印的发文人员！");
                        return false;
                    }
                    var content = '';
                    for (var i = 0; i < $("input[name='tableCheck1']:checked").length; i++) {
                        letter = $("input[name='tableCheck1']:checked")[i].value;
                        str = letter.split(",");
                        content += $(":radio:checked").val().replace(/xxx/g, str[3]) + '';
                    }
                    $("#fileContent").html(content);
                    $("#m1").hide();
                    $("#m203").show();
                } else {
                    alert("请选择一个模板！！！");
                }
            });

        });
    });
}

//增加发文内容
function addfw() {
    $("#save").click(function () {
        content = UM.getEditor('myEditor1').getContent();
        title = $("#titlefw").val();
        if (title == '') {
            alert("请输入发文内容标题！");
            return false;
        }
        url = $("#ValueAjax").attr("href");
        params = { content: content, title: title };
        params.requestMethod = "requestAddFw";
        $.post(url, params, function (datas) {
            if (datas != null) {
                $("#m222").hide();
                alert("添加成功");
                showDataM2(url);
                $("#m22").show();
            } else {
                alert("添加失败");
            }
        });
    });
}
