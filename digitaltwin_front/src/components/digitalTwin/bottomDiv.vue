<template>
  <div id="container" style="display: flex; flex-direction: row">
    <div>
      <el-tabs v-model="activeName" @tab-click="handleClick">
        <el-tab-pane label="空压站1" name="first"><tabOne></tabOne></el-tab-pane>
        <el-tab-pane label="空压站2" name="second"><tabOne></tabOne></el-tab-pane>
        <el-tab-pane label="空压站3" name="third"><tabOne></tabOne></el-tab-pane>
      </el-tabs>
    </div>
    <div>
      <tabTwo style="width:350px;margin-top:30px;margin-left:45px;padding-top:10px;padding-bottom:10px"></tabTwo>
    </div>
    <div>
      <tabThree style="width:200px;margin-top:30px;margin-left:405px;padding-top:10px;padding-bottom:10px"></tabThree>
    </div>
    <div>
      <tabFour style="width:400px;margin-top:30px;margin-left:615px;padding-top:10px;margin-bottom:5px"></tabFour>
    </div>
  </div>
</template>

<script>
import tabOne from "@/components/digitalTwin/tabOne.vue";
import tabTwo from "@/components/digitalTwin/tabTwo.vue";
import tabThree from "@/components/digitalTwin/tabThree.vue";
import tabFour from "@/components/digitalTwin/tabFour.vue";
export default {
  data() {
    return {
      activeName: "first",
    };
  },
  methods: {
    handleClick(tab, event) {
      console.log(tab, event);
    },
  },
  components: {
    tabOne,
    tabTwo,
    tabThree,
    tabFour
  },
};
</script>

<style scoped>
#container {
  margin-left: 20px;
}
</style>
