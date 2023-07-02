<template>
    <div>
        <div class="alert-nav">
            <span style="margin-right: 40px;">
                实时报警/历史报警
            </span>

            <el-select v-model="selectedValue" placeholder="全部空压站" @change="filterTable">
                <el-option v-for="option in options" :key="option.value" :label="option.label"
                    :value="option.value"></el-option>
            </el-select>


            <el-button type="primary" plain style="margin-left:50px;padding: 10px 30px;"
                @click="filterCurrentAlerts">当前报警</el-button>
            <el-button type="primary" plain style="padding: 10px 30px;" @click="filterHistoryAlerts">历史报警</el-button>
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
        // this.getDataM5();
        this.tableData = this.generateRandomData();
        this.alldata = JSON.parse(JSON.stringify(this.tableData));  // 备份原始数据
    },
    data() {
        return {
            alldata: [],
            selectedValue: "option0",
            currentOnly: false,
            options: [
                { value: "option0", label: "全部空压站" },
                { value: "option1", label: "空压站1" },
                { value: "option2", label: "空压站2" },
                { value: "option3", label: "空压站3" },
                { value: "option4", label: "空压站4" },
            ],
            tableData: []
        };
    },
    methods: {
        generateRandomData() {
            let result = [];
            for (let i = 1; i <= 14; i++) {
                let id = i;

                // Random date and time in the last 30 days
                let timestamp = Date.now() - Math.random() * 1000 * 60 * 60 * 24 * 30;
                let randomDate = new Date(timestamp);
                let date;
                if (i < 5) {
                    // 日期为今天 如2023-07-02
                    date = new Date().toISOString().split('T')[0];
                }
                else {
                    date = randomDate.toISOString().split('T')[0];
                }
                let time = randomDate.toTimeString().split(' ')[0];

                let equipId = Math.ceil(Math.random() * 4); // assuming there are 4 equipments
                let msg = `${equipId}#空压机故障`;
                let equip = `${equipId}号空压机`;
                let where = `${equipId}#空压站`;
                let limit = ''; // fill it as per your requirement
                let ltime = '00:00:00'; // fill it as per your requirement

                let data = {
                    id: id.toString(),
                    date: date,
                    time: time,
                    msg: msg,
                    equip: equip,
                    where: where,
                    limit: limit,
                    ltime: ltime
                };

                result.push(data);
            }

            result.sort((a, b) => {
                let dateA = new Date(a.date + ' ' + a.time);
                let dateB = new Date(b.date + ' ' + b.time);
                return dateB - dateA; // for descending order
            });

            for (let i = 0; i < result.length; i++) {
                result[i].id = (i + 1).toString();
            }

            return result;
        },
        filterTable(value) {
            this.selectedValue = value;
            this.filterData();
        },

        filterCurrentAlerts() {
            this.currentOnly = true;
            this.filterData();
        },

        filterHistoryAlerts() {
            this.currentOnly = false;
            this.filterData();
        },

        filterData() {
            let data = this.alldata;

            // Filter by station
            if (this.selectedValue !== 'option0') {
                let stationNumber = this.selectedValue.slice(-1) + "#空压站";
                data = data.filter(data => data.where === stationNumber);
            }

            // Filter by date
            let today = new Date().toISOString().split('T')[0];
            if (this.currentOnly) {
                data = data.filter(data => data.date === today);
            } else {
                data = data.filter(data => data.date !== today);
            }

            this.tableData = data;
        },



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
