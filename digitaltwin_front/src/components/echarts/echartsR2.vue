<template>
  <div class="table-wrapper">
    <el-table :data="tableData" style="font-size: 10px" :row-style="{ height: '10%' }" :cell-style="{ padding: '4px' }"
      :header-cell-style="{ background: '#eef1f6', color: '#606266' }">
      <el-table-column prop="prop1" label="空压站" width="80%" align="center">
      </el-table-column>
      <el-table-column prop="prop2" label="补气时间占比"  width="95%" align="center">
      </el-table-column>
      <el-table-column prop="prop3" label="电费"  width="80%" align="center">
      </el-table-column>
      <el-table-column prop="prop4" label="平均压力"  width="80%" align="center">
      </el-table-column>
      <el-table-column prop="prop5" label="用户数"  width="80%" align="center">
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getData } from "@/api/systemair";
export default {
  data() {
    return {
      tableData: [
        {
          prop1: "空压站1",
          prop2: "72.36%",
          prop3: "12302",
          prop4: "784Kpa",
          prop5: "3",
        },
        {
          prop1: "空压站2",
          prop2: "66.52%",
          prop3: "11254",
          prop4: "803Kpa",
          prop5: "2",
        },
        {
          prop1: "空压站3",
          prop2: "75.26%",
          prop3: "9563",
          prop4: "769Kpa",
          prop5: "2",
        },
      ],
    };
  },
  mounted() {
    this.getDataR2();
  },
  methods: {
    getDataR2() {
      getData()
        .then((data) => {
          console.log(data);
          for (let i = 0; i < data.length; i++) {
            this.tableData[i].prop2 = parseInt(data[i].ratio * 100).toFixed(2) + "%";
            this.tableData[i].prop3 = parseInt(data[i].fee).toFixed(0);
            this.tableData[i].prop4 = data[i].pressure;
            this.tableData[i].prop5 = data[i].usernum;
          }
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
};
</script>

<style scoped>
.table-wrapper /deep/ .el-table,
.el-table__expanded-cell {
  background-color: transparent;
}

.table-wrapper /deep/ .el-table tr {
  background-color: transparent !important;
}

.table-wrapper /deep/ .el-table--enable-row-transition .el-table__body td,
.el-table .cell {
  background-color: transparent;
}
</style>
