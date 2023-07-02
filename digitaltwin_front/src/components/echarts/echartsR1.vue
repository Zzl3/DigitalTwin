<template>
  <div id="r1" style="width: 90%; height: 110%"></div>
</template>

<script>
import { getData } from "@/api/systemelectricity";
export default {
  mounted() {
    this.draw();
    this.getDataR1();
    this.setDate();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("r1"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });
      // 指定图表的配置项和数据
      var option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "cross",
          },
        },
        toolbox: {
          feature: {
            dataView: { show: false, readOnly: false },
            magicType: { show: false, type: ["line", "bar"] },
            restore: { show: false },
            saveAsImage: { show: false },
          },
        },
        legend: {
          top: "10%",
          data: ["电费", "日平均压力"],
        },
        xAxis: [
          {
            type: "category",
            data: ["2018", "2019", "2020", "2021", "2022", "2023"],
            axisPointer: {
              type: "shadow",
            },
          },
        ],
        yAxis: [
          {
            type: "value",
            name: "电费/元",
            interval: 5000,
            axisLabel: {
              formatter: "{value}",
              fontSize: 10,
            },
          },
          {
            type: "value",
            name: "日平均压力/KPa",
            nameTextStyle: {
              fontSize: 10,
            },
            interval: 500,
            axisLabel: {
              formatter: "{value}",
              fontSize: 10,
            },
          },
        ],
        series: [
          {
            name: "电费",
            type: "bar",
            tooltip: {
              valueFormatter: function (value) {
                return value;
              },
            },
            barWidth: 20,//柱图宽度
            data: [1000, 1500, 2000, 2500, 3000],
            itemStyle: {
              color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [
                {
                  offset: 0,
                  color: "#134FFE",
                },
                {
                  offset: 1,
                  color: "rgba(22,75,247,0.1)",
                },
              ]),
              barBorderRadius: [5, 5, 5, 5],//柱形图圆角设置

            },
          },
          {
            name: "日平均压力",
            type: "line",
            yAxisIndex: 1,
            data: [10, 15, 20, 23, 24],
            itemStyle: {
              normal: {
                color: "#1953FC", //改变折线图的颜色
              },
            },
          },
        ],
      };
      // 使用刚指定的配置项和数据显示图表。
      myChart.setOption(option);
    },
    getDataR1() {
      getData()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("r1"));
          var option = chart.getOption();
          var listData = option.series[0].data;
          console.log(listData); // 处理获取到的数据
          for (let i = 0; i < data.length; i++) {
            option.series[0].data[i] = data[i].fee
            option.series[1].data[i] = data[i].pressure
          }
          chart.setOption(option);
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
    setDate() {
      var chart = this.$echarts.getInstanceByDom(document.getElementById("r1"));
      var option = chart.getOption();
      // var listDate = option.xAxis[0].data;
      var listDate = [];
      var date = new Date();
      var month = date.getMonth() + 1;
      for (let i = 5; i >= 0; i--) {
        var day = date.getDate();
        day = day - i;
        var time = month + "." + day;
        listDate.push(time);
      }
      option.xAxis[0].data = listDate;
      chart.setOption(option);
    }
  },
};
</script>
