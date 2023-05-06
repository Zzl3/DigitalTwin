<template>
  <div>
    <div class="table-container">
      <el-table :data="tableData" :row-style="{ height: '50px' }"
        :header-cell-style="{ 'text-align': 'center', border: 'none' }" :cell-style="rowStyle">
        <el-table-column prop="airStation" label="空压站" min-width="30%">
          <!-- 用插槽的方法来改变颜色! -->
          <template slot-scope="scope">
            <span :style="{ color: '#1953FC' }">{{ scope.row.airStation }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="time1" label="第一次补气" min-width="30%">
          <template slot-scope="scope">
            <span :style="{ color: '#1953FC' }">{{ scope.row.time1 }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="time2" label="第二次补气" min-width="30%">
          <template slot-scope="scope">
            <span :style="{ color: '#1953FC' }">{{ scope.row.time2 }}</span>
          </template>
        </el-table-column>
      </el-table>
      <div class="threeButton">
        <el-button type="primary" style="width: 200px">一键应用优化效果</el-button>
        <el-button type="primary" style="width: 200px">应用</el-button>
        <el-button type="primary" style="width: 200px">重置</el-button>
      </div>
    </div>
  </div>
</template>

<script>
import { getData } from "@/api/runstrategy";
export default {
  mounted() {
    this.getData2R2();
  },
  methods: {
    // eslint-disable-next-line
    rowStyle({ row, column, rowIndex, columnIndex }) {
      //设置表格每行间隔边框
      if (rowIndex % 2 === 0) {
        if (columnIndex === 0) {
          return "text-align:center;border:2px solid #1953FC;border-right-width:0;";
        } else if (columnIndex === 1) {
          return "text-align:center;border:2px solid #1953FC;border-right-width:0;border-left-width:0;";
        } else {
          return "text-align:center;border:2px solid #1953FC;border-left-width:0;";
        }
      }
      if (rowIndex % 2 === 1) {
        return "text-align:center;border:none;";
      }
    },
    getData2R2() {
      getData()
        .then((data) => {
          console.log(data);
          for (let i = 0; i < data.length; i++) {
            this.tableData[i].time1 = data[i].pressure1 + "KPa";
            this.tableData[i].time2 = data[i].pressure2 + "KPa";
          }
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
  },
  data() {
    return {
      tableData: [
        {
          airStation: "空压站1",
          time1: "---------- KPa",
          time2: "---------- KPa",
        },
        {
          airStation: "空压站2",
          time1: "---------- KPa",
          time2: "---------- KPa",
        },
        {
          airStation: "空压站3",
          time1: "---------- KPa",
          time2: "---------- KPa",
        }
      ],
    };
  },
};
</script>

<style scoped>
.table-container {
  width: 700px;
  margin-top: -25px;
}

/*最外层透明*/
/deep/.el-table {
  background-color: transparent !important;
}

/deep/.el-table__expanded-cell {
  background-color: transparent !important;
}

/* 去除最外层的下边框 */
/deep/.el-table::before {
  height: 0px;
}

/* 表格内背景颜色 */
/* 表头单元格 */
/deep/.el-table th {
  background-color: rgba(75, 121, 255, 0.1) !important;
  color: #000 !important;
}

/* 一行 */
/deep/.el-table tr {
  background-color: transparent !important;
  width: 100%;
}

/* 一个单元格 */
/deep/.el-table td {
  background-color: transparent !important;
  font-weight: 10000;
}

.threeButton {
  margin-top: 40px;
}
</style>
