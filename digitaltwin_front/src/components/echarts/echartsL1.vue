<template>
  <div id="l1" style="width: 100%; height: 120%"></div>
</template>

<script>
import { getData } from "@/api/userloading";
export default {
  mounted() {
    this.draw();
    this.getDataL1();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("l1"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });
      // 指定图表的配置项和数据
      var option = {
        xAxis: {
          type: "value",
          // 显示单位
          axisLabel: {
            formatter: "{value} KPa",
          },
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
            "用户10",
            "用户11",
            "用户12",
            "用户13",
            "用户14",
            "用户15",
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
                  color: "#134FFE",
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
                fontSize: "5%",
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
    getDataL1() {
      getData()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("l1"));
          var option = chart.getOption();
          var listData = option.series[0].data;
          console.log(listData); // 处理获取到的数据
          var tempdata = data[15]; //用16时获得的数据
          for (let i = 0; i < 15; i++) {
            option.series[0].data[i] = tempdata['user' + i]
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
