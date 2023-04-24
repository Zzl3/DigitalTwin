<template>
    <div class="table-wrapper">
        <el-table :data="tableData" 
            style="font-size: 14px; font-weight: bold; color: #000000;" :row-style="{ height: '40px' }"  :cell-style="{ padding: '4px' }"
            :header-cell-style="{ background: '#eef1f6', color: '#606266' }" :show-header ="false">
            <el-table-column prop="prop1" width="195" align="left">
            </el-table-column>
            <el-table-column prop="prop2" width="195" align="right">
            </el-table-column>
        </el-table>
    </div>
</template>

<script>
export default {
    data() {
        return {
            tableData: [{
                prop1: '  工作状态',
                prop2: '■正常  ',
            }, {
                prop1: '  负荷量',
                prop2: '999  ',
            }, {
                prop1: '  呼出量',
                prop2: '999  ',
            },{
                prop1: '  平均使用时长',
                prop2: '00:02:45  ',
            },]
        }
    }
}
</script>

<style scoped>
.table-wrapper /deep/ .el-table,
.el-table__expanded-cell {
    background-color: rgba(135,206,235,0.5);
}

.table-wrapper /deep/ .el-table tr {
    background-color: rgba(135,206,235,0.5) !important;
}

.table-wrapper /deep/ .el-table--enable-row-transition .el-table__body td,
.el-table .cell {
    background-color: rgba(135,206,235,0.5);
}

</style>