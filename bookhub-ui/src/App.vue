<template>
  <el-container class="layout-container-demo" style="height: 100vh">
    <el-aside class="hidden-xs-and-down"  width="200px">
      <el-scrollbar>
        <AsideMenu :app-name="appName" />
      </el-scrollbar>
    </el-aside>

    <el-container>
      <el-header style="padding-top: 1rem;">
           <el-button text @click="drawer = true">
            <el-icon style="margin-right: .5rem;"><IconMenu /></el-icon> Dashboard
          </el-button>
      </el-header>

      <el-drawer 
        title="I am the title" :with-header="false"
        v-model="drawer"
        direction="ltr"
        size="200px"
    >
      <AsideMenu :app-name="appName" />
    </el-drawer>

      <el-main>
        <div class="container">
        <RouterView />
        </div>
      </el-main>

    </el-container>
  </el-container>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { Menu as IconMenu } from '@element-plus/icons-vue'
import AsideMenu from './components/AsideMenu.vue';

const drawer = ref(false);
const appName = 'BookHub'
</script>

<style scoped>
.container {
  max-width: 1280px;
  margin: 0 auto;
}


@media (max-width:768px) {
  .hidden-xs-and-down {
    display: none;
  }
}
.el-header {
  position: relative;
  background-color: #fff;
  color: var(--el-text-color-primary);
} 
</style>
