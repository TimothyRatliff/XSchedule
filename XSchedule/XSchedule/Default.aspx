<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>XSchedule</h1>
        <p class="lead">XSchedule is a work scheduling app, built by a team of computer science majors at Louisiana State University. It runs on ASP.NET, a web framework for building Web sites and Web applications.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more about ASP.NET &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>GitHub </h2>
            <p>
                Check out XSchedule's GitHub repository.
            </p>
            <p>
                <a class="btn btn-default" href="https://github.com/TimothyRatliff/XSchedule">Code awaits! &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Elastic Beanstalk</h2>
            <p>
                XSchedule is hosted entirely on Amazon Web Services!
            </p>
            <p>
                <a class="btn btn-default" href="https://aws.amazon.com/elasticbeanstalk/">The cloud awaits! &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Project Wiki</h2>
            <p>
                Want to learn more about how XSchedule works?
            </p>
            <p>
                <a class="btn btn-default" href="https://github.com/TimothyRatliff/XSchedule/wiki">Knowledge awaits! &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
