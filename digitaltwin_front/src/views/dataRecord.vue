<template>
    <div>
        <div style="padding-top: 40px;padding-left: 4px;">
            <el-container>
                <el-aside width="200px">
                    <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose"
                        style="height: 700px">
                        <el-menu-item index="1">
                            <i class="el-icon-menu"></i>
                            <span slot="title">4#空压站流量计&电表1</span>
                        </el-menu-item>
                        <el-menu-item index="2">
                            <i class="el-icon-menu"></i>
                            <span slot="title">4#空压站流量计&电表2</span>
                        </el-menu-item>
                        <el-menu-item index="3">
                            <i class="el-icon-menu"></i>
                            <span slot="title">4#空压站流量计&电表3</span>
                        </el-menu-item>
                        <el-menu-item index="4">
                            <i class="el-icon-menu"></i>
                            <span slot="title">4#空压站薄板中心</span>
                        </el-menu-item>
                        <el-menu-item index="5">
                            <i class="el-icon-menu"></i>
                            <span slot="title">4#空压站8、9平台</span>
                        </el-menu-item>
                        <el-menu-item index="6">
                            <i class="el-icon-menu"></i>
                            <span slot="title">4#空压站</span>
                        </el-menu-item>
                    </el-menu>

                </el-aside>
                <el-container>
                    <el-header>
                        <el-menu :default-active="activeIndex2" class="el-menu-demo" mode="horizontal"
                            @select="handleSelect" background-color="#545c64" text-color="#fff" active-text-color="#ffd04b">
                            <el-menu-item index="1">瞬时流量</el-menu-item>
                            <el-menu-item index="2">瞬时压力</el-menu-item>
                            <el-menu-item index="3">瞬时耗电</el-menu-item>
                            <el-menu-item index="4">流量&压力耗电量</el-menu-item>
                            <el-menu-item index="5" style="margin-left: 40px;">显示气压比</el-menu-item>
                        </el-menu>
                    </el-header>

                    <el-main>
                        <Echarts5C></Echarts5C>
                    </el-main>
                </el-container>
            </el-container>
        </div>
    </div>
</template>

<script>
import Echarts5C from "@/components/echarts/echarts5C.vue";
export default {
    components: {
        Echarts5C
    },
}
</script>

<style></style>