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
    <link href="css/style.css" rel="stylesheet">
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
                <a class="navbar-brand" href="booking.aspx">Book en bus</a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="#calendar">Adminpanel</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container">
        <div id="message" class="hidden"></div>
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
        <div class="page-header">
            <h1>Kalender</h1>
        </div>
        <div class="row">
            <div class="col-sm-2">
                <h5>Vælg bus</h5>
                <p>
                    <select id="calendarBus" class="form-control">
                        <option value="NULL">---</option>
                    </select>
                </p>
            </div>
            <div class="col-sm-2">
                <h5>Vælg dag</h5>
                <p>
                    <input id="calendarDate" type="date" class="form-control" placeholder="dd/mm-YYYY" />
                </p>
            </div>
        </div>
        <table class="table table-striped hidden" id="calendar">
            <thead>
                <th>Bus</th>
                <th>Fra</th>
                <th>Til</th>
                <th>Navn</th>
                <th>Telefon</th>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div id="calendarMessage" class="alert-danger hidden"></div>
    </div>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script src="js/common.js"></script>
    <script>
        jQuery(document).ready(function () {
            getAllBusses();


            jQuery("#calendarBus").change(function () {
                if (this.value != "") {
                    loadCalendar(this.value, jQuery("#calendarDate").val());
                }
            });
            jQuery("#calendarDate").change(function () {
                if (this.value != "" && jQuery("#calendarBus").val() != "") {
                    loadCalendar(jQuery("#calendarBus").val(), this.value);
                }
            });


            jQuery("#btnBusses").click(function () {
                if (jQuery("#busses").val() != "NULL") {
                    // Disable busses
                    section("busses", true);

                    // Enable from
                    section("from", false);
                }
                return false;
            });


            jQuery("#btnFrom").click(function () {
                if (jQuery("#txtFromDate").val() != "" && jQuery("#txtFromTime").val() != "") {
                    // Disable from
                    section("from", true);

                    // Enable to
                    section("to", false);
                }
                return false;
            });


            jQuery("#btnTo").click(function () {
                if (jQuery("#txtToDate").val() != "" && jQuery("#txtToTime").val() != "") {
                    var startDate = new Date(jQuery("#txtFromDate").val() + " " + jQuery("#txtFromTime").val()).getTime() / 1000;
                    var endDate = new Date(jQuery("#txtToDate").val() + " " + jQuery("#txtToTime").val()).getTime() / 1000;

                    if (endDate < startDate) {
                        // Put message
                        addMessage("message", "Hovsa, du kan ikke aflevere bussen før du har taget den. Til dato er nyere end fra dato.", "danger", true);
                    } else {
                        // Disable to
                        section("to", true);

                        // Place reservation
                        placeReservation(jQuery("#busses").val(), jQuery("#txtFromDate").val(), jQuery("#txtFromTime").val(), jQuery("#txtToDate").val(), jQuery("#txtToTime").val());
                    }
                }
                return false;
            });


            jQuery("#btnLeaveFrom").click(function () {
                section("from", true);
                jQuery("#txtFromDate").val("");
                jQuery("#txtFromTime").val("");
                section("busses", false);
            });

            jQuery("#btnLeaveTo").click(function () {
                section("to", true);
                jQuery("#txtToDate").val("");
                jQuery("#txtToTime").val("");
                section("from", false);
            });
        });
        function section(section, disabled) {
            if (section == "busses") {
                jQuery("#busses").attr("disabled", disabled);
                jQuery("#btnBusses").attr("disabled", disabled);
            }
            if (section == "from") {
                jQuery("#txtFromDate").attr("disabled", disabled);
                jQuery("#txtFromTime").attr("disabled", disabled);
                jQuery("#btnFrom").attr("disabled", disabled);
                if (disabled == false)
                    jQuery("#btnLeaveFrom").removeClass("hidden");
                else
                    jQuery("#btnLeaveFrom").addClass("hidden");
            }
            if (section == "to") {
                jQuery("#txtToDate").attr("disabled", disabled);
                jQuery("#txtToTime").attr("disabled", disabled);
                jQuery("#btnTo").attr("disabled", disabled);
                if (disabled == false)
                    jQuery("#btnLeaveTo").removeClass("hidden");
                else
                    jQuery("#btnLeaveTo").addClass("hidden");
            }

            // Remove message
            removeMessage();
        }
        function insertIntoCalendar(value) {
            jQuery("#calendar > tbody").append("<tr> <td>" + value.Bus.RegNo + "</td><td>" + value.FromDate + "</td><td>" + value.ToDate + "</td><td>" + value.User.Name + "</td><td>" + value.User.Mobile + "</td> </tr>");
        }
        function loadCalendar(regNo, date) {
            jQuery("#calendar > tbody").empty();


            var url = "http://sum.gim.dk/api/reservation/GetBusReservation?regNo=" + regNo;
            if (typeof (date) !== undefined)
                url += "&date=" + date;


            jQuery.ajax({
                type: "GET",
                dataType: "json",
                url: url,
                success: function (b) {
                    jQuery.each(b, function (key, value) {
                        insertIntoCalendar(value);
                    });

                    if (jQuery("#calendar > tbody > tr").length == 0) {
                        jQuery("#calendar").addClass("hidden");
                        addMessage("calendarMessage", "Der blev desværre ikke fundet nogle reservationer.", "danger", false);
                    } else {
                        jQuery("#calendarMessage").addClass("hidden");
                        jQuery("#calendar").removeClass("hidden");
                    }
                }
            });
        }
        function getBusOptions(value) {
            var options = "";
            jQuery.each(value, function (key, value) {
                options += "<option value=\"" + value.RegNo + "\">" + value.RegNo + "</option>";
            });
            return options;
        }
        function getAllBusses() {
            jQuery.ajax({
                type: "GET",
                dataType: "json",
                url: "http://sum.gim.dk/api/comcenter/getallcomcenters",
                success: function (b) {
                    jQuery.each(b, function (key, value) {
                        jQuery("#busses").append(jQuery("<optgroup></optgroup>").attr("label", value.Name).
							html(
								getBusOptions(value.Busses)
							)
						);
                    });

                    // Populate calendar
                    jQuery("#calendarBus").html(jQuery("#busses").html());
                }
            });
        }
        function placeReservation(regNo, fromDate, fromTime, toDate, toTime) {
            jQuery.ajax({
                type: "POST",
                dataType: "json",
                data: "username=20662541&busId=" + regNo + "&fromDate=" + fromDate + "T" + fromTime + "&toDate=" + toDate + "T" + toTime,
                url: "http://sum.gim.dk/api/reservation/postreservation",
                success: function (b) {
                    if (b.status == "success") {
                        if (b.success == "CREATED") {
                            // Put message
                            addMessage("message", "Reservationen er blevet foretaget. Du kan se den i kalenderen.", "success", true);

                            // Insert into calendar
                            jQuery("#calendar").removeClass("hidden");
                            insertIntoCalendar(b.reservation);
                        }
                    }
                    if (b.status == "error") {
                        if (b.error == "DUPLICATE") {
                            // Enable until from date
                            section("to", true);
                            section("from", false);

                            // Put message
                            addMessage("message", "Det var ikke muligt at reservere, fordi der allerede findes en reservation på det pågældende tidspunkt.", "danger", true);

                            // Empty fields
                            jQuery("#txtFromDate").val("");
                            jQuery("#txtFromTime").val("");
                            jQuery("#txtToDate").val("");
                            jQuery("#txtToTime").val("");
                        }
                    }
                }
            });
        }
    </script>
</body>
</html>
