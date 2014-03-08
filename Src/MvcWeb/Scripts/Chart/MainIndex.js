$(function () {
    //*******begin monthly statistics***/
    var lineoptions = {
        chart: {
            //renderTo: 'linecontainer',          //放置图表的容器
            type: 'line',
            marginRight: 130,
            marginBottom: 25
        },
        title: {
            text: '本年度每月新收人数统计',
            x: -20 //center
        },
        subtitle: {
            text: '不包括已经转出本院患者',
            x: -20 //center
        },
        xAxis: {
            categories: ['一月', '二月', '三月', '四月', '五月', '六月',
                        '七月', '八月', '九月', '十月', '十一月', '十二月']
        },
        yAxis: {
            title: {
                text: '数量统计 (人)'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
//        plotOptions: {
//            line: {
//                dataLabels: {
//                    enabled: true
//                },
//                enableMouseTracking: true
//            }
//        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -10,
            y: 100,
            borderWidth: 0
        },
        series: []
    }

    var lineurl = currentDirPath + "Statistics/MonthlyNew/?Temp="+Math.random();;

    // Load the data from the XML file 
    $.get(lineurl, function (xml) {
        // Split the lines
        var $xml = $(xml);
        // push series
        $xml.find('Series').each(function (i, series) {
            var seriesOptions = {
                name: $(series).find('Name').text(),
                data: []
            };
            // push data points
            $(series).find('Data Point').each(function (i, point) {
                seriesOptions.data.push(
					parseInt($(point).text())
				);
            });
            // add it to the options
            lineoptions.series.push(seriesOptions);
        });
        //var chart = new Highcharts.Chart(options);
        $('#linecontainer').highcharts(lineoptions);
    });
    //*******end monthly statistics***/

    //*****begin gender pie********/
    var pieoptions = {
        chart: {
            type: 'pie',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: '患者性别比例'
        },
        plotOptions: {
            pie: {
                allowPointSelect: false,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    color: '#000000',
                    connectorColor: '#000000',
                    formatter: function () {
                        return '<b>' + this.point.name + '</b>: ' + this.percentage.toFixed(2) + ' %';
                    }
                }
            }
        },
        series: []
    }

    var pieurl = currentDirPath + "Statistics/Gender/?Temp="+Math.random();

    // Load the data from the XML file 
    $.get(pieurl, function (xml) {
        var $xml = $(xml);
            var seriesOptions = {
                name: '人数',
                data: []
            };

            seriesOptions.data.push(
                ['男',parseInt($(xml).find('Data Point[Gender=1]').text())]
            );
            seriesOptions.data.push(
                ['女', parseInt($(xml).find('Data Point[Gender=0]').text())]
            );
            pieoptions.series.push(seriesOptions);

            $('#piecontainer_l').highcharts(pieoptions);
    });
    //*****end gender pie********/

        //*****begin type pie********/
        var pieoptions2 = {
            chart: {
                type: 'pie',
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            },
            title: {
                text: '患者临床分型组别比例'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: false,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        color: '#000000',
                        connectorColor: '#000000',
                        formatter: function () {
                            return '<b>' + this.point.name + '</b>: ' + this.percentage.toFixed(2) + ' %';
                        }
                    }
                }
            },
            series: []
        }

        var pieurl2 = currentDirPath + "Statistics/ClinicalGroup/?Temp=" + Math.random();

        // Load the data from the XML file 
        $.get(pieurl2, function (xml) {
            var $xml = $(xml);
            var seriesOptions = {
                name: '人数',
                data: []
            };
            
            seriesOptions.data.push(
                ['标危', parseInt($(xml).find('Data Point[Type=1]').text())]
            );
            seriesOptions.data.push(
                ['中危', parseInt($(xml).find('Data Point[Type=2]').text())]
            );
            seriesOptions.data.push(
                ['高危', parseInt($(xml).find('Data Point[Type=3]').text())]
            );
            seriesOptions.data.push(
                ['未知', parseInt($(xml).find('Data Point[Type=-1]').text())]
            );

            pieoptions2.series.push(seriesOptions);

            $('#piecontainer_r').highcharts(pieoptions2);
        });
        //*****end type pie********/

});