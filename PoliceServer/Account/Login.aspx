<%@ Page Title="ورود" Language="C#" CodeBehind="Login.aspx.cs" Inherits="PoliceServer.Account.Login" %>

<html lang="en" class="no-js">
	<!-- start: HEAD -->
	<head>
		<title>نرم افزار مرکزی دستبند هوشمند</title>
		<!-- start: META -->
		<meta charset="utf-8" />
		<!--[if IE]><meta http-equiv='X-UA-Compatible' content="IE=edge,IE=9,IE=8,chrome=1" /><![endif]-->
		<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta content="" name="description" />
		<meta content="" name="author" />
    
		<!-- end: META -->
		<!-- start: MAIN CSS -->
        <link rel="shortcut icon" href="~/Content/images/NzLogo.png"/>
		<link href="../Content/ClipOnePlugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" media="screen">
		<link rel="stylesheet" href="../Content/ClipOnePlugins/font-awesome/css/font-awesome.min.css">
		<link rel="stylesheet" href="assets/fonts/style.css">
		<link rel="stylesheet" href="../Content/ClipOneCss/main.css">
		<link rel="stylesheet" href="../Content/ClipOneCss/main-responsive.css">
		<link rel="stylesheet" href="../Content/ClipOnePlugins/iCheck/skins/all.css">
		<link rel="stylesheet" href="../Content/ClipOnePlugins/perfect-scrollbar/src/perfect-scrollbar.css">
		<link rel="stylesheet" href="../Content/ClipOneCss/theme_light.css" id="skin_color">
        <link rel="stylesheet" href="../Content/ClipOneCss/rtl-version.css" />
		<!--[if IE 7]>
		<link rel="stylesheet" href="../Content/ClipOnePlugins/font-awesome/css/font-awesome.min.css">
		<![endif]-->
		<!-- end: MAIN CSS -->
		<!-- start: CSS REQUIRED FOR THIS PAGE ONLY -->
		<!-- end: CSS REQUIRED FOR THIS PAGE ONLY -->
	</head>
	<!-- end: HEAD -->
	<!-- start: BODY -->
	<body class="login example1">
		<div class="main-login col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3">
			<div class="logo">نرم افزار مرکزی دستبند هوشمند
			</div>
			<!-- start: LOGIN BOX -->
			<div class="box-login">
				<h3>ورود</h3>
				<p>
					برای ورود شماره ملی و رمز عبور خود را وارد نمایید.
				</p>
				<form method="post" action="#">
					<div runat="server" id="ErrorHandlerPanel" class="errorHandler alert alert-danger" Visible="False">
						<i class="icon-remove-sign"></i> <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
					</div>
					<fieldset>
						<div class="form-group">
							<span class="input-icon">
                                <input type="text" class="form-control" name="username" placeholder="کد ملی">
								<i class="icon-user"></i> </span>
							<!-- To mark the incorrectly filled input, you must add the class "error" to the input -->
							<!-- example: <input type="text" class="login error" name="login" value="Username" /> -->
						</div>
						<div class="form-group form-actions">
							<span class="input-icon">
								<input type="password" class="form-control" name="password" placeholder="رمز عبور">
								<i class="icon-lock"></i>
								 </span>
						</div>
                        <a href="#" class="hidden">
									رمز عبورم را فراموش کرده‌ام
								</a>
						<div class="form-actions">
							<button type="submit" class="btn btn-bricky pull-left">
							    ورود <i class="icon-circle-arrow-left"></i>
							</button>
						</div>
						</fieldset>
				</form>
			</div>
			<!-- end: LOGIN BOX -->
			<!-- start: COPYRIGHT -->
			<div class="copyright">
				علیرضا احمدیان افشار
			</div>
			<!-- end: COPYRIGHT -->
		</div>
		<!-- start: MAIN JAVASCRIPTS -->
		<!--[if lt IE 9]>
		<script src="../Content/ClipOnePlugins/respond.min.js"></script>
		<script src="../Content/ClipOnePlugins/excanvas.min.js"></script>
		<![endif]-->
		<script src="../../ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
		<script src="../Content/ClipOnePlugins/jquery-ui/jquery-ui-1.10.2.custom.min.js"></script>
		<script src="../Content/ClipOnePlugins/bootstrap/js/bootstrap.min.js"></script>
		<script src="../Content/ClipOnePlugins/blockUI/jquery.blockUI.js"></script>
		<script src="../Content/ClipOnePlugins/iCheck/jquery.icheck.min.js"></script>
		<script src="../Content/ClipOnePlugins/perfect-scrollbar/src/jquery.mousewheel.js"></script>
		<script src="../Content/ClipOnePlugins/perfect-scrollbar/src/perfect-scrollbar.js"></script>
		<script src="../Scripts/ClipOneJs/main.js"></script>
		<!-- end: MAIN JAVASCRIPTS -->
		<!-- start: JAVASCRIPTS REQUIRED FOR THIS PAGE ONLY -->
		<script src="../Content/ClipOnePlugins/jquery-validation/dist/jquery.validate.min.js"></script>
		<script src="../Scripts/ClipOneJs/login.js"></script>
		<!-- end: JAVASCRIPTS REQUIRED FOR THIS PAGE ONLY -->
		<script>
		    jQuery(document).ready(function () {
		        Main.init();
		        Login.init();
		    });
		</script>
	</body>
	<!-- end: BODY -->
</html>