<template>
  <div id="demo">
    <p style="display: inline-block; float: left; margin-left: 20px;font-size: 24px;font-weight: bold;">
      Sense-Air空压系统数字孪生与智能运维平台
    </p>
    <div style="display: inline-block; float: right; margin-top: 15px; margin-left:20px;">
      <div style="display: inline-block; float: left; margin-right: 50px;">
        <el-button @click="dataAna">数据分析</el-button>
        <el-button @click="waySele">策略选择</el-button>
        <el-button @click="digiTwin">数字孪生</el-button>
        <el-button @click="statiForm">统计报表</el-button>
        <el-button @click="dataRecord">数据记录</el-button>
        <el-button @click="alarmStatus">报警状态</el-button>
        <el-button @click="deviceRecord">仪表统计</el-button>
      </div>
      <p class="pTime">
        {{ dateFormat(date) }}
      </p>
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
    statiForm() {
      this.$router.push("/statisticalForm");
    },
    dataRecord() {
      this.$router.push("/dataRecord");
    },
    alarmStatus() {
      this.$router.push("/alarmStatus");
    },
    deviceRecord() {
      this.$router.push("/deviceRecord");
    }
  },
};
</script>

<style>
#demo {
  height: 10%;
  z-index: 9999;
}

.pTime {
  /* 水平垂直居中 */
  display: flex;
  justify-content: center;
  align-items: center;
  /* 字体大小 */
  font-size: 20px;
  display: inline-block;
  margin-right: 40px;
  margin-top: 5px
}
</style>
