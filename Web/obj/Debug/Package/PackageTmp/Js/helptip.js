function showHelpTip(e, sHtml, bHideSelects, sWidth) {
 try{
    if (!e){
        e=(window.event ? window.event : null);
    }
	// find anchor element
	var el = e.target || e.srcElement;
	//while (el.tagName != "A")
	//	el = el.parentNode;

	// is there already a tooltip? If so, remove it
	if (el._helpTip) {
		helpTipHandler.hideHelpTip(el);
	}
	helpTipHandler.hideSelects = Boolean(bHideSelects);
    if (sWidth)
    {
	    helpTipHandler.helpWidth = sWidth;
	}

	// create element and insert last into the body
	helpTipHandler.createHelpTip(el, sHtml);

	// position tooltip
	helpTipHandler.positionToolTip(e);

	// add a listener to the blur event.
	// When blurred remove tooltip and restore anchor
	el.onblur = helpTipHandler.anchorBlur;
	el.onkeydown = helpTipHandler.anchorKeyDown;
   }
   finally{
   	return false;
   }
}

var helpTipHandler = {
	hideSelects:	false,

	helpTip:		null,

	showSelects:	function (bVisible) {
		if (!this.hideSelects) return;
		// only IE actually do something in here
		var selects = document.getElementsByTagName("SELECT");
		var l = selects.length;
		for	(var i = 0; i < l; i++)
			selects[i].runtimeStyle.visibility = bVisible ? "" : "hidden";
	},

	create:	function () {
		var d = document.createElement("DIV");
		d.style.cssText = "width:"+this.helpWidth+";position:absolute;border:1px Solid WindowFrame;background: Infobackground;color:InfoText;font:StatusBar;padding:3px;filter:progid:DXImageTransform.Microsoft.Shadow(color='#777777', Direction=135, Strength=3);z-index:10000; ";
		d.onmousedown = this.helpTipMouseDown;
		d.onmouseup = this.helpTipMouseUp;
		document.body.appendChild(d);
		this.helpTip = d;
	},

	createHelpTip:	function (el, sHtml) {
		if (this.helpTip == null) {
			this.create();
		}

		var d = this.helpTip;
		d.innerHTML = sHtml;
		d._boundAnchor = el;
		el._helpTip = d;
		return d;
	},

	// Allow clicks on A elements inside tooltip
	helpTipMouseDown:	function (e) {
		var d = this;
		var el = d._boundAnchor;
		if (!e) e = event;
		var t = e.target || e.srcElement;
		//while (t.tagName != "A" && t != d)
		//	t = t.parentNode;
		if (t == d) return;

		el._onblur = el.onblur;
		el.onblur = null;
	},

	helpTipMouseUp:	function () {
		var d = this;
		var el = d._boundAnchor;
		el.onblur = el._onblur;
		el._onblur = null;
		el.focus();
	},

	anchorBlur:	function (e) {
		var el = this;
		helpTipHandler.hideHelpTip(el);
	},

	anchorKeyDown:	function (e) {
		if (!e) {e = (window.event ? window.event : null);}
		if (e.keyCode == 27 || e.charCode == 27) {	// ESC
			helpTipHandler.hideHelpTip(this);
		}
	},

	removeHelpTip:	function (d) {
		d._boundAnchor = null;
		d.style.filter = "none";
		d.innerHTML = "";
		d.onmousedown = null;
		d.onmouseup = null;
		d.parentNode.removeChild(d);
		//d.style.display = "none";
	},

	hideHelpTip:	function (el) {
		var d = el._helpTip;
		/*	Mozilla (1.2+) starts a selection session when moved
			and this destroys the mouse events until reloaded
		d.style.top = (-parseInt(el.offsetHeight,10) - 100) + "px";
		*/

		d.style.visibility = "hidden";
		//d._boundAnchor = null;

		el.onblur = null;
		el._onblur = null;
		el._helpTip = null;
		el.onkeydown = null;

		this.showSelects(true);
	},

	positionToolTip:	function (e) {
		this.showSelects(false);
		var scroll = this.getScroll();
		var d = this.helpTip;
        var dw = parseInt(d.offsetWidth,10);
        var dh = parseInt(d.offsetHeight,10);
        var sw = parseInt(scroll.width,10);
        var sl = parseInt(scroll.left,10);
        var sh = parseInt(scroll.height,10);
        var st=parseInt(scroll.top,10);
        var x = parseInt(e.clientX,10);
        var y = parseInt(e.clientY,10);
		// width
		if (dw >= sw)
			d.style.width = (sw - 10) + "px";
		else
			d.style.width = "";

		// left
		if (x > sw - dw)
			d.style.left = (sw - dw + sl) + "px";
		else
			d.style.left = (x - 2 + sl) + "px";

		// top
		if (y + dh + 10 < sh)
			d.style.top = (y + 10 + st) + "px";
		else if (y - dh > 0)
			d.style.top = (y + st - dh) + "px";
		else
			d.style.top = (st + 2) + "px";

		d.style.visibility = "visible";
	},

	// returns the scroll left and top for the browser viewport.
	getScroll:	function () {
		if (document.all && typeof document.body.scrollTop != "undefined") {	// IE model
			var ieBox = document.compatMode != "CSS1Compat";
			var cont = ieBox ? document.body : document.documentElement;
			return {
				left:	cont.scrollLeft,
				top:	cont.scrollTop,
				width:	cont.clientWidth,
				height:	cont.clientHeight
			};
		}
		else {
			return {
				left:	window.pageXOffset,
				top:	window.pageYOffset,
				width:	window.innerWidth,
				height:	window.innerHeight
			};
		}

	}

};
