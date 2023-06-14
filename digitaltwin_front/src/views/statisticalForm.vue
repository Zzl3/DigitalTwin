<template>
  <div class="container">
    <!-- 顶部日期、导出按钮和报表时间选择器 -->
    <div class="header">
      <el-date-picker v-model="date" type="date" placeholder="选择日期"></el-date-picker>
      <el-button
        style="margin-left: 1%"
        type="primary"
        icon="search"
        @click="imtScreen2()"
        >筛选</el-button
      >
      <div style="margin-left: 2%; margin-right: 1%">
        <el-date-picker
          v-model="search_data.startTime"
          type="date"
          placeholder="起始日期"
        >
        </el-date-picker>
        --
        <el-date-picker v-model="search_data.endTime" type="date" placeholder="终止日期">
        </el-date-picker>
      </div>

      <el-button type="primary" icon="search" @click="imtScreen()">筛选</el-button>
      <el-button type="primary" icon="search" @click="backdown()">复原</el-button>
      <el-button
        type="primary"
        icon="el-icon-download"
        style="margin-left: 1%"
        @click="exportData"
        >导出数据</el-button
      >
    </div>
    <!-- 表格 -->
    <el-table :data="tableData" style="width: 100%">
      <el-table-column prop="id" label="编号"></el-table-column>
      <el-table-column prop="time" label="记录时间"></el-table-column>
      <el-table-column label="空压站1">
        <el-table-column prop="station1.machine1" label="空压机1"></el-table-column>
        <el-table-column prop="station1.machine2" label="空压机2"></el-table-column>
        <el-table-column prop="station1.machine3" label="空压机3"></el-table-column>
        <el-table-column prop="station1.machine4" label="空压机4"></el-table-column>
        <el-table-column prop="station1.machine5" label="空压机5"></el-table-column>
        <el-table-column prop="station1.machine6" label="空压机6"></el-table-column>
        <el-table-column prop="station1.machine7" label="空压机7"></el-table-column>
        <el-table-column prop="station1.machine8" label="空压机8"></el-table-column>
        <el-table-column prop="station1.machine9" label="空压机9"></el-table-column>
        <el-table-column prop="station1.machine10" label="空压机10"></el-table-column>
      </el-table-column>

      <!-- 空压站2列 -->
      <el-table-column label="空压站2">
        <el-table-column prop="station2.machine1" label="空压机1"></el-table-column>
        <el-table-column prop="station2.machine2" label="空压机2"></el-table-column>
        <el-table-column prop="station2.machine3" label="空压机3"></el-table-column>
      </el-table-column>

      <!-- 空压站3列 -->
      <el-table-column label="空压站3">
        <el-table-column prop="station3.machine1" label="空压机1"></el-table-column>
        <el-table-column prop="station3.machine2" label="空压机2"></el-table-column>
        <el-table-column prop="station3.machine3" label="空压机3"></el-table-column>
        <el-table-column prop="station3.machine4" label="空压机4"></el-table-column>
      </el-table-column>
    </el-table>
  </div>
</template>
<script>
export default {
  data() {
    return {
      date: "",
      reportDate: null,
      tableData: [
        {
          id: "001",
          time: "2023-05-20 08:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "002",
          time: "2021-05-20 09:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "003",
          time: "2023-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "004",
          time: "2021-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "005",
          time: "2023-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "006",
          time: "2021-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "007",
          time: "2022-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "008",
          time: "2022-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "009",
          time: "2022-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
        {
          id: "010",
          time: "2022-05-20 10:00",
          station1: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
            machine5: "223124",
            machine6: "423124",
            machine7: "123124",
            machine8: "823124",
            machine9: "323124",
            machine10: "523124",
          },
          station2: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
          },
          station3: {
            machine1: "556870",
            machine2: "231232",
            machine3: "234234",
            machine4: "123124",
          },
        },
      ],
      alwaysData: [],
      filterTableData: [], //经过时间筛选后得到的数据
      search_data: {
        startTime: "",
        endTime: "",
      },
    };
  },
  mounted() {
    this.alwaysData = [...this.tableData];
  },
  methods: {
    exportData() {
      window.location.href = "http://1.15.177.18:8080/空压站示例.xlsx";
    },
    imtScreen() {
      //判断是否输入时间区间
      if (!this.search_data.startTime || !this.search_data.endTime) {
        this.$message({
          type: "warning",
          message: "请选择时间区间！",
        });
      }
      const stime = this.search_data.startTime.getTime();
      const etime = this.search_data.endTime.getTime();
      //将筛选后的数据赋值给 allTableDate
      this.filterTableData = [...this.tableData];
      this.tableData = this.filterTableData.filter((item) => {
        //筛选后得到的数据 item 中包含数据日期 date
        //创建一个数组 date，存储得到的item.date
        item.date = item.time.split(" ")[0];
        let date = new Date(item.date);
        let time = date.getTime();
        return time >= stime && time <= etime;
      });
    },
    backdown() {
      this.tableData = [...this.alwaysData];
    },
    imtScreen2() {
      //判断是否输入时间区间
      if (!this.date) {
        this.$message({
          type: "warning",
          message: "请选择时间！",
        });
      }
      console.log(this.date);
      let time2 = new Date(this.date).getTime();
      // console.log(time2);
      let year = new Date(time2).getFullYear();
      let month = new Date(time2).getMonth();
      let day = new Date(time2).getDate();
      let date2 = new Date(year, month, day, 0, 0, 0);
      //将筛选后的数据赋值给 allTableDate
      this.filterTableData = [...this.tableData];
      this.tableData = this.filterTableData.filter((item) => {
        //筛选后得到的数据 item 中包含数据日期 date
        //创建一个数组 date，存储得到的item.date
        item.date = item.time.split(" ")[0];
        let date = new Date(item.date);
        let year2 = date.getFullYear();
        let month2 = date.getMonth();
        let day2 = date.getDate();
        let date3 = new Date(year2, month2, day2, 0, 0, 0);
        console.log(date);
        // let time = date.getTime();
        console.log("筛选时间");
        console.log(date2.getTime());
        console.log("被筛选时间");
        console.log(date.getTime());
        return date3.getTime() == date2.getTime();
      });
    },
  },
};
</script>
<style scoped>
.container {
  margin-top: 0%;
  width: 97%;
  margin-left: 2%;
  margin-right: 2%;
}
.header {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 2%;
  margin-top: 0%;
}
</style>
