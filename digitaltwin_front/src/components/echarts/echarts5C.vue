<template>
    <div id="ct" style="width: 1300px; height: 600px"></div>
</template>
  
<script>
export default {
    mounted() {
        this.draw();
        this.getDataL1();
    },
    methods: {
        draw() {
            // 基于准备好的dom，初始化echarts实例
            var myChart = this.$echarts.init(document.getElementById("ct"));
            // 指定图表的配置项和数据
            var option = {
                xAxis: {
                    type: 'category',
                    data: Array.from({ length: 100 }, (_, i) => `8:${i.toString().padStart(2, '0')}`)
                },
                yAxis: {
                    type: 'value',
                    axisLabel: {
                        formatter: '{value} kPa' // 设置y轴单位 
                    },
                },
                series: [{
                    data: Array.from({ length: 100 }, () => Math.floor(Math.random() * 100)),
                    type: 'line',
                    lineStyle: {
                        color: 'blue'
                    },
                    showSymbol: false // 取消实心点
                }],
                backgroundColor: 'white'
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        }
    }
}
</script>