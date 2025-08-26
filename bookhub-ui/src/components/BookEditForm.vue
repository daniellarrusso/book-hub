<template>
  <el-form
    ref="bookFormRef"
    :model="localBook"
    :rules="rules"
    label-width="100px"
  >
    <el-form-item label="Title" prop="title">
      <el-input v-model="localBook.title" :disabled="!!localBook.id"></el-input>
    </el-form-item>

    <el-form-item label="Author" prop="author">
      <el-input v-model="localBook.author" :disabled="!!localBook.id"></el-input>
    </el-form-item>

    <el-form-item label="ISBN" prop="isbn">
      <el-input v-model="localBook.isbn" :disabled="!!localBook.id"></el-input>
    </el-form-item>

    <el-form-item label="Rating" prop="rating">
      <el-rate v-model="localBook.rating" :max="5" />
    </el-form-item>

    <el-form-item label="Comments" prop="comments">
      <el-input v-model="localBook.comments" type="textarea" />
    </el-form-item>

    <div style="display: flex; justify-content: end; gap: 0.5rem;">
      <el-button @click="$emit('showModal', false)">Cancel</el-button>
      <el-button type="primary" @click="handleSave">Save</el-button>
    </div>
  </el-form>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from 'vue';
import type { Book } from '../types/book';

const props = defineProps<{ book: Book | null }>();
const emit = defineEmits<{
  (e: 'save', val: Book): void;
  (e: 'showModal', val: boolean): void;
}>();

const localBook = ref<Book>({
  title: '',
  author: '',
  isbn: '',
  rating: 1,
  comments: '',
  ...props.book
} as Book);

watch(
  () => props.book,
  (val) => {
    localBook.value = {
      title: '',
      author: '',
      isbn: '',
      rating: 1,
      comments: '',
      ...val
    } as Book;
  },
  { immediate: true }
);

// Validation rules
const rules = {
  title: [
    { required: true, message: 'Title is required', trigger: 'blur' },
    { min: 2, message: 'Title must be at least 2 characters', trigger: 'blur' }
  ],
  author: [
    { required: true, message: 'Author is required', trigger: 'blur' },
    { min: 2, message: 'Author must be at least 2 characters', trigger: 'blur' }
  ],
  isbn: [
    { required: true, message: 'ISBN is required', trigger: 'blur' },
    { pattern: /^[0-9\-]+$/, message: 'ISBN must be numeric or hyphenated', trigger: 'blur' }
  ]
};

const bookFormRef = ref();

function handleSave() {
  bookFormRef.value.validate((valid: boolean) => {
    if (valid) {
      emit('save', localBook.value);
    } else {
      return false;
    }
  });
}
</script>
