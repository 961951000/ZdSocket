(function ($) {
    $.fn.serializeJson = function () {
        var serializeObj = {};
        var array = this.serializeArray();
        var str = this.serialize();
        $(array).each(function () {
            if (serializeObj[this.name]) {
                if ($.isArray(serializeObj[this.name])) {
                    serializeObj[this.name].push(this.value);
                } else {
                    serializeObj[this.name] = [serializeObj[this.name], this.value];
                }
            } else {
                serializeObj[this.name] = this.value;
            }
        });
        return serializeObj;
    };
})(jQuery);

$(function () {
    if ($("#username").val() == "") {
        $("#exitSwitch").click();
    }
    basicInit();
});

//页面初始化
function basicInit() {
    $(".centre").hide();
    $("#clickBtn").css("background-color", "dimgray");
    $("#left div div").click(function () {
        $("#left div div").css("background-color", "");
        $(this).css("background-color", "dimgray");
        $(".centre").hide();
    });
    leftMetu();
    //反选
    $('#seletd_btn').click(function () {
        var selecteds = $(".selected_node");
        for (var i = 0; i < selecteds.length; i++) {
            selecteds[i].checked ^= 1;
        }
    });
    //全选
    $('#seletdAll_btn').click(function () {
        var selecteds = $(".selected_node");
        for (var i = 0; i < selecteds.length; i++) {
            selecteds[i].checked = 1;
        }
    });
    $("#exitSwitch").click(function () {
        url = "../home/login.aspx";
        params = {};
        params.requestMethod = "requestExitLog";
        $.post(url, params, function () {
            location.href = "../home/login.aspx";
        });
    });
    fileValidation();
}
//左菜单
function leftMetu() {
    $(".ltext2 div").hide();
    $("#l1 div").mouseover(function () {
        $(".ltext2 div").hide();
        $("#l2 div").show();
    });
    $("#l3 div").mouseover(function () {
        $(".ltext2 div").hide();
        $("#l4 div").show();
    });
    $("#l5 div").mouseover(function () {
        $(".ltext2 div").hide();
        $("#l6 div").show();
    });

    $(".ltext2 div").click(function () {
        $(".ltext2 div").find("a").css("color", "");
        $(".ltext2 div").css("background-color", "");
        $(this).css("background-color", "dimgray");
        $(this).find("a").css("color", "white");
    });
}

//日期格式化
function data_string(str) {
    var d = eval('new ' + str.substr(1, str.length - 2));
    var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds()];
    for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
    if (d.getFullYear() > 1900) { return ar_date.slice(0, 3).join('-') }
    else {
        return "";
    }
    function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
}
//日期格式化
function dataTime_string(str) {
    var d = eval('new ' + str.substr(1, str.length - 2));
    var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds()];
    for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
    if (d.getFullYear() > 1900) { return ar_date.slice(0, 3).join('-') + ' ' + ar_date.slice(3).join(':'); }
    else {
        return "";
    }
    function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
}
//文件验证
function fileValidation() {
    //验证图片格式
    $(".img").change(function () {
        var filepath = $(this).val();
        var extStart = filepath.lastIndexOf(".");
        var ext = filepath.substring(extStart, filepath.length).toUpperCase();
        if (ext != ".BMP" && ext != ".PNG" && ext != ".GIF" && ext != ".JPG" && ext != ".JPEG") {
            $(this).val("")
            alert("图片限于bmp,png,gif,jpeg,jpg格式");
        }
            //验证上传图片大小
        else if (this.files[0].size / 1048576 > 2) {
            $(this).val("")
            alert("图片大小不能超过2M");
        } else {

        }
    });
    //验证excel
    $(".excel").change(function () {
        var filepath = $(this).val();
        var extStart = filepath.lastIndexOf(".");
        var ext = filepath.substring(extStart, filepath.length).toUpperCase();      
        if (ext != ".XLS"&&ext != ".XLSX" && ext != ".XLSM" && ext != ".XLTX" && ext != ".XLTM" && ext != ".XLSB" && ext != ".XLAM") {
            $(this).val("")
            alert("表格限于xls,xlsx,xlsm,xltx,xltm,xlsb,xlam格式");
        }
    });
}