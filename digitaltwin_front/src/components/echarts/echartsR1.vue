<template>
    <div id="r1" style="width: 340px; height:240px"></div>
</template>

<script>
export default {
    mounted() {
        this.draw();
    },
    methods: {
        draw() {
            // 基于准备好的dom，初始化echarts实例
            var myChart = this.$echarts.init(document.getElementById("r1"));
            // 指定图表的配置项和数据
            var option = {
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross',
                    }
                },
                toolbox: {
                    feature: {
                        dataView: { show: false, readOnly: false },
                        magicType: { show: false, type: ['line', 'bar'] },
                        restore: { show: false },
                        saveAsImage: { show: false }
                    }
                },
                legend: {
                    top: '10%',
                    data: ['电费', '日平均压力']
                },
                xAxis: [
                    {
                        type: 'category',
                        data: ['2018', '2019', '2020', '2021', '2022'],
                        axisPointer: {
                            type: 'shadow'
                        }
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '电费',
                        min: 0,
                        max: 4000,
                        interval: 500,
                        axisLabel: {
                            formatter: '{value}'
                        }
                    },
                    {
                        type: 'value',
                        name: '日平均压力',
                        min: 0,
                        max: 30,
                        interval: 5,
                        axisLabel: {
                            formatter: '{value} %'
                        }
                    }
                ],
                series: [
                    {
                        name: '电费',
                        type: 'bar',
                        tooltip: {
                            valueFormatter: function (value) {
                                return value;
                            }
                        },
                        data: [
                            1000, 1500, 2000, 2500, 3000
                        ],
                        itemStyle: {
                            normal: {
                                color: "#0C68C4", //改变柱状图的颜色
                            },
                        },
                    },
                    {
                        name: '日平均压力',
                        type: 'line',
                        yAxisIndex: 1,
                        tooltip: {
                            valueFormatter: function (value) {
                                return value + ' %';
                            }
                        },
                        data: [10, 15, 20, 23, 24],
                        itemStyle: {
                            normal: {
                                color: "#248FF7", //改变折线图的颜色
                            },
                        },
                    }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        }
    }
}
</script>