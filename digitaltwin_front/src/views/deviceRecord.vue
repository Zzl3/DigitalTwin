<template>
    <div>
        <div style="display: flex; justify-content: space-between; margin-top: 4%;">
            <div>
                <el-card class="box-card" >
                    <div slot="header" class="clearfix">
                        <span style="font-family:Arial, Helvetica, sans-serif">空压机高压电表区</span>
                    </div>
                    <div>
                        <div v-for="o in 3" :key="o" class="text item">
                            <el-card>
                                <div v-for="j in 3" :key="j">
                                    <el-col :span="8">
                                        <el-card shadow="hover"  class="inner-box1">
                                            <span style="font-style:oblique;">{{o*3-3+j}}#空压机</span>
                                            <span>
                                                <p>{{ highPowerElectricity[o*3-3+j-1].volume }} KW·h</p>
                                                <p>{{ highPowerElectricity[o*3-3+j-1].velocity }} KW</p>
                                            </span>
                                        </el-card>
                                    </el-col>
                                </div>
                            </el-card>
                        </div>
                    </div>
                </el-card>
                <el-card class="box-card" >
                    <div slot="header" class="clearfix">
                        <span style="font-family:Arial, Helvetica, sans-serif">空压机流量计区</span>
                    </div>
                    <div>
                        <div v-for="o in 3" :key="o" class="text item">
                            <el-card>
                                <div v-for="j in 3" :key="j">
                                    <el-col :span="8">
                                        <el-card shadow="hover"  class="inner-box2">
                                            <span style="font-style:oblique;">{{o*3-3+j}}#空压机</span>
                                            <span>
                                                <p>{{ streamVolume[o*3-3+j-1].volume }} m<sup>3</sup> /min </p>
                                                <p>{{ streamVolume[o*3-3+j-1].velocity }} m<sup>3</sup> </p>
                                            </span>
                                        </el-card>
                                    </el-col>
                                </div>
                            </el-card>
                        </div>
                    </div>
                </el-card>
            </div>
            <div>
                <el-card class="box-card" shadow="hover">
                    <div slot="header" class="clearfix">
                        <span>空压机低压电表区</span>
                    </div>
                    <div v-for="o in 6" :key="o" class="text item">
                            <el-card>
                                <div v-for="j in 3" :key="j">
                                    <el-col :span="8">
                                        <el-card shadow="hover"  class="inner-box3">
                                            <span style="font-style:oblique;">{{o*3-3+j}}#空压机</span>
                                            <span>
                                                <p>{{ lowPowerElectricity[o*3-3+j-1].volume }} KW·h</p>
                                                <p>{{ lowPowerElectricity[o*3-3+j-1].velocity }} KW</p>
                                            </span>
                                        </el-card>
                                    </el-col>
                                </div>
                            </el-card>
                        </div>
                </el-card>
            </div>
            <div>
                <div>
                    <el-card class="box-card" shadow="hover">
                        <div slot="header" class="clearfix">
                            <span>气配间流量计区</span>
                        </div>
                        <div v-for="o in 4" :key="o" class="text item">
                            <el-card>
                                <div v-for="j in 3" :key="j">
                                    <el-col :span="8">
                                        <el-card shadow="hover"  class="inner-box4">
                                            <span style="font-style:oblique;">{{o*3-3+j}}#码头</span>
                                            <span>
                                                <p>{{ streamVolumeInVapourRoom[o*3-3+j-1].volume }} m<sup>3</sup> /min</p>
                                                <p>{{ streamVolumeInVapourRoom[o*3-3+j-1].velocity }} m<sup>3</sup> </p>
                                            </span>
                                        </el-card>
                                    </el-col>
                                </div>
                            </el-card>
                        </div>
                </el-card>
                </div>
                <div>
                    <el-card class="box-card" shadow="hover">
                        <div slot="header" class="clearfix">
                            <span>水表区</span>
                        </div>
                        <div v-for="o in 2" :key="o" class="text item">
                            <el-card>
                                <div v-for="j in 3" :key="j">
                                    <el-col :span="8">
                                        <el-card shadow="hover"  class="inner-box5">
                                            <span style="font-style:oblique;">{{o*3-3+j}}#水表</span>
                                            <span>
                                                <p>{{ streamVolumeInVapourRoom[o*3-3+j-1].volume }} m<sup>3</sup> /min</p>
                                            </span>
                                        </el-card>
                                    </el-col>
                                </div>
                            </el-card>
                        </div>
                    </el-card>
                </div>
            </div>
        </div>
    </div>
</template>
  
<script>
export default{
    data(){
        return {
            highPowerElectricity:[
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
            ],
            streamVolume:[
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
            ],
            lowPowerElectricity:[
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
            ],
            streamVolumeInVapourRoom:[
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
                {
                    volume : 1215,
                    velocity : 127840,
                },
            ],
            Water:[
                {
                    volume : 1215,
                },
                {
                    volume : 1215,
                },
                {
                    volume : 1215,
                },
                {
                    volume : 1215,
                },
                {
                    volume : 1215,
                },
                {
                    volume : 1215,
                },
            ],
        };
    },
}
</script>
  
<style>
.text {
    font-size: 14px;
}

.item {
    margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
    display: table;
    content: "";
}
.clearfix:after {
    clear: both
}

.box-card {
    opacity: 75%;
    width: 480px;
}

.inner-box1{
    opacity: 85%;
    background: #FFFFF0;
    margin-bottom: 5%;
}

.inner-box2{
    opacity: 85%;
    background: #E6E6FA;
    margin-bottom: 5%;
}

.inner-box3{
    opacity: 85%;
    background: #F8F8FF;
    margin-bottom: 20%;
}

.inner-box4{
    opacity: 85%;
    background: #F0FFF0;
    margin-bottom: 5%;
}

.inner-box5{
    opacity: 85%;
    background: #FFFAFA;
    margin-bottom: 20%;
}
</style>
  