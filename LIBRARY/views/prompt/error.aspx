<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="LIBRARY.views.prompt.error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>错误提示页面</title>
    <link href="/wwwroot/css/basic.css" media="all" rel="stylesheet" />
    <script src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            setInterval("remainTime()", 1000);
        });
        function remainTime() {
            i = $("#endtime").text();
            i--;
            if (i == 0) {
                location.href = "../contacts/contactsShow.aspx";
            } else {
                $("#endtime").text(i);
            }
        };
    </script>
</head>
<body>
    <h1 style="margin-top: 100px;">错误信息</h1>
    <div id="m1" style="text-align: center; font-size: 200%; margin-top: 150px;">
        出错了！<br />
        <br />
        如果<span><strong id="endtime">5</strong></span>秒钟还没跳转，请<a href="../contacts/contactsShow.aspx">点击这里</a>
    </div>
</body>
</html>
