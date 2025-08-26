<template>
    <el-form :model="localBook" label-width="100px">
      <el-form-item label="Title">
        <el-input v-model="localBook.title" :disabled="!!book"></el-input>
      </el-form-item>
      <el-form-item label="Author">
        <el-input v-model="localBook.author" :disabled="!!book"></el-input>
      </el-form-item>
      <el-form-item label="ISBN">
        <el-input v-model="localBook.isbn" :disabled="!!book"></el-input>
      </el-form-item>
      <el-form-item label="Rating">
        <el-rate v-model="localBook.rating" />
      </el-form-item>
      <el-form-item label="Comments">
        <el-input v-model="localBook.comments" type="textarea" />
      </el-form-item>
    </el-form>
    <div style="display: flex; justify-content: end;">
      <el-button @click="$emit('showModal', false)">Cancel</el-button>
      <el-button type="primary" @click="$emit('save', localBook)">Save</el-button>
    </div>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from 'vue'
import type { Book } from '../types/book';

const props = defineProps<{ book: Book | null; }>()
defineEmits<{
  (e: 'save', val: Book): void,
  (e: 'showModal', val: boolean): void
}>()

const localBook = ref<Book>({
  title: '',
  author: '',
  isbn: '',
  rating: 0,
  comments: ''
} as Book)

watch(
  () => props.book,
  val => {
    localBook.value = val ? { ...val } : { title: '', author: '', isbn: '', rating: 0, comments: '' } as Book
  },
  { immediate: true }
)
</script>
