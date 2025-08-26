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
  <div v-else v-for="book in response?.items" class="flex-container card">
    <div class="left-column" style="display: flex; gap: 10px; align-items: center;">
      <el-image
        style="width: 75px; object-fit: cover; border-radius: 8px;"
        :src="book.coverImageUrl"
        fit="cover"
        alt="Book Cover"
        lazy
      />
      <div class="book-details">
        {{ book.title }}
        <div> {{ book.author }}</div>
        <div> {{ book.isbn }}</div>
      </div>
      <div v-if="book.hasNotes" style="align-self: end; color: green;" class="has-notes">
           <el-icon style="margin-right: .1rem;"><ChatLineSquare /></el-icon> Has notes
      </div>
    </div>
    <div class="right-column">  
      <div style="display: flex; align-items: center;">
         <el-rate style="margin-right: 1rem;" disabled v-model="book.rating"  show-score score-template="{value}/5" />
         <el-button type="primary" size="small" :icon="Edit" @click="selectedBook = book; addEditModalVisible = true;"  />
         <el-button type="primary" size="small" :icon="View" />
         <el-button type="primary" size="small" :icon="Delete" @click="selectedBook = book; deleteDialogVisible = true;" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { computed, onMounted, ref, watch } from 'vue';
  import type { PagedResponse } from '../types/paged-response';
  import type { Book } from '../types/book';
  import bookService from '../services/api/books';
  import { ElMessage } from 'element-plus';
  import { useDebounceFn } from '@vueuse/core';
  import GridToggle from './GridToggle.vue';
  import { Edit, View, Delete, ChatLineSquare } from '@element-plus/icons-vue'
  
  const response = ref<PagedResponse<Book>>()
  const search = ref<string>('');
  const sort = ref<string>('title');
  const sortOrder = ref<'asc' | 'desc'>('asc');
  const ascending = ref(true);  
  const isLoading = computed(() => bookService.isLoading.value)
  const error = computed(() => bookService.error.value)
  const isGridView = ref(false);
  const selectedBook = ref<Book | null>(null);
  const deleteDialogVisible = ref(false);
  const addEditModalVisible = ref(false);

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
  .flex-container.card {
    padding: .75rem;
    background-color: #fff;
    border-radius: 10px;
  }
</style>