<template>
  <el-container class="layout-container-demo" style="height: 100vh">
    <el-aside class="hidden-xs-and-down"  width="200px">
      <el-scrollbar>
        <AsideMenu :app-name="appName" />
      </el-scrollbar>
    </el-aside>

    <el-container>
      <el-header style="display: flex; align-items: center; justify-content: space-between;">
           <el-button text @click="drawer = true">
            <el-icon style="margin-right: .5rem;"><IconMenu /></el-icon> Dashboard
          </el-button>
          <div class="user-avatar">
            <router-link to="/settings">
                <img v-if="settings.avatar" alt="avatar" :src="settings.avatar" class="avatar" />
                <el-icon v-else><UserFilled /></el-icon>
            </router-link>
          </div>
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
import { Menu as IconMenu, UserFilled } from '@element-plus/icons-vue'
import AsideMenu from './components/AsideMenu.vue';
import { useUserSettings } from './composables/useUserSettings'

const drawer = ref(false);
const { settings } = useUserSettings()
const appName = 'BookHub'
</script>

<style scoped>
.container {
  max-width: 1280px;
  margin: 0 auto;
}

.user-avatar .avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
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
