<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Customer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            margin-bottom: 5px;
        }
        .auto-style4 {
            width: 121px;
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
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td id="2" class="auto-style5">
                    <asp:Label ID="UserLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Complexity</td>
                <td id="tf3" class="auto-style5">
                    <asp:RadioButtonList ID="complexityButtons" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td id="typeField" class="auto-style5">
                    <asp:Button ID="SubmitButton" runat="server" CssClass="auto-style3" Height="39px" OnClick="SubmitButton_Click" Text="Submit" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="completedJobs"  runat="server" Caption="Completed Jobs" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="checkedIn" HeaderText="Job Started" />
                <asp:BoundField DataField="dequeueTime" HeaderText="Finished" />
			    <asp:BoundField DataField="technician" HeaderText="Technician" />
            </Columns>
        </asp:GridView>
    
        <asp:Button ID="ViewCompletedButton" runat="server" OnClick="ViewCompleted_Click" Text="View Completed" />
    
        <asp:GridView ID="startedJobs"  runat="server" Caption="Started Jobs" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="checkedIn" HeaderText="Job Started" />
                <asp:BoundField DataField="technician" HeaderText="Technician" />
            </Columns>
        </asp:GridView>
    
        <asp:Button ID="ViewStartedButton" runat="server" OnClick="ViewStarted_Click" Text="View Started" />
    
        <asp:GridView ID="unstartedJobs"  runat="server" Caption="Unstarted Jobs" AutoGenerateColumns="False" onrowdeleting="unstartedJobs_RowDeleting" >
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="position" HeaderText="Queue Position" />
                <asp:BoundField />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    
        <asp:Button ID="UnstartedButton" runat="server" OnClick="ViewUnstarted_Click" Text="View Unstarted" />
    
        <asp:Button ID="LogOutButton" runat="server" OnClick="LogOutButton_Click" Text="Log Out" />
    
    </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
