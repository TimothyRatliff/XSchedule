<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LoggedIn.Master"
    CodeFile="Technician.aspx.cs" Inherits="Technician" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div runat="server" id="alertDiv1" class="alert alert-info" visible="false">
    </div>


    <div class="panel panel-default">
        <div class="panel-heading">Status</div>
        <div class="panel-body" runat="server" id="CurrentJobLabel">No current job</div>
    </div>
    <div>
        <asp:Table ID="billtable" class="table table-hover" runat="server" GridLines="Both" Height="145px" 
            Width="200px" BorderWidth="1px" Caption="Bill Generated" CellPadding="5">
            <asp:TableRow runat="server" HorizontalAlign="Left">
                <asp:TableCell runat="server">Hours Billed</asp:TableCell>
                <asp:TableCell ID="hoursbilled" runat="server" HorizontalAlign="Right"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" HorizontalAlign="Left">
                <asp:TableCell runat="server">Pay Rate</asp:TableCell>
                <asp:TableCell ID="payrate" runat="server" HorizontalAlign="Right"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" BorderColor="Black" HorizontalAlign="Left" 
                TableSection="TableFooter">
                <asp:TableCell runat="server">Total Bill</asp:TableCell>
                <asp:TableCell ID="totalbill" runat="server" HorizontalAlign="Right"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div><p>
    </p><p>
    </p>
        </div>
    </div>
    <div class="form-group-horizontal">
        <div class="=col-sm-10">
            <asp:Button ID="CheckInButton" class="btn btn-default" runat="server" OnClick="CheckInButton_Click"
                Text="Check In" />
            <asp:Button ID="CheckOutButton" class="btn btn-default" runat="server" OnClick="CheckOutButton_Click"
                Text="Check Out" />
        </div>
    </div>
    <p>
    </p>
   
    <%--      for debugging  <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />--%>

    </p>
</asp:Content>
