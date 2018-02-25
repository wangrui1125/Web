<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkDaySet.aspx.cs" Inherits="MyQuery.Web.Sys.WorkDaySet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工作日设置</title>

    <script src="../Js/ClientValidate.js" type="text/javascript"></script>

    <script src="../Js/calendar.js" type="text/javascript"></script>

    <style type="text/css">
        th, td
        {
            padding: 3px;
        }
        .datatable
        {
            border: solid 1px #c1c1c1;
            width: 100%;
        }
        .datatable th
        {
            background-image: url(../Img/date.gif);
            background-repeat: repeat-x;
            background-position: top;
            border-right: solid 1px #c1c1c1;
            border-bottom: solid 1px #c1c1c1;
            height: 20px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .datatable td
        {
            border-right: solid 1px #c1c1c1;
            border-bottom: solid 1px #c1c1c1;
            height: 20px;
            text-align: center;
        }
        .nowork
        {
            color: #f69f4f;
            background-color: #fef8d6;
            background-image: none !important;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <legend>生成工作日</legend>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        起始日期
                    </th>
                    <td>
                        <asp:TextBoxVal ID="StartDate" runat="server" ClientValidate="r_date" ToolTip="请输入日期，格式:2008-08-08"></asp:TextBoxVal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择起始日期。"
                            ControlToValidate="StartDate" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <th>
                        结束日期
                    </th>
                    <td>
                        <asp:TextBoxVal ID="EndDate" runat="server" ClientValidate="r_date" ToolTip="请输入日期，格式:2008-08-08"></asp:TextBoxVal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择结束日期。"
                            ControlToValidate="EndDate" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="起始日期需小于结束日期。"
                            ControlToCompare="EndDate" ControlToValidate="StartDate" Operator="LessThanEqual"
                            SetFocusOnError="True" Type="Date" ForeColor="Red">*</asp:CompareValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnBuild" CssClass="input_button" runat="server" Text="生 成" OnClick="btnBuild_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <legend>工作日设置</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Calendar ID="calendarWorkDay" runat="server" DayNameFormat="Full" FirstDayOfWeek="Saturday"
                            BackColor="Transparent" CssClass="datatable" Font-Names="SimSun" ForeColor="Black"
                            Height="100%" Width="100%" CellPadding="4" OnDayRender="calendarWorkDay_DayRender"
                            OnSelectionChanged="calendarWorkDay_SelectionChanged" NextPrevFormat="ShortMonth">
                            <SelectedDayStyle Font-Bold="True" Wrap="True" BorderWidth="3px" ForeColor="Blue"
                                Font-Underline="False" BorderColor="Blue" />
                            <TodayDayStyle Font-Bold="True" BorderWidth="3px" ForeColor="Green" BorderColor="Green" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle ForeColor="Blue" />
                            <DayHeaderStyle BackColor="Transparent" Font-Bold="True" Height="1px" />
                            <TitleStyle BackColor="Transparent" Font-Bold="True" ForeColor="Black" />
                        </asp:Calendar>
                    </td>
                </tr>
            </table>
            <div class="operation">
                <asp:CheckBox ID="WorkDayState" runat="server" Text="工作日" Checked="false" />
                &nbsp;&nbsp;<asp:Button ID="btnSet" runat="server" Text="提 交" CssClass="input_button"
                    OnClick="btnSet_Click" CausesValidation="false" />
                &nbsp;&nbsp;<asp:Button ID="btnReturnCurrent" runat="server" CssClass="input_button"
                    Text="返回当前日期" OnClick="btnReturnCurrent_Click" CausesValidation="false" />
            </div>
        </fieldset>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
</html>
