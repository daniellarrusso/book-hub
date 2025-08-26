<template>
  <div class="flex-container card">
    <div class="left-column" style="display: flex; gap: 10px; align-items: center;">
      <el-image
        style="width: 75px; object-fit: cover; border-radius: 8px;"
        :src="`${book.coverImageUrl}/75`"
        fit="cover"
        alt="Book Cover"
        lazy
      />
      <div class="book-details">
        {{ book.title }}
        <div>{{ book.author }}</div>
        <div>{{ book.isbn }}</div>
      </div>
      <div v-if="book.hasNotes" style="align-self: end; color: green;" class="has-notes">
        <el-icon style="margin-right: .1rem;"><ChatLineSquare /></el-icon> Has notes
      </div>
    </div>
    <div class="right-column">  
      <div style="display: flex; align-items: center;">
         <el-rate style="margin-right: 1rem;" disabled v-model="book.rating" show-score score-template="{value}/5" />
         <el-button type="primary" size="small" :icon="Edit" @click="$emit('edit', book)" />
         <el-button type="primary" size="small" :icon="View" @click="$emit('view', book)" />
         <el-button type="primary" size="small" :icon="Delete" @click="$emit('delete', book)" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Book } from '../types/book'
import { Edit, View, Delete, ChatLineSquare } from '@element-plus/icons-vue'

defineProps<{ book: Book }>()
defineEmits<{
  (e: 'edit', book: Book): void
  (e: 'view', book: Book): void
  (e: 'delete', book: Book): void
}>()
</script>
