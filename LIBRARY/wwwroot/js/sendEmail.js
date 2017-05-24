
$(function () {
    //打开发送邮件界面
    $("#li5").click(function () {
        $("#exampleInputTopic").val("");
        $("#contactPerson").hide();
        $("#copyPersonList").hide();
        $("#m3").show();
        var editor = UM.getEditor('myEditor');
    });

    url = "valueAddedShow.aspx";
    params = {}
    params.requestMethod = "requestGetEmail";

    //请求查询到的id，name，email
    $.post(url, params, function (jsonObject) {
        var json = jQuery.parseJSON(jsonObject);
        for (var i = 0; i < json.length; i++) {
            $('#infoTab').append("<tr><td><input type='checkbox' style='width:20px;height:20px;' name='tableCheck' value='" + json[i].email + "' id='" + json[i].name + "'></td>" +
				"<td>" + json[i].name + "</td></tr>");
            $('#infoTab2').append("<tr><td><input type='checkbox' style='width:20px;height:20px;' name='tableCheck2' value='" + json[i].email + "' id='" + json[i].name + "'></td>" +
				"<td>" + json[i].name + "</td></tr>");
        }


        //确定按钮  把姓名（拼起来）放到收件人那里
        $("#checkSure").click(function () {
            var str = "";
            for (var i = 0; i < $("input[name='tableCheck']:checked").length; i++) {
                str += $("input[name='tableCheck']:checked")[i].id + "      ";
            }
            $("#exampleInputEmail1").val(str);
            $("#contactPerson").hide();
        });

        //确定按钮  把姓名（拼起来）放到抄送人那里
        $("#checkSure2").click(function () {
            var str = "";
            for (var i = 0; i < $("input[name='tableCheck2']:checked").length; i++) {
                str += $("input[name='tableCheck2']:checked")[i].id + "      ";
            }
            $("#copyEmail").val(str);
            $("#copyPersonList").hide();
        });

        //全选按钮
        $("#checkAll").click(function () {
            $('input[name="tableCheck"]').prop("checked", this.checked);
        });
        //全选抄送
        $("#checkAll2").click(function () {
            $('input[name="tableCheck2"]').prop("checked", this.checked);
        });


        //发送邮件
        $("#sendEmail").click(function () {

            url = $("#sendValue").attr("href");
            var checkids = new Array();
            var checkids2 = new Array();
            var email = '';

            //发件邮箱配置
            smtp = $("#smtp").val();
            upass = $("#password").val();
            name = $("#peizhiEmail").val();
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if ($("#peizhiEmail").val() == '' || $("#password").val() == '') {
                alert('请配置发件人信息！');
                return false;
            } else if (!filter.test(name)) {
                alert('您的发件人邮箱格式不正确');
                return false;
            }

            //遍历收件人
            for (var i = 0; i < $("input[name='tableCheck']:checked").length; i++) {
                if ($("input[name='tableCheck']:checked")[i].value != '') {
                    checkids.push($("input[name='tableCheck']:checked")[i].value);
                }
            }

            //遍历抄送人
            for (var i = 0; i < $("input[name='tableCheck2']:checked").length; i++) {
                if ($("input[name='tableCheck2']:checked")[i].value != '') {
                    checkids2.push($("input[name='tableCheck2']:checked")[i].value);
                }
            }

            //收件人邮箱验证
            if ($("input[name='tableCheck']:checked").length > 0) {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                email1 = checkids.toString();
                email1Arr = email1.split(',');
                for (var i = 0; i < email1Arr.length; i++) {
                    if (!filter.test(email1Arr[i])) {
                        alert('您的电子邮件格式不正确');
                        return false;
                    }
                }
            } else {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                email1 = $("#exampleInputEmail1").val().replace(/\ /g, ",").replace(/\，/g, ",");
                email1Arr = email1.split(',');
                for (var i = 0; i < email1Arr.length; i++) {
                    if (!filter.test(email1Arr[i])) {
                        alert('您的电子邮件格式不正确');
                        return false;
                    }
                }
            }

            //判断是否选择了抄送人
            if ($("#copyEmail").val() == '' && $("input[name='tableCheck2']:checked").length <= 0) {
                email = email1;
            } else {
                //抄送人邮箱验证
                if ($("input[name='tableCheck2']:checked").length > 0) {
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    email2 = checkids2.toString();
                    email2Arr = email2.split(',');
                    for (var i = 0; i < email2Arr.length; i++) {
                        if (!filter.test(email2Arr[i])) {
                            alert('您的电子邮件格式不正确');
                            return false;
                        }
                    }
                } else {
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    email2 = $("#copyEmail").val().replace(/\ /g, ",").replace(/\，/g, ",");
                    email2Arr = email2.split(',');
                    for (var i = 0; i < email2Arr.length; i++) {
                        if (!filter.test(email2Arr[i])) {
                            alert('您的电子邮件格式不正确');
                            return false;
                        }
                    }
                }
                email = email1 + "," + email2;
            }

            //附件
            file = $("#exampleInputFile").val();
            fileList = file.split(',');
            var FileNames = '';
            for (var i = 0; i < fileList.length; i++) {
                var FileName = fileList[i].replace(/^.+?\\([^\\]+?)(\.[^\.\\]*?)?$/gi, "$1");
                FileNames += FileName + "  ";
            }

            //发送主题验证  如果填写了主题就按填写的发送，否则按照附件的名称作为主题，如果都没有则主题为空
            if ($("#exampleInputTopic").val() == '' && FileName == '') {
                if (!confirm("您还没有添加主题，确定要继续吗？")) { return false; }
                topic = '';
            } else if ($("#exampleInputTopic").val() != '') {
                topic = $("#exampleInputTopic").val();
            } else {
                topic = $("#exampleInputTopic").val(FileNames).val();
            }

            //正文部分
            var str = [];
            str.push(UM.getEditor('myEditor').getContent());
            var Body_bak = encodeURIComponent(str.toString());   //编码  后台相应解码

            params = { email: email, Body_bak: Body_bak, topic: topic, file: file, smtp: smtp, name: name, upass: upass };
            params.requestMethod = "requestSendEmail";

            $.ajaxFileUpload({
                url: url,  //需要链接到服务器地址 
                secureuri: false,
                fileElementId: 'exampleInputFile', //文件选择框的id属性 
                data: params,
                dataType: 'string', //服务器返回的格式，可以是json、xml 
                success: function (datas, status) //相当于java中try语句块的用法 
                {
                    if (datas > 0) {
                        $("#m3").hide();
                        $("#exampleInputEmail1").val("");
                        $("#copyEmail").val("");
                        $("input[name='tableCheck']:checked").removeAttr("checked");
                        $("input[name='tableCheck2']:checked").removeAttr("checked");
                        $("#exampleInputTopic").val("");
                        $("#exampleInputFile").remove();
                        $("#abc123").append('<input type="file" style="display: inline-block;" multiple="multiple"  name="file" id="exampleInputFile" />');
                        $("#myEditor").html("");
                        $("#emailSuccess").show();
                    } else {
                        $("#m3").hide();
                        $("#exampleInputEmail1").val("");
                        $("#copyEmail").val("");
                        $("input[name='tableCheck']:checked").removeAttr("checked");
                        $("input[name='tableCheck2']:checked").removeAttr("checked");
                        $("#exampleInputTopic").val("");
                        $("#exampleInputFile").remove();
                        $("#abc123").append('<input type="file" style="display: inline-block;" multiple="multiple"  name="file" id="exampleInputFile" />');
                        $("#myEditor").html("");
                        $("#emailError").show();
                    }

                },
                error: function (data, status, e) { //相当于java中catch语句块的用法 

                }
            });

        });
    });
});

$(function () {
    //input 输入框内容改变时触发事件二
    $("#selectName").keyup(function () {
        var name = $("#selectName").val();
        url = "valueAddedShow.aspx";
        params = { name: name }
        params.requestMethod = "requestGetName";
        $.post(url, params, function (jsonObject) {
            $("#infoTab").empty();
            json = jQuery.parseJSON(jsonObject);
            for (var i = 0; i < json.length; i++) {
                $('#infoTab').append("<tr><td><input type='checkbox' name='tableCheck' value='" + json[i].email + "' id='" + json[i].name + "'></td>" +
                    "<td>" + json[i].name + "</td></tr>");
            }
        });
    });

});

//发送成功后刷新页面
$(function () {
    $("#againEmail").click(function () {
        $("#emailSuccess").hide();
        $("#m3").show();
    //  window.location.href = 'valueAddedShow.aspx'; //跳转刷新refulse
    });

    //发送失败返回写信页面
    $("#backEmail").click(function () {
        $("#emailError").hide();
        $("#m3").show();
    });

    //取消发信
    $("#cancel").click(function () {
        window.location.href='valueAddedShow.aspx';
    });
});