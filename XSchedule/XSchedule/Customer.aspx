<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/LoggedIn.Master" CodeFile="Customer.aspx.cs" Inherits="Customer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
     .alert{
          margin-top: 15px;
      }
     .panel panel-default{
        padding-bottom: 20px;
        margin-top: 20px;
     }
      .alert alert-info{
          margin-top: 15px;
      }
      .form-group {
        padding-bottom: 20px;
        margin-top: 20px;

      }

      .form-group {
        padding-bottom: 20px;
        margin-top: 20px;

      }

      .collapseButton {
        margin-top: 10px;
        margin-bottom: 10px;

      }

      .radios{
          width: 120px !important;
      }
/**/

    </style>
    <div runat="server" id="alertDiv1" class="alert alert-info" visible="false">
         
    </div>

    <div class="row">
        <a href="#collapseJob" class="btn btn-default collapseButton" data-toggle="collapse">Create a Job</a>
    </div>

    <div id="collapseJob" class ="collapse row">
        <div id="jobPanel" class="panel panel-default col-sm-6 text-center">
          <div class="panel-heading">Enter complexity and press submit</div>
             <div class="btn-group" GroupName="complexityButtons" data-toggle="buttons">
                <label class="btn btn-default radios active" >
                 <asp:RadioButton runat="server" value="1" id="radio1" Checked="true" GroupName="complexityButtons"  AutoPostBack="True"/>
                 1(Easiest)
                </label>
                <label class="btn btn-default radios">
                 <asp:RadioButton runat="server"  value="2" id="radio2" GroupName="complexityButtons" AutoPostBack="True"/>
                 2
                </label>
                <label class="btn btn-default radios">
                 <asp:RadioButton runat="server"  value="3" id="radio3" GroupName="complexityButtons" AutoPostBack="True"/>
                 3(Hardest)
                </label>
            </div>

            <div class="col-sm-12">
              <asp:Button ID="Button1" runat="server" class="btn btn-default" OnClick="SubmitButton_Click" Text="Create Job" />
            </div>

        </div>
    </div>

    <div class="row">
        <a href="#compDiv" class="btn btn-default collapseButton" data-toggle="collapse">View Completed Jobs</a>
     </div>

    <div class="collapse" id="compDiv">
        <asp:GridView ID="completedJobs" class="table table-hover" runat="server" Caption="Completed Jobs" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="checkedIn" HeaderText="Job Started" />
                <asp:BoundField DataField="dequeueTime" HeaderText="Finished" />
			    <asp:BoundField DataField="technician" HeaderText="Technician" />
            </Columns>
        </asp:GridView>
    </div>

    <div runat="server" id="noCompletedDiv" class="alert alert-warning" visible="false">
         No Jobs Completed
    </div>

     <div class="row">
        <a href="#startedDiv" class="btn btn-default collapseButton" data-toggle="collapse">View Started Jobs</a>
     </div>
    <div class="collapse" id="startedDiv">
        <asp:GridView ID="startedJobs" class="table table-hover" runat="server" Caption="Started Jobs" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="checkedIn" HeaderText="Job Started" />
                <asp:BoundField DataField="technician" HeaderText="Technician" />
            </Columns>
        </asp:GridView>
    </div>

    <div runat="server" id="noStartedDiv" class="alert alert-warning" visible="false">
         No Jobs Started
    </div>

     <div class="row">
        <a href="#unstartedDiv" class="btn btn-default collapseButton" data-toggle="collapse">View Unstarted Jobs</a>
     </div>    
     
    <div class="collapse" id="unstartedDiv">   
        <asp:GridView ID="unstartedJobs" class="table table-hover" runat="server" Caption="Unstarted Jobs" AutoGenerateColumns="False" onrowdeleting="unstartedJobs_RowDeleting" >
            <Columns>
                <asp:BoundField DataField="jobId" HeaderText="Job ID" />
                <asp:BoundField DataField="enqueueTime" HeaderText="Enqueue Time" />
			    <asp:BoundField DataField="position" HeaderText="Queue Position" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    <div runat="server" id="noUnstartedDiv" class="alert alert-warning" visible="false">
         No Jobs Unstarted
    </div>
        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    
    
        <p>
            &nbsp;</p>
</asp:Content>
