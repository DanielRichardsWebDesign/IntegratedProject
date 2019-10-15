<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectionTest.aspx.cs" Inherits="ConnectionTest_v2.WebForm1" %>

<!DOCTYPE html>

<script runat="server">

    void Page_Load(Object sender, EventArgs e)
    {
        // Manually register the event-handling method for
        // the Click event of the Button control.
        Button1.Click += new EventHandler(this.ConnectDB);
        Button2.Click += new EventHandler(this.DisconnectDB);
    }

    //void GreetingBtn_Click(Object sender,
    //                       EventArgs e)
    //{
    //    ConnectDB();
    //}

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div>
      <h3>ASP Connection Test</h3>
      <p id="Display" runat="server"></p>  
      <asp:Button id="Button1"
           Text="Connect and Display"
           OnClick="ConnectDB" 
           runat="server"/>
      <br />
      <br />
      <asp:Button id="Button2"
           Text="Disconnect"
           OnClick="DisconnectDB" 
           runat="server"/>
    </div>
    </form>
</body>
</html>
