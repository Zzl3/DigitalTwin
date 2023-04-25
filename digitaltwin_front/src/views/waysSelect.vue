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
            </div>
            <ElementR1></ElementR1>

        </div>
        <div class="content" id="box2r2">
            <div style="text-align: left">
                <i class="el-icon-news titletext"></i>
                <p class="titletext">设置补气压力</p>
            </div>
            <ElementR2></ElementR2>
        </div>
    </div>
</template>

<script>
import EchartsL1 from "@/components/echarts/echarts2L1.vue";
import EchartsL2 from "@/components/echarts/echarts2L2.vue";
import ElementR1 from "@/components/elements/elementsR1";
import ElementR2 from "@/components/elements/elementsR2";
export default {
    components: {
        EchartsL1,
        EchartsL2,
        ElementR1,
        ElementR2
    },
}
</script>

<style>
#box2l1 {
    width: 959px;
    height: 438px;
    position: absolute;
    left: 44px;
    top: 30px;
    margin: 0 0;
}

#box2l2 {
    width: 959px;
    height: 438px;
    position: absolute;
    left: 44px;
    top: 498px;
    margin: 0 0;
}

#box2r1 {
    width: 837px;
    height: 438px;
    position: absolute;
    top: 30px;
    margin: 0 0;
    left: 1038px;
}

#box2r2 {
    width: 837px;
    height: 438px;
    position: absolute;
    margin: 0 0;
    left: 1038px;
    top: 498px;
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