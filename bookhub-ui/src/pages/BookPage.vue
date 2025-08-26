<template>
  <div v-if="isLoading">
    <el-skeleton :rows="10" animated />
  </div>

  <div v-else-if="book">
    <h2>{{ book.title }}</h2>
    <p><strong>Author:</strong> {{ book.author }}</p>
    <p><strong>ISBN:</strong> {{ book.isbn }}</p>
    <p>
      <strong>Rating:</strong>
      <el-rate v-model="book.rating" disabled show-score />
    </p>
    <p><strong>Comments:</strong> {{ book.comments || 'No comments' }}</p>
    <el-image
      v-if="book.coverImageUrl"
      :src="book.coverImageUrl"
      style="width: 200px; border-radius: 8px"
      fit="cover"
      alt="Book Cover"
    />
  </div>

  <div v-else>
    <p>Book not found.</p>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import type { Book } from '../types/book';
import bookService from '../services/api/books';
import { ElMessage } from 'element-plus';

const route = useRoute();
const book = ref<Book | null>(null);
const isLoading = ref(true);

async function fetchBook() {
  const id = Number(route.params.id);
  if (isNaN(id)) {
    ElMessage.error('Invalid book ID');
    isLoading.value = false;
    return;
  }

  try {
    // assuming your service has a method getBookById
    book.value = await bookService.getBookById(id);
  } catch (error: any) {
    ElMessage.error(`Failed to load book: ${error.message}`);
  } finally {
    isLoading.value = false;
  }
}

onMounted(fetchBook);
</script>
