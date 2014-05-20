<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Book_See_sharp.Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Book en bus</title>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" rel="stylesheet">
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style>
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #eee;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="col-sm-4 col-sm-offset-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title">Logind</h1>
                </div>
                <div class="panel-body">
                    <form method="POST" onsubmit="submitLogin();return false;" id="loginForm">
                        <div class="form-group" id="usernameGroup">
                            <input class="form-control input-lg" type="text" id="username" name="username" placeholder="Brugernavn" autofocus>
                        </div>
                        <div class="form-group" id="passwordGroup">
                            <input class="form-control input-lg" type="password" id="password" name="password" placeholder="Kodeord">
                        </div>
                        <button type="submit" class="btn btn-success btn-lg">Godkend</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script src="/common.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#username").on('input', function () {
                jQuery("#username").val(jQuery("#username").val().toLowerCase());
            });
            jQuery("#password").bind('focusout keydown keypress keyup', function () {
                if (jQuery("#password").val().length > 0)
                    errorRemove("passwordGroup");
            });
        });
        function submitLogin() {
            var anError = false;
            var username = jQuery("#username").val();
            var password = jQuery("#password").val();


            // Cleanup
            errorRemove("usernameGroup");
            errorRemove("passwordGroup");


            if (username == "") {
                errorAdd("usernameGroup", "Brugernavn kan ikke v&aelig;re tomt");
                anError = true;
            }
            if (password == "") {
                errorAdd("passwordGroup", "Kodeord kan ikke v&aelig;re tomt");
                anError = true;
            }


            if (anError == false) {
                document.getElementById("loginForm").action = "Default.aspx";
                document.getElementById("loginForm").submit();
            }

            // Reset
            document.activeElement.blur();
        }
    </script>
</body>
</html>
