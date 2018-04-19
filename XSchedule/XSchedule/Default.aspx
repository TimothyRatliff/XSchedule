
﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="http://fonts.googleapis.com/css?family=Corben:bold" rel="stylesheet" type="text/css">
    <link href="http://fonts.googleapis.com/css?family=Nobile" rel="stylesheet" type="text/css">
    <style>
.carousel-inner > .item > img, .carousel-inner > .item > a > img {
    width: 100%;
    height: 330px;
}
h1,h2 {
    font-family: Impact;
}
.lead{
    font-family:'Lucida Sans Unicode';
}
    </style>
    <div class="jumbotron">
        <h1 style ="color:purple">XSchedule</h1> 
        <p class="lead">XSchedule is a work scheduling app, built by a team of computer science majors at Louisiana State University. It runs on ASP.NET, a web framework for building Web sites and Web applications.</p>

         <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div id="imageCarousel" class="carousel slide" data-interval="2000"
                     data-ride="carousel" data-pause="hover" data-wrap="true">

                    <ol class="carousel-indicators">
                        <li data-target="#imageCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#imageCarousel" data-slide-to="1"></li>
                        <li data-target="#imageCarousel" data-slide-to="2"></li>
                        <li data-target="#imageCarousel" data-slide-to="3"></li>
                        <li data-target="#imageCarousel" data-slide-to="4"></li>
                        <li data-target="#imageCarousel" data-slide-to="5"></li>
                        <li data-target="#imageCarousel" data-slide-to="6"></li>
                    </ol>

                    <div class="carousel-inner">
                        <div class="item active">
                            <a href="http://www.asp.net">
                                <img src="http://websoftwaredesigns.com/img/asp-net-web-hosting.jpg" class="img-responsive">
                            </a>
                        </div>
                        <div class="item" >
                            <a href="Register.aspx">
                                <img src="https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX18416041.jpg" class="img-responsive" >
                            </a>
                        </div>
                        <div class="item" >
                            <a href="Register.aspx">
                                <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSeNVX-bcufBh8kOFBSjXfakf9b-gvegvNOvaEHz-h8vXjQkC04FA" class="img-responsive" >
                            </a>
                        </div>
                        <div class="item" >
                            <a href="Register.aspx">
                                <img src="https://golf.kaleisure.com/wp-content/uploads/2017/02/signup.png" class="img-responsive" >
                            </a>
                        </div>
                        <div class="item">
                            <a href="Login.aspx">
                                <img src="https://www.odoo.com/apps/icon_image?module_id=19186" class="img-responsive">
                            </a>
                        </div>
                        <div class="item">
                            <img src="https://gsb.net.in/wp-content/uploads/2016/10/commingsoon-497x330.jpg" class="img-responsive">
                        </div>
                        <div class="item">
                            <a href="Contact.aspx"> 
                            <img src="http://ericabrownpromotions.com/wp-content/uploads/2017/07/contact-us.jpg" class="img-responsive">
                            </a>
                        </div>                       
                    </div>

                    <a href="#imageCarousel" class="carousel-control left" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                    </a>
                    <a href="#imageCarousel" class="carousel-control right" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                    </a>
                </div>

            </div>
        </div>
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    </div>

    
    <div class="row">
        <div class="col-md-4">
            <h2 style ="color:purple">GitHub </h2>
            <p class="lead">
                Check out XSchedule's GitHub repository.
            </p>
            <p>
                <a class="btn btn-default" href="https://github.com/TimothyRatliff/XSchedule">Code awaits! &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2 style ="color:purple">Elastic Beanstalk</h2>
            <p class="lead">
                XSchedule is hosted entirely on Amazon Web Services!
            </p>
            <p>
                <a class="btn btn-default" href="https://aws.amazon.com/elasticbeanstalk/">The cloud awaits! &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2 style ="color:purple">Project Wiki</h2>
            <p class="lead">
                Want to learn more about how XSchedule works?
            </p>
            <p>
                <a class="btn btn-default" href="https://github.com/TimothyRatliff/XSchedule/wiki">Knowledge awaits! &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>