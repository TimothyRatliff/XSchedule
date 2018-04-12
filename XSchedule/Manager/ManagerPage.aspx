<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerPage.aspx.cs" Inherits="XSchedule.Manager.ManagerPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Xml ID="Xml1" runat="server"></asp:Xml>
    <p>
        <asp:TextBox ID="managerpagetitle" runat="server" Height="76px" 
            OnTextChanged="managerpagetitle_TextChanged" Width="739px">Only Manager roles can view this page. Here is a display of the aspnetusers table</asp:TextBox>
    </p>
    <div>
    </div>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
            SelectCommand="SELECT [Email] FROM [AspNetUsers]"></asp:SqlDataSource>
    </p>
</asp:Content>
