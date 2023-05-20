<template>
  <div id="container" style="display: flex; flex-direction: row">
    <numberCard></numberCard>
    <numberCard></numberCard>
    <div id="number" style="display: flex; flex-direction: row">
        <div id="text">
            <p>冷却水压力</p>
            <p>压力下限报警</p>
            <p>压力上限报警</p>
        </div>
        <div id="numtext">
            <p><el-tag>0.00 Bar</el-tag></p>
            <p><el-tag>0.00 Bar</el-tag></p>
            <p><el-tag>0.00 Bar</el-tag></p>
        </div>
    </div>
  </div>
</template>

<script>
import numberCard from "@/components/digitalTwin/numberCard.vue";
export default {
  components: {
    numberCard,
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
