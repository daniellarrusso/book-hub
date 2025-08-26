import { ref, computed, watch } from 'vue';
import bookService from '../services/api/books';
import type { Book } from '../types/book';
import type { PagedResponse } from '../types/paged-response';

export function useBooks() {
  const response = ref<PagedResponse<Book>>();
  const search = ref('');
  const sort = ref<'asc' | 'desc'>('asc');

  const isLoading = computed(() => bookService.isLoading.value);
  const error = computed(() => bookService.error.value);

  const fetchBooks = async (page = 1, pageSize = 10) => {
    response.value = await bookService.getBooks(page, pageSize, search.value, sort.value);
  };

  watch([search, sort], () => fetchBooks());

  return {
    response,
    search,
    sort,
    isLoading,
    error,
    fetchBooks,
  };
}
