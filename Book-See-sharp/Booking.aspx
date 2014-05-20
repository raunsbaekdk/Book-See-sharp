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
                <a class="navbar-brand" href="/Booking.aspx">Book en bus</a>
            </div>
            <asp:Literal ID="loginLoggedin" runat="server" />
        </div>
    </div>
    <div class="container">
        <div id="message" class="alert hidden"></div>
        <div class="row">
            <div class="col-sm-4">
                <h2>Vælg bus</h2>
                <p>
                    <select id="busses" class="form-control">
                        <option value="NULL">---</option>
                    </select>
                </p>
                <p>
                    <button id="btnBusses" class="btn btn-default">Videre &raquo;</button>
                </p>
            </div>
            <div class="col-sm-4">
                <h2>Vælg fra</h2>
                <div class="row">
                    <div class="col-sm-6">
                        <input id="txtFromDate" type="date" class="form-control" placeholder="dd/mm-YYYY" disabled />
                    </div>
                    <div class="col-sm-6">
                        <input id="txtFromTime" type="time" class="form-control" placeholder="HH:mm" disabled />
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
                        <input id="txtToDate" type="date" class="form-control" placeholder="dd/mm-YYYY" disabled />
                    </div>
                    <div class="col-sm-6">
                        <input id="txtToTime" type="time" class="form-control" placeholder="HH:mm" disabled />
                    </div>
                </div>
                <p></p>
                <p>
                    <button id="btnTo" class="btn btn-success" disabled><i class="fa fa-apple"></i>&nbsp;&nbsp;Reserver</button>
                    &nbsp;
                        <button id="btnLeaveTo" class="btn btn-danger hidden">Afbryd</button>
                </p>
            </div>
        </div>
    </div>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script src="/js/common.js"></script>
    <script>
        jQuery(document).ready(function () {
            getAllBusses();

            jQuery("#btnBusses").click(function () {
                if (jQuery("#busses").val() != "NULL") {
                    // Disable busses
                    jQuery("#busses").attr("disabled", true);
                    jQuery("#btnBusses").attr("disabled", true);

                    // Enable from
                    jQuery("#txtFromDate").attr("disabled", false);
                    jQuery("#txtFromTime").attr("disabled", false);
                    jQuery("#btnFrom").attr("disabled", false);
                    jQuery("#btnLeaveFrom").removeClass("hidden");
                }
                return false;
            });


            jQuery("#btnFrom").click(function () {
                if (jQuery("#txtFromDate").val() != "" && jQuery("#txtFromTime").val() != "") {
                    // Disable from
                    jQuery("#txtFromDate").attr("disabled", true);
                    jQuery("#txtFromTime").attr("disabled", true);
                    jQuery("#btnFrom").attr("disabled", true);
                    jQuery("#btnLeaveFrom").addClass("hidden");

                    // Disable to
                    jQuery("#txtToDate").attr("disabled", false);
                    jQuery("#txtToTime").attr("disabled", false);
                    jQuery("#btnTo").attr("disabled", false);
                    jQuery("#btnLeaveTo").removeClass("hidden");
                }
                return false;
            });


            jQuery("#btnTo").click(function () {
                if (jQuery("#txtToDate").val() != "" && jQuery("#txtToTime").val() != "") {
                    // Disable to
                    jQuery("#txtToDate").attr("disabled", true);
                    jQuery("#txtToTime").attr("disabled", true);
                    jQuery("#btnTo").attr("disabled", true);
                    jQuery("#btnLeaveTo").addClass("hidden");


                    // Put message
                    addMessage("Jeep, et styks reservation, on the f* way!", "success", true);
                }
                return false;
            });
        });
        function getAllBusses() {
            jQuery.ajax({
                type: "GET",
                dataType: "json",
                url: "http://sum.gim.dk/api/bus/getAllBusses",
                success: function (b) {
                    jQuery.each(b, function (key, value) {
                        jQuery("#busses").append(jQuery("<option></option>").attr("value", key.RegNo).text(value.RegNo));
                    });
                }
            });
        }
    </script>
</body>
</html>
