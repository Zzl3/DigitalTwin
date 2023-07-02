<template>
    <div ref="chart" style="width: 100%; height: 100%;"></div>
</template>
  
<script>
import * as echarts from 'echarts';

export default {
    props: {
        activeIndex1: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            chart: null
        };
    },
    mounted() {
        this.chart = echarts.init(this.$refs.chart);
        this.drawChart();
    },
    watch: {
        activeIndex1() {
            this.drawChart();
        }
    },
    methods: {
        drawChart() {
            const option = this.getOptionBasedOnIndex(this.activeIndex1);
            this.chart.setOption(option);
        },
        getOptionBasedOnIndex(index) {
            let titleText = '';
            let data = [];

            switch (index) {
                case '1':
                    titleText = '4#空压站流量计&电表1';
                    data = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '2':
                    titleText = '4#空压站流量计&电表2';
                    data = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '3':
                    titleText = '4#空压站流量计&电表3';
                    data = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '4':
                    titleText = '4#空压站薄板中心';
                    data = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '5':
                    titleText = '4#空压站8、9平台';
                    data = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '6':
                    titleText = '4#空压站';
                    data = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                default:
                    titleText = '默认标题';
                    data = [200, 400, 600, 800, 1000, 1200, 1400];
            }

            return {
                title: {
                    text: titleText
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: '{b}<br/>{a}: {c} 度' // 格式化工具提示，显示单位为度
                },
                xAxis: {
                    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
                },
                yAxis: {
                    name: '耗电量 (度)' // 设置 y 轴名称及单位
                },
                series: [
                    {
                        name: '耗电量',
                        type: 'line',
                        data: data
                    }
                ]
            };
        }
    }
};
</script>
  
<style></style>
  