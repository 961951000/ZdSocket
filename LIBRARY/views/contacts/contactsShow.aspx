<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contactsShow.aspx.cs" Inherits="LIBRARY.views.contacts.contactsShow" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>联系人信息</title>
    <link href="/wwwroot/lib/bootstrap/bootstrap.min.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/table.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/basic.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/contacts.css" rel="stylesheet" />
    <script src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script src="/wwwroot/lib/bootstrap/bootstrap.min.js"></script>
    <script src="/wwwroot/lib/jquery/jquery.upload.js"></script>
    <script src="/My97DatePicker/WdatePicker.js"></script>
    <script src="/wwwroot/js/basic.js"></script>
    <script src="/wwwroot/js/informationQuery.js"></script>
    <script src="/wwwroot/js/informationModify.js"></script>
    <script src="/wwwroot/js/informationConfim.js"></script>
</head>
<body>
    <%LIBRARY.models.Contacts add = prepareAdd(); %>
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
                <span><a href="../systemManage/systemShow.aspx">系统管理</a></span>
                <span><a href="../valueAdded/valueAddedShow.aspx">增值功能</a></span>
                <span id="clickBtn"><a href="contactsShow.aspx">联系人信息</a></span>
            </div>
        </div>
        <!--  -->
        <div id="left" style="padding-top: 160px;">
            <div id="l1" class="ltext1">

                <div id="li1" class="li">
                    <p><span><a>信息管理</a></span></p>
                </div>

                <div id="li2" class="li">
                    <p><span><a>信息查询</a></span></p>
                </div>

                <div id="li3" class="li">
                    <p><span><a>上传信息确认</a></span></p>
                </div>
                <div id="li4" class="li" onclick="m4_noneBtn.click()">
                    <p><span><a>导入表单数据</a></span></p>
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
            <!--信息管理 -->
            <div id="m1" class="centre">
                <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m1_addContacts" style="position: absolute; top: 20px; font-size: 150%; left: 15%;">新增联系人</button>
                <table id="m1_infoTab" class="infoTab">
                    <caption>信息管理</caption>
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
                            <th>操作22</th>
                        </tr>
                    </thead>
                </table>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m1_pageCurrent"></span>页/共<span id="m1_totalPage">1</span>页</span>
                        <span><a href="contactsAjax.aspx" id="m1_start">首页</a></span>
                        <span><a href="#" id="m1_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m1_current">当前页</a></span>
                        <span><a href="#" id="m1_after">下一页</a></span>
                        <span><a href="#" id="m1_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m1_totalRecord"></span>条记录</span>
                </div>
            </div>

            <!--新增联系信息 -->
            <div class="modal fade" id="m1_addContacts" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close"
                                data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title">新增联系人
                            </h4>
                        </div>
                        <form action="contactsAdd.aspx" enctype="multipart/form-data" method="post">
                            <div class="modal-body">
                                <%List<LIBRARY.models.Information> Informations = queryAllIsSelect();
                                    foreach (LIBRARY.models.Information information in Informations)
                                    {
                                        if (information.name == "姓名")
                                        {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">姓名：</span><input type="text" name="name" />
                                    <br />
                                </div>
                                <%}
                                    else if (information.name == "出生年月")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">出生年月：</span><input class="Wdate" onclick="WdatePicker()" type="text" name="birthday" /><br />
                                </div>
                                <%}
                                    else if (information.name == "职务")
                                    { %>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">职务：</span><input type="text" name="position" /><span style="margin-left: 2%;">
                                        <label>
                                            <input name="positionShow" type="radio" value="1" checked="checked" />显示
                                        </label>
                                        <label>
                                            <input name="positionShow" type="radio" value="0" />隐藏
                                        </label>
                                    </span>
                                    <br />
                                </div>
                                <%}
                                    else if (information.name == "手机")
                                    { %>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">手机：</span><input type="text" name="phone" />
                                    <span style="margin-left: 2%;">
                                        <label>
                                            <input name="phoneShow" type="radio" value="1" checked="checked" />显示
                                        </label>
                                        <label>
                                            <input name="phoneShow" type="radio" value="0" />隐藏
                                        </label>
                                    </span>
                                    <br />
                                </div>
                                <%}
                                    else if (information.name == "E-mail")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">E-mail：</span><input type="text" name="email" /><br />
                                </div>
                                <%}
                                    else if (information.name == "QQ")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">QQ：</span><input type="text" name="qq" /><br />
                                </div>
                                <%}
                                    else if (information.name == "传真")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">传真：</span><input type="text" name="fax" /><br />
                                </div>
                                <%}
                                    else if (information.name == "照片")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">照片1：</span><input type="text" id="m1_upfile" />
                                    <input type="button" value="浏览" onclick="m1_path.click()" style="margin-left: 2%;" />
                                    <input type="file" id="m1_path" style="display: none" onchange="m1_upfile.value=this.value" name="upfile" class="img" />
                                </div>
                                <%}
                                    else if (information.name == "工作单位")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">工作单位：</span><input type="text" name="clientName" value="<%=add.clientName %>" /><br />
                                </div>
                                <%}
                                    else if (information.name == "单位地址")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">单位地址：</span><input type="text" name="clientAddress" value="<%=add.clientAddress %>" /><br />

                                </div>
                                <%}
                                    else if (information.name == "单位电话")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">单位电话：</span><input type="text" name="clientPhone" value="<%=add.clientPhone %>" /><br />

                                </div>
                                <%}
                                    else if (information.name == "主营业务")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">主营业务：</span><input type="text" name="clientBusiness" value="<%=add.clientBusiness %>" /><br />

                                </div>
                                <%}
                                    else if (information.name == "公司网址")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">公司网址：</span><input type="text" name="clientUrl" value="<%=add.clientUrl %>" /><br />
                                </div>
                                <%}
                                    else if (information.name == "邮编")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">邮编：</span><input type="text" name="zip" value="<%=add.zip %>" /><br />
                                </div>
                                <%}
                                    else if (information.name == "性质")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">性质：</span><input type="text" name="nature" value="<%=add.nature %>" /><br />
                                </div>
                                <%}
                                    else if (information.name == "分类")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">分类：</span><input type="text" name="classify" value="<%=add.classify %>" /><br />
                                </div>
                                <%}
                                    else if (information.name == "法人代表")
                                    {%>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">法人代表：</span><input type="text" name="legalPerson" value="<%=add.legalPerson %>" /><br />
                                </div>
                                <%}
                                    } %>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default"
                                    data-dismiss="modal">
                                    取消
                                </button>
                                <button type="submit" class="btn btn-primary">
                                    确定操作1
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <!-- 信息修改 -->
            <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m1_modyfyTarget" id="m1_modyfyNone" style="display: none">修改44</button>
            <div class="modal fade" id="m1_modyfyTarget" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close"
                                data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title">联系人模板11
                            </h4>
                        </div>

                        <div class="modal-body">
                            <form id="m1_modify">
                                <div style="margin: 15px 0 0 0">
                                    <input type="hidden" id="m1_modifyId" name="id" />
                                    <span class="key">姓名：</span><input class="value" id="m1_modifyName" name="name" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">出生年月：</span><input class="Wdate" onclick="WdatePicker()" id="m1_modifyBirthday" name="birthday" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">职务：</span><input class="value" id="m1_modifyPosition" name="position" /><span style="margin-left: 2%;">
                                        <label>
                                            <input name="phoneShow" type="radio" id="m1_modifyPositionOpen" value="1" />显示
                                        </label>
                                        <label>
                                            <input name="phoneShow" type="radio" id="m1_modifyPositionClose" value="0" />隐藏
                                        </label>
                                    </span>
                                    <br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">手机：</span><input class="value" id="m1_modifyPhone" name="phone" /><span style="margin-left: 2%;">
                                        <label>
                                            <input name="positionShow" type="radio" value="1" id="m1_modifyPhoneOpen" />显示
                                        </label>
                                        <label>
                                            <input name="positionShow" type="radio" value="0" id="m1_modifyPhoneClose" />隐藏
                                        </label>
                                    </span>
                                    <br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">E-mail：</span><input class="value" id="m1_modifyEmail" name="email" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">QQ：</span><input class="value" id="m1_modifyQQ" name="qq" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">传真：</span><input class="value" id="m1_modifyFax" name="fax" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">照片：</span><span class="value" id="m1_modifyImg"></span>
                                    <br />
                                    <br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">工作单位：</span><input class="value" id="m1_modifyClientName" name="clientName" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0" class="noModyfyNode">
                                    <span class="key">单位地址：</span><input class="value" id="m1_modifyClientAddress" name="clientAddress" />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">单位电话：</span><input class="value" id="m1_modifyClientPhone" name="clientPhone" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">主营业务：</span><input class="value" id="m1_modifyClientBusiness" name="clientBusiness" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">公司网址：</span><input class="value" id="m1_modifyClientUrl" name="clientUrl" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">邮编：</span><input class="value" id="m1_modifyZip" name="zip" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">性质：</span><input class="value" id="m1_modifyNature" name="nature" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">分类：</span><input class="value" id="m1_modifyClassify" name="classify" /><br />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">法人代表：</span><input class="value" id="m1_modifyLegalPerson" name="legalPerson" /><br />
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default"
                                        data-dismiss="modal" id="m1_modifyCancel">
                                        取消
                                    </button>
                                    <button type="button" class="btn btn-primary" id="m1_modifySubmit">
                                        确定操作
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

                    <!--删除确定-->
		<div class="modal fade" id="myModal" tabindex="-1" role="dialog" 
		   aria-labelledby="myModalLabel" aria-hidden="true" style="display:none;">
		   <div class="modal-dialog">
			  <div class="modal-content">
				 <div class="modal-header">
					<button type="button" class="close" 
					   data-dismiss="modal" aria-hidden="true">
						  &times;
					</button>
					<h4 class="modal-title" id="myModalLabel">
					  提示信息
					</h4>
				 </div>
                   <form id="m1_delSure">
                        <input type="hidden" id="m1_delSureId" name="id" />
				 <div class="modal-body">
					确定删除吗？
				 </div>
				 <div class="modal-footer">
					<button type="button" class="btn btn-info" 
					   data-dismiss="modal">取消
					</button>
					<button type="button" class="btn btn-info"   data-dismiss="modal" id='delSure'>
					   确定
					</button>
				 </div>
                </form>
			</div>
			</div>
		</div>

            <!--信息查询 -->
            <div id="m2" class="centre">

                <span id="m2_btns" style="position: absolute; top: 20px; left: 15%;">
                    <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m2_searchTarget" style="font-size: 150%;">高级搜索</button>
                    <button class="btn btn-primary btn-sm" style="font-size: 150%;" id="m2_derived">导出联系人数据</button>
                </span>
                <form action="contactsImportAll.aspx" method="post" id="m2_derivedForm">
                    <input type="hidden" name="elxStr" />
                </form>
                <table id="m2_infoTab" class="infoTab">
                    <caption>信息查询</caption>
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
                            <th>操作</th>
                        </tr>
                    </thead>
                </table>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m2_pageCurrent"></span>页/共<span id="m2_totalPage">1</span>页</span>
                        <span><a href="contactsAjax.aspx" id="m2_start">首页</a></span>
                        <span><a href="#" id="m2_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m2_current">当前页</a></span>
                        <span><a href="#" id="m2_after">下一页</a></span>
                        <span><a href="#" id="m2_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m2_totalRecord"></span>条记录</span>
                </div>
            </div>

            <!--高级搜索 -->
            <div class="modal fade" id="m2_searchTarget" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close"
                                data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title">高级搜索
                            </h4>
                        </div>
                        <div class="modal-body">
                            <form id="m2_form">
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">姓名：</span><input type="text" name="name" />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">电话号码：</span><input type="text" name="phone" />
                                </div>
                                <div style="margin: 15px 0 0 0">
                                    <span class="key">工作单位：</span><input type="text" name="clientName" />
                                </div>
                            </form>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-default"
                                data-dismiss="modal" id="m2_cancel1">
                                取消
                            </button>
                            <button type="submit" class="btn btn-primary" id="m2_submit">
                                确定操作
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 详细信息-->
            <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m2_target" id="m2_queryAllNone" style="display: none">联系人详细</button>
            <div class="modal fade" id="m2_target" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close"
                                data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title">用户详细信息
                            </h4>
                        </div>

                        <div class="modal-body">
                            <input type="hidden" />
                            <div style="margin: 15px 0 0 0">
                                <span class="key">姓名：</span><span class="value" id="m2_name"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">出生年月：</span><span class="value" id="m2_birthday"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">职务：</span><span class="value" id="m2_position"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">手机：</span><span class="value" id="m2_phone"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">E-mail：</span><span class="value" id="m2_email"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">QQ：</span><span class="value" id="m2_qq"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">传真：</span><span class="value" id="m2_fax"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">照片：</span><span class="value" id="m2_img"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">工作单位：</span><span class="value" id="m2_clientName"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0" class="noModyfyNode">
                                <span class="key">单位地址：</span><span class="value" id="m2_clientAddress"></span><span style="color: red" id="m1_err" class="error"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">单位电话：</span><span class="value" id="m2_clientPhone"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">主营业务：</span><span class="value" id="m2_clientBusiness"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">公司网址：</span><span class="value" id="m2_clientUrl"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">邮编：</span><span class="value" id="m2_zip"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">性质：</span><span class="value" id="m2_nature"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">分类：</span><span class="value" id="m2_classify"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">法人代表：</span><span class="value" id="m2_legalPerson"></span><br />
                            </div>
                            <div style="margin: 15px 0 0 0">
                                <span class="key">操作时间：</span><span class="value" id="m2_operateTime"></span><br />
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default"
                                    data-dismiss="modal" id="m2_cancel">
                                    退出
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--上传信息确认 -->
            <div id="m3" class="centre">
                <table id="m3_infoTab" class="infoTab">
                    <caption>上传信息确认</caption>
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>联系人姓名</th>
                            <th>工作单位</th>
                            <th>主营业务</th>
                            <th>职务</th>
                            <th>邮箱</th>
                            <th>提交日期</th>
                            <th colspan="2">操作</th>
                        </tr>
                    </thead>
                    <tbody>                       
                    </tbody>
                </table>
                <div class="select_node">
                    <button id="seletdAll_btn">全选</button>
                    <button id="seletd_btn">反选</button><br />
                </div>
                <div class="select_node">
                    <button id="m3_submit" class="btn btn-primary">确认</button>
                    <button id="m3_cancel" class="btn btn-primary">驳回</button><br />
                </div>
                <div class="page">
                    <span>
                        <span class="page_1">第<span id="m3_pageCurrent"></span>页/共<span id="m3_totalPage">1</span>页</span>
                        <span><a href="contactsAjax.aspx" id="m3_start">首页</a></span>
                        <span><a href="#" id="m3_before">上一页</a></span>
                        <span style="display: none"><a href="#" id="m3_current">当前页</a></span>
                        <span><a href="#" id="m3_after">下一页</a></span>
                        <span><a href="#" id="m3_end">尾页</a></span></span>
                    <span class="page_2">共查询出<span id="m3_totalRecord"></span>条记录</span>
                </div>
            </div>
            <!--导入表单数据 -->
            <div id="m4" class="centre1">
                <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m4_target" style="display: none" id="m4_noneBtn">导入表单数据</button>
                <div class="modal fade" id="m4_target" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="margin: 30px 0 0 24%; width: 60%">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close"
                                    data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title">联系人信息导入
                                </h4>
                            </div>
                            <div class="modal-body">
                                <form action="contactsImport.aspx" enctype="multipart/form-data" method="post">
                                    <div style="margin: 15px 0 0 0">
                                        <span class="key" style="width:150px;">请选择表格：</span><input type="text" id="m4_upfile" />
                                        <input type="button" value="浏览" onclick="m4_path.click()" style="margin-left: 2%;" />
                                        <input type="file" id="m4_path" style="display: none" onchange="m4_upfile.value=this.value" name="upfile" class="excel" />
                                    </div>

                                    <br />
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default"
                                            data-dismiss="modal">
                                            取消
                                        </button>
                                        <button type="button" class="btn btn-primary" id="m4_submit">
                                            开始导入
                                        </button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
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
