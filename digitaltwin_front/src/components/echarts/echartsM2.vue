<template>
    <div id="m2" style="width: 50px; height:50px"></div>
</template>

<script>
export default {
    mounted() {
        this.draw();
    },
    methods:{
        draw(){
            var myChart = this.$echarts.init(document.getElementById('m2'));
            var txt=27
            var option = {
                title: {
                text: txt+'%',
                x: 'center',
                y: 'center',
                textStyle: {
                    fontWeight: 'normal',
                    color: '#000000',
                    fontSize: '16'
                }
                },
                color:'rgba(0,0,0,.3)',
            
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
                        color:'#0000ff',
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
                    value: 100-txt
                }]
                }]
            };

            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
            window.addEventListener("resize",function(){
                myChart.resize();
            });
        }
    }
}
</script>
