$(function () {
    $("#li1").click(function () {
        $("#m1").show();
        $("#m1_btn").click();
    });
    modyfyPsw();
});
function modyfyPsw() {
    $("#m1_psw").blur(function () {
        name = $("#m1_psw").val().trim();
        if (name == "") {
            $("#m1_pswErr").html("旧密码不能为空");
        } else {
            $("#m1_pswErr").html("");
        }
    });
    $("#m1_psw1").blur(function () {
        psw = $("#m1_psw1").val().trim();
        if (psw.length < 6) {
            $("#m1_psw1Err").html("密码长度不能小于六位");
        } else {
            $("#m1_psw1Err").html("");
        }
    });

    $("#m1_psw2").blur(function () {
        psw = $("#m1_psw1").val().trim();
        psw1 = $("#m1_psw2").val().trim();
        if (psw != psw1) {
            $("#m1_psw2Err").html("您两次输入的密码不一致，请重新输入");
        } else {
            $("#m1_psw2Err").html("");
        }
    });
    $("#m1_submit").click(function () {
        $("#m1_psw").blur();
        $("#m1_psw1").blur();
        $("#m1_psw2").blur();
        if ($("#m1_pswErr").text() == "" && $("#m1_psw1Err").text() == "" && $("#m1_psw2Err").text() == "") {
            url = "resourceAjax.aspx";
            params = {
                password: $("#m1_psw").val().trim(),
                newPassword: $("#m1_psw1").val().trim(),
                requestMethod: "requestModyfyPassword"
            }
            $.post(url, params, function (datas) {
                if (datas > 0) {
                    $("#m1 input[type=password]").val("");
                    alert("操作成功");
                    $("#exitSwitch").click();
                } else if (datas == 0) {
                    $("#m1_nameErr").html("");
                    alert("操作失败，您输入的密码有误");
                }
            });
        }
    });
}
