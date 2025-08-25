<template>
  <el-menu 
        style="height: 100vh;" 
        :default-openeds="['1']" 
        :default-active="'1-1'"
        active-text-color="#ffd04b" 
        background-color="#2562EA" 
        text-color="#fff" >
           <div class="logo"><el-icon><PictureFilled /></el-icon> {{ props.appName }}</div>
           <el-sub-menu index="1">
          <template #title>
            <el-icon><IconMenu /></el-icon>
            <span>Dashboard</span>
          </template>
          <el-menu-item index="1-1">My Books</el-menu-item>
        </el-sub-menu index="2">
         <el-menu-item index="2-1">
          <el-icon><DataAnalysis /></el-icon>
          Analytics
        </el-menu-item>
          <el-menu-item index="3-1">
          <el-icon index="3"><Setting /></el-icon>
          Settings
        </el-menu-item>
        </el-menu>
</template>

<script setup lang="ts">
import { Menu as IconMenu, PictureFilled, Setting, DataAnalysis } from '@element-plus/icons-vue'
const props = defineProps({
  appName: String
})

</script>