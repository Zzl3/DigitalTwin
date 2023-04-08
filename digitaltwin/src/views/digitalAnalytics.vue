<template>
  <div>
    <div class="content" id="boxl1">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">用户负荷情况</p>
      </div>
      <EchartsL1 style="margin-top: -70px; position: absolute"></EchartsL1>
    </div>
    <div class="content" id="boxl2">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">空压站使用情况</p>
      </div>
      <EchartsL2 style="margin-top: -50px; position: absolute"></EchartsL2>
    </div>
    <div class="content" id="boxl3">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">近日耗电量</p>
      </div>
      <EchartsL3 style="margin-top: -50px; position: absolute"></EchartsL3>
    </div>
    <div class="content" id="boxr1">123</div>
    <div class="content" id="boxr2">123</div>
    <div class="content" id="boxr3">123</div>
    <div class="content" id="center">123</div>
  </div>
</template>

<script>
import EchartsL1 from "@/components/echarts/echartsL1.vue";
import EchartsL2 from "@/components/echarts/echartsL2.vue";
import EchartsL3 from "@/components/echarts/echartsL3.vue";
export default {
  components: {
    EchartsL1,
    EchartsL2,
    EchartsL3,
  },
};
</script>

<style scoped>
#boxl1 {
  width: 400px;
  height: 220px;
  position: absolute;
  left: 0;
  top: 0;
}
#boxl2 {
  width: 400px;
  height: 220px;
  position: absolute;
  left: 0;
  top: 255px;
}
#boxl3 {
  width: 400px;
  height: 220px;
  position: absolute;
  left: 0;
  top: 510px;
}
#boxr1 {
  width: 400px;
  height: 220px;
  position: absolute;
  right: 0;
  top: 0;
}
#boxr2 {
  width: 400px;
  height: 220px;
  position: absolute;
  right: 0;
  top: 255px;
}
#boxr3 {
  width: 400px;
  height: 220px;
  position: absolute;
  right: 0;
  top: 510px;
}
#center {
  width: 600px;
  height: 220px;
  position: absolute;
  left: 50%;
  top: 620px;
  transform: translate(-50%, -50%);
}
.content {
  border-radius: 10px;
  background: rgba(255, 255, 255, 0.5);
  box-shadow: 20px 20px 60px #bebebe, -20px -20px 60px #ffffff;
}
.titletext {
  display: inline-block;
  margin-left: 10px;
}
</style>
