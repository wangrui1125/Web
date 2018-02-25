//弹出提示窗口
//msg 提示内容 空则关闭窗口
//title 窗口标题 空默认"弹出提示"
//pWidth 宽度 空默认100
//pHeight 高度 空默认50
//parentDisenabled 父窗口是否不可用 空默认可用

function popupWin(msg, title, pWidth, pHeight, parentDisenabled){ 
 var msgObj=document.getElementById("divPopupWin");
 if (!msg){
	if (msgObj){
		msgObj.style.display="none";
	}
	return;
 }

 if (!title){
	title="弹出提示";
 }
 if (!pWidth)
 {
	pWidth=100;
 }
  if (!pHeight)
 {
	pHeight=50;
 }
 var titleheight = "20px"; // 提示窗口标题高度 
 var bordercolor = "#FF0000"; // 提示窗口的边框颜色 
 var titlecolor = "#FF0000"; // 提示窗口的标题颜色 
 var titlebgcolor = "#FFFFFF"; // 提示窗口的标题背景色
 var bgcolor = "#FFFFFF"; // 提示内容的背景色
 var popupPosition = getPosition();

 if (msgObj){
	 msgObj.style.display="";
	 msgObj.style.width=pWidth+"px";
	 msgObj.style.height=pHeight+"px";
	 document.getElementById("titlePopupWin").innerHTML = title;
	 document.getElementById("msgPopupWin").innerHTML = msg;
 } else {
	 var bgObj;
	 if (parentDisenabled){
		bgObj = document.createElement("div"); 
		bgObj.style.cssText = "position:absolute;left:0px;top:0px;width:"+document.body.clientWidth+"px;height:"+document.body.clientHeight+"px;filter:Alpha(Opacity=30);opacity:0.3;background-color:#000000;z-index:101;";
		document.body.appendChild(bgObj); 
	 }
 	 msgObj=document.createElement("div");
	 msgObj.style.cssText = "position:absolute;font:11px '宋体';top:"+popupPosition.top+"px;left:"+popupPosition.left+"px;width:"+pWidth+"px;height:"+pHeight+"px;text-align:center;border:1px solid "+bordercolor+";background-color:"+bgcolor+";padding:1px;line-height:22px;z-index:1000;";
	 msgObj.setAttribute("id","divPopupWin");
	 document.body.appendChild(msgObj);
 
	 var table = document.createElement("table");
	 msgObj.appendChild(table);
	 table.style.cssText = "margin:0px;border:0px;padding:0px;";
	 table.cellSpacing = 0;
	 var tr = table.insertRow(-1);
	 var titleBar = tr.insertCell(-1);
	 titleBar.style.cssText = "width:100%;height:"+titleheight+"px;text-align:left;padding:2px;margin:0px;font:bold 13px '宋体';color:"+titlecolor+";border:0px;cursor:move;background-color:" + titlebgcolor;
	 titleBar.setAttribute("id","titlePopupWin");
	 titleBar.innerHTML = title;
	 var moveX = 0;
	 var moveY = 0;
	 var moveTop = 0;
	 var moveLeft = 0;
	 var moveable = false;
	 titleBar.onmousedown = function() {
	  var evt = getEvent();
	  moveable = true; 
	  moveX = evt.clientX;
	  moveY = evt.clientY;
	  moveTop = parseInt(msgObj.style.top);
	  moveLeft = parseInt(msgObj.style.left);
	  
	  titleBar.onmousemove = function() {
	   if (moveable) {
		var evt = getEvent();
		var x = moveLeft + evt.clientX - moveX;
		var y = moveTop + evt.clientY - moveY;
		if ( x > 0 &&( x < popupPosition.left) && y > 0 && (y < popupPosition.top) ) {
		 msgObj.style.left = x + "px";
		 msgObj.style.top = y + "px";
		}
	   } 
	  };
	  titleBar.onmouseup = function () { 
	   if (moveable) { 
		moveable = false; 
		moveX = 0;
		moveY = 0;
		moveTop = 0;
		moveLeft = 0;
	   } 
	  };
	 }
	 
	 var closeBtn = tr.insertCell(-1);
	 closeBtn.style.cssText = "cursor:pointer; padding:1px;background-color:" + titlebgcolor;
	 closeBtn.innerHTML = "<span style='font-size:13pt; color:#000000;'>×</span>";
	 closeBtn.onclick = function(){
	    if (parentDisenabled && bgObj){
			document.body.removeChild(bgObj); 	
			document.body.removeChild(msgObj); 	
		} else {
			msgObj.style.display="none";
		}
	 } 
	 var msgBox = table.insertRow(-1).insertCell(-1);
	 msgBox.style.cssText = "font:10pt '宋体';padding:5px";
	 msgBox.colSpan  = 2;
	 msgBox.setAttribute("id","msgPopupWin");
	 msgBox.innerHTML = msg;
   }
    // 获得事件Event对象，用于兼容IE和FireFox
 function getEvent() {
  return window.event || arguments.callee.caller.arguments[0];
 };
 function getPosition() {
	var top = document.documentElement.scrollTop+document.documentElement.clientHeight-pHeight-5;
	var left = document.documentElement.scrollLeft+document.documentElement.clientWidth-pWidth-5;
	if (msgObj){
		msgObj.style.top=top+"px";
		msgObj.style.left=left+"px";
	}	
	return {top:top,left:left};
 };
 window.onscroll = function (){
	getPosition();
 };
 window.onresize = function (){
	getPosition();
 };
}

function AjaxGet(serviceUrl,soapAction,data)
{
var xmlHttp;
try { xmlHttp = new ActiveXObject("Msxml2.XMLHTTP"); }
catch (e) { try { xmlHttp = new ActiveXObject("Microsoft.XMLHTTP"); }
catch (e) { try { xmlHttp = new XMLHttpRequest(); }
catch (e) { return null; }}}
try {
var url = serviceUrl+"/"+soapAction;
xmlHttp.open("POST", url, false);
xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
xmlHttp.send(data);
if (xmlHttp.status != 200) 
{
return "0";
}
var str = xmlHttp.responseText.replace("</string>","");
return str.substr(str.lastIndexOf(">")+1);
}
catch(e)
{
return "0";
}
}

 