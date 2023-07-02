<template>
  <div class="content">
    <div>
      <span>系统操作</span>
      <br />
      <i class="el-icon-edit"></i>
      <span class="space">用气计划</span>
      <el-tag class="tag">已完成</el-tag>
      <br />
      <i class="el-icon-share"></i>
      <span class="space">设备参数</span>
      <el-tag class="tag" type="warning">在修订</el-tag>
      <br />
      <i class="el-icon-help"></i>
      <span class="space">水管阀门</span>
      <el-tag class="tag">已完成</el-tag>
      <br />
      <i class="el-icon-s-order"></i>
      <span class="space">数据抄袭</span>
      <el-tag class="tag" type="danger">待修订</el-tag>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
.content {
  border-radius: 10px;
  background-color: #ffffff;
  box-shadow: 20px 20px 60px #bebebe, -20px -20px 60px #ffffff;
}
.space {
  margin-left: 10px;
}
.tag{
    margin-left:30px
}
</style>
