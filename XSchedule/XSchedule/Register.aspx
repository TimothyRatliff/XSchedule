<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Register.aspx.cs" Inherits="Register" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    
    <div runat="server" id="alertDiv1" class="alert alert-danger" visible="false">
        Username taken
    </div>
    <div runat="server" id="alertDiv2" class="alert alert-danger" visible="false">
        Fill out both fields
    </div>
    
      <div class="form-group">
        <label class="control-label col-sm-2" for="username">Username:</label>
        <div class="col-sm-10">
          <asp:TextBox ID="usernameField" runat="server"></asp:TextBox>
        </div>
      </div>

      <div class="form-group">
        <label class="control-label col-sm-2" for="password">Password:</label>
        <div class="col-sm-10">
          <asp:TextBox ID="passwordField" runat="server"></asp:TextBox>
        </div>
      </div>
      
      <div class="form-group">
        <label class="control-label col-sm-2" for="type">Type:</label>
        <div class="col-sm-10">
          <asp:DropDownList ID="optionField" CssClass="form-control" runat="server">
                        <asp:ListItem Value="0">Customer</asp:ListItem>
                        <asp:ListItem Value="1">Technician</asp:ListItem>
                        <asp:ListItem Value="2">Manager</asp:ListItem>
           </asp:DropDownList>
        </div>
      </div>

      <div class="form-group"> 
        <div class="col-sm-offset-2 col-sm-10">
          <asp:Button ID="Button1" runat="server" class="btn btn-default" OnClick="SubmitButton_Click" Text="Register" />
        </div>
      </div>

       
        <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />
    
        <p>
            &nbsp;</p>
</asp:Content>
