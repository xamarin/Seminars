<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>The Best Message Board Ever</title>
	<style>body { font-family: sans-serif; }
		.time { font-size: 80%; color: gray; }
		.from { font-weight: bold; }
	</style>
</head>
<body>
	<h1>The Best Message Board Ever</h1>
	<% using (Html.BeginForm ("New", "Messages")) { %>
	
	<p><label for="From">Name:</label> <%= Html.TextBox("From") %></p>
	<p><%= Html.TextArea("Text") %></p>
	<input type="submit" value="Submit" />
	<% } %>
	
	<h2>Messages</h2>
	<% foreach (var m in (List<MessageBoard.Message>)ViewData["Messages"]) { %>
	<div><span class="time"><%= Html.Encode (m.Time.ToLocalTime ().ToString ()) %></span> <span class="from"><%= Html.Encode (m.From) %></span> <%= Html.Encode (m.Text) %></div>
	<% } %>
</body>

