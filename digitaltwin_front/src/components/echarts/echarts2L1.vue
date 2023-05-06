<template>
  <div id="2l1" style="width: 705px; height: 280px"></div>
</template>

<script>
import { getOnandoffs } from "@/api/onandoff";
export default {
  mounted() {
    this.draw();
    this.getData2L1();
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
            for (let i = 0; i <= 1440; i++) {
              list.push(i);
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
    getData2L1() {
      getOnandoffs()
        .then((data) => {
          console.log(data);
          var chart = this.$echarts.getInstanceByDom(document.getElementById("2l1"));
          var option = chart.getOption();
          var listData = option.series[0].data;
          console.log(listData); // 处理获取到的数据
          for (let i = 0; i < data.length; i++) {
            // console.log(data[i].air1);
            // console.log(data[i].air2);
            option.series[0].data[i] = data[i].air1;
            option.series[1].data[i] = data[i].air2;
            option.series[2].data[i] = data[i].air3;
          }
          chart.setOption(option);
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
};
</script>

<style></style>
