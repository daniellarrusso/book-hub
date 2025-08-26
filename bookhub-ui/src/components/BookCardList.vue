<template>
  <div class="flex-container card">
    <div class="left-column">
      <el-image
        class="book-cover"
        :src="`${book.coverImageUrl}/75`"
        fit="cover"
        alt="Book Cover"
        lazy
      />
      <div class="book-details">
        <div class="title">{{ book.title }}</div>
        <div class="author">{{ book.author }}</div>
        <div class="isbn">{{ book.isbn }}</div>

        <div class="mobile-actions">
          <el-rate disabled v-model="book.rating" show-score score-template="{value}/5" />
          <div class="buttons">
            <el-button type="primary" size="small" :icon="Edit" @click="$emit('edit', book)" />
            <el-button type="primary" size="small" :icon="View" @click="$emit('view', book)" />
            <el-button type="primary" size="small" :icon="Delete" @click="$emit('delete', book)" />
          </div>
        </div>
      </div>
    </div>
    <div class="right-column desktop-actions">
      <el-rate style="margin-right: 1rem;" disabled v-model="book.rating" show-score score-template="{value}/5" />
      <el-button type="primary" size="small" :icon="Edit" @click="$emit('edit', book)" />
      <el-button type="primary" size="small" :icon="View" @click="$emit('view', book)" />
      <el-button type="primary" size="small" :icon="Delete" @click="$emit('delete', book)" />
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


<style>
.flex-container.card {
  display: flex;
  justify-content: space-between;
  padding: .75rem;
  background-color: #fff;
  border-radius: 10px;
  gap: 10px;
  flex-wrap: wrap;
}

.left-column {
  display: flex;
  gap: 10px;
  align-items: flex-start;
  flex: 1;
}

.book-cover {
  width: 75px;
  height: 100px;
  object-fit: cover;
  border-radius: 8px;
}

.book-details {
  display: flex;
  flex-direction: column;
  gap: 5px;
  flex: 1;
}

.mobile-actions {
  display: none;
  flex-direction: column;
  gap: 5px;
  margin-top: 8px;
}

.desktop-actions {
  display: flex;
  align-items: center;
  gap: 5px;
}

/* Responsive */
@media (max-width: 768px) {
  .desktop-actions {
    display: none;
  }
  .mobile-actions {
    display: flex;
  }
}
</style>