// JavaScript Document
//支持Enter键登录
document.onkeydown = function (e) {
    if ($(".bac").length == 0) {
        if (!e) e = window.event;
        if ((e.keyCode || e.which) == 13) {
            var obtnLogin = document.getElementById("submit_btn")
            obtnLogin.focus();
        }
    }
}

$(function () {  
    initLogin();
    //提交表单
    $('#submit_btn').click(function () {
        show_loading();
        var serialStr = "";
        var obj = document.getElementById("s4activeX");    //获取控件对象
        //serialStr = obj.GetDongleSerial();
        //alert(serialStr);
        $("#serial").val(serialStr);
        if ($('#username').val() == '') {
            show_err_msg('用户名还没填呢！');
            $('#username').focus();
        }
        else if ($('#password').val() == '') {
            show_err_msg('密码还没填呢！');
            $('#password').focus();
        }
        else {
            login();
        }
        function login() {
            var username = $("#username").val().trim();
            var password = $("#password").val().trim();
            var serial = $("#serial").val().trim();
            url = "login.aspx";
            params = { "username": username, "password": password, requestMethod: "requestLogin" };
            $.post(url, params, function (data) {
                if (data == 1) {
                    show_msg('登录成功咯！  正在为您跳转...', '../contacts/contactsShow.aspx');
                } else if (data == "") {
                    show_err_msg('用户名或者密码错误！');
                } else {
                    json = $.parseJSON(data);
                    //var key1 = key1.replace(/^\s+|\s+$/g, "");   
                    if ((!!window.ActiveXObject || "ActiveXObject" in window)&&document.getElementById("s4activeX").GetDongleSerial().substr(0, document.getElementById("s4activeX").GetDongleSerial().length - 1) == json.serial) {
                        params = { id: json.id, username: json.username, role: json.role, serial: json.serial, customerId: json.customerId, requestMethod: "requestLoginSerial" }
                        $.post(url, params, function (data) {
                            if (data == 1) {
                                show_msg('登录成功咯！  正在为您跳转...', '../contacts/contactsShow.aspx');
                            } else {
                                show_err_msg('请检查加密锁是否准确放置！');
                            }
                        });
                    } else {
                        show_err_msg('请检查加密锁是否准确放置！');
                    }
                }
            });
        }
    });
});

function initLogin() {
    $('#login #password').focus(function () {
        $('#owl-login').addClass('password');
    }).blur(function () {
        $('#owl-login').removeClass('password');
    });
}


var msgdsq;
//错误时：提示调用方法
function show_err_msg(msg) {
    $('.msg_bg').html('');
    clearTimeout(msgdsq);
    $('body').append('<div class="sub_err" style="position:absolute;top:60px;left:0;width:500px;z-index:999999;"></div>');
    var errhtml = '<div  class="bac" style="padding:8px 0px;border:1px solid #ff0000;width:100%;margin:0 auto;background-color:#fff;color:#B90802;border:3px #ff0000 solid;text-align:center;font-size:16px;font-family:微软雅黑;"><img style="margin-right:10px;" src="/wwwroot/images/login_img/error.png">';
    var errhtmlfoot = '</div>';
    $('.msg_bg').height($(document).height());
    $('.sub_err').html(errhtml + msg + errhtmlfoot);
    var left = ($(document).width() - 500) / 2;
    $('.sub_err').css({ 'left': left + 'px' });
    var scroll_height = $(document).scrollTop();
    $('.sub_err').animate({ 'top': scroll_height + 120 }, 300);
    msgdsq = setTimeout(function () {
        $('.sub_err').animate({ 'top': scroll_height + 80 }, 300);
        setTimeout(function () {
            $('.msg_bg').remove();
            $('.sub_err').remove();
        }, 300);
    }, "1000");
}

//正确时：提示调用方法
function show_msg(msg, url) {
    $('.msg_bg').html('');
    clearTimeout(msgdsq);
    $('body').append('<div class="sub_err" style="position:absolute;top:60px;left:0;width:500px;z-index:999999;"></div>');
    var htmltop = '<div class="bac" style="padding:8px 0px;border:1px solid #090;width:100%;margin:0 auto;background-color:#FFF2F8;color:#090;border:3px #090 solid;;text-align:center;font-size:16px;"><img style="margin-right:10px;" src="/wwwroot/images/login_img/loading.gif">';
    var htmlfoot = '</div>';
    $('.msg_bg').height($(document).height());
    var left = ($(document).width() - 500) / 2;
    $('.sub_err').css({ 'left': left + 'px' });
    $('.sub_err').html(htmltop + msg + htmlfoot);
    var scroll_height = $(document).scrollTop();
    $('.sub_err').animate({ 'top': scroll_height + 120 }, 500);
    msgdsq = setTimeout(function () {
        $('.sub_err').animate({ 'top': scroll_height + 80 }, 500);
        setTimeout(function () {
            $('.msg_bg').remove();
            $('.sub_err').remove();
            if (url != '') {
                location.href = url;
            }
        }, 800);

    }, "1200");
}

//显示加载动画
function show_loading() {
    var str = '<div class="msg_bg" style="background:#000;opacity:0.5;filter:alpha(opacity=50);z-index:99998;width:100%;position:absolute;left:0;top:0"></div>';
    str += '<div class="msg_bg" style="z-index:99999;width:100%;position:absolute;left:0;top:0;text-align:center;"><img src="/wwwroot/images/login_img/loading.gif" alt="" class="loading"></div>'
    $('body').append(str);
    var scroll_height = $(document).scrollTop();
    $('.msg_bg').height($(document).height());
    $('.loading').css('margin-top', scroll_height + 240);
}