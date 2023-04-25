<template>
    <div id="2l1" style="width: 705px; height: 280px"></div>
  </template>
  
  <script>
  export default {
    mounted() {
      this.draw();
    },
    methods: {
      draw() {
        // 基于准备好的dom，初始化echarts实例
        var myChart = this.$echarts.init(document.getElementById("2l1"));
  
        var option = {
          tooltip: {
            trigger: "axis",
          },
          legend: {
            top: "5%",
            data: ["空压站1", "空压站2", "空压站3"],
          },
          grid: {
            left: "3%",
            right: "4%",
            bottom: "3%",
            containLabel: true,
          },
          xAxis: {
            type: "category",
            boundaryGap: false,
            data: (function () {
              let list = [];
              for (let i = 0; i <= 24; i++) {
                list.push(i + ":00");
              }
              return list;
            })(),
          },
          yAxis: {
            type: "value",
          },
          series: [
            {
              itemStyle: {
                normal: {
                  color: {
                    type: "linear",
                    x: 0,
                    y: 1,
                    x2: 0,
                    y2: 0,
                    colorStops: [
                      { offset: 0, color: "#FFFFFF" },
                      { offset: 1, color: "#5b82e8" },
                    ],
                  },
                },
              },
              name: "空压站1",
              type: "line",
              step: "start",
              data: [
                1,
                1,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                1,
                0,
                0,
                0,
                0,
              ],
              areaStyle: {},
            },
            {
              itemStyle: {
                normal: {
                  color: {
                    type: "linear",
                    x: 0,
                    y: 1,
                    x2: 0,
                    y2: 0,
                    colorStops: [
                      { offset: 0, color: "#FFFFFF" },
                      { offset: 1, color: "#829adb" },
                    ],
                  },
                },
              },
              name: "空压站2",
              type: "line",
              step: "start",
              data: [
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
              ],
              areaStyle: {},
            },
            {
              itemStyle: {
                normal: {
                  color: {
                    type: "linear",
                    x: 0,
                    y: 1,
                    x2: 0,
                    y2: 0,
                    colorStops: [
                      { offset: 0, color: "#FFFFFF" },
                      { offset: 1, color: "#195bfa" },
                    ],
                  },
                },
              },
              name: "空压站3",
              type: "line",
              step: "start",
              data: [
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                0,
                0,
                0,
                0,
                0,
              ],
              areaStyle: {},
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
  