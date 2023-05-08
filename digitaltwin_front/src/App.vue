<template>
  <div id="app">
    <navTop class="nav"></navTop>
    <div class="background">
      <img :src="imgSrc" width="100%" height="100%" alt=""/>
    </div>
    <router-view class="content" />
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

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.background {
  width: 100%;
  height: 98%;
  z-index: -1;
  position: absolute;
}

.content {
  width: 100%;
  z-index: 1;
  position: absolute;
  margin-top: 40px;
}

.nav {
  -webkit-backdrop-filter: blur(10px);
  backdrop-filter: blur(10px);
  width: 100%;
  z-index: 1;
  position: fixed;
}</style>
