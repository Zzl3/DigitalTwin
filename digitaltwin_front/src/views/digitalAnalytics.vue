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
    <div class="content" id="boxr1">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">系统整体分析</p>
      </div>
      <EchartsR1 style="margin-top: -20px; position: absolute"></EchartsR1>
    </div>
    <div class="content" id="boxr2">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">空压系统整体分析</p>
      </div>
      <EchartsR2 style="margin-top: -15px; position: absolute"></EchartsR2>
    </div>
    <div class="content" id="boxr3">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">各空压机耗电量</p>
      </div>
      <EchartsR3 style="margin-top: -20px; position: absolute"></EchartsR3>
    </div>
    <div class="content" id="center">123</div>
  </div>
</template>

<script>
import EchartsL1 from "@/components/echarts/echartsL1.vue";
import EchartsL2 from "@/components/echarts/echartsL2.vue";
import EchartsL3 from "@/components/echarts/echartsL3.vue";
import EchartsR1 from '@/components/echarts/echartsR1.vue';
import EchartsR2 from '@/components/echarts/echartsR2.vue';  // 其实使用 element-ui实现
import EchartsR3 from '@/components/echarts/echartsR3.vue';

export default {
  components: {
    EchartsL1,
    EchartsL2,
    EchartsL3,
    EchartsR1,
    EchartsR2,
    EchartsR3,
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
