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
                <td id="5" class="auto-style5">
                    <asp:Label ID="DailyAvWaitLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Average Queue Length</td>
                <td id="tf2" class="auto-style5">
                    <asp:Label ID="DailyAvLengthLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Percentage of Time Empty</td>
                <td id="typeField3" class="auto-style5">
                    <asp:Label ID="DailyTimeEmptyLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Jobs Not Addressed Same Day</td>
                <td id="typeField4" class="auto-style5">
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
                <td id="4" class="auto-style5">
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
                <td id="typeField8" class="auto-style5">
                    <asp:Label ID="MonthlyTimeEmptyLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Jobs Not Addressed Same Day</td>
                <td id="typeField9" class="auto-style5">
                    <asp:Label ID="MonthlySameDayLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div class="row">
        <a href="#collapseDailyTech" class="btn btn-default collapseButton" data-toggle="collapse">Daily Technician Report</a>
    </div>

    <div id="collapseDailyTech" class ="collapse row">
        <asp:GridView ID="DailyTechnicianGrid" AutoPostBack="true" class="table table-hover" runat="server" Caption="Technician Stats" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="username" HeaderText="Name" />
                <asp:BoundField DataField="downTime" HeaderText="Down Time (Minutes)" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="row">
        <a href="#collapseMonthlyTech" class="btn btn-default collapseButton" data-toggle="collapse">Monthly Technician Report</a>
    </div>

    <div id="collapseMonthlyTech" class ="collapse row">
        <asp:GridView ID="MonthlyTechnicianGrid" AutoPostBack="true" class="table table-hover" runat="server" Caption="Technician Stats" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="username" HeaderText="Name" />
                <asp:BoundField DataField="downTime" HeaderText="Down Time (Minutes)" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="row">
        <a href="#collapseQueue" class="btn btn-default collapseButton" data-toggle="collapse">Job Queue</a>
    </div>

    <div id="collapseQueue" class ="collapse row">
        <asp:GridView ID="queueGrid"  DataKeyNames = "priority,enqueueTime" class="table table-hover" runat="server" Caption="Job Queue" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="baseEnqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="position" HeaderText="Queue Position" />
            </Columns>
        </asp:GridView>
    </div>

    <div runat="server" id="emptyQueueDiv" class="alert alert-warning" visible="true">
         Queue is empty
    </div>

    <div class="row">
        <a href="#collapseJobEdit" class="btn btn-default collapseButton" data-toggle="collapse">Change Job Position</a>
    </div>

    <div id="collapseJobEdit" class ="collapse row">
      <div class="form-group">
        <label class="control-label col-sm-2" for="jobId">Job ID</label>
        <div class="col-sm-10">
           <asp:TextBox ID="jobEditField" runat="server"></asp:TextBox>
        </div>
      </div>

      <div class="form-group">
        <label class="control-label col-sm-2" for="position">New Position</label>
        <div class="col-sm-10">
           <asp:TextBox ID="newPositionField" runat="server"></asp:TextBox>
        </div>
      </div>
      <div class="col-sm-12">
          <asp:Button ID="Button1" runat="server" class="btn btn-default" OnClick="JobEditButton_Click" Text="Change Job" />
      </div>
    </div>


<%--      for debugging  <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />--%>
    
    

        <p>
            &nbsp;</p>
</asp:Content>