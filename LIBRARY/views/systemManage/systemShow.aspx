<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="systemShow.aspx.cs" Inherits="LIBRARY.views.systemManage.systemShow" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统管理</title>
    <link href="/wwwroot/lib/bootstrap/bootstrap.min.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/basic.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/system.css" rel="stylesheet" />
    <script src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script src="/wwwroot/lib/bootstrap/bootstrap.min.js"></script>
    <script src="/wwwroot/js/basic.js"></script>
    <script src="/wwwroot/js/columnManage.js"></script>
    <script src="/wwwroot/js/columnSet.js"></script>
    <script src="/wwwroot/js/operatorManagement.js"></script>
</head>
<body>
    <input type="hidden" value="<%=Session["role"] %>" id="role" />
    <div id="box">
        <div id="top">
            <div id="t1">
                <img src="/wwwroot/images/addressBook/0.jpg" />
            </div>
            <div id="title">
                <h1>智定云通讯管理软件</h1>
            </div>
            <div id="t2">
                <img src="/wwwroot/images/addressBook/1.jpg" />
            </div>
            <div id="t3">
                <span id="exitSwitch"><a href="#">退出系统</a></span>
                <span><a href="../resource/resourceShow.aspx">资源规划</a></span>
                <span id="clickBtn"><a href="systemShow.aspx">系统管理</a></span>
                <span><a href="../valueAdded/valueAddedShow.aspx">增值功能</a></span>
                <span><a href="../contacts/contactsShow.aspx">联系人信息</a></span>
            </div>

        </div>
        <!--  -->
        <div id="left" style="padding-top: 160px;">
            <div id="l1" class="ltext1">

                <div id="li1" class="li">
                    <p><span><a>联系人栏目管理</a></span></p>
                </div>

                <div id="li2" class="li">
                    <p><span><a>常用栏目设置</a></span></p>
                </div>

                <div id="li3" class="admin" style="display: none">
                    <p><span><a>操作员信息管理</a></span></p>
                </div>

            </div>
        </div>
        <div id="l9">
            <p><span>用户名：<%=HttpContext.Current.Session["username"] %></span></p>
            <p><span>技术支持   智定科技</span></p>
            <p><span>智定通讯录管理系统</span></p>
            <p><span><a href="http://www.zzdd.com.cn" target="_blank">www.zzdd.com.cn</a></span></p>
        </div>
        <div id="centre">
            <!--用户账号管理 -->
            <div id="column">
                <div id="m1" class="centre">
                    <button class="btn btn-primary btn-sm" style="position: absolute; top: 20px; font-size: 150%; left: 15%;" onclick="m1_derivedForm.submit()">导出表单结构</button>
                    <form action="derivedStructure.aspx" id="m1_derivedForm" style="display: none"></form>
                    <table id="m1_infoTab" class="infoTab">
                        <caption>用户账号</caption>
                    </table>
                </div>
                <!--  -->

                <!--栏目信息管理 -->
                <div id="m2" class="centre">
                    <table id="m2_infoTab" class="infoTab">
                        <caption>常用栏目设置</caption>
                    </table>
                    <div class="select_node">
                        <button id="seletdAll_btn">全选</button>
                        <button id="seletd_btn">反选</button><br />
                    </div>
                    <div>
                        <button id="m2_submit" style="margin-left: 80%; margin-top: 2%;" class="btn btn-primary">提交设置</button><br />
                    </div>
                </div>
            </div>
            <!--操作员信息管理 -->
            <div id="m3" class="centre">
                <span id="m3_btns">
                    <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m1_addContacts" style="position: absolute; top: 20px; font-size: 150%; left: 15%;">新增信息操作员</button>
                </span>
                <table id="m3_infoTab" class="infoTab">
                    <caption>信息操作员管理</caption>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>账户</th>
                            <th>角色</th>
                            <th colspan="2">操作</th>
                        </tr>
                    </thead>
                </table>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m3_pageCurrent"></span>页/共<span id="m3_totalPage">1</span>页</span>
                        <span><a href="systemAjax.aspx" id="m3_start">首页</a></span>
                        <span><a href="#" id="m3_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m3_current">当前页</a></span>
                        <span><a href="#" id="m3_after">下一页</a></span>
                        <span><a href="#" id="m3_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m3_totalRecord"></span>条记录</span>
                </div>

                <!--新增信息操作员 -->
                <div class="modal fade" id="m1_addContacts" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close"
                                    data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title">新增信息操作员
                                </h4>
                            </div>

                            <form action="systemAjax.aspx" method='post'>
                                <div class="modal-body" id="m1_add">
                                    <div style="margin: 15px 0 0 0">
                                        <span>账号：</span><input type="text" id="m3_username" /><span id="m3_nameErr" class="err"></span>
                                        <br />
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default"
                                        data-dismiss="modal" id="m3_cancelAdd">
                                        取消
                                    </button>
                                    <button type="button" class="btn btn-primary" id="m3_submitAdd">
                                        确定操作
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>

                <div class="manage_node" style="display: none">
                    <!-- 按钮触发模态框 -->
                    <button class="btn btn-primary btn-sm" data-toggle="modal"
                        data-target="#m3_modelModify" id="m3_modifyBtn">
                        修改
                    </button>
                </div>
                <!--修改 -->
                <div class="modal fade" id="m3_modelModify" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close"
                                    data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title">修改
                                </h4>
                            </div>

                            <form id="m3_modifyForm">
                                <div class="modal-body">
                                    <div style="margin: 15px 0 0 0">
                                        <input type="hidden" name="id" id="m3_id" />
                                        用户：<input style="margin-left: 12px" type='text' name="username" id="m3_name" /><span id="m3_err" class="err"></span>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default"
                                        data-dismiss="modal" id="m3_cancelModify">
                                        取消
                                    </button>
                                    <button type="button" class="btn btn-primary" id="m3_submitModify">
                                        确认操作
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
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
