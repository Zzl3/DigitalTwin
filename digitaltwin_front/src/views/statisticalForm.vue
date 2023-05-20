<template>
  <div>
    <!-- 顶部日期、导出按钮和报表时间选择器 -->
    <div class="header">
      <el-date-picker v-model="date" type="date" placeholder="选择日期"></el-date-picker>
      <el-button
        type="primary"
        icon="el-icon-download"
        style="margin-left: 20px"
        @click="exportData"
        >导出数据</el-button
      >
      <el-date-picker
        v-model="reportDate"
        type="daterange"
        range-separator="至"
        start-placeholder="开始日期"
        end-placeholder="结束日期"
        style="margin-left: 20px"
      ></el-date-picker>
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
import XLSX from "xlsx";
export default {
  data() {
    return {
      date: new Date(),
      reportDate: null,
      tableData: [
        {
          id: "001",
          time: "2022-05-20 08:00",
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
          time: "2022-05-20 09:00",
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
          id: "004",
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
          id: "005",
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
          id: "006",
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
    };
  },
  methods: {
    exportData() {
      const data = this.tableData;
      const worksheet = XLSX.utils.json_to_sheet(data);
      const workbook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1");
      XLSX.writeFile(workbook, "data.xlsx");
    },
  },
};
</script>
<style scoped>
.container {
  margin-top: 60px;
  width: 1670px;
  margin: 0 auto;
  padding: 20px;
  margin-right: 20px;
}
.header {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 20px;
  margin-top: 70px;
}
</style>
