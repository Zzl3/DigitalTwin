<template>
  <div v-on:click="doClick()" id="background22">
    <div class="overlay"></div>
    <div :style="cssVars" v-if="showModel"
      style="position: absolute; top: var(--pointY); left: var(--pointX); z-index: 100">
      <EchartsM5></EchartsM5>
    </div>
    <div class="content" id="boxl1">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">用户负荷情况</p>
      </div>
      <EchartsL1 style="margin-left:2%; margin-top: -15%; position: absolute"></EchartsL1>
    </div>
    <div class="content" id="boxl2">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">空压站使用情况</p>
      </div>
      <EchartsL2 style="margin-left:2%; margin-top: -10%; position: absolute"></EchartsL2>
    </div>
    <div class="content" id="boxl3">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">近日耗电量</p>
      </div>
      <EchartsL3 style="margin-top: -10%; position: absolute;"></EchartsL3>
    </div>
    <div class="content" id="boxr1">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">系统电费分析</p>
      </div>
      <EchartsR1 style="margin-top: -10%; margin-left:4%; position: absolute"></EchartsR1>
    </div>
    <div class="content" id="boxr2">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">空压系统整体分析</p>
      </div>
      <EchartsR2 style="margin-top: -4%; margin-left:4%; position: absolute"></EchartsR2>
    </div>
    <div class="content" id="boxr3">
      <div style="text-align: left">
        <i class="el-icon-news titletext"></i>
        <p class="titletext">各空压机耗电量</p>
      </div>
      <EchartsR3 style="margin-top: -5%; position: absolute"></EchartsR3>
    </div>
    <div class="content" id="center">
      <div class="flex-div">
        <div style="text-align: left">
          <i class="el-icon-news titletext"></i>
          <p class="titletext">今日电费</p>
        </div>
        <div style="text-align: center">
          <h1 style="text-align: center; font-size: 180%; color: #134FFE">{{ todayvalue }}元</h1>
        </div>
        <div>
          <div class="tit01" style="text-align: center">空压站1</div>
          <br />
          <EchartsM1 style="margin-top: -3%; position: absolute"></EchartsM1>
        </div>
        <div>
          <div class="tit02" style="text-align: center">空压站2</div>
          <br />
          <EchartsM2 style="margin-top: -3%; position: absolute"></EchartsM2>
        </div>
        <div>
          <div class="tit03" style="text-align: center">空压站3</div>
          <br />
          <EchartsM3 style="margin-top: -3%; position: absolute"></EchartsM3>
        </div>
        <div><!-- 强制性放一个div把tit03顶回来 --></div>
      </div>
      <div style="justify-content: space-between">
        <div style="text-align: left; margin-top: -5%">
          <i class="el-icon-news titletext"></i>
          <p class="titletext">优化前后耗电量</p>
        </div>
        <div>
          <EchartsM4 style="margin-left: 5%; margin-top: -5%; position: absolute"></EchartsM4>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getData } from "@/api/electricityfee";
import EchartsL1 from "@/components/echarts/echartsL1.vue";
import EchartsL2 from "@/components/echarts/echartsL2.vue";
import EchartsL3 from "@/components/echarts/echartsL3.vue";
import EchartsR1 from "@/components/echarts/echartsR1.vue";
import EchartsR2 from "@/components/echarts/echartsR2.vue"; // 其实使用 element-ui实现
import EchartsR3 from "@/components/echarts/echartsR3.vue";
import EchartsM1 from "@/components/echarts/echartsM1.vue";
import EchartsM2 from "@/components/echarts/echartsM2.vue";
import EchartsM3 from "@/components/echarts/echartsM3.vue";
import EchartsM4 from "@/components/echarts/echartsM4.vue";
import EchartsM5 from "@/components/echarts/echartsM5.vue";
export default {
  components: {
    EchartsL1,
    EchartsL2,
    EchartsL3,
    EchartsR1,
    EchartsR2,
    EchartsR3,
    EchartsM1,
    EchartsM2,
    EchartsM3,
    EchartsM4,
    EchartsM5,
  },
  mounted() {
    this.getDataM();
  },
  methods: {
    doClick(event) {
      // 点击事件后，需要显示一下图表
      this.showModel = true;
      var e = event || window.event;
      var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
      var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
      var x = e.pageX || e.clientX + scrollX;
      var y = e.pageY || e.clientY + scrollY;
      // 获得一下当前点击位置的坐标
      this.pointX = x;
      this.pointY = y;
      //alert('x: ' + this.pointX + '\ny: ' + this.pointY );
    },
    getDataM() {
      getData()
        .then((data) => {
          var tempdata = data[0];
          this.todayvalue = (parseFloat(tempdata.feetoday)).toFixed(2);
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
  data() {
    return {
      todayvalue: 35063.24,
      showModel: false,
      pointX: 0,
      pointY: 0,
    };
  },
  computed: {
    cssVars() {
      return {
        "--pointX": this.pointX + "px",
        "--pointY": this.pointY + "px",
      };
    },
  },
};
</script>

<style scoped>

#boxl1 {
  width: 28%;
  height: 27%;
  position: absolute;
  left: 0;
  top: 0;
}

#boxl2 {
  width: 28%;
  height: 27%;
  position: absolute;
  left: 0;
  top:30%;
}

#boxl3 {
  width: 28%;
  height: 27%;
  position: absolute;
  left: 0;
  top: 60%;
}

#boxr1 {
  width: 28%;
  height: 27%;
  position: absolute;
  right: 0;
  top: 0;
}

#boxr2 {
  width: 28%;
  height: 27%;
  position: absolute;
  right: 0;
  top:30%;
}

#boxr3 {
  width: 28%;
  height: 27%;
  position: absolute;
  right: 0;
  top:60%;
}

#center {
  width: 40%;
  height: 40%;
  position: absolute;
  left: 50%;
  top: 65%;
  transform: translate(-50%, -50%);
}

.content {
  border-radius: 5%;
  background: rgba(255, 255, 255, 0.5);
  box-shadow: 5% 5% 10% #bebebe, -10% -10% 20% #ffffff;
}

.overlay {
  position: fixed;
  top: 30%;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: -9999;
  /* 将其置于所有其他元素之下 */
}

.titletext {
  display: inline-block;
  margin-left: 5%;
}

.flex-div {
  display: flex;
  width: auto;
  justify-content: space-between;
  /*重要*/
}
</style>
