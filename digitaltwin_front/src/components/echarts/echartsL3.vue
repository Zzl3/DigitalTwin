<template>
  <div id="l3" style="width: 95%; height: 100%"></div>
</template>

<script>
import { getData } from "@/api/recentconsumption";
export default {
  mounted() {
    this.draw();
    this.getDataL3();
    this.setDate();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("l3"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });
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
            data: ["4.30", "5.1", "5.2", "5.3", "5.4", "5.5"],
          },
        ],
        yAxis: [
          {
            name: "KW·h",
            type: "value",
            axisLabel: {
              formatter: "{value}",
              fontSize: 8,
            },
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
    getDataL3() {
      getData()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("l3"));
          var option = chart.getOption();
          var listData = option.series[0].data;
          console.log(listData); // 处理获取到的数据
          console.log(data); // 处理获取到的数据
          for (let i = 0; i < data.length; i++) {
            option.series[0].data[i] = data[i].air1
            option.series[1].data[i] = data[i].air2
            option.series[2].data[i] = data[i].air3
          }
          chart.setOption(option);
        })
        .catch((error) => {
          console.log(error); // 处理错误
        })
    },
    setDate() {
      var chart = this.$echarts.getInstanceByDom(document.getElementById("l3"));
      var option = chart.getOption();
      // var listDate = option.xAxis[0].data;
      var listDate = [];
      var date = new Date();
      var month = date.getMonth() + 1;
      for (let i = 4; i >= 0; i--) {
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

<style></style>
