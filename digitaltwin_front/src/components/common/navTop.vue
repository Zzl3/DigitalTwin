<template>
  <div id="demo">
    <p style="display: inline-block; float: left; margin-left: 20px">
      Sense-Air空压系统数字孪生与智能运维平台
    </p>
    <div style="display: inline-block; float: right; margin-top: 15px; margin-right: 20px">
      <div style="display: inline-block; float: left; margin-right: 200px">
        <el-button @click="dataAna">数据分析</el-button>
        <el-button @click="waySele">策略选择</el-button>
        <el-button @click="digiTwin">数字孪生</el-button>
      </div>
      <p style="display: inline-block; float: right; margin-left: 40px; margin-top: 5px">{{ dateFormat(date) }}</p>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      date: new Date(),
    };
  },
  mounted() {
    //获取当前日期
    let that = this;
    this.timer = setInterval(function () {
      that.date = new Date().toLocaleString();
    });
  },
  beforeDestroy: function () {
    if (this.timer) {
      clearInterval(this.timer);
    }
  },
  methods: {
    //当前日期格式化
    dateFormat() {
      var date = new Date();
      var year = date.getFullYear();
      var month =
        date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
      var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
      return year + "-" + month + "-" + day;
    },
    dataAna() {
      this.$router.push("/digitalAnalytics");
    },
    waySele() {
      this.$router.push("/waysSelect");
    },
    digiTwin() {
      this.$router.push("/digitalTwin");
    },
  },
};
</script>

<style>
#demo {
  height: 100px;
}
</style>
