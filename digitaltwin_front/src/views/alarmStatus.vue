<template>
    <div>
        <div class="alert-nav">
            <span style="margin-right: 40px;">
                实时报警/历史报警
            </span>

            <el-select v-model="selectedValue" placeholder="全部空压站">
                <el-option v-for="option in options" :key="option.value" :label="option.label"
                    :value="option.value"></el-option>
            </el-select>

            <el-button type="primary" plain style="margin-left:50px;padding: 10px 30px;">当前报警</el-button>
            <el-button type="primary" plain style="padding: 10px 30px;">历史报警</el-button>
        </div>

        <div style="margin-top:100px">
            <el-table :data="tableData" border :row-style="{ height: '40px' }" style="width: 100%; height: 700px;">
                <el-table-column prop="id" label="" width="40">
                </el-table-column>
                <el-table-column prop="date" label="日期" width="180">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.date }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="time" label="时间" width="180">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.time }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="msg" label="报警信息" width="450">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.msg }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="equip" label="故障设备" width="180">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.equip }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="where" label="报警发生地" width="180">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.where }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="limit" label="报警阈值" width="180">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.limit }}</span>
                    </template>
                </el-table-column>
                <el-table-column prop="ltime" label="持续时间" width="180">
                    <template scope="scope">
                        <span style="color:red">{{ scope.row.ltime }}</span>
                    </template>
                </el-table-column>
            </el-table>
        </div>
    </div>
</template>

<script >
export default {
    mounted() {
        this.getDataM5();
    },
    data() {
        return {
            alldata: [],
            selectedValue: "option0",
            options: [
                { value: "option0", label: "全部空压站" },
                { value: "option1", label: "空压站1" },
                { value: "option2", label: "空压站2" },
                { value: "option3", label: "空压站3" },
            ],
            tableData: [{
                id: '1',
                date: '2023-05-16',
                time: '13:54:27',
                msg: '1#空压机故障',
                equip: '1号空压机',
                where: '1#空压站',
                limit: '',
                ltime: '00:00:00',
            }, {
                id: '2',
                date: '2023-05-16',
                time: '13:54:27',
                msg: '3#空压机故障',
                equip: '3号空压机',
                where: '4#空压站',
                limit: '',
                ltime: '00:00:00',
            }, {
                id: '3',
                date: '2023-05-16',
                time: '13:54:27',
                msg: '4#空压机故障',
                equip: '4号空压机',
                where: '4#空压站',
                limit: '',
                ltime: '00:00:00',
            },
            {
                id: '4',
            }, {
                id: '5'
            }, {
                id: '6'
            }, {
                id: '7'
            }, {
                id: '8'
            }, {
                id: '9'
            }, {
                id: '10'
            }, {
                id: '11'
            }, {
                id: '12'
            }, {
                id: '13'
            }, {
                id: '14'
            }]
        };
    },
}
</script>

<style>
.alert-nav {
    /* 左侧 */
    float: left;
    text-align: left !important;
    margin-top: 50px;
    margin-left: 40px;
}
</style>
