<template>
  <div id="2l1" style="width: 100%; height: 80%"></div>
</template>

<script>
import { getOnandoffs,getnewOnandoffs } from "@/api/onandoff";
export default {
  mounted() {
    this.draw();
    this.getData2L1();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("2l1"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });

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
          axisLabel: {
            formatter: "{value} h", // 添加横坐标单位
          },
          data: (function () {
            let list = [];
            for (let i = 0; i <= 1440; i++) {
              list.push(Math.round(i / 60));
            }
            return list;
          })(),

        },
        yAxis: {
          type: "value",
          // axisLabel: {
          //   formatter: function (value) {
          //     // 只显示 0 和 1
          //     if (value === 0 || value === 1)
          //       return value;
          //   }
          // }
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
    getData2L1new() {
      //这里从数据取新的值
      getnewOnandoffs()
        .then((data) => {
          console.log(data);
          var chart = this.$echarts.getInstanceByDom(document.getElementById("2l1"));
          var option = chart.getOption();
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
    }
  },
  created() {
    this.$watch(
      () => this.$root.iftemp,
      (newVal) => {
        console.log('Global iftemp changed:', newVal)
        if (newVal == "true") {
          this.getData2L1new();
        }
      }
    )
  },
};
</script>

<style></style>
