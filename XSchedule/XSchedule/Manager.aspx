<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/LoggedIn.Master" CodeFile="Manager.aspx.cs" Inherits="Manager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    
    <div runat="server" id="alertDiv1" class="alert alert-info" visible="false">
         
    </div>

    <div class="row">
        <a href="#collapseDaily" class="btn btn-default collapseButton" data-toggle="collapse">Daily Report</a>
    </div>

    <div id="collapseDaily" class ="collapse row">
        <table class="table table-hover">
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
    </div>
    
    <div class="row">
        <a href="#collapseMonthly" class="btn btn-default collapseButton" data-toggle="collapse">Monthly Report</a>
    </div>

    <div id="collapseMonthly" class ="collapse row">
        <table class="table table-hover">
            <caption>
                <asp:Label ID="MonthlyLabel" runat="server" Text="Label"></asp:Label>
            </caption>
            <tr>
                <td class="auto-style4">Average Wait</td>
                <td id="2" class="auto-style5">
                    <asp:Label ID="MonthlyAvWaitLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Average Queue Length</td>
                <td id="tf3" class="auto-style5">
                    <asp:Label ID="MonthlyAvLengthLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Percentage of Time Empty</td>
                <td id="typeField" class="auto-style5">
                    <asp:Label ID="MonthlyTimeEmptyLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Jobs Addressed Same Day</td>
                <td id="typeField" class="auto-style5">
                    <asp:Label ID="MonthlySameDayLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div class="row">
        <a href="#collapseDailyTech" class="btn btn-default collapseButton" data-toggle="collapse">Daily Technician Report</a>
    </div>

    <div id="collapseDailyTech">
        <asp:GridView ID="DailyTechnicianGrid" AutoPostBack="true" class="table table-hover" runat="server" Caption="Technician Stats" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="username" HeaderText="Name" />
                <asp:BoundField DataField="downTime" HeaderText="Down Time" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="row">
        <a href="#collapseMonthlyTech" class="btn btn-default collapseButton" data-toggle="collapse">Monthly Technician Report</a>
    </div>

    <div id="collapseMonthlyTech">
        <asp:GridView ID="MonthlyTechnicianGrid" AutoPostBack="true" class="table table-hover" runat="server" Caption="Technician Stats" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="username" HeaderText="Name" />
                <asp:BoundField DataField="downTime" HeaderText="Down Time" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="row">
        <a href="#collapseQueue" class="btn btn-default collapseButton" data-toggle="collapse">Job Queue</a>
    </div>

    <div id="collapseQueue">
        <asp:GridView ID="queueGrid"  class="table table-hover" runat="server" Caption="Job Queue" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="position" HeaderText="Queue Position" />
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>

    <div runat="server" id="emptyQueueDiv" class="alert alert-warning" visible="false">
         Queue is empty
    </div>



        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    
    

        <p>
            &nbsp;</p>
</asp:Content>