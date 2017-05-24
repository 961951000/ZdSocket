<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contactsImportAll.aspx.cs" Inherits="LIBRARY.views.contacts.contactsImportAll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>联系人信息</title>
</head>
<body>
    <%List<LIBRARY.models.Information> Informations = queryAllIsSelect(); %>
    <table class="infoTab">
        <thead>
            <tr>
                <% foreach (LIBRARY.models.Information information in Informations)
                    {
                        if (information.name == "姓名")
                        {%>
                <th>姓名</th>
                <%}
                    else if (information.name == "出生年月")
                    {%>

                <th>出生年月</th>
                <%}
                    else if (information.name == "职务")
                    { %>
                <th>职务</th>
                <%}
                    else if (information.name == "手机")
                    { %>
                <th>手机</th>
                <%}
                    else if (information.name == "E-mail")
                    {%>
                <th>E-mail</th>
                <%}
                    else if (information.name == "QQ")
                    {%>
                <th>QQ</th>
                <%}
                    else if (information.name == "传真")
                    {%>
                <th>传真</th>
                <%}
                    else if (information.name == "照片")
                    {%>
                <th>照片</th>
                <%}
                    else if (information.name == "工作单位")
                    {%>
                <th>工作单位</th>
                <%}
                    else if (information.name == "单位地址")
                    {%>
                <th>单位地址</th>
                <%}
                    else if (information.name == "单位电话")
                    {%>
                <th>单位电话</th>
                <%}
                    else if (information.name == "主营业务")
                    {%>
                <th>主营业务</th>
                <%}
                    else if (information.name == "公司网址")
                    {%>
                <th>公司网址</th>
                <%}
                    else if (information.name == "邮编")
                    {%>
                <th>邮编</th>
                <%}
                    else if (information.name == "性质")
                    {%>
                <th>性质</th>
                <%}
                    else if (information.name == "分类")
                    {%>
                <th>分类</th>
                <%}
                    else if (information.name == "法人代表")
                    {%>
                <th>法人代表</th>
                <%}
                    } %>
            </tr>
        </thead>
        <tbody>
            <%List<LIBRARY.models.Contacts> list = getContacts();

                foreach (LIBRARY.models.Contacts contacts in list)
                {%>
            <tr>
                <% foreach (LIBRARY.models.Information information in Informations)
                    {
                        if (information.name == "姓名")
                        {%>
                <td><%=contacts.name %></td>
                <%}
                    else if (information.name == "出生年月")
                    {%>

                <td><%=contacts.birthday %></td>
                <%}
                    else if (information.name == "职务")
                    { %>
                <td><%=contacts.position %></td>
                <%}
                    else if (information.name == "手机")
                    { %>
                <td><%=contacts.phone %></td>
                <%}
                    else if (information.name == "E-mail")
                    {%>
                <td><%=contacts.email %></td>
                <%}
                    else if (information.name == "QQ")
                    {%>
                <td><%=contacts.qq %></td>
                <%}
                    else if (information.name == "传真")
                    {%>
                <td><%=contacts.fax %></td>
                <%}
                    else if (information.name == "照片")
                    {%>
                <td>
                    <img src="<%=contacts.img%>" />
                </td>
                <%}
                    else if (information.name == "工作单位")
                    {%>
                <td><%=contacts.clientName %></td>
                <%}
                    else if (information.name == "单位地址")
                    {%>
                <td><%=contacts.clientAddress %></td>
                <%}
                    else if (information.name == "单位电话")
                    {%>
                <td><%=contacts.clientPhone %></td>
                <%}
                    else if (information.name == "主营业务")
                    {%>
                <td><%=contacts.clientBusiness %></td>
                <%}
                    else if (information.name == "公司网址")
                    {%>
                <td><%=contacts.clientUrl %></td>
                <%}
                    else if (information.name == "邮编")
                    {%>
                <td><%=contacts.zip %></td>
                <%}
                    else if (information.name == "性质")
                    {%>
                <td><%=contacts.nature %></td>
                <%}
                    else if (information.name == "分类")
                    {%>
                <td><%=contacts.classify %></td>
                <%}
                    else if (information.name == "法人代表")
                    {%>
                <td><%=contacts.legalPerson %></td>

                <%}
                    } %>
            </tr>
            <%}%>
        </tbody>
    </table>
</body>
</html>
