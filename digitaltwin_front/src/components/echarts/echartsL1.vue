<template>
  <div id="l1" style="width: 400px; height:265px"></div>
</template>

<script>
export default {
  mounted() {
    this.draw();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("l1"));
      // 指定图表的配置项和数据
      var option = {
        xAxis: {
          type: "value",
        },
        yAxis: {
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
        series: [
          {
            data: [120, 200, 150, 80, 70, 110, 130, 160, 53],
            type: "bar",
            itemStyle: {
              color: new this.$echarts.graphic.LinearGradient(0, 0, 1, 0, [
                {
                  offset: 0,
                  color: "#248ff7",
                },
                {
                  offset: 1,
                  color: "rgba(22,75,247,0.1)",
                },
              ]),
            },
            label: {
              //柱体上显示数值
              show: true, //开启显示
              position: "right", //在右方显示
              textStyle: {
                //数值样式
                fontSize: "11px",
                color: "#666",
              },
              formatter: "{c}",
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
