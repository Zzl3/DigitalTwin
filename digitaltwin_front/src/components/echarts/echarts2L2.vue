<template>
  <div>
    <div class="texttable">
      <span>优化前：{{this.before}}</span>
      <br>
      <span>优化后：{{this.after}}</span>
    </div>
    <div id="2l2" style="width: 100%; height: 220%"></div>
  </div>
</template>

<script>
import { getConsumption, getnewConsumption } from "@/api/consumption";
export default {
  data() {
    return {
      before: 0,
      after: 0,
    };
  },
  mounted() {
    this.draw();
    this.getData2L2();
  },
  methods: {
    draw() {
      var myChart = this.$echarts.init(document.getElementById("2l2"));
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
              rotate: 0,
              textStyle: {
                color: "rgba(0,0,0,.6)",
                fontSize: 14,
              },
              formatter: "{value} h", // 添加横坐标单位
            },
            axisLine: {
              lineStyle: {
                color: "rgba(0,0,0,.1)",
              },
            },
            data: (function () {
              let list = [];
              for (let i = 0; i <= 1440; i++) {
                list.push(Math.round(i / 60));
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
          
          this.before = data[data.length - 1].before;
          this.after = data[data.length - 1].after;
          chart.setOption(option);
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
    getData2L2new() {
      //这里从数据取新的值
      getnewConsumption()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("2l2"));
          var option = chart.getOption();
          console.log(data);
          for (let i = 0; i < data.length; i++) {
            // console.log(data[i].before);
            // console.log(data[i].after);
            //before不需要改变
            // option.series[0].data[i] = data[i].before;
            option.series[1].data[i] = data[i].after;
          }
          //this.before = data[data.length - 1].before;
          this.after = data[data.length - 1].after;
          chart.setOption(option);
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
  created() {
    this.$watch(
      () => this.$root.iftemp,
      (newVal) => {
        console.log("Global iftemp changed:", newVal);
        if (newVal == "true") {
          this.getData2L2new();
        }
      }
    );
  },
};
</script>

<style scoped>
.texttable{
  font-size: 16px;
  text-align: left;
  margin-top: -40px;
  margin-bottom: 10px;
  margin-left:450px;
}
</style>
