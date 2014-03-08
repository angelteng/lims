// JavaScript Document
var input_text = $(".input_text");
var show_password = $("#showPwd");
var input_password = $("#txtPassword");
input_text.focus(function (e) {
    if ($(this).val() == this.defaultValue) {
        $(this).val("");
        return;
    }
    $(this).css("color", "#000");
}).blur(function (e) {
    if ($(this).val() == '') {
        $(this).val(this.defaultValue);
        $(this).css("color", "#999");
        return;
    }
    $(this).css("color", "#000");
});
show_password.focus(function () {
    input_password.show().focus();
    show_password.hide();
});
input_password.blur(function () {
    if (input_password.val() == "") {
        show_password.show();
        input_password.hide();
    }
});


window.onload = function () {
    //$("txtAccount").focus();
    $("#txtAccount").bind("keypress", checkSubmit);
    $("#txtPassword").bind("keypress", checkSubmit);
    $("#txtValidatorCode").bind("keypress", checkSubmit);
    $("#btnLogin").bind("click", CheckLogin);
    $("#txtPassword").bind("focus", EmptyPassword);
}

//当前目录，用于找到对应的页面
var currentDirPath = "";

if (top.location != self.location) {
    top.location = self.location;
}

function $F(objID) {
    return $('#' + objID).val();
}

function handler() { }

function CheckLogin() {
    if ($F("txtAccount") == "" || $F("txtAccount") == "用户名或手机号码") {
        alert("用户名称不能为空！");
        return false;
    }
    else if ($F("txtPassword") == "") {
        alert("密码不能为空！");
        return false;
    }
    var validateMode = $("#hiValidateMode");
    if (validateMode != "Off" && ($F("txtValidatorCode") == "" || $F("txtValidatorCode") == "验证码")) {
        alert("验证码不能为空！");
        return false;
    }

    var url = currentDirPath + "Manage/Login/Check/";
    var pars = "Account=" + $F("txtAccount") + "&Password=" + $F("txtPassword");
    if (validateMode != "Off") {
        pars += "&ValidateCode=" + $F("txtValidatorCode");
    }
    var myAjax = new jQuery.ajax({ url: url, type: 'post', data: pars, complete: ResponseLogin });   // 对指定的URL产生一个 HTTP request
    return;
}

function ResponseLogin(xhrObj) {
    var message = jQuery.evalJSON(xhrObj.responseText);
    if (!message.Succeed) {
        alert(message.Text);
        return;
    }
    JumpPage();
}

function JumpPage() {
    var jumpUrl = "";
    if ($F("hiBackUrl") != "") {
        jumpUrl = "/?Backurl=" + $F("hiBackUrl");
    }
    top.location = currentDirPath + "Manage/Login/Jump" + jumpUrl;
}

function GetNodeValue(node) {
    var text = "";
    //for IE
    if (window.ActiveXObject) {
        text = node.text;
    }
    else {
        text = node.textContent;
    }
    return text;
}

function ReloadValidateCode(obj) {
    obj.src = currentDirPath + "login/ValidateCode/?Temp=" + GetRoundCode();
    $("#txtValidatorCode").value = ""; //清空用户输入的验证码
}

function GetRoundCode() {
    return Math.round(9000 * Math.random() + 1000); //随机数字
}

function checkSubmit(event) {
    if (event.keyCode != 13) { return; }
    else if (jQuery.browser.msie || jQuery.browser.opera) { event.keyCode = 0; }
    $("#btnLogin").click();
}

function EmptyPassword() {
    $("#txtPassword").value = "";
}