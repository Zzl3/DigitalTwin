<template>
  <div id="m4" style="width: 90%; height: 65%"></div>
</template>

<script>
import { getConsumption } from "@/api/consumption";
export default {
  mounted() {
    this.draw();
    this.getDataM4();
  },
  methods: {
    draw() {
      var myChart = this.$echarts.init(document.getElementById("m4"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });
      var option = {
        tooltip: {
          trigger: "axis",
          axisPointer: { type: "shadow" },
          // formatter:'{c}' ,
        },
        grid: {
          left: "0",
          top: "30",
          right: "20",
          bottom: "5",
          containLabel: true,
        },
        legend: {
          data: ["优化前", "优化后"],
          right: "center",
          top: 0,
          textStyle: {
            color: "#000000",
          },
          itemWidth: 12,
          itemHeight: 10,
          // itemGap: 35
        },
        xAxis: [
          {
            type: "category",
            boundaryGap: false,
            axisLabel: {
              formatter: "{value} h", // 添加横坐标单位
              rotate: 0,
              textStyle: {
                color: "rgba(0,0,0,.6)",
                fontSize: 14,
              },
            },
            axisLine: {
              lineStyle: {
                color: "rgba(0,0,0,.1)",
              },
            },
            data: (function () {
              let list = [];
              for (let i = 0; i <= 1440; i++) {
                // i 是多少分钟，转成多少小时
                list.push(Math.round(i / 60));
              }
              return list;
            })(),
            // data: [
            //   "00:00",
            //   "02:00",
            //   "04:00",
            //   "06:00",
            //   "08:00",
            //   "10:00",
            //   "12:00",
            //   "14:00",
            //   "16:00",
            //   "18:00",
            //   "20:00",
            //   "22:00",
            // ],
          },
          {
            axisPointer: { show: false },
            axisLine: { show: false },
            position: "bottom",
            offset: 20,
          },
        ],
        yAxis: [
          {
            type: "value",
            axisTick: { show: false },
            // splitNumber: 6,
            axisLine: {
              lineStyle: {
                color: "rgba(0,0,0,.1)",
              },
            },
            axisLabel: {
              formatter: "{value} KW·h",
              textStyle: {
                color: "rgba(0,0,0,.6)",
                fontSize: 14,
              },
            },

            splitLine: {
              lineStyle: {
                color: "rgba(0,0,0,.1)",
              },
            },
          },
        ],
        series: [
          {
            name: "优化前",
            type: "line",
            smooth: true,
            symbol: "circle",
            symbolSize: 5,
            showSymbol: false,
            lineStyle: {
              normal: {
                color: "rgba(66, 115, 243, 1)",
                width: 2,
              },
            },
            areaStyle: {
              normal: {
                color: new this.$echarts.graphic.LinearGradient(
                  0,
                  0,
                  0,
                  1,
                  [
                    {
                      offset: 0,
                      color: "rgba(66, 115, 243, .5)",
                    },
                    {
                      offset: 1,
                      color: "rgba(228, 228, 126, 0)",
                    },
                  ],
                  false
                ),
                shadowColor: "rgba(0, 0, 0, 0.1)",
              },
            },
            itemStyle: {
              normal: {
                color: "rgba(66, 115, 243, 1)",
                borderColor: "rgba(66, 115, 243, .1)",
                borderWidth: 12,
              },
            },
            data: [
              62.5,
              64.4,
              66.1,
              64.9,
              70.1,
              67.2,
              67.0,
              63.42,
              70.12,
              68.94,
              67.27,
              66.1,
            ],
          },
          {
            name: "优化后",
            type: "line",
            smooth: true,
            symbol: "circle",
            symbolSize: 5,
            showSymbol: false,
            lineStyle: {
              normal: {
                color: "rgba(65, 189, 195, 1)",
                width: 2,
              },
            },
            areaStyle: {
              normal: {
                color: new this.$echarts.graphic.LinearGradient(
                  0,
                  0,
                  0,
                  1,
                  [
                    {
                      offset: 0,
                      color: "rgba(65, 189, 195, .5)",
                    },
                    {
                      offset: 1,
                      color: "rgba(255, 128, 128, 0)",
                    },
                  ],
                  false
                ),
                shadowColor: "rgba(0, 0, 0, 0.1)",
              },
            },
            itemStyle: {
              normal: {
                color: "rgba(65, 189, 195, 1)",
                borderColor: "rgba(65, 189, 195, .1)",
                borderWidth: 12,
              },
            },
            data: [
              0.5,
              5.2,
              6.6,
              11.2,
              42.1,
              26.0,
              20.2,
              18.31,
              21.59,
              24.42,
              34.03,
              32.9,
            ],
          },
        ],
      };
      // 使用刚指定的配置项和数据显示图表。
      myChart.setOption(option);
      window.addEventListener("resize", function () {
        myChart.resize();
      });
    },

    getDataM4() {
      getConsumption()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("m4"));
          var option = chart.getOption();
          var listData = option.series[0].data;
          console.log(listData); // 处理获取到的数据
          for (let i = 0; i < data.length; i++) {
            // console.log(data[i].before);
            // console.log(data[i].after);
            option.series[0].data[i] = data[i].before;
            option.series[1].data[i] = data[i].after;
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
