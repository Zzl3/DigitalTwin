<template>
  <div id="2l2" style="width: 705px; height: 280px"></div>
</template>

<script>
import { getConsumption } from "@/api/consumption";
export default {
  mounted() {
    this.draw();
    this.getData2L2();
  },
  methods: {
    draw() {
      var myChart = this.$echarts.init(document.getElementById("2l2"));
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
              for (let i = 0; i <= 1044; i++) {
                list.push(i);
              }
              return list;
            })(),
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
              formatter: "{value} kw",
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
                      color: "rgba(66, 115, 243, .2)",
                    },
                    {
                      offset: 1,
                      color: "rgba(66, 115, 243, 0)",
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
                      color: "rgba(65, 189, 195,.2)",
                    },
                    {
                      offset: 1,
                      color: "rgba(65, 189, 195, 0)",
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
              32.9,
              18.31,
              21.59,
              24.42,
              34.03,
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
    getData2L2() {
      getConsumption()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("2l2"));
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
