<template>
    <div>
        <div style="padding-top: 40px;padding-left: 4px;">
            <el-container>
                <el-aside width="200px">
                    <el-menu v-model="activeIndex1" class="el-menu-vertical-demo" @select="handleSelect1"
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
                        <el-menu v-model="activeIndex2" class="el-menu-demo" mode="horizontal" @select="handleSelect2"
                            background-color="#545c64" text-color="#fff" active-text-color="#ffd04b">
                            <el-menu-item index="1">瞬时流量</el-menu-item>
                            <el-menu-item index="2">瞬时压力</el-menu-item>
                            <el-menu-item index="3">瞬时耗电</el-menu-item>
                            <el-menu-item index="4">流量&压力耗电量</el-menu-item>
                            <el-menu-item index="5" style="margin-left: 40px;">显示气压比</el-menu-item>
                        </el-menu>
                    </el-header>

                    <el-main>
                        <Echarts5A v-if="activeIndex2 === '1'" :active-index1="activeIndex1"></Echarts5A>
                        <Echarts5B v-else-if="activeIndex2 === '2'" :active-index1="activeIndex1"></Echarts5B>
                        <Echarts5C v-else-if="activeIndex2 === '3'" :active-index1="activeIndex1"></Echarts5C>
                        <Echarts5D v-else-if="activeIndex2 === '4'" :active-index1="activeIndex1"></Echarts5D>
                        <Echarts5E v-else-if="activeIndex2 === '5'" :active-index1="activeIndex1"></Echarts5E>
                    </el-main>
                </el-container>
            </el-container>
        </div>
    </div>
</template>

<script>
import Echarts5A from "@/components/echarts/echarts5A.vue";
import Echarts5B from "@/components/echarts/echarts5B.vue";
import Echarts5C from "@/components/echarts/echarts5C.vue";
import Echarts5D from "@/components/echarts/echarts5D.vue";
import Echarts5E from "@/components/echarts/echarts5E.vue";

export default {
    data() {
        return {
            activeIndex1: "1",
            activeIndex2: "1",
        };
    },
    methods: {
        handleSelect1(index) {
            this.activeIndex1 = index;
        },
        handleSelect2(index) {
            this.activeIndex2 = index;
        },
    },
    components: {
        Echarts5A,
        Echarts5B,
        Echarts5C,
        Echarts5D,
        Echarts5E,
    },
};
</script>

<style></style>
