<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Technician.aspx.cs" Inherits="Technician" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="UserLabel" runat="server" Text="Label"></asp:Label>
    
    </div>
        <asp:Label ID="CurrentJobLabel" runat="server" Text="Label"></asp:Label>
        <p>
            <asp:Button ID="CheckInButton" runat="server" OnClick="CheckInButton_Click" Text="Check In" />
            <asp:Button ID="CheckOutButton" runat="server" OnClick="CheckOutButton_Click" Text="Check Out" />
        </p>
        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <p>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    
        <asp:Button ID="LogOutButton" runat="server" OnClick="LogOutButton_Click" Text="Log Out" />
    
        </p>
    </form>
</body>
</html>
