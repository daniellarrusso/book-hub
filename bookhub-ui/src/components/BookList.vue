<template>
  <section class="flex-container">
    <div> 
      <h2>My Books</h2>
      <small>Manage your book collection and discover new reads</small>
    </div>
    <div>
      <el-button type="primary" @click="openEdit" >Add Book</el-button>
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
    title="Save Book"
    width="500px">
    <BookEditForm :book="selectedBook" @save="saveBook" @show-modal="(event) => addEditModalVisible = event" />
  </el-dialog>

  <el-pagination v-if="response"
      v-model:current-page="response.page"
      :page-size="response.pageSize"
      :total="response.totalCount"
      layout="prev, pager, next"
      class="mt-3"
      @current-change="fetchBooks"
    />
</template>

<script setup lang="ts">
  import { computed, onMounted, ref, watch } from 'vue';
  import type { Book } from '../types/book';
  import bookService from '../services/api/books';
  import { ElMessage, ElMessageBox } from 'element-plus';
  import { useDebounceFn } from '@vueuse/core';
  import GridToggle from './GridToggle.vue';
  import { useBooks } from '../composables/useBooks';
  import BookCardList from './BookCardList.vue';
  import BookEditForm from './BookEditForm.vue';
import { useRouter } from 'vue-router';
  

  const { response, search, sort, isLoading, fetchBooks } = useBooks()
  const error = computed(() => bookService.error.value)
  const isGridView = ref(false);
  const selectedBook = ref<Book | null>(null);
  const router = useRouter();

  const addEditModalVisible = ref(false);
  const openEdit = (book: Book) => { selectedBook.value = book;  addEditModalVisible.value = true; }
  const openDelete = (book: Book) => { confirmDeleteDialog(book.id!) }
  const openView = (book: Book) => { if (!book.id) return; router.push({ name: 'Book', params: { id: book.id } }); }

  watch(error, (newError) => {
    if (newError) {
      ElMessage.error(`Sorry, we couldn't load your books. ${newError}`)
    }
  })

  const confirmDeleteDialog = async (bookId: number) => {
      try {
      await ElMessageBox.confirm(
        `Are you sure you want to delete a book/review?`,
        'Delete Book',
        {
          confirmButtonText: 'Delete',
          cancelButtonText: 'Cancel',
          type: 'warning',
        }
      );

      await bookService.deleteBook(bookId);
      ElMessage.success('Book deleted successfully');
      fetchBooks(); 
    } catch (err) {
      if (err === 'cancel') {

        return;
      }
      ElMessage.error('Failed to delete book');
    }
  }

  function toggleSort() {
    sort.value = sort.value === 'desc'? 'asc' : 'desc';
    fetchBooks();
  }

  async function saveBook(updatedBook: Book) {
    try {
      if (updatedBook.id) {
        await bookService.updateBook(updatedBook);
      } else {
        await bookService.createBook(updatedBook);
      }
      fetchBooks()
    } finally {
      addEditModalVisible.value = false
    }
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