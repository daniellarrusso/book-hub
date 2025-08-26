<!-- GridToggle.vue -->
<template>
  <el-button-group>
    <el-button :type="!showGrid ? 'primary' : ''" :icon="List" @click="toggleGridView" />
    <el-button :type="showGrid ? 'primary' : ''" :icon="Grid" @click="toggleGridView" />
  </el-button-group>
</template>

<script setup lang="ts">
import { List, Grid } from '@element-plus/icons-vue'
import { ref, defineEmits } from 'vue'

const showGrid = ref(false)

const emit = defineEmits<{
  (e: 'showGrid', value: boolean): void
}>()

const toggleGridView = () => {
  showGrid.value = !showGrid.value
  emit('showGrid', showGrid.value)
}
</script>
