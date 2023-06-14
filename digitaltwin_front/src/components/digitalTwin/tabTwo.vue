<template>
  <div style="display: flex; flex-direction: row" class="content">
    <div id="m1" style="width: 150px; height: 150px"></div>
    <div style="margin-left:30px">
      <p style="margin-bottom: -2px;margin-top:-10px">今日已用气量</p>
      <el-button type="danger">{{water}} m2</el-button>
      <el-divider style="margin-top:-20px"></el-divider>
      <p style="margin-bottom: -2px;margin-top:-20px">今日已累计用电</p>
      <el-button type="primary">{{elec}} kW.h</el-button>
    </div>
  </div>
</template>

<script>
import { getWaterAndElectricity } from "@/api/c++_right";
export default {
  mounted() {
    this.draw();
    this.getWaterAndElectricity();
  },
  data() {
    return {
      water:3.00,
      elec:20.00
    };
  },
  methods: {
    draw() {
      var myChart = this.$echarts.init(document.getElementById("m1"));
      var txt = 0.1009;
      var option = {
        title: {
          text: "气电比\n" + txt + "%\n" + "kw.h/m2",
          x: "center",
          y: "center",
          textStyle: {
            fontWeight: "normal",
            color: "#000000",
            fontSize: "20",
          },
        },
        color: "rgba(0,0,0,0.3)",

        series: [
          {
            name: "Line 1",
            type: "pie",
            clockWise: true,
            radius: ["65%", "80%"],
            itemStyle: {
              normal: {
                label: {
                  show: false,
                },
                labelLine: {
                  show: false,
                },
              },
            },
            hoverAnimation: false,
            data: [
              {
                value: 70,
                name: "已使用",
                itemStyle: {
                  normal: {
                    color: "#409EFF",
                    label: {
                      show: false,
                    },
                    labelLine: {
                      show: false,
                    },
                  },
                },
              },
              {
                name: "未使用",
                value: 30,
              },
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
    getWaterAndElectricity() {
      var that=this
      getWaterAndElectricity()
        .then((data) => {
          console.log(data); // 处理获取到的数据
          that.water=(data+40000).toFixed(2)
          that.elec=(data+40000-10232).toFixed(2)
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
};
</script>

<style scoped>
.content {
  border-radius: 10px;
  box-shadow: 20px 20px 60px #bebebe, -20px -20px 60px #ffffff;
}
</style>
