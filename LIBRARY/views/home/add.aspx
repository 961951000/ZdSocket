<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="LIBRARY.views.home.add" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册用户</title>
    <link href="/wwwroot/lib/bootstrap/bootstrap.min.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/basic.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/table.css" media="all" rel="stylesheet" />
    <script src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script src="/wwwroot/lib/bootstrap/bootstrap.min.js"></script>
    <script src="/wwwroot/js/basic.js"></script>
    <script src="/wwwroot/js/internal/customerManage.js"></script>
    
    <style>
        .key {
            display: -moz-inline-box;
            display: inline-block;
            width: 100px;
            text-align: right;
        }
    </style>
</head>
<body>
    <div id="box">
        <div id="top">
            <div id="t1">
                <img src="/wwwroot/images/addressBook/0.jpg" />
            </div>
            <div id="title">
                <h1>智定通讯录管理系统</h1>
            </div>
            <div id="t2">
                <img src="/wwwroot/images/addressBook/1.jpg" />
            </div>
            <div id="t3">
            </div>
        </div>
        <!--  -->
        <div id="left" style="padding-top: 160px;">
            <div id="l1" class="ltext1">

                <div id="li1" class="li">
                    <p><span><a>添加客户</a></span></p>
                </div>
                <!--
                <div id="li2" class="li">
                    <p><span><a>信息查询</a></span></p>
                </div>-->

                <div id="li3" class="li">
                    <p><span><a>客户管理</a></span></p>
                </div>

            </div>
        </div>
        <div id="l9">
            <p><span>用户名：<%=HttpContext.Current.Session["username"] %></span></p>
            <p><span>技术支持   智定科技</span></p>
            <p><span>智定图书ATM管理系统</span></p>
            <p><span><a href="http://www.zzdd.com.cn" target="_blank">www.zzdd.com.cn</a></span></p>
        </div>

        <div id="centre">
            <div id="m1" class="centre">
                <!-- 添加客户 -->
                <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m1_addTarget" id="m1_addHidden" style="display: none">添加客户</button>
                <div class="modal fade" id="m1_addTarget" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close"
                                    data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title">添加客户
                                </h4>
                            </div>

                            <div class="modal-body">
                                <form id="m1_add">
                                    <div style="margin: 15px 0 0 0">
                                        <input type="hidden" id="m1_modifyId" name="id" />
                                        <span class="key">用户名称：</span><input type="text" name="name" /><br />
                                    </div>
                                    <div style="margin: 15px 0 0 0">
                                        <span class="key">账号ID：</span><input type="text" name="customerCode" /><br />
                                    </div>
                                    <div style="margin: 15px 0 0 0">
                                        <span class="key">用户地址：</span><input type="text" name="address" /><br />
                                    </div>
                                    <div style="margin: 15px 0 0 0">
                                        <span class="key">联系人：</span><input type="text" name="contacts" />
                                        <br />
                                    </div>
                                    <div style="margin: 15px 0 0 0">
                                        <span class="key">登陆账号：</span><input type="text" name="username" id="m1_username"/><span id="m1_err" style="color:red;margin-left:2%;"></span>
                                        <br />
                                    </div>
                                     <div style="margin: 15px 0 0 0">
                                        <span class="key">密钥：</span><input type="text" name="serial" />
                                        <br />
                                    </div>
                                    <div style="margin: 15px 0 0 0">
                                        <span class="key">验证码：</span><input type="text" id="m1_verification" />
                                        <input type="button" value="点击发送验证码" id="m1_verificationCode" style="margin-left: 2%;" />
                                        <input type="hidden" id="m1_hidden" />
                                        <br />
                                        <br />
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default"
                                            data-dismiss="modal" id="m1_setCancel">
                                            取消
                                        </button>
                                        <button type="button" class="btn btn-primary" id="m1_setSubmit">
                                            确定操作
                                        </button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--用户账号管理 -->
            <div id="m3" class="centre">
                <span id="m3_btns"></span>
                <table id="m3_infoTab" class="infoTab">
                    <caption>用户账号</caption>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>用户账户</th>
                            <th>账号ID</th>
                            <th>用户地址</th>
                            <th>联系人</th>
                            <th>开通日期</th>
                        </tr>
                    </thead>
                </table>
                <div class="page" style="display: none">
                    <span>
                        <span class="page_1">第<span id="m3_pageCurrent"></span>页/共<span id="m3_totalPage">1</span>页</span>
                        <span><a href="add.aspx" id="m3_start">首页</a></span>
                        <span><a href="#" id="m3_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m3_current">当前页</a></span>
                        <span><a href="#" id="m3_after">下一页</a></span>
                        <span><a href="#" id="m3_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m3_totalRecord"></span>条记录</span>
                </div>
            </div>
            <!--  -->
        </div>
        <!-- -->
        <div id="bottom">
            <div id="b9">
                <p>
                    <span>商务合作  </span>
                    <span>隐私保护  </span>
                    <span>联系我们  </span>
                    <span>投诉建议  </span>
                </p>
            </div>
            <div>
                <p><span>© 2001-2016 上海智定科技股份有限公司 版权所有，并保留所有权利。</span></p>
            </div>
            <div>
                <p><span>ICP备案证书号:沪ICP备06001246号</span></p>
            </div>
            <div>
                <p><span>热线电话：400-615-3033&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;技术支持：(021)54531210*8023&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;邮箱：hhf@zzdd.com.cn</span></p>
            </div>
        </div>
    </div>
</body>
</html>
