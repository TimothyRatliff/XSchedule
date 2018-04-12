<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manager.aspx.cs" Inherits="Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style4 {
            width: 209px;
            height: 23px;
        }
        .auto-style5 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                    <asp:Label ID="UserLabel" runat="server" Text="Label"></asp:Label>
    
        <table class="auto-style1">
            <caption>
                <asp:Label ID="DailyLabel" runat="server" Text="Label"></asp:Label>
            </caption>
            <tr>
                <td class="auto-style4">Average Wait</td>
                <td id="2" class="auto-style5">
                    <asp:Label ID="DailyAvWaitLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Average Queue Length</td>
                <td id="tf3" class="auto-style5">
                    <asp:Label ID="DailyAvLengthLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Percentage of Time Empty</td>
                <td id="typeField" class="auto-style5">
                    <asp:Label ID="DailyTimeEmptyLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Jobs Addressed Same Day</td>
                <td id="typeField" class="auto-style5">
                    <asp:Label ID="DailySameDayLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    
        <asp:GridView ID="technicianGrid"  runat="server" Caption="Technician Stats" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Job ID" />
                <asp:BoundField DataField="username" HeaderText="Name" />
			    <asp:BoundField DataField="downTime" HeaderText="Down Time" />
            </Columns>
        </asp:GridView>
    
        <asp:GridView ID="queueGrid"  runat="server" Caption="Queue" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="position" HeaderText="Queue Position" />
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    
        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    
        <asp:Button ID="LogOutButton" runat="server" OnClick="LogOutButton_Click" Text="Log Out" />
    
    </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
