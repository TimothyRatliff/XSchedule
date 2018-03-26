<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Group members:</h3>
    <address>

        <asp:Table ID="ContactTable"
            runat="server" 
            Font-Size="X-Large" 
            Width="550" 
            Font-Names="Palatino"
            BackColor="Black"
            BorderColor="Black"
            BorderWidth="2"
            ForeColor="Snow"
            CellPadding="5"
            CellSpacing="5"
        >
            <asp:TableHeaderRow
                ForeColor="Snow"
                BackColor="Purple"
                Font-Bold="true"
            >
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell>GitHub</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Email</asp:TableHeaderCell>
            </asp:TableHeaderRow>

            <asp:TableRow 
                    ID="TableRow1" 
                    BackColor="Purple"
                    ForeColor="Gold"
                    >
                    <asp:TableCell>Sonia Azad</asp:TableCell>
                    <asp:TableCell><a href="https://github.com/sazad">sazad</a></asp:TableCell>
                    <asp:TableCell>sazad3@lsu.edu</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow 
                    ID="TableRow2" 
                    BackColor="Purple"
                    ForeColor="Gold"
                    >
                    <asp:TableCell>Timothy Ratliff</asp:TableCell>
                    <asp:TableCell><a href="https://github.com/TimothyRatliff">TimothyRatliff</a></asp:TableCell>
                    <asp:TableCell>tratli2@lsu.edu</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow 
                    ID="TableRow3" 
                    BackColor="Purple"
                    ForeColor="Gold"
                    >
                    <asp:TableCell>Joseph Nguyen</asp:TableCell>
                    <asp:TableCell><a href="https://github.com/jnguyen037">jnguyen037</a></asp:TableCell>
                    <asp:TableCell>jngu124@lsu.edu</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow 
                    ID="TableRow4" 
                    BackColor="Purple"
                    ForeColor="Gold"
                    >
                    <asp:TableCell>Mark Prutz</asp:TableCell>
                    <asp:TableCell><a href="https://github.com/Markp97">Markp97</a></asp:TableCell>
                    <asp:TableCell>mprutz2@lsu.edu</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow 
                    ID="TableRow5" 
                    BackColor="Purple"
                    ForeColor="Gold"
                    >
                    <asp:TableCell>Dylan Russell</asp:TableCell>
                    <asp:TableCell><a href="https://github.com/druss15">druss15</a></asp:TableCell>
                    <asp:TableCell>druss15@lsu.edu</asp:TableCell>
            </asp:TableRow>
        </asp:Table>



        <!--
        Sonia Azad <br />
        Timothy Ratliff <br />
        Dylan Russell <br />
        Joseph Nguyen <br />
        Mark Prutz <br />
        -->


    </address>

    <%--<address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>--%>
</asp:Content>
