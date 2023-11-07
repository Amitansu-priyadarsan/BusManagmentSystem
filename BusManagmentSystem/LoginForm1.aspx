<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm1.aspx.cs" Inherits="BusManagmentSystem.LoginForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bus Managment System</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <style>
        .login,
        .image {
            min-height: 100vh;
            background-color: rgba(64,64,64);
        }

        .logo {
            text-align: center;
            padding: 20px; 
            background-color:rgba(64,64,64);
        }

        .logo img {
            max-width: 200px; 
        }

        .box-container {
            background-color:whitesmoke;
            border-radius: 10px; 
            padding: 20px; 
        }

        .bg-image {
            background-image: url('../Image/image1.PNG');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            background-color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row no-gutter">
                <div class="col-md-6 d-none d-md-flex bg-image">             
                </div>
                <div class="col-md-6 bg-light">
                    <div class="login d-flex align-items-center py-5">
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-10 col-xl-8 mx-auto">
                                    <div class="logo">
                                        <img src="../Image/logo-1.png" alt="Company Logo">
                                    </div>
                                    <div class="box-container">
                                        <h3 class="display-4 pb-4 text-muted mb-4">Bus Portal</h3>
                                        <p class="text-muted mb-4 ">LOGIN PAGE</p>
                                        <div class="form-group mb-3">
                                            <input id="inputUsername" type="text" placeholder="UserName" required="required" runat="server" autofocus="autofocus" text="Amitansu Priyadarsan" class="form-control rounded-pill border-0 shadow-sm px-4" />
                                        </div>
                                        <div class="form-group mb-3">
                                            <input id="inputPassword" type="password" placeholder="Password" required="required" runat="server" text="123" class="form-control rounded-pill border-0 shadow-sm px-4" />
                                        </div>
                                        <asp:Button ID="btnlog" runat="server" Text="LogIn" class="btn btn-primary btn-block text-uppercase mb-4 rounded-pill shadow-sm" Style="background-color: #5558C9" OnClick="btnlog_Click" />
                                        <div class="text-center d-flex justify-content-between mt-4">
                                            <asp:Label ID="lb1Msg" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
