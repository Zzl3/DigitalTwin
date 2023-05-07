<template>
  <div>
    <div class="table-container">
      <el-table
        :data="tableData"
        :row-style="{ height: '20px' }"
        :header-cell-style="{ 'text-align': 'center', border: 'none' }"
        :cell-style="{ 'text-align': 'center', border: 'none' }"
      >
        <el-table-column prop="airStation" label="空压站" min-width="30%">
          <!-- 用插槽的方法来改变颜色! -->
          <template slot-scope="scope">
            <span :style="{ color: '#1953FC' }">{{ scope.row.airStation }}</span>
          </template>
        </el-table-column>
        <el-table-column prop="time1" label="运行时间段1" min-width="40%">
          <template slot-scope="scope">
            <input type="text" v-model="scope.row.time1" :style="{ color: '#1953FC' }" />
            <span style="margin-left: 10px; color: #1953fc">h</span>
          </template>
        </el-table-column>
        <el-table-column prop="time2" label="运行时间段2" min-width="30%">
          <template slot-scope="scope">
            <input type="text" v-model="scope.row.time2" :style="{ color: '#1953FC' }" />
            <span style="margin-left: 10px; color: #1953fc">h</span>
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
import { getData } from "@/api/runstrategy";
export default {
  props: {
    childData: {
      type: Object,
      required: true,
    },
  },
  mounted() {
    this.getData2R1();
  },
  data() {
    return {
      tableData: [
        {
          airStation: "空压站1",
          time1: "1:00 - 2:00",
          time2: "7:00 - 9:00",
        },
        {
          airStation: "空压站2",
          time1: "1:00 - 2:00",
          time2: "7:00 - 9:00",
        },
        {
          airStation: "空压站3",
          time1: "1:00 - 2:00",
          time2: "7:00 - 9:00",
        },
      ],
    };
  },
  watch: {
    tableData: {
      deep: true, // 监听对象变化
      handler(newVal, oldVal) {
        this.updateR1Data(); //触发函数
        console.log("tableData changed: ", newVal, oldVal);
      },
    },
  },
  methods: {
    getData2R1() {
      getData()
        .then((data) => {
          console.log(data);
          for (let i = 0; i < data.length; i++) {
            this.tableData[i].time1 = data[i].time1;
            this.tableData[i].time2 = data[i].time2;
          }
        })
        .catch((error) => {
          console.log(error); // 处理错误
        });
    },
    updateR1Data() {
      const newChildData = {
        S1time1: this.tableData[0].time1,
        S1time2: this.tableData[0].time2,
        S2time1: this.tableData[1].time1,
        S2time2: this.tableData[1].time2,
        S3time1: this.tableData[2].time1,
        S3time2: this.tableData[2].time2,
      };
      this.$emit("R1DataChanged", newChildData);
    },
  },
};
</script>

<style scoped>
/* 在scoped环境下修改element-ui组件样式需要加/deep/ */
.table-container {
  width: 700px;
  margin-top: -25px;
}

/*最外层透明*/
/deep/.el-table {
  background-color: transparent !important;
  padding: 4px;
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
</style>
