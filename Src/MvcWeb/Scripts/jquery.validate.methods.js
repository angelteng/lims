// 手机号码验证
jQuery.validator.addMethod("Mobile", function (value, element) {
    var length = value.length;
    var mobile = /^(((1[0-9]{2}))+\d{8})$/
    return this.optional(element) || (length == 11 && mobile.test(value));
}, "手机号码格式错误");


// 电话号码验证   
jQuery.validator.addMethod("tel", function (value, element) {
    var tel = /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/;
    return this.optional(element) || (tel.test(value));
}, "电话号码格式错误");


// 联系电话(手机/电话皆可)验证 
jQuery.validator.addMethod("phone", function (value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    var tel = /^\d{3,4}-?\d{7,9}$/;
    return this.optional(element) || (tel.test(value) || mobile.test(value));
}, "请正确填写您的联系电话"); 


// 邮政编码验证   
jQuery.validator.addMethod("zipCode", function (value, element) {
    var tel = /^[0-9]{6}$/;
    return this.optional(element) || (tel.test(value));
}, "邮政编码格式错误");


// QQ号码验证   
jQuery.validator.addMethod("QQ", function (value, element) {
    var tel = /^[1-9]\d{4,9}$/;
    return this.optional(element) || (tel.test(value));
}, "qq号码格式错误");


// IP地址验证
jQuery.validator.addMethod("IPAdress", function (value, element) {
    var ip = /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
    return this.optional(element) || (ip.test(value) && (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256));
}, "Ip地址格式错误");


// 字符验证 
jQuery.validator.addMethod("stringCheck", function (value, element) {
    return this.optional(element) || /^[\u0391-\uFFE5\w]+$/.test(value);
}, "只能包括中文字、英文字母、数字和下划线"); 


// 字母和数字的验证
jQuery.validator.addMethod("chrnum", function (value, element) {
    var chrnum = /^([a-zA-Z0-9]+)$/;
    return this.optional(element) || (chrnum.test(value));
}, "只能输入数字和字母(字符A-Z, a-z, 0-9)");

// 字母和数字、下划线的验证
jQuery.validator.addMethod("codeName", function (value, element) {
    var chrnum = /^[a-zA-Z]\w*$/;
    return this.optional(element) || (chrnum.test(value));
}, "只能输入数字和字母(字符A-Z, a-z, 0-9)和下划线(不能开头)");


// 中文的验证
jQuery.validator.addMethod("chinese", function (value, element) {
    var chinese = /^[\u4e00-\u9fa5]+$/;
    return this.optional(element) || (chinese.test(value));
}, "只能输入中文");


// 下拉框验证
$.validator.addMethod("selectNone", function (value, element) {
    return value == "请选择";
}, "必须选择一项");


// 身份证号码验证   
jQuery.validator.addMethod("isIdCardNo", function (value, element) {
    return this.optional(element) || isIdCardNo(value);
}, "请正确输入您的身份证号码");

// 日期的验证
jQuery.validator.addMethod("date", function (value, element) {
    var date = /^(\d{4,4})-(\d{1,2})-(\d{1,2})$/;
    return this.optional(element) || (date.test(value));
}, "只能输入日期(例：2008-08-08)");

// 日期的验证
jQuery.validator.addMethod("monthdate", function (value, element) {
    var monthdate = /^(\d{4,4})-(\d{1,2})$/;
    return this.optional(element) || (monthdate.test(value));
}, "只能输入日期(例：2008-08)");

