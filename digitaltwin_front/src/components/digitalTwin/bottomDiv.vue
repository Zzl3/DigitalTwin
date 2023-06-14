<template>
  <div id="container" style="display: flex; flex-direction: row">
    <div id="tabs">
      <el-tabs v-model="activeName" @tab-click="handleClick">
        <el-tab-pane label="空压站1" name="first"><tabOne></tabOne></el-tab-pane>
        <el-tab-pane label="空压站2" name="second"><tabOne2></tabOne2></el-tab-pane>
        <el-tab-pane label="空压站3" name="third"><tabOne3></tabOne3></el-tab-pane>
      </el-tabs>
    </div>
    <div>
      <tabTwo style="width:110%;margin-top:10%;margin-left:5%;padding-top:10%;padding-bottom:10%"></tabTwo>
    </div>
    <div>
      <tabThree style="width:150%;margin-top:25%;margin-left:50%;padding-top:10%;padding-bottom:40%"></tabThree>
    </div>
    <div>
      <tabFour style="width:140%;margin-top:5%;margin-left:75%;padding-top:10%;padding-bottom:20%"></tabFour>
    </div>
  </div>
</template>

<script>
import tabOne from "@/components/digitalTwin/tabOne.vue";
import tabOne2 from "@/components/digitalTwin/tabOne2.vue";
import tabOne3 from "@/components/digitalTwin/tabOne3.vue";
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
    tabOne2,
    tabOne3,
    tabTwo,
    tabThree,
    tabFour
  },
};
</script>

<style scoped>
#container {
  margin-left: 0%;
  width:100%;
}
#tabs{
  width: 45%;
  margin-top: 2%;
  margin-left: 2%;
}
</style>
