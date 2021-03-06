﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Login.aspx.cs" Inherits="Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">


    <div runat="server" id="alertDiv" class="alert alert-danger" visible="false">
        Username or password invalid.
    </div>
    
      <div class="form-group">
        <label class="control-label col-sm-2" for="usernaim">Username:</label>
        <div class="col-sm-10">
          <asp:TextBox ID="usernameField" runat="server"></asp:TextBox>
        </div>
      </div>

      <div class="form-group">
        <label class="control-label col-sm-2" for="password">Password:</label>
        <div class="col-sm-10"> 
            <asp:TextBox ID="passwordField" TextMode="Password" runat="server"></asp:TextBox>
        </div>
      </div>
      

      <div class="form-group"> 
        <div class="col-sm-offset-2 col-sm-10">
          <asp:Button ID="Button1" runat="server" class="btn btn-default" OnClick="LogInButton_Click" Text="Login" />
        </div>
      </div>

    <div>
<%--      for debugging  <asp:GridView ID="testGV"  runat="server">
            <Columns>

            </Columns>
        </asp:GridView>
    
        <asp:Button ID="TestButton" runat="server" OnClick="Button1_Click" Text="Test" />--%>
    
    </div>
        <p>
            &nbsp;</p>
</asp:Content>
