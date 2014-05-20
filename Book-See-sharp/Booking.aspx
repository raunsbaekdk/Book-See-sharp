<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="Book_See_sharp.Booking" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Book en bus</title>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" rel="stylesheet">
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet">
    <link href="/css/style.css" rel="stylesheet">
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">Book en bus</a>
            </div>
            <asp:Literal ID="loginLoggedin" runat="server" />
        </div>
    </div>
    <div class="container">
        <form runat="server">
            <asp:Literal ID="errMessage" runat="server" />
            <div class="row">
                <div class="col-sm-4">
                    <h2>Vælg bus</h2>
                    <p>
                        <select id="busser" class="form-control">
                            <option value="0">---</option>
                        </select>
                    </p>
                    <p>
                        <button id="btnBusser" class="btn btn-default">Videre &raquo;</button>
                    </p>
                </div>
                <div class="col-sm-4">
                    <h2>Vælg fra</h2>
                    <div class="row">
                        <div class="col-sm-6">
                            <input id="txtFromDate" type="date" class="form-control" placeholder="dd/mm-YYYY" />
                        </div>
                        <div class="col-sm-6">
                            <input id="txtFromTime" type="time" class="form-control" placeholder="HH:mm" />
                        </div>
                    </div>
                    <p></p>
                    <p>
                        <button id="btnFrom" class="btn btn-default" disabled>Videre &raquo;</button>
                        &nbsp;
                        <button id="btnLeaveFrom" class="btn btn-danger hidden">Afbryd</button>
                    </p>
                </div>
                <div class="col-sm-4">
                    <h2>Vælg til</h2>
                    <div class="row">
                        <div class="col-sm-6">
                            <input id="txtToDate" type="date" class="form-control" placeholder="dd/mm-YYYY" />
                        </div>
                        <div class="col-sm-6">
                            <input id="txtToTime" type="time" class="form-control" placeholder="HH:mm" />
                        </div>
                    </div>
                    <p></p>
                    <p>
                        <button id="btnTo" class="btn btn-success" disabled><i class="fa fa-apple"></i>&nbsp;&nbsp;Videre</button>
                        &nbsp;
                        <button id="btnLeaveTo" class="btn btn-danger hidden">Afbryd</button>
                    </p>
                </div>
            </div>
        </form>
    </div>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script src="/js/common.js"></script>
</body>
</html>