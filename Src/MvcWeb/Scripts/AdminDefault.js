// Admin Default

//window.onload = function() {
//    var pageList = new Array(
//                "Default.aspx",
//                "../Default.aspx",
//                "../Common/HopeValidatorCode1.aspx",
//                "../Install/Default.aspx",
//                "../User/Default.aspx"
//                );
//    var ajaxArray = new Array(pageList.length);
//    for (i = 0; i < pageList.length; i++) {
//        ajaxArray[i] = new Ajax.Request(pageList[i], { method: 'get', onComplete: function() { } });
//    }
//}

var installDir="";

function CallLogin() {
    alert("无访问权限，请重新登录！");
    location.href = installDir + '/login/';
    //ymPrompt.alert({ message: '无访问权限，请重新登录！', slideShowHide: false, title: '访问受限', handler: function() { location.href = installDir+'/login/' } });
}

function ShowStartMenu(obj) {
    $(obj).setStyle({ 'background': 'url(../images/fv/fv_start_hover.png)' });
    $(obj).next().setStyle({ 'display': 'block' });
}

function HideStartMenu(obj) {
    $(obj).down().setStyle({ 'background': 'url(../images/fv/fv_start.png)' });
    $(obj).down().next().setStyle({ 'display': 'none' });
}

function GotoLink(url) {
    document.getElementsByTagName("iframe")["main"].contentWindow.location = url;
}

//function ShowReminder(messageText) {
//    ymPrompt.alert({ message: messageText, fixPosition: true, winPos: 'rb', showMask: false })
//}

function OpenMainFrame(url) {
    window.document.getElementsByTagName("iframe")["main"].contentWindow.location = url;   //兼容
}

// 更新一级菜单样式
function UpdateMenu(obj, id) {
    $(obj).siblings().removeClass("clicked");
    $(obj).addClass('clicked');

    var eleID = 'menu_l2_' + id;
    $(eleID).show();
    $(eleID).siblings().hide();
}

//鼠标移到obj上显示className样式
function MouseOverMenu(obj, className) {
    $(obj).addClass(className);
}

//鼠标移开bj上隐藏className样式
function MouseOutMenu(obj, className) {
    $(obj).removeClass(className);
}

//鼠标移到obj上显示className样式
function MouseOverMenuL2(obj, className) {
    $(obj).children('ul:first').addClass(className);
}

//鼠标移开bj上隐藏className样式
function MouseOutMenuL2(obj, className) {
    $(obj).children('ul:first').removeClass(className);
}

//回到首页
function Home() {
    window.parent.document.getElementsByTagName("iframe")["main"].contentWindow.location = installDir+"/home/main/";
}

//修改密码
function EditPwd() {
    window.parent.document.getElementsByTagName("iframe")["main"].contentWindow.location = installDir + "/admin/SelfEdit/";
}

//退出
function Logout() {
    handlerLogout(window.confirm("确实要退出系统？"));
}

function handlerLogout(tp) {
    if (tp) {
        window.parent.location = installDir+"/login/logout/";
    }
}
//function ShowDLLInfo() {
//    var url = installDir+"Common/SysInfo/DLLInfo.aspx";
//    ymPrompt.win({ message: url, width: 400, height: 500, title: 'DLL 版本信息', handler: handler, maxBtn: false, minBtn: false, iframe: true, fixPosition: false, winPos: '' });
//}