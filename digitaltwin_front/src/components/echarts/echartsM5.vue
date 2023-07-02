<template>
  <div class="table-wrapper">
    <el-select v-model="selectedValue" placeholder="请选择">
      <el-option v-for="option in options" :key="option.value" :label="option.label" :value="option.value"></el-option>
    </el-select>
    <el-table :data="tableData" style="font-size: 14px; font-weight: bold; color: #000000" :row-style="{ height: '20%' }"
      :cell-style="{ padding: '4px' }" :header-cell-style="{ background: '#eef1f6', color: '#606266' }"
      :show-header="false">
      <el-table-column prop="prop1" width="195" align="left"> </el-table-column>
      <el-table-column prop="prop2" width="195" align="right"> </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getData } from "@/api/airstate";
export default {
  mounted() {
    this.getDataM5();
  },
  data() {
    return {
      alldata: [],
      selectedValue: "option1",
      options: [
        { value: "option1", label: "空压站1" },
        { value: "option2", label: "空压站2" },
        { value: "option3", label: "空压站3" },
      ],
      tableData: [
        {
          prop1: "  工作状态",
          prop2: "■正常  ",
        },
        {
          prop1: "  负荷量",
          prop2: "811.584  ",
        },
        {
          prop1: "  平均使用时长",
          prop2: "00:02:45  ",
        },
      ],
    };
  },
  watch: {
    selectedValue(value) {
      if (value === "option1") {
        this.tableData[0].prop2 = this.alldata[0].state;
        this.tableData[1].prop2 = this.alldata[0].stress;
        this.tableData[2].prop2 = this.alldata[0].timepre;
      } else if (value === "option2") {
        this.tableData[0].prop2 = this.alldata[1].state;
        this.tableData[1].prop2 = this.alldata[1].stress;
        this.tableData[2].prop2 = this.alldata[1].timepre;
      } else if (value === "option3") {
        this.tableData[0].prop2 = this.alldata[2].state;
        this.tableData[1].prop2 = this.alldata[2].stress;
        this.tableData[2].prop2 = this.alldata[2].timepre;
      } else {
        this.tableData[0].prop2 = this.alldata[0].status;
        this.tableData[1].prop2 = this.alldata[0].stress;
        this.tableData[2].prop2 = this.alldata[0].timepre;
      }
    },
  },
  methods: {
    getDataM5() {
      getData()
        .then((data) => {
          this.alldata = data;
          console.log(this.alldata);
          //   for (let i = 0; i < data.length; i++) {
          //     this.tableData[i].prop2 = parseInt(data[i].ratio * 100).toFixed(2) + "%";
          //     this.tableData[i].prop3 = parseInt(data[i].fee).toFixed(0);
          //     this.tableData[i].prop4 = data[i].pressure;
          //     this.tableData[i].prop5 = data[i].usernum;
          //   }
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
  background-color: rgba(135, 206, 235, 0.5);
}

.table-wrapper /deep/ .el-table tr {
  background-color: rgba(135, 206, 235, 0.5) !important;
}

.table-wrapper /deep/ .el-table--enable-row-transition .el-table__body td,
.el-table .cell {
  background-color: rgba(135, 206, 235, 0.5);
}
</style>
