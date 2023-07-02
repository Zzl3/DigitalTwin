<template>
  <div id="l2" style="width: 90%; height: 85%"></div>
</template>

<script>
import { getData } from "@/api/userusing";
export default {
  mounted() {
    this.draw();
    this.getDataL2();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("l2"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });
      // 指定图表的配置项和数据
      var option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            // Use axis to trigger tooltip
            type: "line", // 'shadow' as default; can also be 'line' or 'shadow'
          },
        },
        legend: {
          top: '20%'
        },
        grid: {
          left: "3%",
          right: "3%",
          bottom: "0%",
          containLabel: true,
        },
        xAxis: {
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
        yAxis: {
          type: "value",
          axisLabel: {
            formatter: "{value} %",
          },
        },
        series: [
          {
            name: "空压站1",
            type: "bar",
            stack: "total",
            label: {
              show: true,
              // 设置默认文字样式
              textStyle: {
                fontSize: 8
              },
              formatter: function (params) {
                var res = params.value + "%";
                return res;
              },
            },
            emphasis: {
              focus: "series",
            },
            data: [320, 302, 301, 334, 390, 330, 320, 301, 302],
            itemStyle: {
              normal: {
                color: "#1157fc", //改变折线点的颜色
              },
            },
          },
          {
            name: "空压站2",
            type: "bar",
            stack: "total",
            label: {
              show: true,
              // 设置默认文字样式
              textStyle: {
                fontSize: 8
              },
              formatter: function (params) {
                var res = params.value + "%";
                return res;
              },
            },
            emphasis: {
              focus: "series",
            },
            data: [120, 132, 101, 134, 90, 230, 210, 203, 123],
            itemStyle: {
              normal: {
                color: "#658df6", //改变折线点的颜色
              },
            },
          },
          {
            name: "空压站3",
            type: "bar",
            stack: "total",
            label: {
              show: true,
              // 设置默认文字样式
              textStyle: {
                fontSize: 8
              },
              formatter: function (params) {
                var res = params.value + "%";
                return res;
              },
            },
            emphasis: {
              focus: "series",
            },
            data: [220, 182, 191, 234, 290, 330, 310, 241, 543],
            itemStyle: {
              normal: {
                color: "#88a2e6", //改变折线点的颜色
              },
            },
          },
        ],
      };
      // 使用刚指定的配置项和数据显示图表。
      myChart.setOption(option);
    },
    getDataL2() {
      getData()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("l2"));
          var option = chart.getOption();
          var listData = option.series[0].data;
          console.log(listData); // 处理获取到的数据
          console.log(data); // 处理获取到的数据
          for (let i = 0; i < data.length; i++) {
            option.series[0].data[i] = parseInt(data[i].air1 * 100)
            option.series[1].data[i] = parseInt(data[i].air2 * 100)
            option.series[2].data[i] = parseInt(data[i].air3 * 100)
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
