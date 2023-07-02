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
            let data1 = [];
            let data2 = [];

            switch (index) {
                case '1':
                    titleText = '4#空压站流量计&电表1';
                    data1 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 100));
                    data2 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '2':
                    titleText = '4#空压站流量计&电表2';
                    data1 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 100));
                    data2 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '3':
                    titleText = '4#空压站流量计&电表3';
                    data1 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 100));
                    data2 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '4':
                    titleText = '4#空压站薄板中心';
                    data1 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 100));
                    data2 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '5':
                    titleText = '4#空压站8、9平台';
                    data1 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 100));
                    data2 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                case '6':
                    titleText = '4#空压站';
                    data1 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 100));
                    data2 = Array.from({ length: 7 }, () => Math.floor(Math.random() * 1000));
                    break;
                default:
                    titleText = '默认标题';
                    data1 = [20, 40, 60, 80, 100, 120, 140];
                    data2 = [200, 400, 600, 800, 1000, 1200, 1400];
            }

            return {
                title: {
                    text: titleText
                },
                tooltip: {
                    trigger: 'axis'
                },
                xAxis: {
                    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
                },
                yAxis: [
                    {
                        name: '流量',
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} m³/h' // 设置流量坐标轴刻度标签的格式化，显示单位为 m³/h
                        }
                    },
                    {
                        name: '耗电量',
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} 度' // 设置耗电量坐标轴刻度标签的格式化，显示单位为度
                        }
                    }
                ],
                series: [
                    {
                        name: '流量',
                        type: 'line',
                        data: data1,
                        yAxisIndex: 0 // 使用第一个 y 轴
                    },
                    {
                        name: '耗电量',
                        type: 'line',
                        data: data2,
                        yAxisIndex: 1 // 使用第二个 y 轴
                    }
                ]
            };
        }
    }
};
</script>
  
<style></style>
  