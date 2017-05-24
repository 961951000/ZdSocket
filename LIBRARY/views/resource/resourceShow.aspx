<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resourceShow.aspx.cs" Inherits="LIBRARY.views.resource.resourceShow" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源规划</title>
    <link href="/wwwroot/lib/bootstrap/bootstrap.min.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/table.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/basic.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/resource.css" rel="stylesheet" />
    <script src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script src="/wwwroot/lib/bootstrap/bootstrap.min.js"></script>
    <script src="/wwwroot/js/basic.js"></script>
    <script src="/wwwroot/js/passwordManage.js"></script>
    <script src="/wwwroot/js/customerManage.js"></script>
</head>
<body>
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
                <span id="clickBtn"><a href="resourceShow.aspx">资源规划</a></span>
                <span><a href="../systemManage/systemShow.aspx">系统管理</a></span>
                <span><a href="../valueAdded/valueAddedShow.aspx">增值功能</a></span>
                <span><a href="../contacts/contactsShow.aspx">联系人信息</a></span>
            </div>

        </div>
        <!--  -->
        <div id="left" style="padding-top: 160px;">
            <div id="l1" class="ltext1">

                <div id="li1" class="li">
                    <p><span><a>密码管理</a></span></p>
                </div>

                <div id="li2" class="li" style="display: none">
                    <p><span><a>密钥管理</a></span></p>
                </div>

                <div id="li3" class="li">
                    <p><span><a>用户账户管理</a></span></p>
                </div>
                <!--
                <div id="li4" class="li">
                    <p><span><a>数据备份</a></span></p>
                </div>
                <div id="li5" class="li">
                    <p><span><a>维护日志</a></span></p>
                </div>-->
            </div>
        </div>
        <div id="l9">
            <p><span>用户名：<%=HttpContext.Current.Session["username"] %></span></p>
            <p><span>技术支持   智定科技</span></p>
            <p><span>智定通讯录管理系统</span></p>
            <p><span><a href="http://www.zzdd.com.cn" target="_blank">www.zzdd.com.cn</a></span></p>
        </div>

        <div id="centre">
            <!--密码管理 -->
            <div id="m1" class="centre" style="margin-left: 35%; margin-top: 10%;">
                <span>请输入原密码：</span>
                <input type="password" id="m1_psw" /><span id="m1_pswErr" class="err"></span><br />
                <br />
                <span>设置新的密码：</span>
                <input type="password" id="m1_psw1" /><span id="m1_psw1Err" class="err"></span><br />
                <br />
                <span>重复新的密码：</span>
                <input type="password" id="m1_psw2" /><span id="m1_psw2Err" class="err"></span><br />
                <input type="button" id="m1_submit" value="修改" class="btn btn-primary" style="margin-left: 20%; margin-top: 20px;" />
            </div>
            <!-- 高级搜索-->
            <!--信息查询 -->
            <div id="m2" class="centre">
                <table id="m2_infoTab" class="infoTab">
                    <caption>密钥管理</caption>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>姓名</th>
                            <th>职务</th>
                            <th>电话</th>
                            <th>工作单位</th>
                            <th>QQ</th>
                            <th>传真</th>
                            <th>E-mail</th>
                            <th colspan="2">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>序号</td>
                            <td>姓名</td>
                            <td>职务</td>
                            <td>电话</td>
                            <td>工作单位</td>
                            <td>QQ</td>
                            <td>传真</td>
                            <td>E-mail</td>
                            <td><a href="#">详细</a></td>
                        </tr>
                    </tbody>
                </table>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m2_pageCurrent"></span>页/共<span id="m2_totalPage">1</span>页</span>
                        <span><a href="resourceAjax.aspx" id="m2_start">首页</a></span>
                        <span><a href="#" id="m2_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m2_current">当前页</a></span>
                        <span><a href="#" id="m2_after">下一页</a></span>
                        <span><a href="#" id="m2_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m2_totalRecord"></span>条记录</span>
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
                        <span><a href="resourceAjax.aspx" id="m3_start">首页</a></span>
                        <span><a href="#" id="m3_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m3_current">当前页</a></span>
                        <span><a href="#" id="m3_after">下一页</a></span>
                        <span><a href="#" id="m3_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m3_totalRecord"></span>条记录</span>
                </div>
            </div>
            <!--  -->
            <!--数据备份 -->
            <div id="m4" class="centre">
                <table id="m4_infoTab" class="infoTab">
                    <caption>数据备份</caption>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>联系人姓名</th>
                            <th>工作单位</th>
                            <th>职务</th>
                            <th>邮箱</th>
                            <th>提交日期</th>
                            <th colspan="2">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>序号</td>
                            <td>联系人</td>
                            <td>上传类型</td>
                            <td>上传时间</td>
                            <td>确认人</td>
                            <td>确认时间</td>
                            <td>是</td>
                            <td>否</td>
                        </tr>
                    </tbody>
                </table>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m4_pageCurrent"></span>页/共<span id="m4_totalPage">1</span>页</span>
                        <span><a href="resourceAjax.aspx" id="m4_start">首页</a></span>
                        <span><a href="#" id="m4_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m4_current">当前页</a></span>
                        <span><a href="#" id="m4_after">下一页</a></span>
                        <span><a href="#" id="m4_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m4_totalRecord"></span>条记录</span>
                </div>
            </div>

            <div id="m5" class="centre">
                <table id="m5 _infoTab" class="infoTab">
                    <caption>维护日志</caption>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>联系人姓名</th>
                            <th>工作单位</th>
                            <th>职务</th>
                            <th>邮箱</th>
                            <th>提交日期</th>
                            <th colspan="2">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>序号</td>
                            <td>联系人</td>
                            <td>上传类型</td>
                            <td>上传时间</td>
                            <td>确认人</td>
                            <td>确认时间</td>
                            <td>是</td>
                            <td>否</td>
                        </tr>
                    </tbody>
                </table>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m5_pageCurrent"></span>页/共<span id="m5_totalPage">1</span>页</span>
                        <span><a href="resourceAjax.aspx" id="m5_start">首页</a></span>
                        <span><a href="#" id="m5_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m5_current">当前页</a></span>
                        <span><a href="#" id="m5_after">下一页</a></span>
                        <span><a href="#" id="m5_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m5_totalRecord"></span>条记录</span>
                </div>
            </div>
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
