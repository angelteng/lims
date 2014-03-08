jQuery(document).ready(function () {
    $('ul#Tab li').click(function () {

        $("ul#Tab li").removeClass("tab_active");  //先全部移除
        $(this).addClass("tab_active");            //目标 tab加上激活标识

        var targetDiv = $('#' + this.lang);        //目标层

        // 隐藏其他层，显示选中Tab对应的层
        targetDiv.siblings().css("display", "none");   // siblings() 方法取得同级的所有table对象
        targetDiv.show();
        if ($("#btns")) $("#btns").show();   //显示按钮
    })
});
