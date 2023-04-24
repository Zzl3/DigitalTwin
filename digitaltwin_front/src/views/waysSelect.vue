<template>
<div class="about">
    <div class="content" id="box2l1">
        <div style="text-align: left">
            <i class="el-icon-news titletext"></i>
            <p class="titletext">优化后开关站时间</p>
        </div>
        <EchartsL1 style="margin-top: -15px; position: absolute"></EchartsL1>
    </div>
    <div class="content" id="box2l2">
        <div style="text-align: left">
            <i class="el-icon-news titletext"></i>
            <p class="titletext">优化前后耗电量</p>
        </div>
        <EchartsL2 style="margin-top: -50px; position: absolute"></EchartsL2>
    </div>
    <div class="content" id="box2r1">
        <div style="text-align: left">
            <i class="el-icon-news titletext"></i>
            <p class="titletext">设置空压站开关时间</p>
        </div>
    </div>
    <div class="content" id="box2r2">
        <div style="text-align: left">
            <i class="el-icon-news titletext"></i>
            <p class="titletext">设置补气压力</p>
        </div>
    </div>
</div>
</template>

<script>
import EchartsL1 from "@/components/echarts/echarts2L1.vue";
import EchartsL2 from "@/components/echarts/echarts2L2.vue";

export default {
    components: {
    EchartsL1,
    EchartsL2,
  },
}
</script>

<style scoped>
#box2l1 {
    width: 740px;
    height: 330px;
    position: absolute;
    left: 0;
    top: 0;
}
  
#box2l2 {
    width: 740px;
    height: 330px;
    position: absolute;
    left: 0;
    top: 370px;
}

#box2r1 {
    width: 740px;
    height: 330px;
    position: absolute;
    right: 0;
    top: 0px;
}

#box2r2 {
    width: 740px;
    height: 330px;
    position: absolute;
    right: 0;
    top: 370px;
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