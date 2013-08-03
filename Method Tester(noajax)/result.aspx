<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="result.aspx.cs" Inherits="Method_Tester.result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Program result</title>
    <style type="text/css">
        .result
        {
        	font-family:Consolas, Monospace;
        	margin-right:300px;
        	
        	padding:10px 5px 10px 5px;
        	
        }
        .msg
        {
        	position:fixed;
        	width:270px;
        	top:5px;
        	right:5px;
        	
        	padding:5px;
        	border:solid 1px black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="result">
    <p><a href="javascript:history.back(-1)">Back</a></p>
    <p><asp:Literal ID="lblFlag" Text="" runat="server"></asp:Literal></p>
        <p>Here are the result(s) of the program 
            <asp:Label ID="lblProgname" runat="server" Text=""></asp:Label>:</p>
        <div id="divResult" class="result" runat="server">asd</div>
        <div id="divMessages" class="msg" runat="server">asd</div>
    </div>
    </form>
</body>
</html>
