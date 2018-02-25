function getkeyCode(evt) {
    evt = evt ? evt : (window.event ? window.event : null);
    if (evt.charCode) {
        return String.fromCharCode(evt.charCode);
    } else {
        return String.fromCharCode(evt.keyCode);
    }
}
function getStrInput(obj, inputStr) {
    var docSel = document.selection.createRange();
    oSel = docSel.duplicate();
    oSel.text = "";
    var srcRange = obj.createTextRange();
    oSel.setEndPoint("StartToStart", srcRange);
    return oSel.text + inputStr + srcRange.text.substr(oSel.text.length);
}
function validateKey(obj, isUseTime, inputStr) {
    var str = "";
    try {
        str = getStrInput(obj, inputStr);
    } catch (e) {
        str = trim(obj.value + inputStr);
    }
    if (isUseTime) { return /^[ \:\-\d]*$/.test(str); }
    else { return /^[\-\d]*$/.test(str); }
}
function validateCode(obj, inputStr) {
    var str = "";
    try {
        str = getStrInput(obj, inputStr);
    } catch (e) {
        str = trim(obj.value + inputStr);
    }
    return /^[0-9a-zA-Z\_\-\.\[\]:\/\?\=\&]*$/.test(str);
}
function validateCode2(obj, inputStr) {
    var str = "";
    try {
        str = getStrInput(obj, inputStr);
    } catch (e) {
        str = trim(obj.value + inputStr);
    }
    return /^[0-9a-zA-Z \-\+\*\/\_\.\,\:\;\=\!\@\$\&\?\[\]\{\}\#\%\(\)\~]*$/.test(str);
}
function validateDouble(obj, inputStr) {
    var str = "";
    try {
        str = getStrInput(obj, inputStr);
    } catch (e) {
        str = trim(obj.value + inputStr);
    }
    return /^[\+\-]?\d*\.?\d*$/.test(str);
}
function validateDouble2(obj, inputStr) {
    var str = "";
    try {
        str = getStrInput(obj, inputStr);
    } catch (e) {
        str = trim(obj.value + inputStr);
    }
    return /^[\+\-]?\d*\.?\d{0,2}$/.test(str);
}
function validateInt(obj, inputStr) {
    var str = "";
    try {
        str = getStrInput(obj, inputStr);
    } catch (e) {
        str = trim(obj.value + inputStr);
    }
    return /^\d*$/.test(str);
}
function validateDate(obj, isUseTime) {
    var str = trim(obj.value);
    if (str == "") { return true; }
    var isOk = false;
    if (isUseTime) {
        isOk = /^((((1[6-9]|[2-9]\d)\d{2})[-](0?[13578]|1[02])[-](0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})[-](0?[13456789]|1[012])[-](0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})[-]0?2[-](0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))\s([0-1]{1}\d{1}|[2]{1}[0-3]{1}):([0-5]{1}\d{1}):([0-5]{1}\d{1})$/.test(str);
    }
    else {
        isOk = /^((((1[6-9]|[2-9]\d)\d{2})[-](0?[13578]|1[02])[-](0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})[-](0?[13456789]|1[012])[-](0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})[-]0?2[-](0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$/.test(str);
    }
    if (isOk) { return true; }
    else {
        alert("请输入正确的日期值，如：2008-8-8");
        obj.select();
        return false;
    }
}
function dateCompare(startdate, enddate) {
    var arr = startdate.split("-");
    var starttime = new Date(arr[0], arr[1], arr[2]);

    var arrs = enddate.value.split("-");
    var lktime = new Date(arrs[0], arrs[1], arrs[2]);

    if (starttime.getTime() > lktime.getTime()) {
        alert("结束时间不能早于开始时间");
        enddate.focus();
        return false;
    }
    else {
        return true;
    }
}
function getBrowser() {
    //判断浏览器
    var Sys = {};
    try {
        var ua = navigator.userAgent.toLowerCase();
        var s;
        (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
        (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
        (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
        (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
        (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
    } catch (e) { }
    return Sys;
}
function openWindow(url, width, height) {
    if (!width) {
        width = parseInt(window.screen.availWidth, 10);
    }
    if (!height) {
        height = parseInt(window.screen.availHeight, 10);
    }
    var top = (parseInt(window.screen.availHeight, 10) - height) / 2;
    var left = (parseInt(window.screen.availWidth, 10) - width) / 2;
    var activeModalWin = window.open(getUrl(url), "", "height=" + height + "px,width=" + width + "px,top=" + top + "px,left=" + left + "px,center=yes,resizable=yes,menubar=no,toolbar=no,status=no,scrollbars=yes,location=no");
    if (url.indexOf("&m=d") != -1 && activeModalWin) {
        window.onfocus = function () {
            if (activeModalWin) {
                if (activeModalWin.closed) {
                    window.location.href = document.location.href;
                } else {
                    activeModalWin.focus();
                }
            }
        }
    }
}
function openDialog(url, width, height) {
    var Sys = getBrowser();
    if (!width) {
        width = parseInt(window.screen.availWidth, 10) - 88;
    }
    if (!height) {
        height = parseInt(window.screen.availHeight, 10);
    }
    if (Sys.opera || Sys.chrome) {
        openWindow(getUrl(url) + "&m=d", width, height + 20);
    } else {
        if (Sys.firefox || (Sys.ie && Sys.ie > 6)) {
            height += 30;
        }
        var top = (parseInt(window.screen.availHeight, 10) - height) / 2;
        var left = (parseInt(window.screen.availWidth, 10) - width) / 2;
        return window.showModalDialog(getUrl(url) + "&m=d", window, "dialogWidth:" + width + "px;" + "dialogHeight:" + height + "px;left:" + left + "px;top:" + top + "px;center:yes;resizable:yes;status:no;menubar:no;scrollbars:no;titlebar:no;toolbar:no;help:no;location=no;edge:Raised");
    }
}
function openDialogRefresh(url, width, height) {
    if (openDialog(url, width, height)) {
        window.location.href = document.location.href;
    }
}
function openDialogByArguments(url, arguments, width, height) {
    var top = (parseInt(window.screen.availHeight, 10) - height) / 2;
    var left = (parseInt(window.screen.availWidth, 10) - width) / 2;
    return window.showModalDialog(getUrl(url) + "&m=d", arguments, "dialogWidth:" + width + "px;" + "dialogHeight:" + height + "px;resizable:1;status:0;menubar:0;scrollbars:1;titlebar:0;toolbar:0;center:1;help:0;left:" + left + "px;top:" + top + "px;");
}

function redirect(url) {
    //alert(1111);
    window.location.href = getUrl(url);
    
}
function getUrl(url) {
    if (url.indexOf("?") > -1) { return url + "&sy=1"; }
    else { return url + "?sy=1"; }
}
function getLength(str) {
    if (str) {
        return str.replace(/[^\x00-\xff]/g, "xx").length;
    } else {
        return 0;
    }
}
function leftTrim(str) {
    if (str) {
        return str.replace(/(^[\s]*)/g, "");
    } else {
        return "";
    }
}
function rightTrim(str) {
    if (str) {
        return str.replace(/([\s]*$)/g, "");
    } else {
        return "";
    }
}
function trim(str) {
    if (str) {
        return leftTrim(rightTrim(str));
    } else {
        return "";
    }
}
function checkControl(id, regStr, msg) {
    try {
        var obj = document.getElementById(id);
        if (obj) {
            var objValue = obj.value;

            if (regStr == "isnotnull") {
                objValue = obj.value.replace(/^[ 　]+/, "").replace(/[ 　]+$/, "");
                if (objValue == "") {
                    alert(msg);
                    setFocus(obj);
                    return false;
                }
            }
            else if (objValue) {
                var reg = new RegExp(regStr, "im");
                if (objValue.search(reg) == -1) {
                    alert(msg);
                    setFocus(obj);
                    return false;
                }
            }
        }
    }
    catch (e) { }
    return true;
}
function setFocus(obj) {
    if (obj) {
        try {
            obj.select();
            obj.focus();
        }
        catch (e) { }
    }
}

function SelOne(n, cid, cidName) {
    var url;
    if (n.indexOf(".aspx") == -1) {
        url = "../Tmp/MySel.aspx?n=" + n;
    }
    else {
        url = n;
    }
    var str = openDialog(url, 800, 600);
    if (str) {
        var p = str.indexOf(",");
        cid.value = str.substr(0, p);
        cidName.value = str.substr(p + 1);
        return true;
    }
    else {
        return false;
    }
}

function SelMulti(n, txt) {
    var str = openDialog("../Tmp/MySel.aspx?n=" + n, 800, 600);
    if (str) {
        if (txt.value) {
            //剔除重复的
            var strs = str.split(";");
            for (i = 0; i < strs.length; i++) {
                if ((";" + txt.value + ";").indexOf(";" + strs[i] + ";") == -1) {
                    txt.value += ";" + strs[i];
                }
            }
        } else {
            txt.value = str;
        }
    }
}
function SetMulti(n, id) {
    var txtValue = document.getElementById(id);
    if (txtValue) {
        var txtText = document.getElementById("text_" + id);
        var url;
        if (n.indexOf(".aspx") == -1) {
            url = "../Tmp/MySel.aspx?n=" + n + "&";
        } else if (n.indexOf("?") == -1) {
            url = n + "?";
        } else {
            url = n + "&";
        }
        var str = openDialog(url + "s=" + txtValue.value, 800, 600);
        if (str) {
            if (str == "CLEARALL") {
                txtValue.value = "";
                txtText.value = "";
            }
            else {
                var resultValue = "";
                var resultText = "";
                //剔除重复的
                var strs = str.split(";");
                for (i = 0; i < strs.length; i++) {
                    var strValue = "";
                    var strText = "";
                    if (strs[i].indexOf(",") == -1) {
                        strValue = strs[i];
                        strText = strs[i];
                    } else {
                        var strsValue = strs[i].split(",");
                        strValue = strsValue[0];
                        strText = strsValue[1];
                    }
                    if (i == 0 && strValue == "CLEAR") {
                        txtValue.value = "";
                        txtText.value = "";
                    }
                    else {
                        resultValue += "," + strValue;
                        resultText += "、" + strText;
                    }
                }
                txtValue.value = resultValue.substr(1);
                txtText.value = resultText.substr(1);
            }
        }
    }
}
var isshowwait = true;
function showWait(isshow) {
    var obj = document.getElementById("divshowwait");
    if (obj) {
        if (isshow && isshowwait) {
            obj.style.display = "";
        }
        else {
            obj.style.display = "none";
            if (!isshowwait) {
                isshowwait = true;
            }
        }
    }
    return true;
}

//将回车键改为tab键//

document.onkeydown = function (event) {
    try {
        var evt = null;
        if (arguments.length == 1) {
            evt = arguments[0];
        } else {
            evt = (window.event ? window.event : null);
        }
        var el = evt.srcElement || evt.target;
        if (el && !(el.type == "textarea" || el.type == "submit" || el.type == "button" || el.type == "reset" || el.type == "checkbox") || el.type == "radio") {
            if (evt.charCode) {
                if (evt.charCode == 13) {
                    evt.charCode = 9;
                }
            } else {
                if (evt.keyCode == 13) {
                    evt.keyCode = 9;
                }
            }
        }
    } catch (e) { }
}

//二级联动 说明:联动的select的值必须匹配 allinfo为二级所有值的数组 格式为value|text
function selectChange(obj, changeId, allinfo) {
    var selValue = obj.options[obj.selectedIndex].value; //得到select对象中当前被选中的值
    var objChange = document.getElementById(changeId);
    if (objChange) {
        objChange.options.length = 0;
        for (i = 0; i < allinfo.length; i++) {
            if (allinfo[i].substring(0, selValue.length) == selValue) {
                objChange.options.length++;
                var p = allinfo[i].indexOf("|");
                var text = allinfo[i].substr(p + 1);
                var value = allinfo[i].substr(0, p);
                objChange.options[objChange.options.length - 1] = new Option(text, value);
            }
        }
    }
}
//取消右键//

//document.oncontextmenu = function () {
//    return false;
//}

//获得WebService的xml
// 访问方法xmlDoc.getElementsByTagName("string")[0].childNodes[0].nodeValue
function AjaxGetXml(serviceUrl, soapAction, data) {
    var xmlHttp;
    try { xmlHttp = new ActiveXObject("Msxml2.XMLHTTP"); }
    catch (e) {
        try { xmlHttp = new ActiveXObject("Microsoft.XMLHTTP"); }
        catch (e) {
            try { xmlHttp = new XMLHttpRequest(); }
            catch (e) { return null; }
        }
    }
    try {
        var url = serviceUrl + "/" + soapAction;
        xmlHttp.open("POST", url, false);
        xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xmlHttp.send(data);
        if (xmlHttp.status != 200) {
            return null;
        }
        try //Internet Explorer  
        {
            var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.loadXML(xmlHttp.responseText);
            return xmlDoc;
        } catch (e) {
            try //Firefox, Mozilla, Opera, etc.  
            {
                parser = new DOMParser();
                return parser.parseFromString(xmlHttp.responseText, "text/xml");
            } catch (e) {
                return null;
            }
        }
    }
    catch (e) {
        return null;
    }
}

function AjaxUpdate(obj, data) {
    var xmlDoc = AjaxGetXml("../WebService.asmx", "UpdateData", data + "&value=" + obj.value);
    if (xmlDoc) {
        var str = xmlDoc.getElementsByTagName("string")[0].childNodes[0].nodeValue;
        if (str != "1") {
            alert(str);
        }
    } else {
        alert("提交失败请重试");
    }
}

function refreshThis(pn, pv) {
    var url;
    var p = document.location.href.indexOf("&" + pn + "=");
    if (p == -1) {
        url = document.location.href;
    } else {
        url = document.location.href.substr(0, p);
    }
    window.location.href = url + "&" + pn + "=" + pv;
}

