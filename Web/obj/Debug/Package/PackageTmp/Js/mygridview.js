var mygridview_selectedRow;
var mygridview_oldStyle;
var mygridview_preStyle;
function mygridview_keypressfun(id, evt) {
    var kc;
    if (evt.charCode) {
        kc = evt.charCode;
    } else {
        kc = evt.keyCode;
    }
    if (kc == 13) {
        var btn = document.getElementById(id);
        if (btn) { btn.click(); }
        return true;
    }
    else if (kc < 48 || kc > 57) { return false; }
}
function mygridview_gotofun(id, max, postScript) {
    var txt = document.getElementById(id);
    if (txt) {
        if (txt.value == "") {
            txt.value = "1";
        }
        if (isNaN(txt.value)) {
            alert("请输入1至" + max + "的数值");
            txt.focus();
            txt.select();
            return false;
        } else if (txt.value < 1) {
            txt.value = 1;
        } else if (txt.value > max) {
            txt.value = max;
        }
        eval(postScript);
        return false;
    }
}
function mygridview_clickfun(obj, id, selectrowclass, ismulti) {
    if (obj && obj.id) {
        if (selectrowclass) {
            if (mygridview_selectedRow) { mygridview_selectedRow.className = mygridview_preStyle; }
            if (mygridview_oldStyle != selectrowclass) { mygridview_preStyle = mygridview_oldStyle; }
            mygridview_selectedRow = obj;
            obj.className = selectrowclass;
            mygridview_oldStyle = selectrowclass;
        }
        if (ismulti) {
            if (obj.cells[0].hasChildNodes()) {
                var ckb = obj.cells[0].firstChild;
                if (ckb && ckb.tagName == "INPUT") { ckb.click(); }
            }
        } else {
            var hiddenObj = document.getElementById(id);
            if (hiddenObj) { hiddenObj.value = obj.id; }
            if (obj.cells[0].hasChildNodes()) {
                var rb = obj.cells[0].firstChild;
                if (rb && rb.tagName == "INPUT") { rb.click(); }
            }
        }
    }
}
function mygridview_selectfun(obj, id) {
    var selectedIds = "";
    var p = obj.id.indexOf("_");
    var per = obj.id.substring(0, p + 1);
    var objs = document.getElementsByTagName("INPUT");
    for (i = 0; i < objs.length; i++) {
        if (objs[i].type == "checkbox" && objs[i].id.indexOf(per) == 0 && objs[i].id != per + "all") {
            if (obj.id == per + "all") { objs[i].checked = obj.checked; }
            if (objs[i].checked) { selectedIds += ";" + objs[i].id.substring(p + 1); }
        }
    }
    var hiddenObj = document.getElementById(id);
    if (hiddenObj) {
        if (selectedIds == "") { hiddenObj.value = ""; }
        else { hiddenObj.value = selectedIds.substr(1); }
    }
}
function mygridview_parentrowfun(objTd, parentName, openUrl, closeUrl) {
    if (objTd) {
        var isOpen = true;
        var tdIndex = 0;
        var objs = objTd.parentNode.cells;
        for (var i = 0; i < objs.length; i++) {
            if (objs[i] == objTd) {
                tdIndex = i;
                break;
            }
        }
        objs = objTd.parentNode.parentNode.rows;
        for (var i = 1; i < objs.length; i++) {
            if (objs[i].getAttribute("name") == parentName) {
                if (objs[i].style.display == "none") { objs[i].style.display = ""; isOpen = true; }
                else {
                    isOpen = false;
                    objs[i].style.display = "none";
                    if (objs[i].cells[tdIndex].onclick && objs[i].cells[tdIndex].title == "+") { objs[i].cells[tdIndex].click(); }
                }
            }
        }
        if (isOpen) {
            objTd.title = "+";
            objTd.innerHTML = objTd.innerHTML.replace(closeUrl, openUrl);
        }
        else {
            objTd.title = "-";
            objTd.innerHTML = objTd.innerHTML.replace(openUrl, closeUrl);
        }
    }
}
function getSelectId(id) {
    var obj = document.getElementById(id);
    if (obj) { return obj.value; }
    else { return ""; }
}

var JPos = {};
(function ($) {
    $.getAbsPos = function (pTarget) {
        var x_ = y_ = 0;
        if (pTarget.style.position != "absolute") {
            while (pTarget.offsetParent) {
                x_ += pTarget.offsetLeft;
                y_ += pTarget.offsetTop;
                pTarget = pTarget.offsetParent;
            }
        }
        x_ += pTarget.offsetLeft;
        y_ += pTarget.offsetTop;
        return { x: x_, y: y_ };
    }
    $.getEventPos = function (evt) {
        var _x, _y;
        evt = JEvent.getEvent(evt);
        if (evt.pageX || evt.pageY) {
            _x = evt.pageX;
            _y = evt.pageY;
        } else if (evt.clientX || evt.clientY) {
            _x = evt.clientX + (document.body.scrollLeft || document.documentElement.scrollLeft) - (document.body.clientLeft || document.documentElement.clientLeft);
            _y = evt.clientY + (document.body.scrollTop || document.documentElement.scrollTop) - (document.body.clientTop || document.documentElement.clientTop);
        } else {
            return $.getAbsPos(evt.target);
        }
        return { x: _x, y: _y };
    }
})(JPos);

var JEvent = {};
(function ($) {
    $.getEvent = function (evt) {
        evt = window.event || evt;
        if (!evt) {
            var fun = JEvent.getEvent.caller;
            while (fun != null) {
                evt = fun.arguments[0];
                if (evt && evt.constructor == Event)
                    break;
                fun = fun.caller;
            }
        }
        return evt;
    }
    $.doEvent = function (fun) {
        var args = arguments;
        return function () {
            return fun(args);
        }
    }
})(JEvent);

var mask_mousemove = function (evt) {
    if (document.selection) {//IE ,Opera
        if (document.selection.empty)
            document.selection.empty(); //IE
        else {//Opera
            document.selection = null;
        }
    } else if (window.getSelection) {//FF,Safari
        window.getSelection().removeAllRanges();
    }
    var oPosX = parseInt(JPos.getAbsPos(this).x, 10);
    var mPosX = parseInt(JPos.getEventPos(evt).x, 10);
    var x = oPosX - mPosX - mAWidth / 2;
    var tmpX = parseInt(mygridview_cols[mygridview_colIndex].style.width, 10) + x;
    mygridview_dragMask.style.left = (oPosX - mAWidth / 2) + "px";
    mygridview_cols[mygridview_colIndex].style.width = tmpX >= 2 ? tmpX + "px" : "2px";
    if (!document.all) {
        mygridview_dataTable.parentNode.style.width = (parseInt(mygridview_dataTable.parentNode.clientWidth, 10) + x) + "px"; //这一句为处理FF时用。
    }
}
var mask_mouseup = function (evt) {
    setCellMouseDown(false);
}
var cell_mousedown = function (evt) {
    mygridview_dragMask.style.display = "";
}
var dbl_click = function (evt) {
    var theTable = this.parentNode;
    while (theTable.tagName != "TABLE") {
        theTable = theTable.parentNode;
    }
    initResizeTable(theTable);
    setCellMouseDown(true);

    mygridview_colIndex = this.colIndex;
    with (mygridview_cols[mygridview_colIndex].style) {
        width = (parseInt(width, 10) - mAWidth) + "px";
    }
    var mPos = JPos.getEventPos(evt);
    with (mygridview_dragMask.style) {
        left = (mPos.x - mAWidth) + "px";
        top = (parseInt(mPos.y, 10) - mAHeight / 2) + "px";
        display = "";
    }
}
var dbd_click = function (evt) {
    with (mygridview_cols[mygridview_colIndex].style) {
        width = (parseInt(width, 10) + mAWidth) + "px";
    }
}
var mygridview_colIndex;
var mygridview_dataTable;
var mygridview_dataTableID;
var mygridview_cols;
var mAWidth = 20;
var mAHeight = 20;
var mygridview_dragMask = document.createElement("IMG");
mygridview_dragMask.style.cssText = "width:" + mAWidth + "px;height:" + mAHeight + "px;position:absolute;display:none;z-index:999999999;cursor:e-resize;";
mygridview_dragMask.title = "双击改变列表宽度（单击此图片变宽，双击表格列变窄）";
mygridview_dragMask.src = "../Img/jt.gif";
mygridview_dragMask.onclick = dbd_click;
//mygridview_dragMask.onmousemove = mask_mousemove;
mygridview_dragMask.onmouseout = mask_mouseup;
function initResizeTable(theTable) {
    if (mygridview_dataTableID) {
        if (mygridview_dataTableID == theTable.id) {
            return;
        }
    } else {
        document.body.insertBefore(mygridview_dragMask, document.body.lastChild);
    }
    mygridview_dataTableID = theTable.id
    mygridview_dataTable = theTable;
    mygridview_cols = mygridview_dataTable.getElementsByTagName("COL")
    var o;
    for (i = 0; o = mygridview_dataTable.rows[0].cells[i]; i++) {
        o.colIndex = i;
        o.ondblclick = dbl_click;
    }
}
function setCellMouseDown(isSet) {
    if (isSet) {
        mygridview_dragMask.style.display = "";
    } else {
        mygridview_dragMask.style.display = "none";
    }
    if (mygridview_dataTable) {
        var o;
        for (i = 0; o = mygridview_dataTable.rows[0].cells[i]; i++) {
            if (isSet) {
                o.onmouseover = cell_mousedown;
            } else {
                o.onmouseover = null;
            }
        }
    }
}