<template>
  <div id="r3" style="width: 90%; height: 110%"></div>
</template>

<script>
import { getData } from "@/api/airelectricity";
export default {
  mounted() {
    this.draw();
    this.getDataR3();
  },
  methods: {
    draw() {
      // 基于准备好的dom，初始化echarts实例
      var myChart = this.$echarts.init(document.getElementById("r3"));
      window.addEventListener('resize', function () {
        myChart.resize();
      });
      // 指定图表的配置项和数据
      var option = {
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b}: {c} ({d}%)",
        },
        legend: {
          data: ["空压站1", "空压站2", "空压站3"],
        },
        series: [
          {
            // name: 'Access From',
            type: "pie",
            selectedMode: "single",
            radius: [0, "30%"],
            label: {
              show: true,
              position: "inner",
              formatter: " {t1|各空压站} \n {c|{c}} \n {b|} \n {t2|耗电总量} ",
              // fontSize: 20,
              rich: {
                t1: {
                  fontSize: 10,
                  align: "center",
                  verticalAlign: "middle",
                  lineHeight: 25,
                },
                t2: {
                  fontSize: 10,
                  align: "center",
                  verticalAlign: "middle",
                  lineHeight: 0,
                },
                b: {
                  // 实现下划线
                  width: 40,
                  height: 0,
                  borderWidth: 2,
                  borderColor: "#FFF",
                  lineHeight: 0,
                },
                c: {
                  borderColor: "#FFF",
                  align: "center",
                  verticalAlign: "middle",
                  fontSize: 15,
                  lineHeight: 0,
                  textDecoration: "underline",
                },
              },
              offset: [0, -20], // 调整中间圆形的文字位置 第一个数字表示左右 第二个数据表示上下
            },
            labelLine: {
              show: false,
            },
            data: [{ value: 5132, name: "各空压站耗电量" }],
          },
          {
            // name: 'Access From',
            fontSize: 2,
            type: "pie",
            radius: ["33%", "40%"],
            labelLine: {
              length: 30,
            },
            label: {
              formatter: "  {b|{b}}  \n  {c|{c} KW·h}  ",
              backgroundColor: "#F6F8FC",
              borderColor: "#8C8D8E",
              borderWidth: 1,
              borderRadius: 4,
              rich: {
                b: {
                  color: "#4C5058",
                  fontSize: 10,
                  // fontWeight: 'bold',
                  // lineHeight: 33
                },
                c: {
                  color: "#8C8D8E",
                  align: "center",
                  fontWeight: "bold",
                  fontSize: 10,
                },
              },
            },
            data: [
              { value: 2521, name: "空压站1", itemStyle: { color: "#1953FC" } },
              { value: 2154, name: "空压站2", itemStyle: { color: "#668BF6" } },
              { value: 457, name: "空压站3", itemStyle: { color: "#4B79FF" } },
            ],
          },
        ],
      };
      // 使用刚指定的配置项和数据显示图表。
      myChart.setOption(option);
      myChart.resize();
    },
    getDataR3() {
      getData()
        .then((data) => {
          var chart = this.$echarts.getInstanceByDom(document.getElementById("r3"));
          var option = chart.getOption();
          var tempdata = data[0];
          option.series[0].data[0].value = parseFloat(tempdata.total).toFixed(2);
          for (let i = 0; i < 3; i++) {
            var d = i + 1;
            option.series[1].data[i].value = tempdata["air" + d];
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
