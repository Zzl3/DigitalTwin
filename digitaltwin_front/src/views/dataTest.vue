<template>
  <div>
    <el-button type="primary" @click="getData">获取数据</el-button>
    <el-button type="primary" @click="addData">添加数据</el-button>
    <el-button type="primary" @click="deleteData">删除数据</el-button>
  </div>
</template>

<script>
import { getData, addData,deleteData } from "@/api/test";
export default {
  methods: {
    getData() {
      getData()
        .then((data) => {
          console.log(data); // 处理获取到的数据
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
    addData() {
      const data = {
        id: 0,
        proper1: "test",
        proper2: "test",
        proper3: "test",
      };
      addData(data)
        .then((data) => {
          console.log(data); // 处理获取到的数据
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
    deleteData() {
      deleteData(2)
        .then((data) => {
          console.log(data); // 处理获取到的数据
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    }
  },
};
</script>

<style></style>
