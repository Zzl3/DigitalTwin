<template>
  <div class="content">
    <p>当前时段设定<br>用气量</p>
    <el-button type="success" style="margin-bottom:10px">{{usage}} m/mm</el-button>
  </div>
</template>

<script>
import { getNowElectricity } from "@/api/c++_right";
export default {
  methods: {
   getNowElectricity() {
      var that=this
      getNowElectricity()
        .then((data) => {
          console.log(data); // 处理获取到的数据
          that.usage=data.toFixed(2)
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
  mounted() {
    this.getNowElectricity();
  },
  data() {
    return {
      usage:14.00
    };
  },
}
</script>

<style scoped>
.content {
    border-radius: 10px;
    background-color: #ffffff;
    box-shadow: 20px 20px 60px #bebebe, -20px -20px 60px #ffffff;
  }
</style>