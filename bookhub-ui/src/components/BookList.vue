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
      <el-select valiue="Sort" placeholder="Sort by Title">
        <el-option label="Title" value="title" @click="toggleSort" />
      </el-select>
    </el-col>
    <el-col :span="2">
      <GridToggle @show-grid="val => isGridView = val" />
    </el-col>
  </el-row>
  <el-skeleton v-if="isLoading" :rows="25" animated />
  <div v-if="isGridView">GridView</div>
  <div v-else>
    <BookCardList
      v-for="book in response?.items"
      :key="book.id"
      :book="book"
      @edit="openEdit"
      @delete="openDelete"
      @view="openView"
    />
  </div>

  <el-dialog
    v-model="addEditModalVisible"
    title="Edit Book"
    width="500px">
    <BookEditForm :book="selectedBook" @save="saveBook" @show-modal="(event) => addEditModalVisible = event" />
  </el-dialog>
</template>

<script setup lang="ts">
  import { computed, onMounted, ref, watch } from 'vue';
  import type { Book } from '../types/book';
  import bookService from '../services/api/books';
  import { ElMessage } from 'element-plus';
  import { useDebounceFn } from '@vueuse/core';
  import GridToggle from './GridToggle.vue';
  import { useBooks } from '../composables/useBooks';
  import BookCardList from './BookCardList.vue';
  import BookEditForm from './BookEditForm.vue';
  

  const { response, search, sort, isLoading, fetchBooks } = useBooks()
  const error = computed(() => bookService.error.value)
  const isGridView = ref(false);
  const selectedBook = ref<Book | null>(null);
  const deleteDialogVisible = ref(false);
  const addEditModalVisible = ref(false);
  const openEdit = (book: Book) => { selectedBook.value = book;  addEditModalVisible.value = true; console.log('something') }
  const openDelete = (book: Book) => console.log('delete', book)
  const openView = (book: Book) => console.log('view', book)

  watch(error, (newError) => {
    if (newError) {
      ElMessage.error(`Sorry, we couldn't load your books. ${newError}`)
    }
  })

  function toggleSort() {
    sort.value = sort.value === 'desc'? 'asc' : 'desc';
    fetchBooks();
  }

  async function saveBook(updatedBook: Book) {
  // await bookService.updateBook(updatedBook.id, updatedBook)
    addEditModalVisible.value = false
    fetchBooks()
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