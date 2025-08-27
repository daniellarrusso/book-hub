<template>
  <el-card class="settings-card">
    <h2>User Settings</h2>

    <el-form :model="settings" label-width="120px" :rules="rules" ref="formRef">
      
      <el-form-item label="Avatar" style="align-items: center;">
      <el-upload
        class="avatar-uploader"
        :show-file-list="false"
        :on-change="handleAvatarChange"
        :before-upload="beforeUpload"
        :auto-upload="false"
      >
        <img v-if="settings.avatar" alt="avatar" :src="settings.avatar" class="avatar" />
        <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
      </el-upload>
      </el-form-item>


      <el-form-item label="Name" prop="name">
        <el-input v-model="settings.name" placeholder="Enter your name" />
      </el-form-item>

      <el-form-item label="Email" prop="email">
        <el-input v-model="settings.email" placeholder="Enter your email" />
      </el-form-item>

      <el-form-item label="Notifications">
        <el-switch
          v-model="settings.notifications"
          active-text="Enabled"
          inactive-text="Disabled"
        />
      </el-form-item>

      <el-form-item>
        <el-button @click="resetSettings">Reset</el-button>
        <el-button type="primary" @click="saveSettings">Save</el-button>
      </el-form-item>
    </el-form>
  </el-card>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { ElMessage } from 'element-plus';
import { Plus } from '@element-plus/icons-vue';
import { useUserSettings } from '../composables/useUserSettings';


const { settings, saveSettings, resetSettings } = useUserSettings()
const formRef = ref()

const rules = {
  name: [{ required: true, message: 'Name is required', trigger: 'blur' }],
  email: [
    { required: true, message: 'Email is required', trigger: 'blur' },
    { type: 'email', message: 'Please enter a valid email', trigger: 'blur' }
  ]
};

watch(settings, (newSettings) => {
  localStorage.setItem('userSettings', JSON.stringify(newSettings));
}, { deep: true });

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function handleAvatarChange(file: any) {
  const reader = new FileReader()
  reader.onload = (e) => {
    settings.avatar = e.target?.result as string
  }
  reader.readAsDataURL(file.raw)
}

function beforeUpload(file: File) {
  const isImage = file.type.startsWith('image/')
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isImage) ElMessage.error('Avatar must be an image file!')
  if (!isLt2M) ElMessage.error('Avatar size must be smaller than 2MB!')
  return isImage && isLt2M
}
</script>

<style scoped>
.settings-card {
  max-width: 600px;
  margin: 2rem auto;
  padding: 2rem;
}

.avatar-uploader {
  display: flex;
  align-items: center;
  cursor: pointer;
  width: 80px;
  height: 80px;
  border-radius: 50%;
  overflow: hidden;
  background-color: #f0f0f0;
  justify-content: center;
}

.avatar {
  width: 80px;
  height: 80px;
  border-radius: 50%;
  object-fit: cover;
}

.avatar-placeholder {
  font-size: 32px;
  color: #999;
}
</style>
