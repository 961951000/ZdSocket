<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="key.aspx.cs" Inherits="LIBRARY.views.home.key" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>获取密钥</title>
    <script type="text/javascript" src="/wwwroot/lib/jquery/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var obj = document.getElementById("s4activeX");
            serialStr = obj.GetDongleSerial();          
            $("#key").text(serialStr);
            $("#key1").val(serialStr);
        });
    </script>
</head>
<body>
    <object id="s4activeX" classid="clsid:0D1928EA-F7B7-4F1D-BDA1-2796F5419FD0" codebase="../bin/s4com.dll" style="display: none"></object>
    <h1 id="key" style="margin-left: 40%; margin-top: 500px;"></h1>
    <input type="text" id="key1" style="margin-left: 40%;"/>
</body>
</html>
