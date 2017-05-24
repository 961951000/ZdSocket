<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exportLetter.aspx.cs" Inherits="LIBRARY.views.valueAdded.exportLetter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>联系人信息</title>
</head>
<body>
            <%List<LIBRARY.models.Contacts> list = getContacts();
                foreach (LIBRARY.models.Contacts contacts in list)
                {%>
                        <span><%=contacts.zip %></span><br/>
                        <span><%=contacts.clientAddress %></span><br/>
                        <span><%=contacts.clientName %></span><br/>
                        <span><%=contacts.name %></span><br/>
                <%}%>
</body>
</html>

