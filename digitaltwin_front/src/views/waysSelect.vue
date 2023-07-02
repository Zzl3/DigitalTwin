<template>
  <div class="about">
    <div class="content" id="box2l1">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">优化后开关站时间</p>
      </div>
      <EchartsL1></EchartsL1>
    </div>
    <div class="content" id="box2l2">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">优化前后耗电量</p>
      </div>
      <EchartsL2></EchartsL2>
    </div>
    <div class="content" id="box2r1">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">设置空压站开关时间</p>
        <el-tooltip class="item" content="时间段1应在0-12之间，时间段2应在12-24之间" placement="top">
          <i style="margin-left: 20px" class="el-icon-question"></i>
        </el-tooltip>
      </div>
      <ElementR1 :R1Data="pR1Data" @R1DataChanged="updateR1ParentData"></ElementR1>
    </div>
    <div class="content" id="box2r2">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">设置补气压力</p>
      </div>
      <ElementR2 :R2Data="pR2Data" @R2DataChanged="updateR2ParentData" @click="updateBestData"></ElementR2>
      <div class="threeButton">
        <el-button type="primary" style="width: 30%" @click="getdata">一键应用优化效果</el-button>
        <el-button type="primary" style="width: 30%" @click="updatedata">应用</el-button>
        <el-button type="primary" style="width: 30%" @click="resetdata">重置</el-button>
      </div>
    </div>
  </div>
</template>

<script>
import { upgradeData } from "@/api/runstrategy";
import EchartsL1 from "@/components/echarts/echarts2L1.vue";
import EchartsL2 from "@/components/echarts/echarts2L2.vue";
import ElementR1 from "@/components/elements/elementsR1";
import ElementR2 from "@/components/elements/elementsR2";
export default {
  components: {
    EchartsL1,
    EchartsL2,
    ElementR1,
    ElementR2,
  },
  data() {
    return {
      pR1Data: {
        S1time1: 7.14,
        S1time2: 13.36,
        S2time1: 8.03,
        S2time2: 12.56,
        S3time1: 8.53,
        S3time2: 12.42,
      },
      pR2Data: {
        S1time1: 833.982,
        S1time2: 811.584,
        S2time1: 819.897,
        S2time2: 815.373,
        S3time1: 827.273,
        S3time2: 806.116,
      },
    };
  },
  methods: {
    //更新父组件的数据
    updateR1ParentData(R1Data) {
      this.pR1Data = R1Data;
      console.log("下面是父组件中R1的数据---");
      console.log(this.pR1Data);
    },
    updateR2ParentData(R2Data) {
      this.pR2Data = R2Data;
      console.log("下面是父组件中R2的数据---");
      console.log(this.pR2Data);
    },
    getdata() {
      this.$router.go(0); //直接从数据库拿数据
    },
    updatedata() {
      if (this.pR1Data.S1time1 < 0 || this.pR1Data.S1time1 > 12) {
        this.$message({
          message: "空压站1时间1应在0-12之间",
          type: "warning",
        });
        return;
      }
      if (this.pR1Data.S1time2 < 12 || this.pR1Data.S1time2 > 24) {
        this.$message({
          message: "空压站1时间2应在12-24之间",
          type: "warning",
        });
        return;
      }
      if (this.pR1Data.S2time1 < 0 || this.pR1Data.S2time1 > 12) {
        this.$message({
          message: "空压站2时间1应在0-12之间",
          type: "warning",
        });
        return;
      }
      if (this.pR1Data.S2time2 < 12 || this.pR1Data.S2time2 > 24) {
        this.$message({
          message: "空压站2时间2应在12-24之间",
          type: "warning",
        });
        return;
      }
      if (this.pR1Data.S3time1 < 0 || this.pR1Data.S3time1 > 12) {
        this.$message({
          message: "空压站3时间1应在0-12之间",
          type: "warning",
        });
        return;
      }
      if (this.pR1Data.S3time2 < 12 || this.pR1Data.S3time2 > 24) {
        this.$message({
          message: "空压站3时间2应在12-24之间",
          type: "warning",
        });
        return;
      } else {
        //传递给后端数据，从后端拿到优化数据
        const data = {
          s1time1: this.pR1Data.S1time1,
          s1time2: this.pR1Data.S1time2,
          s2time1: this.pR1Data.S2time1,
          s2time2: this.pR1Data.S2time2,
          s3time1: this.pR1Data.S3time1,
          s3time2: this.pR1Data.S3time2,
          s1pressure1: this.pR2Data.S1time1,
          s1pressure2: this.pR2Data.S1time2,
          s2pressure1: this.pR2Data.S2time1,
          s2pressure2: this.pR2Data.S2time2,
          s3pressure1: this.pR2Data.S3time1,
          s3pressure2: this.pR2Data.S3time2,
        };
        upgradeData(data)
          .then((data) => {
            console.log(data); // 处理获取到的数据
            //由于存到了数据库中，所以直接刷新页面重新从数据库获得数据
            //this.$router.go(0)
            this.$message({
              message: "应用优化成功",
              type: "success",
            });
          })
          .catch((error) => {
            console.log(error); // 处理错误
          });
        this.$root.iftemp = "true"; //更新数据

        setTimeout(() => {
          // 方法区
          this.$root.iftemp = "false";
        }, 500);
      }
    },
    resetdata() {
      this.$router.go(0); //直接从数据库拿数据
    },
  },
};
</script>

<style>
#box2l1 {
  width: 44%;
  height: 42%;
  position: absolute;
  left: 2%;
  top: 1%;
  margin: 0 0;
}

#box2l2 {
  width: 44%;
  height: 42%;
  position: absolute;
  left: 2%;
  top: 44%;
  margin: 0 0;
}

#box2r1 {
  width: 44%;
  height: 30%;
  position: absolute;
  top: 1%;
  margin: 0 0;
  right: 2%;
}

#box2r2 {
  width: 44%;
  height: 42%;
  position: absolute;
  margin: 0 0;
  right: 2%;
  top: 35%;
}

.content {
  border-radius: 5%;
  background: rgba(255, 255, 255, 0.5);
  box-shadow: 20px 20px 60px #bebebe, -20px -20px 60px #ffffff;
}

.titletext {
  display: inline-block;
  margin-left: 5%;
}

.threeButton {
  margin-top: 5%;
}
</style>
