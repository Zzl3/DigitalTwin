<template>
    <div id="m3" style="width: 20%; height: 20%"></div>
</template>

<script>
import { getData } from "@/api/electricityfee";
export default {
    mounted() {
        this.draw();
        this.getDataM3();
    },
    methods: {
        draw() {
            var myChart = this.$echarts.init(document.getElementById('m3'));
            window.addEventListener('resize', function () {
        myChart.resize();
      });
            var txt = 22;
            var option = {
                title: {
                    text: txt + '%',
                    x: 'center',
                    y: 'center',
                    textStyle: {
                        fontWeight: 'normal',
                        color: '#000000',
                        fontSize: '10'
                    }
                },
                color: 'rgba(0,0,0,.3)',

                series: [{
                    name: 'Line 1',
                    type: 'pie',
                    clockWise: true,
                    radius: ['65%', '80%'],
                    itemStyle: {
                        normal: {
                            label: {
                                show: false
                            },
                            labelLine: {
                                show: false
                            }
                        }
                    },
                    hoverAnimation: false,
                    data: [{
                        value: txt,
                        name: '已使用',
                        itemStyle: {
                            normal: {
                                color: '#1852FC',
                                label: {
                                    show: false
                                },
                                labelLine: {
                                    show: false
                                }
                            }
                        }
                    }, {
                        name: '未使用',
                        value: 100 - txt
                    }
                    ]
                }]
            };

            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
            window.addEventListener("resize", function () {
                myChart.resize();
            });
        },
        getDataM3() {
            getData()
                .then((data) => {
                    var chart = this.$echarts.getInstanceByDom(document.getElementById("m3"));
                    var option = chart.getOption();
                    console.log(data); // 处理获取到的数据
                    var tempdata = data[0];
                    option.series[0].data[0].value = (parseFloat(tempdata.air3) * 100).toFixed(1);
                    option.series[0].data[1].value = (
                        100 -
                        parseFloat(tempdata.air3) * 100
                    ).toFixed(1);
                    option.title[0].text = (parseFloat(tempdata.air3) * 100).toFixed(1) + "%";
                    chart.setOption(option);
                })
                .catch((error) => {
                    console.log(error); // 处理错误
                });
        },
    }
}
</script>
