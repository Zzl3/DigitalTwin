<template>
    <div id="container" style="display: flex; flex-direction: row">
      <numberCard4></numberCard4>
      <numberCard5></numberCard5>
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
  import numberCard4 from "@/components/digitalTwin/numberCard4.vue";
  import numberCard5 from "@/components/digitalTwin/numberCard5.vue";
  export default {
    components: {
            numberCard4,
            numberCard5
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
        stress:0,
        upstress:100.00,
        downstress:5.00,
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
  