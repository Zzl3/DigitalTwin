<template>
  <div id="l3" style="width: 400px; height: 240px"></div>
</template>

<script>
export default {
  mounted() {
    this.draw();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("l3"));
      // 指定图表的配置项和数据
      var option = {
        tooltip: {
          trigger: "axis",
        },
        legend: {
          data: ["空压站1", "空压站2", "空压站3"],
          top: 30,
        },
        calculable: true,
        xAxis: [
          {
            type: "category",
            data: ["3.22", "3.23", "3.24", "3.25", "3.26", "3.27"],
          },
        ],
        yAxis: [
          {
            type: "value",
          },
        ],
        series: [
          {
            name: "空压站1",
            type: "bar",
            data: [23.0, 21.9, 21.0, 23.2, 25.6, 25.7],
            itemStyle: {
              normal: {
                color: "#1157fc", //改变折线点的颜色
              },
            },
          },
          {
            name: "空压站2",
            type: "bar",
            data: [23.6, 21.9, 20.0, 20.3, 21.4, 20.3],
            itemStyle: {
              normal: {
                color: "#648cf5", //改变折线点的颜色
              },
            },
          },
          {
            name: "空压站3",
            type: "bar",
            data: [20.6, 22.9, 21.0, 22.3, 23.4, 21.3],
            itemStyle: {
              normal: {
                color: "#8ba4e6", //改变折线点的颜色
              },
            },
          },
        ],
      };
      // 使用刚指定的配置项和数据显示图表。
      myChart.setOption(option);
    },
  },
};
</script>

<style></style>
