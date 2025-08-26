<template>
  <section class="flex-container">
    <div> 
      <h2>My Books</h2>
      <small>Manage your book collection and discover new reads</small>
    </div>
    <div>
      <el-button type="primary" >Add Book</el-button>
    </div>
  </section>
  <el-skeleton v-if="isLoading" :rows="25" animated />
  <ul>
    <li v-for="book in response?.items">{{ book.title }}</li>
  </ul>
</template>

<script setup lang="ts">
  import { computed, onMounted, ref, watch } from 'vue';
  import type { PagedResponse } from '../types/paged-response';
  import type { Book } from '../types/book';
  import bookService from '../services/api/books';
  import { ElMessage } from 'element-plus';

  const response = ref<PagedResponse<Book>>()
  const search = ref('');
  const isLoading = computed(() => bookService.isLoading.value)
  const error = computed(() => bookService.error.value)

    watch(error, (newError) => {
      if (newError) {
        ElMessage.error(`Sorry, we couldn't load your books. ${newError}`)
      }
    })

  const fetchBooks = async (page: number = 1) => {
    response.value = await bookService.getBooks(search.value, page)
  } 

  onMounted(fetchBooks)

</script>

<style>
  .flex-container {
    display: flex;
    justify-content: space-between; 
    margin-bottom: 1rem;
    align-items: center;
  }
</style>