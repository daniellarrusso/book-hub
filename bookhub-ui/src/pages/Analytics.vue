<template>

<el-row :gutter="100">
  <el-col :sm="24" :md="12">
    <AuthorChart />
  </el-col>
  <el-col :sm="24" :md="12">
    <BookChart />
  </el-col>
</el-row>
    

</template>

<script setup lang="ts">
import AuthorChart from '../components/AuthorChart.vue';
import BookChart from '../components/BookChart.vue';


</script>
