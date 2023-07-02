<template>
  <div id="container" style="display: flex; flex-direction: row">
    <numberCard></numberCard>
    <numberCard1></numberCard1>
    <div id="number" style="display: flex; flex-direction: row">
        <div id="text">
            <p>冷却水压力</p>
            <p>压力下限报警</p>
            <p>压力上限报警</p>
        </div>
        <div id="numtext">
            <p><el-tag>{{stress}}Bar</el-tag></p>
            <p><el-tag>{{upstress}}Bar</el-tag></p>
            <p><el-tag>{{downstress}}Bar</el-tag></p>
        </div>
    </div>
  </div>
</template>

<script>
import { getStress } from "@/api/c++_right";
import numberCard from "@/components/digitalTwin/numberCard.vue";
import numberCard1 from "@/components/digitalTwin/numberCard1.vue";
export default {
  components: {
    numberCard,
    numberCard1,
  },
  methods: {
    getStress() {
      var that=this
      getStress()
        .then((data) => {
          console.log(data); // 处理获取到的数据
          that.stress=data.toFixed(2)
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
  mounted() {
    this.getStress();
  },
  data() {
    return {
      stress:20,
      upstress:120.00,
      downstress:10.00,
    };
  },
};
</script>

<style scoped>
#number{
    margin-left: 20px;
    margin-top:-20px;
}
#text{
    float:left;
    text-align:left;
}
#numtext{
    margin-left:30px;
    margin-top:-5px;
}
</style>
