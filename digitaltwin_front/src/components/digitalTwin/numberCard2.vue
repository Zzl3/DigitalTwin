<template>
  <div class="card">
    <div class="title">
      <span>
        <i class="el-icon-s-release"></i>
      </span>
      <p class="title-text">总管压力</p>
      <p class="percent">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 1792 1792"
          fill="currentColor"
          height="20"
          width="20"
        >
          <path
            d="M1408 1216q0 26-19 45t-45 19h-896q-26 0-45-19t-19-45 19-45l448-448q19-19 45-19t45 19l448 448q19 19 19 45z"
          ></path>
        </svg>
        {{percent}}%
      </p>
    </div>
    <div class="data">
      <p>{{number}} bar</p>
      <!-- <div class="range">
        <div class="fill"></div>
      </div> -->
    </div>
  </div>
</template>

<script>
import { getPercent,getNumber } from "@/api/c++_left";
export default {
  methods: {
    getPercent() {
      var that=this
      getPercent()
        .then((data) => {
          console.log(data); // 处理获取到的数据
          that.percent=data.toFixed(2)
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
    getNumber() {
      var that=this
      getNumber()
        .then((data) => {
          console.log(data); // 处理获取到的数据
          that.number=data.toFixed(2)
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
  mounted() {
    this.getPercent();
    this.getNumber();
  },
  data() {
    return {
      percent:30,
      number:4.91,
    };
  },

};

</script>

<style scoped>
.card {
  padding: 1rem;
  background-color: #fff;
  box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05);
  width:200px;
  border-radius: 20px;
  height:100px;
  margin-top:-20px;
}

.title {
  display: flex;
  align-items: center;
}

.title span {
  position: relative;
  padding: 0.3rem;
  background-color: #10a8b9;
  width: 1.0rem;
  height: 1.0rem;
  border-radius: 9999px;
}

.title span svg {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #ffffff;
  height: 1rem;
}

.title-text {
  margin-left: 0.5rem;
  color: #374151;
  font-size: 18px;
}

.percent {
  margin-left: 0.5rem;
  color: #10a8b9;
  font-weight: 600;
  display: flex;
}

.data {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
}

.data p {
  margin-top: 0rem;
  margin-bottom: 1rem;
  color: #1f2937;
  font-size: 1.8rem;
  line-height: 2.5rem;
  font-weight: 700;
  text-align: left;
}

.data .range {
  position: relative;
  background-color: #e5e7eb;
  width: 100%;
  height: 0.5rem;
  border-radius: 0.25rem;
}

.data .range .fill {
  position: absolute;
  top: 0;
  left: 0;
  background-color: #10b981;
  width: 76%;
  height: 100%;
  border-radius: 0.25rem;
}
</style>
