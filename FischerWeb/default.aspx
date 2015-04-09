<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FischerWeb._default" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
	<script src="Script/Cookies.js"></script>

	<script>

		//-------------------------------------------------------------------------------------------------------------------------------------------

		$(function() {
			$('#CallExternalWebAPI').click( function () { OnCallExternalWebAPI() });
			$('#CallWebAPI').click( function () { OnCallWebAPI() });
			$('#Login').click( function () { OnLogin() });
		});

		//-------------------------------------------------------------------------------------------------------------------------------------------

		function OnCallExternalWebAPI () {

			var params = {
				'key':	$('#Key').val(),
				'poNumber':	'91234567'
			}

			$.ajax({
				type: 'GET',
				url: 'api/fischerwebapi/GetPO',
				data: params,
				dataType: 'json'
			})
			.done( function(data) {
				$('#ExternalResult').html(data);
			})
			.fail( function(data) {
				alert(data.statusText);
			});
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		function OnCallWebAPI () {

			var params = {
				'key':	$('#Key').val()
			}

			$.ajax({
				type: 'GET',
				url: 'api/fischerwebapi/ValidateKey',
				data: params,
				dataType: 'json'
			})
			.done( function(data) {
				$('#CallWebAPIResult').html(data);
			})
			.fail( function(data) {
				alert(data.statusText);
			});
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		function OnLogin () {

			var params = {
				'user':		$('#User').val(),
				'password':	$('#Password').val()
			}

			$.ajax({
				type: 'GET',
				url: 'api/fischerwebapi/Login',
				data: params,
				dataType: 'json'
			})
			.done( function(data) {
				docCookies.setItem("key", data)
				$('#Key').val(data);
			})
			.fail( function(data) {
				alert(data.statusText);
			});
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

	</script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		SSL Connection Established
    </div>

	<br />
	<br />

	<div>
		<label>User:</label>
		<input id="User" type="text" value="Fischer" />
		<label>Password:</label>
		<input id="Password" type="text" value="Homes" />
		<input id="Login" type="button" value="Login" />
		<label>Key:</label>
		<input id="Key" type="text" />
	</div>

	<br />
	<br />

	<div>
		<input id="CallWebAPI" type="button" value="Validate Key" />
		<label>Validation Result: </label>
		<label id="CallWebAPIResult"></label>
	</div>

	<br />
	<br />

	<div>
		<input id="CallExternalWebAPI" type="button" value="Call External Web Site" />
		<label>External Result: </label>
		<label id="ExternalResult"></label>
	</div>
    </form>
</body>
</html>
