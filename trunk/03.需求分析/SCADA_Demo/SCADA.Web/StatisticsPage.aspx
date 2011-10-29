<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsPage.aspx.cs" Inherits="SCADA.Web.StatisticsPage" %>
<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #imgdev {
            width: 1024;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      选择条件：
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>2011</asp:ListItem>
            <asp:ListItem>2010</asp:ListItem>
            <asp:ListItem>2009</asp:ListItem>
            <asp:ListItem>2008</asp:ListItem>
            <asp:ListItem>2007</asp:ListItem>
            <asp:ListItem>2006</asp:ListItem>
        </asp:DropDownList>
        年<asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
        月&nbsp;&nbsp;  
        <asp:Button ID="Button1" runat="server" Text="统计" />
    </div>
    <div>
    
    <img src="Images/analysis.PNG" id="imgdev" />

    <%-- <DCWC:Chart ID="Chart1" runat="server" Width="1200px">
        <legends>
            <DCWC:Legend Name="Default">
            </DCWC:Legend>
        </legends>
        <series>
            <DCWC:Series Name="Default">
            </DCWC:Series>
        </series>
        <chartareas>
            <DCWC:ChartArea Name="Default">
            </DCWC:ChartArea>
        </chartareas>
    </DCWC:Chart>
    --%>
    </div>
    
    </form>
</body>
</html>
