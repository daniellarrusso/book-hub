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
   <el-row :gutter="10" style="margin-bottom: 1rem;">
    <el-col :span="18">
      <el-input v-model="search" @input="searchByTerm" placeholder="Search by title or author" clearable />
    </el-col>
      <el-col :span="4">
      <el-select v-model="sort" placeholder="Sort by Title">
        <el-option label="Title" value="title" @click="toggleSort" />
      </el-select>
    </el-col>
    <el-col :span="2">
      <GridToggle @show-grid="val => isGridView = val" />
    </el-col>
  </el-row>
  <el-skeleton v-if="isLoading" :rows="25" animated />
  <div v-if="isGridView">GridView</div>
  <div v-else>ListView</div>
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
  import { useDebounceFn } from '@vueuse/core';
  import GridToggle from './GridToggle.vue';

  const response = ref<PagedResponse<Book>>()
  const search = ref<string>('');
  const sort = ref<string>('title');
  const sortOrder = ref<'asc' | 'desc'>('asc');
  const ascending = ref(true);  
  const isLoading = computed(() => bookService.isLoading.value)
  const error = computed(() => bookService.error.value)
  const isGridView = ref(false);

  watch(error, (newError) => {
    if (newError) {
      ElMessage.error(`Sorry, we couldn't load your books. ${newError}`)
    }
  })

  function toggleSort() {
    ascending.value = !ascending.value; // flip boolean
    sortOrder.value = ascending.value ? 'asc' : 'desc';
    fetchBooks();
  }

  const fetchBooks = async (page: number = 1) => {
    response.value = await bookService.getBooks(page, search.value, sortOrder.value)
  } 

  const searchByTerm = useDebounceFn(() => {
      fetchBooks()
  }, 300)

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