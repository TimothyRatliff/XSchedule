<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
.carousel-inner > .item > img, .carousel-inner > .item > a > img {
    width: 100%;
    height: 330px;
}
    </style>
    <div class="jumbotron">
        <h1>XSchedule</h1>
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
                    </ol>

                    <div class="carousel-inner">
                        <div class="item active">
                            <a href="http://csharp-video-tutorials.blogspot.com/2016/08/bootstrap-image-carousel.html"> 
                                <img src="http://websoftwaredesigns.com/img/asp-net-web-hosting.jpg" class="img-responsive">
                            </a>
                        </div>
                        <div class="item" >
                            <a href="http://www.asp.net">
                                <img src="http://www.extrememicrotech.com/wp-content/uploads/2015/03/ASP-NET.jpg" class="img-responsive" >
                            </a>
                        </div>
                        <div class="item">
                            <a href="Login.aspx">
                                <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQsmSxgZZQOU7wq4Fio06-rru7G_6CgcwxB6XDC7ldQDBgIqVy4UA" class="img-responsive">
                            </a>
                        </div>
                        <div class="item">
                            <img src="https://image.freepik.com/free-vector/geometric-background-with-text-of-coming-soon_1017-5069.jpg" class="img-responsive">
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
