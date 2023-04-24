<template>
  <div id="l2" style="width: 400px; height: 180px"></div>
</template>

<script>
export default {
  mounted() {
    this.draw();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("l2"));
      // 指定图表的配置项和数据
      var option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            // Use axis to trigger tooltip
            type: "line", // 'shadow' as default; can also be 'line' or 'shadow'
          },
        },
        legend: {
          top: '20%'
        },
        grid: {
          left: "3%",
          right: "3%",
          bottom: "0%",
          containLabel: true,
        },
        xAxis: {
          type: "category",
          data: [
            "用户1",
            "用户2",
            "用户3",
            "用户4",
            "用户5",
            "用户6",
            "用户7",
            "用户8",
            "用户9",
          ],
        },
        yAxis: {
          type: "value",
        },
        series: [
          {
            name: "空压站1",
            type: "bar",
            stack: "total",
            label: {
              show: true,
            },
            emphasis: {
              focus: "series",
            },
            data: [320, 302, 301, 334, 390, 330, 320, 301, 302],
            itemStyle: {
              normal: {
                color: "#0C68C4", //改变折线点的颜色
              },
            },
          },
          {
            name: "空压站2",
            type: "bar",
            stack: "total",
            label: {
              show: true,
            },
            emphasis: {
              focus: "series",
            },
            data: [120, 132, 101, 134, 90, 230, 210, 203, 123],
            itemStyle: {
              normal: {
                color: "#248FF7", //改变折线点的颜色
              },
            },
          },
          {
            name: "空压站3",
            type: "bar",
            stack: "total",
            label: {
              show: true,
            },
            emphasis: {
              focus: "series",
            },
            data: [220, 182, 191, 234, 290, 330, 310, 241, 543],
            itemStyle: {
              normal: {
                color: "#63A4E6", //改变折线点的颜色
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
