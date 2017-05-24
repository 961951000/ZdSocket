<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="valueAddedShow.aspx.cs" Inherits="LIBRARY.views.valueAdded.valueAddedShow" validateRequest="false"  %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>增值功能</title>
    <link href="/wwwroot/bootstrap-3.3.5/dist/css/bootstrap.min.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/table.css" media="all" rel="stylesheet" />
    <link href="/wwwroot/css/basic.css" media="all" rel="stylesheet" />
    <script src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script src="/wwwroot/lib/jquery/jquery.upload.js"></script>
    <script src="/wwwroot/bootstrap-3.3.5/dist/js/bootstrap.min.js"></script>
    <script src="/wwwroot/js/basic.js"></script>
    <script src="/wwwroot/js/sendEmail.js"></script>
    <script src="/wwwroot/js/valueAdded.js"></script>
    <link href="/wwwroot/umeditor1_2_2-utf8-asp/themes/default/css/umeditor.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" src="/wwwroot/umeditor1_2_2-utf8-asp/third-party/jquery.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/wwwroot/umeditor1_2_2-utf8-asp/umeditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/wwwroot/umeditor1_2_2-utf8-asp/umeditor.min.js"></script>
    <script src="/wwwroot/lib/jquery/ajaxfileupload.js"></script>
    <style>
        .ltext1 #li6 { opacity: 0.5;  background-color: dimgray; }
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
                <span id="exitSwitch"><a href="#">退出系统</a></span>
                <span><a href="../resource/resourceShow.aspx">资源规划</a></span>
                <span><a href="../systemManage/systemShow.aspx">系统管理</a></span>
                <span id="clickBtn"><a href="valueAddedShow.aspx">增值功能</a></span>
                <span><a href="../contacts/contactsShow.aspx">联系人信息</a></span>
            </div>
        </div>
        <!--  -->
        <div id="left" style="padding-top: 160px;">
            <div id="l1" class="ltext1">

                <div id="li1" class="li">
                    <p><span><a>信封地址打印</a></span></p>
                </div>

                <div id="li2" class="li">
                    <p><span><a>签名文件打印</a></span></p>
                </div>

                <div id="li3" class="li">
                    <p><span><a>通讯录打印</a></span></p>
                </div>

                <div id="li4" class="li">
                    <p><span><a>二维码打印</a></span></p>
                </div>

                <div id="li5" class="li">
                    <p><span><a>邮件发送</a></span></p>
                </div>

                <%--<div id="li6" class="li">
                    <p><span><a>传真发送</a></span></p>
                </div>--%>
            </div>
        </div>
        <div id="l9">
            <p><span>用户名：<%=HttpContext.Current.Session["username"] %></span></p>
            <p><span>技术支持   智定科技</span></p>
            <p><span>智定图书ATM管理系统</span></p>
            <p><span><a href="http://www.zzdd.com.cn" target="_blank">www.zzdd.com.cn</a></span></p>
        </div>
        <div id="centre">
            <!--用户信息管理 -->
             <!--信封地址打印 -->
                <div id="m1" class="centre">
                    <div id="m1_cause" class="cause">
                        <div>
                            <div>
                                <span>输入姓名查询：</span><input type="text" name="name" class="m1_value" /><input type="button" value="查询" class="btn" />
                            </div>
                            <div>
                                <input type="button" value="查询所有" id="m1_quertAll" class="quertAll" style="margin-left:200px;" />
                            </div>
                        </div>
                    </div>
                    <div id="m1_result" class="result">
                        <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m1_addContacts" id="outCheck" style="position: absolute; top: 20px; font-size: 150%; left: 11%;">导出所选信封</button>
                        <button class="btn btn-primary btn-sm" data-toggle="modal" data-target="#m1_addContacts" id="printFileContent" style="position: absolute; top: 20px; font-size: 150%; left: 21%;" >导出所选发文内容</button>
                      <!--导出表格打印信封-->
                     <!--   <form action="../contacts/exportToExcel.aspx" method="post" id="m2_derivedForm">
                           <input type="hidden" name="elxStr" />
                        </form>
                        -->
                         <table id="m1_infoTab" class="infoTab">
                            <caption>联系人信息</caption>
                            <thead>
                                <tr>
                                    <th><input type='checkbox' style='width:20px;height:20px;'  id='checkAll3'/>全选</th>
                                    <th>序号</th>
                                    <th>姓名</th>
                                    <th>工作单位</th>
                                    <th>单位地址</th>
                                    <th>邮编</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                        <div class="page">
                            <span>
                                <span class="page_1">第<span id="m1_pageCurrent"></span>页/共<span id="m1_totalPage">1</span>页</span>
                                <span><a href="ValueAjax.aspx" id="m1_start">首页</a></span>
                                <span><a href="#" id="m1_before">上一页</a></span>
                                <span style="display: none"><a href="#" id="m1_current">当前页</a></span>
                                <span><a href="#" id="m1_after">下一页</a></span>
                                <span><a href="#" id="m1_end">尾页</a></span></span>
                            <span class="page_2">共查询出<span id="m1_totalRecord"></span>条记录</span>
                        </div>
                    </div>
                </div>
            <!--------------------------->

             <!------------发送邮件----------------->
            <div id="m3" class="centre" width:"100%">
               <div style="width:100%;">
                <button type="submit" class="btn btn-success" id="sendEmail">&nbsp;&nbsp;&nbsp;发&nbsp;&nbsp;&nbsp;送&nbsp;&nbsp;&nbsp;</button>
                <button type="button" class="btn btn-default" id="cancel">&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;</button>
                <button type="button" class="btn btn-default" data-toggle='modal' data-target='#sendModal2'>&nbsp;&nbsp;配&nbsp;&nbsp;置&nbsp;&nbsp;</button>
              </div>
              <hr style="margin-top: 2px;" />
              <div  style="width:100%;">
                <div style="margin-left: 30px;float:left;width:70%">
                        <div class="form-group">
                            <label for="exampleInputEmail1">收件人：</label>
                            <input type="email" class="form-control" style="display: inline-block; width: 65%;"  id="exampleInputEmail1" data-toggle="tooltip" data-placement="bottom" title="提示：多个邮件请用逗号(，)或者空格隔开！" />
                            <span class="glyphicon glyphicon-plus-sign" aria-hidden="true" id="addPerson" style="font-size:25px; vertical-align: middle; cursor:pointer;"></span>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputPassword1">抄送人：</label>
                            <input type="email" class="form-control" style="display: inline-block; width: 65%;" id="copyEmail" />
                            <span class="glyphicon glyphicon-plus-sign" aria-hidden="true" id="copyPerson" style="font-size:25px; vertical-align: middle; cursor:pointer;"></span>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputEmail1">主&nbsp;&nbsp;&nbsp;&nbsp;题：</label>
                            <input type="text" class="form-control" style="display: inline-block; width: 68%;" id="exampleInputTopic" />
                        </div>
                        <div class="form-group">
                            <label for="exampleInputFile">添加附件：</label>
                            <div id="abc123">
                            <input type="file" style="display: inline-block;" multiple="multiple"  name="uploadfile" id="exampleInputFile" />
                            </div>
                        </div>
      
                        <script type="text/plain" id="myEditor"   style="width:880px;height:240px;">
                        </script>
                   </div>
                    <div id="contactPerson" style="float:left;">
                    <div>
                       <b>检索：</b><input type="text" class="form-control" style="display: inline-block; width: 65%;"  id="selectName" />
                    </div>
                    <div style=" width:240px; height:350px; margin:20px 0px; overflow:auto;"  class="result1">
                        <table class="table table-condensed table-hover"  style="border:10px;">
                            <thead>
                              <tr class="danger"><th><input type='checkbox' style='width:20px;height:20px;'  id='checkAll'/></th><th>从联系人中获取</th></tr>
                            </thead>
                            <tbody style="cursor:pointer;" id="infoTab">
                            
                            </tbody>
                        </table>
                      </div>
                      <div style="margin-left:90px;">
                         <button type="button" class="btn btn-success" id="checkSure" >确&nbsp;&nbsp;&nbsp;定</button>
                      </div>
                   </div>
                  <!--抄送人-->
                   <div id="copyPersonList" style="float:left;">
                    <div>
                       <b>检索：</b><input type="text" class="form-control" style="display: inline-block; width: 65%;"  id="selectName2" />
                    </div>
                    <div style=" width:240px; height:350px; margin:20px 0px; overflow:auto;"  class="result1">
                        <table class="table table-condensed table-hover"  style="border:10px;">
                            <thead>
                              <tr class="danger"><th><input type='checkbox' style='width:20px;height:20px;' id='checkAll2'/></th><th>从联系人中获取</th></tr>
                            </thead>
                            <tbody style="cursor:pointer;" id="infoTab2">
                            
                            </tbody>
                        </table>
                      </div>
                      <div style="margin-left:90px;">
                         <button type="button" class="btn btn-success" id="checkSure2" >确&nbsp;&nbsp;&nbsp;定</button>
                      </div>
                   </div>
            </div>
          </div>
            
            <div id="emailSuccess" class="centre">
                    <div style="margin:200px 400px; float:left;">
                        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true" style="font-size:50px;"></span><span style="font-weight:bold;font-size:30px;margin-left:50px;">发送成功！</span><br/>
                        <button type="button" class="btn btn-default" id="againEmail" style="margin-left:400px;" >在写一封</button>
                    </div>
             </div>

            <div id="emailError" class="centre">
                    <div style="margin:200px 400px; float:left;">
                        <span class="glyphicon glyphicon-remove-sign" aria-hidden="true" style="font-size:50px;"></span><span style="font-weight:bold;font-size:30px;margin-left:50px;">发送失败！</span><span style="font-size:20px;margin-left:50px;">（可能原因：发件人信息有误！）</span><br/>
                        <button type="button" class="btn btn-default" id="backEmail" style="margin-left:400px;" >返回</button>
                    </div>
             </div>
        <!----------------------------->

              <!--配置发送人-->
        <div id="sendModal2" class="modal fade" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true" style="width: 700px; margin-left: 680px; margin-top: 100px;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header clearfix">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 id="sendModalLabel">配置发件人信息</h4>
                    </div>
                    <div class="modal-body1">
                       <table>
                            <tr>
                                <td style="width:100px;">邮箱：</td>
                                <td><input type="email" id="peizhiEmail" value="18368081577@163.com"/></td>
                            </tr>
                            <tr>
                                <td>密码：</td>
                                <td><input type="password" id="password" value="qq442054809"/></td>
                           </tr>
                           <tr>
                                <td>smtp服务器地址：</td>
                                <td>
                                    <span style="margin-left:100px;width:18px;overflow:hidden;">      
			                            <select id="a2"  style="width:118px; height:31px; margin-left:3px"  onchange="this.parentNode.nextSibling.value=this.value">      
				                            <option value="">请选择</option>   
				                            <option value="smtp.163.com">smtp.163.com</option>   
				                            <option value="smtp.163.yeah.com">smtp.163.yeah.com</option>      
				                            <option value="smtp.126.com">smtp.126.com</option>      
				                            <option value="smtp.qq.com">smtp.qq.com</option>  
				                            <option value="smtp.gmail.com">smtp.gmail.com</option>   
				                            <option value="smtp.sohu.com">smtp.sohu.com</option>   
				                            <option value="smtp.sina.com">smtp.sina.com</option>   
				                            <option value="smtp.aol.com">smtp.aol.com</option>   
				                            <option value="smtp.139.com">smtp.139.com</option>   
				                            <option value="smtp.21cn.com">smtp.21cn.com</option>   
				                            <option value="smtp.tom.com">smtp.tom.com</option>   
				                            <option value="smtp.live.com">smtp.live.com</option>   
			                            </select>
		                            </span><input name="box" id="smtp" style="width:205px;position:absolute;left:237px;"/>

                                </td>
                            </tr>
                       </table>
                    </div>
                    <div class="modal-footer" style="text-align: center;">
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true" id="peizhiSure">确&nbsp;&nbsp;定</button>
                    </div>
                </div>
            </div>
        </div>

            <!---------打印通讯录-------->
                <div id="m5" class="centre" width:"100%">
                   <div style="margin:80px 700px;">
		               <img src="/wwwroot/images/TelPrint.png" />
	                </div>
	                <button style="margin-left:770px;"  id="m2_derived">导出打印通讯录</button>
                  <form action="../contacts/contactsImportAll.aspx" method="post" id="m2_derivedForm">
                    <input type="hidden" name="elxStr" />
                  </form>
              </div>
            <!--------------------------->

            <!---------打印二维码-------->
              <div id="m4" class="centre" width:"100%">
                   <div id="print" style="margin:80px 700px;">
		               <img src="/wwwroot/images/QR_code.jpg" />
	                </div>
	                <button style="margin:0px 810px;"  onclick="myPrint(document.getElementById('print'))">打 印</button>
              </div>

            <!-------------------------------->
           
             <!---------打印签名文件-------->
              <div id="m22" class="centre" width:"100%">
                   <div id="nameFile" style="margin:80px 200px; height:350px; overflow:auto;">
                        <table id="m3_infoTab">
                        </table>
	                </div>
                  <div>
                       <button id="addfw" style="margin:50px 810px;"  >新增模板</button>
                  </div>
              </div>

                <span><a href="ValueAjax.aspx" id="ValueAjax"></a></span>
                <!--富文本编辑框-->
                <div id="m222" class="centre" width:"100%">
                       <div id="nameFile1" style="margin:20px 100px;">
                           <div style="margin-bottom:6px;">
                                <label>标题：</label>
                                <input type="text" class="form-control" style="display: inline-block; width: 65%;"  id="titlefw" />
                            </div>
                            <script type="text/plain" id="myEditor1"  style="width:1400px;height:400px;">
                            </script>
                       </div>
                      <div>
                           <button id="save" style="margin-left:700px;" >保存</button>
                           <button id="cancel1" style="margin:0px 80px;"  >取消</button>
                      </div>
                </div>
               <!--修改（编辑）富文本编辑框-->
                <div id="m22222" class="centre" width:"100%">
                       <div style="margin:20px 100px;">
                           <div style="margin-bottom:6px;">
                                <label>标题：</label>
                                <input type="text" class="form-control" style="display: inline-block; width: 65%;"  id="titleUpdate" />
                            </div>
                            <script type="text/plain" id="myEditor2"  style="width:1400px;height:400px;">
                            </script>
                       </div>
                      <div>
                           <button id="saveUpdate" style="margin-left:700px;" >保存</button>
                           <button id="cancelUpdate" style="margin:0px 80px;" >取消</button>
                      </div>
                </div>

                <!--点击发文标题显示对应的发文内容-->
                <div id="m2222" class="centre" width:"100%">
                    <div style="background:#ffffff; margin:20px 250px; height:520px; width:1250px; overflow-y:scroll;" >
                        <div id="titleShow" style="text-align:center; margin-top:20px;"></div><br/>
                        <div id="contentShow" ></div>
                    </div>
                    <div style=" margin-left: 43%; margin-top: 10px;">
                        <button onclick="myPrint(document.getElementById('contentShow'))">打 印</button>
                        <button style="margin-left:30px;" id="back">返 回</button>
                     </div>
                </div>

               <!--点击所选人员放置信封-->
                <div id="m202" class="centre" width:"100%">
                    <div style="background:#ffffff; margin:20px 300px; height:500px; overflow-y:scroll;" >
                        <div style=" width:600px; margin:30px 200px;" id="letter" ></div>
                    </div>
                    <div style=" margin-left: 43%; margin-top: 10px;">
                         <button  onclick="myPrint(document.getElementById('letter'))">打 印</button>
                        <button style="margin-left:30px;" id="back2">返 回</button>
                    </div>
                </div>

            <!--点击关联打印发文内容循环所选人员姓名打印不同多份发文内容-->
                <div id="m203" class="centre" width:"100%">
                    <div style="background:#ffffff; margin:20px 300px; height:500px; overflow-y:scroll;" >
                        <div id="fileContent" ></div>
                    </div>
                    <div style=" margin-left: 43%; margin-top: 10px;">
                        <button  onclick="myPrint(document.getElementById('fileContent'))">打 印</button>
                        <button style="margin-left:30px;" id="back3">返 回</button>
                    </div>
                </div>
            <!-------------------------------->
        </div>
       </div>

    <!--提示信息框 message-->
        <div id="messageModal" class="modal fade" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true" style="width: 700px; margin-left: 680px; margin-top: 100px;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header clearfix">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 id="myModalLabel">提示信息</h4>
                    </div>
                    <div class="modal-body1">
                        <label style="font-size: 20px; margin: 20px 220px;">
                            <b id="message">打印<input type="text" size="1" value="1" id="number" name="number" style="margin-left: 10px; margin-right: 10px; line-height: 10px;" />份

                            </b>
                        </label>
                    </div>
                    <div class="modal-footer" style="text-align: center;">
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">确&nbsp;&nbsp;定</button>
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

</body>
</html>
