<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LoggedIn.Master" CodeFile="Technician.aspx.cs" Inherits="Technician" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <div runat="server" id="alertDiv1" class="alert alert-info" visible="false">
         
    </div>

    
    <div class="panel panel-default">
      <div class="panel-heading">Status</div>
      <div class="panel-body" runat="server" id="CurrentJobLabel">No current job</div>
    </div>
        
     <div class="form-group-horizontal"> 
        <div class="col-sm-offset-2 col-sm-10">
            <asp:Button ID="CheckInButton" class="btn btn-default" runat="server" OnClick="CheckInButton_Click" Text="Check In" />
            <asp:Button ID="CheckOutButton" class="btn btn-default" runat="server" OnClick="CheckOutButton_Click" Text="Check Out" />
        </div>
      </div>
        <p>

        </p>
        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <p>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    

    
        </p>

</asp:Content>
