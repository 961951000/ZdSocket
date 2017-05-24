<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="derivedStructure.aspx.cs" Inherits="LIBRARY.views.systemManage.derivedStructure" %>

<!DOCTYPE html>
<meta http-equiv="content-type" content="application/ms-excel; charset=UTF-8" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单结构</title>
</head>
<body>
    <table style="width: 100%; height: 100%">
        <tr>
            <%List<LIBRARY.models.Information> informations = queryAllIsSelect();
                foreach (LIBRARY.models.Information information in informations)
                {%>
            <th style="border: 2px solid; width: 100px; height: 50px; background-color: lavenderblush"><%=information.name %></th>
            <%}
            %>
        </tr>
    </table>
</body>
</html>
