<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Method_Tester._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Method Tester</title>
    <link href="style.css" rel="Stylesheet" type="text/css" />
    <style runat="server" id="inlineStyle" type="text/css"></style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="head"> 
        <h1>Method Tester(without Ajax)</h1>
    </div>
    <div id="content">
        <div id="properties">
            <h3>Add using these namespaces:</h3>
            <asp:TextBox runat="server" TextMode="MultiLine" Height="100px" ID="txtUsinglist">System</asp:TextBox>
            
        </div>
        <div id="code">
        
            <h3>Description:</h3>
            <asp:TextBox ID="txtDescription"  runat="server" TextMode="MultiLine" Width="100%" Height="100px"></asp:TextBox>
            <h3>
            Enter your main method here:</h3>
        
            <p>public static string
            &nbsp;
            
            <asp:TextBox ID="txtMethodname" runat="server"></asp:TextBox>
            (string args){</p>
        <p>
            <asp:TextBox ID="txtMainmethod" runat="server" Height="150px" Width="100%" 
                TextMode="MultiLine" Wrap="False"></asp:TextBox>
        </p>
        <p>
            }</p>
            <h3>Enter other code here:</h3>
            <asp:TextBox ID="txtOthercode" TextMode="MultiLine" runat="server" Width="100%" Height="100px"></asp:TextBox>
            <p><asp:Button ID="btnFormat" runat="server" Text="Format" PostBackUrl="result.aspx?action=format" />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" PostBackUrl="result.aspx" /></p>
        </div>
    </div>
    <div id="footer">
        <p>Developed by <a href="http://www.wanganran.com/" target="_blank">Anran</a>.</p>
    </div>
    </form>
</body>
</html>
