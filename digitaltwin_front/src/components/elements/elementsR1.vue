<template>
    <div>
        <div class="table-container">
            <el-table :data="tableData" :row-style="{ height: '40px' }"
                :header-cell-style="{ 'text-align': 'center', 'border': 'none' }"
                :cell-style="{ 'text-align': 'center', 'border': 'none' }">
                <el-table-column prop="airStation" label="空压站" min-width="30%">
                    <!-- 用插槽的方法来改变颜色! -->
                    <template slot-scope="scope">
                        <span :style="{ color: '#1953FC' }">{{ scope.row.airStation }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="time1" label="运行时间段1" min-width="40%">
                    <template slot-scope="scope">
                        <span :style="{ color: '#1953FC' }">{{ scope.row.time1 }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="time2" label="运行时间段2" min-width="30%">
                    <template slot-scope="scope">
                        <span :style="{ color: '#1953FC' }">{{ scope.row.time2 }}</span>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <div>
            <el-button type="primary" style="width: 382px;">一键应用优化效果</el-button>
            <el-button type="primary" style="width: 382px">重置</el-button>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            tableData: [{
                airStation: '空压站1',
                time1: '1:00 - 2:00',
                time2: '7:00 - 9:00'
            }, {
                daairStationte: '',
                time1: '',
                time2: ''
            }, {
                airStation: '空压站2',
                time1: '1:00 - 2:00',
                time2: '7:00 - 9:00'
            }, {
                airStation: '',
                time1: '',
                time2: ''
            },
            {
                airStation: '空压站3',
                time1: '1:00 - 2:00',
                time2: '7:00 - 9:00'
            }, {
                airStation: '',
                time1: '',
                time2: ''
            }]
        }
    }
}
</script>

<style scoped>
/* 在scoped环境下修改element-ui组件样式需要加/deep/ */

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