//======.net客户段验证3.0============//
//   原作者:高处不胜寒   修改 贾世义 2005-03-09 e-mail:jsyhello76@126.com//

function fob(n, d) {
    var p;
    var i;
    var x;

    if (!d) {
        d = document;
    }
    p = n.indexOf("?");
    if (p > 0 && d.parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document;
        n = n.substring(0, p);
    }
    x = d[n];
    if (!(x) && d.all)
        x = d.all[n];
    for (i = 0; !x && i < d.forms.length; i++)
        x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++)
        x = fob(n, d.layers[i].document);
    return x;
}


function vdf() {
    var i, j;
    var max = 0, min = 0;
    var flag = true;
    var name, cb_name, type, value;
    var message;
    var a = arguments;

    for (i = 0; i < (a.length - 2); i += 3) {
        if (a[i].indexOf('#') != -1) {
            name = fob(a[i].substr(0, a[i].indexOf('#')));
            cb_name = fob(a[i].substr((a[i].indexOf('#') + 1), a[i].length));
        }
        else {
            name = fob(a[i]);
        }

        if (!name) {
            continue;
        }

        message = a[i + 1];
        type = a[i + 2];
        value = name.value;

        if (type == "r" || type == "" || type == null) {
            continue;
        }
        if (value == "无") {
            continue;
        }

        if (type.indexOf("r_") != -1 || type == "notnull") {
            if (value == "") {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            else if (type == "r_") {
                continue;
            }
        }

        if (type.indexOf("^") == 0 && type.indexOf("$") == type.length - 1) {
            //自定义 正则表达式
            var reg = new RegExp(type, "im");
            if (value.search(reg) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
        }

        if (type.indexOf("nchar") != -1) {
            if (value.search(/^[0-9a-zA-Z\_\-\.\[\]:\/]+$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            if (type.indexOf("r_nchar>") != -1) {
                min = parseInt(type.substring((type.indexOf('>') + 1)), 10);
                if (value.length < min) {
                    alert(message + "!\n");
                    name.focus();
                    name.select();
                    return false;
                }
            }
            continue;
        }

        if (type.indexOf("char") != -1) {
            if (value.search(/^[a-zA-Z]+$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            if (type.indexOf("r_char>") != -1) {
                min = parseInt(type.substring((type.indexOf('>') + 1)), 10);
                if (value.length < min) {
                    alert(message + "!\n");
                    name.focus();
                    name.select();
                    return false;
                }
            }
            continue;
        }

        if (type.indexOf("num") != -1) {
            if (value.search(/^[0-9]+$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            flag = true;
            if (type != "r_num" && type != "num") {
                if (type.indexOf("<") == -1) {
                    min = type.substring((type.indexOf('>') + 1), type.length);

                    flag = (parseInt(value, 10) >= parseInt(min, 10));
                }
                else {
                    if (type.indexOf(">") == -1) {
                        max = type.substring((type.indexOf('<') + 1), type.length);

                        flag = (parseInt(value, 10) <= parseInt(max, 10));
                    }
                    else {
                        min = type.substring((type.indexOf('>') + 1), type.indexOf('-'));

                        max = type.substring(type.indexOf('-') + 1, type.length);

                        flag = (parseInt(value, 10) >= parseInt(min, 10) && parseInt(value, 10) <= parseInt(max, 10));
                    }
                }
                if (flag == false) {
                    alert(message + "!\n");
                    name.focus();
                    name.select();
                    return false;
                }
            }
            continue;
        }
        if (type.indexOf("float") != -1) {
            if (!(value.search(/^[0-9]+$/) != -1 || value.search(/^([0-9]+)|([0-9]+\.[0-9]*)|([0-9]*\.[0-9]+)$/) != -1)) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("date") != -1) {
            var isOk = false;
            if (type.indexOf("datetime") != -1) {
                isOk = /^((((1[6-9]|[2-9]\d)\d{2})[-](0?[13578]|1[02])[-](0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})[-](0?[13456789]|1[012])[-](0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})[-]0?2[-](0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))\s([0-1]{1}\d{1}|[2]{1}[0-3]{1}):([0-5]{1}\d{1}):([0-5]{1}\d{1})$/.test(value);
            }
            else {
                isOk = /^((((1[6-9]|[2-9]\d)\d{2})[-](0?[13578]|1[02])[-](0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})[-](0?[13456789]|1[012])[-](0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})[-]0?2[-](0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29))$/.test(value);
            }

            if (!isOk) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("china") != -1) {
            if (value.search(/[\u4e00-\u9fa5]+/) != -1) {
                continue;
            }
            else {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("chinese") != -1) {
            if (value == "" && type.indexOf("r_") == -1) {
                continue;
            }
            if (value.search(/^[\u4e00-\u9fa5]+$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("addr") != -1) {
            if (value.search(/[\u4e00-\u9fa5]+/) != -1) {
                flag = true;
            }
            else {
                flag = false;
            }
            if (flag && value.search(/[0-9]*/) != -1) {
                continue;
            }
            else {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
        }
        // ===============身份证判断================ //
        if (type.indexOf("PID") != -1) {
            flag = (value.length == 15 || value.length == 18);
            if (flag) {
                flag = (value.search(/^[1-9]{1}\d{14}/) != -1);
                if (!flag) {
                    flag = (value.search(/^[1-9]{1}\d{16}[0-9xX]{1}$/) != -1);
                }
            }
            if (flag) {
                if (value.length == 15) {
                    year = "19" + value.substr(6, 2);
                    month = value.substr(8, 2);
                    day = value.substr(10, 2);
                }
                else {
                    year = value.substr(6, 4);
                    month = value.substr(10, 2);
                    day = value.substr(12, 2);
                }
                newDate = new Date(year, month - 1, day);
                if (newDate.toString() == "NaN") {
                    flag = false;
                }
                else {
                    flag = (parseInt(month, 10) == newDate.getMonth() + 1);
                }
            }
            if (flag == false) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("linker") != -1) {
            if (value.length < 7 || value.search(/^[0-9\-\/]+$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("tel") != -1) {
            if (value.search(/^[0-9]{7,8}$/) == -1 && value.search(/^(\([0-9]{3}\)|[0-9]{4}-)[0-9]{7}$/) == -1 && value.search(/^(\([0-9]{3}\)|[0-9]{3}-)[0-9]{8}$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("mobile") != -1) {
            if (value.search(/^[0-9]{11}$/) == -1 && value.search(/^[0-9]{7,8}$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            continue;
        }

        if (type.indexOf("email") != -1) {
            if (value.search(/^[_\.a-z0-9A-Z]+@[a-z0-9A-Z]+[\.][a-z0-9A-Z]{2,}$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            else {
                continue;
            }
        }
        if (type.indexOf("nameid") != -1) {
            if (value.search(/^.+\[\w+\]$/) == -1) {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            else {
                continue;
            }
        }
        // ===============判断给定的两个对象值相等================ //	
        if (type.indexOf("==") != -1) {
            if (value == "") {
                alert(message + "!\n");
                name.focus();
                name.select();
                return false;
            }
            var newname = fob(type.substring(2, type.length));
            if (newname && value != newname.value) {
                alert(message + "!\n");
                name.value = "";
                newname.value = "";
                name.focus();
                return false;
            }
            continue;
        }

    }
    return true;
}

function IsDigit(evt) {
    evt = evt ? evt : (window.event ? window.event : null);
    var kc;
    if (evt.charCode) {
        kc = evt.charCode;
    } else {
        kc = evt.keyCode;
    }
    return ((kc >= 48) && (kc <= 57));
}
