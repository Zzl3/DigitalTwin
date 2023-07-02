<template>
  <div id="app">
    <navTop class="nav"></navTop>
    <div class="background">
      <img :src="imgSrc" width="100%" height="100%" alt=""/>
    </div>
    <router-view class="content2" />
  </div>
</template>

<script>
import navTop from "@/components/common/navTop";
export default {
  components: {
    navTop,
  },
  data() {
    return {
      imgSrc: require("@/assets/background.jpg"),
    };
  },
};
</script>

<style scoped>
html, body {
  height: 100%; /* 设置 html 和 body 的高度为 100% */
  margin: 0; /* 取消默认的外边距 */
}

#app {
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  height: 100%;
  width: 100%;
}
.background {
  width: 100%;
  height: 98%;
  z-index: -1;
  position: absolute;
}
.content2 {
  position: absolute;
  top: 10%;
  bottom: 5%;
  left: 0;
  right: 0;
  z-index: 1;
  height: 100%; /* 设置 .content2 的高度为 90% */
}

.nav {
  -webkit-backdrop-filter: blur(10px);
  backdrop-filter: blur(10px);
  width: 100%;
  height: 10%;
  z-index: 1;
  position: absolute;
}
</style>
