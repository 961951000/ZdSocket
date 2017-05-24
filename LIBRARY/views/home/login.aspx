<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="LIBRARY.Views.home.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>登录页面</title>
	<link href="/wwwroot/css/lrtk.css" media="all" rel="stylesheet" />
	<script type="text/javascript" src="/wwwroot/lib/jquery/jquery.min.js"></script>
	<script type="text/javascript" src="/wwwroot/js/login.js"></script>
</head>
<body>
	<!-- 代码 开始 -->
	<div id="loginTitle">
		<h1>智定云通讯管理软件</h1>
	</div>
	<div id="login">
		<div class="wrapper">
			<div class="login">
				<form action="http://www.lanrentuku.com/" method="post" class="container offset1 loginform">
					<object id="s4activeX" classid="clsid:0D1928EA-F7B7-4F1D-BDA1-2796F5419FD0" codebase="../bin/s4com.dll"></object>
					<input type="hidden" name="serial" id="serial" value="" />
					<div id="owl-login">
						<div class="hand"></div>
						<div class="hand hand-r"></div>
						<div class="arms">
							<div class="arm"></div>
							<div class="arm arm-r"></div>
						</div>
					</div>
					<div class="pad">
						<input type="hidden" name="_csrf" value="9IAtUxV2CatyxHiK2LxzOsT6wtBE6h8BpzOmk=" />
						<div class="control-group">
							<div class="controls">
								<label for="username" class="control-label fa fa-envelope"></label>
								<input id="username" type="text" name="email" placeholder="用户名" tabindex="1" autofocus="autofocus" class="form-control input-medium" />
							</div>
						</div>
						<div class="control-group">
							<div class="controls">
								<label for="password" class="control-label fa fa-asterisk"></label>
								<input id="password" type="password" name="password" placeholder="密码" tabindex="2" class="form-control input-medium" />
							</div>
						</div>
					</div>
					<div class="form-actions">
						<!--<a href="../../moban920/index.html" tabindex="5" class="btn pull-left btn-link text-muted" style="color:black;">下载APP</a><a href="http://www.lanrentuku.com/" tabindex="6" class="btn btn-link text-muted">Sign Up</a>-->
						<button type="button" tabindex="4" class="btn btn-primary" id="submit_btn">&nbsp;登录&nbsp;</button>
					</div>
				</form>
			</div>
		</div>
	</div>
	<!-- 代码 结束 -->
	<div style="text-align: center; margin: 300px 0 0 0">
		<div id="bottom">
			<div>
				<p><span>© 2001-2016 上海智定科技股份有限公司 版权所有，并保留所有权利。</span></p>
			</div>

			<div>
				<p><span>热线电话：400-615-3033&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;技术支持：(021)54531210*8023&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;邮箱：hhf@zzdd.com.cn</span></p>
			</div>
			<div>
				<p><span>ICP备案证书号:<a style="color: #000;" href="http://www.miitbeian.gov.cn/">沪ICP备06001246号-4</a></span></p>
			</div>
		</div>
	</div>
</body>
</html>
