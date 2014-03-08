// 定义全局变量

document.write("<a id=\"pageLink\"></a>");

//自定义方法,用于兼容原JS方法
function $F(objID) {
    return $('#' + objID).val();
}

var currentPageUrl = "";
var currentOrderColumn = "";
var currentOrderType = "ASC";

// 选中的Id长字符串，以","分割
var selectedIDList = "";
//当前目录，用于找到对应的删除页面
var currentDirPath = "";

function handler(){}

//数据哈希表
function Hashtable(){
    var table = new Array();
    
    this.Add = function(key,value){
        for(var i=0; i<table.length; i++){
            if(table[i][0] == key){
                table[i][1] = value;
                return;
            }
        }
        table.push(new Array(key,value));
        this.Count = table.length;
        this.Item = table;
    }
        
    this.Remove = function(key){
        var removeIndex = -1;
        for(var i=0; i<table.length; i++){
            if(table[i][0] == key){
                removeIndex = i;
                break;
            }
        }
        if(removeIndex != -1){
            //table.slice(removeIndex,1);
            table = table.slice(0,removeIndex).concat(table.slice(removeIndex+1,table.length));
            this.Count = table.length;
            this.Item = table;
        }
    }
    
    this.Clear = function(){
        table = new Array();
        this.Count = table.length;
        this.Item = new Array();
    }
    
    this.Contains = function(key){
        if(table.length <= 0){
            return false;
        }
        
        for(var i=0; i<table.length; i++){
            if(table[i][0] == key){
                return true;
            }
        }
        return false;
    }
    
    this.GetValue = function(key){
        if(table.length <= 0){
            return null;
        }
        
        for(var i=0; i<table.length; i++){
            if(table[i][0] == key){
                return table[i][1];
            }
        }
    }
    
    this.Item = new Array();
    this.Count = 0;
}

/// <summary>
/// 函数名称：MessageBoxA
/// 函数说明：
/// </summary>
/// <param name="AText">参数1说明</param>
/// <returns>返回值说明</returns>
function MessageBoxA(AText){
    alert(AText);
}

/// <summary>
/// 函数名称：GetNodeValue
/// 函数说明：获取节点的内容
/// </summary>
/// <param name="node">XML节点</param>
/// <returns>节点的内容</returns>
function GetNodeValue(node){
    var text = "";
    //for IE
    if(window.ActiveXObject){
        text = node.text;
    }
    else{
        text = node.textContent;
    }
    return text;
}

/// <summary>
/// 函数名称：GetNodeAttributeValue
/// 函数说明：获取节点的属性值
/// </summary>
/// <param name="node">XML节点</param>
/// <param name="nodeName">XML节点属性</param>
/// <returns>节点的节点属性值</returns>
function GetNodeAttributeValue(node, nodeName){
    var text = "";
    //for IE
    if(window.ActiveXObject){
        text = node.getAttribute(nodeName);
    }
    else{
        text = node.getAttribute(nodeName);
    }
    return text;
}

/// <summary>
/// 函数名称：SelectAll
/// 函数说明：选择全部活动的复选框
/// </summary>
/// <param name="obj">参数1说明</param>
/// <returns>返回值说明</returns>
function SelectAll(obj){
    var prefix = obj.id.replace("_All","");
    var elements = document.body.getElementsByTagName("input");
    for(var index=0;index<elements.length;index++){
        if(elements[index].id==obj.id || elements[index].disabled){
            continue;
        }
        else if(elements[index].type !="checkbox" || elements[index].id.indexOf(prefix)<0){
            continue;
        }
        
        elements[index].checked = obj.checked;
    }
}

/// <summary>
/// 函数名称：ToggleChb
/// 函数说明：选择或取消选择全部活动的复选框
/// </summary>
/// <returns></returns>
function ToggleChb() {
    $("input[type='checkbox']").each(function(index, obj) {      //use input type and cssname to identity the items
    if (obj.name == 'cbID_All') return;  // exit if the checkbox is sellect all
    if (obj.id == 'blurSearch') return;  // exit if the checkbox is blur search
        if ($('#cbID_All').attr("checked")) {
            if ($(obj).attr('disabled')) { return; }      //when the item is disabled, just return
            $(obj).attr("checked", true);
        }
        else { $(obj).attr("checked", false); }
    });
}    

/// <summary>
/// 函数名称：GetSelectedID
/// 函数说明：获取第一个带前缀的复选框
/// </summary>
/// <param name="prefix">前缀</param>
/// <returns>复选框的值</returns>
function GetSelectedID(prefix){        
    var selectedIDs = "";
    var elements = document.body.getElementsByTagName("input");
    for(var index=0;index<elements.length;index++){   
        if(elements[index].id==prefix+"_All" || elements[index].disabled){      
            continue;           
        }
        else if(elements[index].type !="radio" || elements[index].id.indexOf(prefix)<0){
            continue;
        }
        else if(!elements[index].checked){
            continue;
        }
        
        return elements[index].value;        
    }
    return "";
}

/// <summary>
/// 函数名称：GetSelectedIDList
/// 函数说明：获取带前缀的复选框
/// </summary>
/// <param name="prefix">前缀</param>
/// <returns>字符串，复选框的值，以","作分隔符</returns>
function GetSelectedIDList(prefix){        
    var selectedIDs = "";
    var elements = document.body.getElementsByTagName("input");
    for(var index=0;index<elements.length;index++){   
        if(elements[index].id==prefix+"_All" || elements[index].disabled){      
            continue;           
        }
        else if(elements[index].type !="checkbox" || elements[index].id.indexOf(prefix)<0){
            continue;
        }
        else if(!elements[index].checked){
            continue;
        }
        
        if(selectedIDs.length>0){
            selectedIDs += ",";
        }
        selectedIDs += elements[index].value;        
    }
    return selectedIDs;
}

function Go2Page(url)
{
    window.location.href=url;
}

function Go2AndRefresh(url) {
    if (url == "javascript:window.history.back()") 
        window.location.href = document.referrer;    
    else
        window.location.href = url;    
}

//执行删除操作
function handlerDelete(tp) {
    if (tp) {        
        ProcessDelete();
    }
}

//删除
function ProcessDelete() {
    var url = currentDirPath + "/Delete/";
    var pars = "ID=" + selectedIDList;
    var myAjax = new jQuery.ajax({ url: url, type: 'post', data: pars, complete: ActionComplete });
    //var myAjax = new Ajax.Request(url, { method: 'post', parameters: pars, onComplete: ResponseProcessResult });
    selectedIDList = null;
}

/* 删除操作完成后，刷新页面 */
function ActionComplete(xhrObj) {
    try {
        var message = jQuery.evalJSON(xhrObj.responseText);
        showTips(message.Text, message.Succeed);  
        if (!message.Succeed) {
            //alert(message.Text);
            return;
        }
        ReloadPage();
        
    } catch (e) {
        alert("处理过程出现异常");
    }
}

/* 添加、修改 操作成功后，返回上一页 */
function ActionCompleteGo(xhrObj) {
    try {
        var message = jQuery.evalJSON(xhrObj.responseText);
        showTips(message.Text, message.Succeed);  
        if (!message.Succeed) {
            alert(message.Text);
            return;
        }
        Go2AndRefresh(message.TargetUrl);
    } catch (e) {
        console.log(e);
        alert("处理过程出现异常");
    }
}

function showTips(tipText,isSucceed) {
    var obj = $("#topAlertTips", window.parent.document);
    if (!isSucceed) {
        obj.addClass("error");
    }
    else {
        obj.removeClass("error");
    }
    obj.html(tipText).show();
    window.parent.HideTips();  //调用父级方法隐藏
}

//重载页面
function ReloadPage(url) {
    if (url != null) {
        self.location = url;
        return;
    }
    self.location = self.location;
}

//验证日期格式
function IsDate(text){
  var reg = /^(\d{4,4})-(\d{1,2})-(\d{1,2})$/; 
  return reg.test(text);
}

//Verify the format of Phone Number
//Full format: +86-020-84031867 or
//              +86 (020) 8406 1867 or
//              (020) 8403 1867
function IsPhoneNo(text){
    //var reg = /^(\+)?(\d*\-?)*\d+$/;
    var reg = /^(\+?\d{1,3}\ ?)?(\ ?\(?\d{1,3}\)?)?(\-{0,1}\ {0,1}\({0,1}\d{1,3}\ {0,1}\){0,1}\-{0,1})?(\d{1,12}\ ?\d{1,12})$/;
    return reg.test(text);
}

//Verify the format of Fax Number
//Full format: +86-020-84031867 or
//              +86 (020) 8406 1867 or
//              (020) 8403 1867
function IsFaxNo(text){
    //var reg = /^(\+)?(\d*\-?)*\d+$/;
    var reg = /^(\+?\d{1,3}\ ?)?(\ ?\(?\d{1,3}\)?)?(\-{0,1}\ {0,1}\({0,1}\d{1,3}\ {0,1}\){0,1}\-{0,1})?(\d{1,12}\ ?\d{1,12})$/;
    return reg.test(text);
}

//验证电子邮箱
function IsEmail(text){
  var reg = /^\w+((\-\w+)|(\.\w+))*\@\w+((\.|\-)\w+)*\.\w+$/; 
  return reg.test(text);
}

//验证网址
function IsUrl(text){
  var reg = /^((http|https)\:\/\/)?([\w\d\-]+\.)+[\w\d\-]+(\/?|(\/\w+\/?)+)*$/;
  return reg.test(text);
}

//判断字符串是否为数字
// strNum 被判断的字符串
// isInteger 是否必须为整数 true：必须为正数 false：可以带小数点
// canNegative 是否能为负数 true：可以为负数 false：不能为负数（必须为正数）
function IsNumeric(strNum, isInteger, canNegative) {
    if (jQuery.trim(strNum.toString()) == "") {
        return false;
    }
    else if (isNaN(strNum)) {
        return false;
    }
    if (isInteger && strNum.toString().indexOf(".") >= 0) {
        return false;
    }
    else if (!isInteger && strNum.toString().indexOf(".") == 0) {
        return false;
    }
    if (!canNegative && strNum.toString().indexOf("-") >= 0) {
        return false;
    }
    return true;
}

//检查是否存在非法字符：若“存在”则返回false，若“不存在”则返回true
function CheckString(s) {
	var str0 = "<>'~@#$%^&*()+-=[]\\;?/:{}|.`_\"";
		for (var i=0; i<str0.length; i++) { 
			if(s.indexOf(str0.substring(i,i+1))!=-1) {
				return false;
			}
		}
	return true;
}

//检查是否非负整数：若“否”则返回false，若“是”则返回true
function CheckNum(s){
	for (var loc=0; loc<s.length; loc++) {
		if ((s.charAt(loc) < '0') || (s.charAt(loc) > '9')) {
			return false;
		}
	}
	return true; 
}

//检查是否英文或数字：若“否”则返回false，若“是”则返回true
function CheckStrAndNum(s) {
	for (var loc=0; loc<s.length; loc++) {
		if ((s.charAt(loc) < '0') || (s.charAt(loc) > '9')) {
			if ((s.charAt(loc) < 'a') || (s.charAt(loc) > 'z')) {
				if ((s.charAt(loc) < 'A') || (s.charAt(loc) > 'Z')) {
					return false;
				}
			}
		}
	}
	return true; 
}

//是否为空：若“是”则返回false，若“否”则返回true
function CheckEmpty(s) {
	if (s.length == 0 || s == "") {
		return false;
	}
	return true; 
}

//验证最大长度：若“超出长度”则返回false，若“未超出长度”则返回true
function CheckLength(s,i) {
	if (s.length > i) {
		return false;
	}
	return true;
}

//验证最小长度：若“小于最小长度”则返回false，若“大于最小长度”则返回true
function CheckLengthshort(s,i) {
	if (s.length < i) {
		return false;
	}
	return true;
}

//验证固定长度：若“不为固定长度”则返回false，若“为固定长度”则返回true
function CheckFixLength(s,i) {
	if (s.length != i) {
		return false;
	}
	return true;
}

//验证电子邮件：若“不合法”则返回false，若“合法”则返回true
function CheckEmail(s) {
	if (s.length > 50) {
		alert("Email地址长度不能超过50位！");
		return false;
	}
	var regu ="^(([0-9a-zA-Z]+)|([0-9a-zA-Z]+[_.0-9a-zA-Z-]*[0-9a-zA-Z]+))@([a-zA-Z0-9-]+[.])+([a-zA-Z]{2}|net|com|gov|mil|org|cc|edu|biz|int|tv)$"
	var re = new RegExp(regu);
	if (s.search(re) != -1) {
		return true;
	} 
	else {
		alert("请输入有效合法的Email地址！");
		return false;
	}
}

//验证日期：若“不合法”则返回false，若“合法”则返回true；参数s为年份合法性，参数i为日期合法性
function CheckDate(s,i)
{
	if (s <= 1900 || s >= 2079 )
	{
		return false;	
	}
	var r = i.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
	if (r == null)
	{
		return false; 
	}
	var d = new Date(r[1], r[3]-1,r[4]); 
	return (d.getFullYear()==r[1]&&(d.getMonth()+1)==r[3]&&d.getDate()==r[4]);
}


///////////////////////////////////////////
// 列表排序
///////////////////////////////////////////

function SortList(columnName) {
    currentOrderColumn = Request("OrderColumn");
    currentOrderType = Request("OrderType");
    currentOrderType = currentOrderType == "ASC" ? "DESC" : "ASC";

    var index = currentPageUrl.lastIndexOf('?');
    if (index < 0) {
        LinkPage(currentPageUrl + "?OrderColumn=" + columnName + "&OrderType=" + currentOrderType);
    }
    else if (index == 1) {
        LinkPage(currentPageUrl + "OrderColumn=" + columnName + "&OrderType=" + currentOrderType);
    }
    else {
        LinkPage(currentPageUrl + "&OrderColumn=" + columnName + "&OrderType=" + currentOrderType);
    }
}

function Request(strParame) {
    var args = new Object();
    var query = location.search.substring(1);

    var pairs = query.split("&"); // Break at ampersand 
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        value = decodeURIComponent(value);
        args[argname] = value;
    }
    return args[strParame];
}

///////////////////////////////////////////
// 右边表格列表的鼠标悬停效果：背景颜色改变，鼠标为手状
///////////////////////////////////////////
function focusit(obj)
{
    $(obj).children('td').each(function(i, o) {
        $(o).addClass('focusit');
        $(o).css('background-color', '#eefbff');
    });
}
function blurit(obj)
{
    $(obj).children('td').each(function(i, o) {
		$(o).removeClass('focusit');
		$(o).css('background-color', '#ffffff');
	});
}

/*------------------------------------------------
===================分页函数开始===================
------------------------------------------------*/
//分页处理
function GotoPage(targetPage) {
    if (!IsNumeric(targetPage, true, false)) {
        return;
    }
    targetPage = jQuery.trim(targetPage.toString());
    targetPage = parseInt(targetPage);
    pagerUrl = $("#txtPagerUrl").val();
    pagerUrl = pagerUrl.replace("Page=***", "Page=" + targetPage.toString())
    pagerUrl = pagerUrl.replace("PageSize=***", "PageSize=" + $("#pager_txtPageSize").val());
    LinkPage(pagerUrl);
}

function CheckPageNumeric(obj, controlDefaultValue) {
    if (!IsNumeric(obj.value, true, false)) {
        obj.value = $('#' + controlDefaultValue).val();
    }
}

function EnterKey(e) {
    var key = window.event ? e.keyCode : e.which;
    if (key == 13) {
        var targetPage = $("#pager_txtPage").val();
        GotoPage(targetPage);
        return false;
    }
    return true;
}

/*------------------------------------------------
===================分页函数结束===================
------------------------------------------------*/
function LinkPage(url) {
  if(url != null){
      self.location = url;
      return;
  }
}

///////////////////////////////////////////
// 格式化字符串
///////////////////////////////////////////
String.format = function() {
    if( arguments.length == 0 )
        return null;

    var str = arguments[0]; 
    for(var i=1;i<arguments.length;i++) {
        var re = new RegExp('\\{' + (i-1) + '\\}','gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

/*
 * iframe的处理函数
 * 提供批量和单独iframe自适应高度功能
 */
function autoHeight(obj)
{
    var id = obj.id;
    var subWeb = document.frames ? document.frames[id].document : obj.contentDocument;   
    if(obj != null && subWeb != null)
    {
       obj.height = parseInt(subWeb.body.scrollHeight) + "px";
    }   
}


/* 加减日期操作
* dateParameter: 2013-05-05
* num: 20
*/
function addDate(dateParameter, num) {
    if (num == 0) {return dateParameter; }
    var translateDate = "", dateString = "", monthString = "", dayString = "";
    translateDate = dateParameter.replace("-", "/").replace("-", "/");

    var newDate = new Date(translateDate);
    newDate = newDate.valueOf();
    newDate = newDate + num * 24 * 60 * 60 * 1000;
    newDate = new Date(newDate);

    //如果月份长度少于2，则前加 0 补位   
    if ((newDate.getMonth() + 1).toString().length == 1) {
        monthString = 0 + "" + (newDate.getMonth() + 1).toString();
    } else {
        monthString = (newDate.getMonth() + 1).toString();
    }

    //如果天数长度少于2，则前加 0 补位   
    if (newDate.getDate().toString().length == 1) {

        dayString = 0 + "" + newDate.getDate().toString();
    } else {

        dayString = newDate.getDate().toString();
    }

    dateString = newDate.getFullYear() + "-" + monthString + "-" + dayString;
    return dateString;
}


/*
*
* 显示或隐藏上传窗口
*/
function ShowUploader() {
    $('#divUploader').fadeToggle(500);
}

/******从Flex中回传上传文件ID至html页面***/
function SetUploadFiles(idList) {
    $("hiUploadFilesID").val(idList);
}