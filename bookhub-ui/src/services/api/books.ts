import type { AxiosResponse } from 'axios';
import type { Book } from '../../types/book';
import type { PagedResponse } from '../../types/paged-response';
import httpClient from '../http';
import { ref } from 'vue';

class BookService {
  private readonly endpoint = '/books';
  isLoading = ref(false);
  error = ref<string | null>(null);

  async getBooks(page: number = 1, search: string | undefined = undefined, sort: string | undefined = undefined): Promise<PagedResponse<Book>> {
    this.isLoading.value = true;
    this.clearError();
    search = search ? search : undefined;

    try {
      const response: AxiosResponse<PagedResponse<Book>> = await httpClient.get(
        `${this.endpoint}`, {
        params: { page, pageSize: 10, search, sort }
      }
      );
      return response.data;
    } catch (error) {
      const handledError = this.handleError(error);
      this.setError(handledError);
      throw handledError;
    } finally {
      this.isLoading.value = false;
    }
  }

  async getBookById(id: number): Promise<Book> {
    this.clearError();
    try {
      const res = await httpClient.get<Book>(`${this.endpoint}/${id}`);
      return res.data;
    } catch (error) {
      const handledError = this.handleError(error);
      this.setError(handledError);
      throw handledError;
    }
  }

  async createBook(book: Book): Promise<Book> {
    this.clearError();
    try {
      const res = await httpClient.post<Book>(`${this.endpoint}`, book);
      return res.data;
    } catch (error) {
      const handledError = this.handleError(error);
      this.setError(handledError);
      throw handledError;
    }
  }

  async updateBook(book: Book): Promise<Book> {
    this.clearError();
    try {
      const res = await httpClient.put<Book>(`${this.endpoint}/${book.id}`, book);
      return res.data;
    } catch (error) {
      const handledError = this.handleError(error);
      this.setError(handledError);
      throw handledError;
    }
  }

  async deleteBook(bookId: number) {
    this.clearError();
    try {
      await httpClient.delete(`${this.endpoint}/${bookId}`);
    } catch (error) {
      const handledError = this.handleError(error);
      this.setError(handledError);
      throw handledError;
    }
  }

  private setError(error: Error) {
    this.error.value = error.message;
  }

  private clearError() {
    this.error.value = null;
  }

  private handleError(error: any): Error {
    if (error.response) {
      //  server response
      const { status, data } = error.response;
      return new Error(data?.message || `HTTP Error ${status} ${data}`);
    } else if (error.request) {
      return new Error('Network error - please check your connection');
    } else {
      return new Error(error.message || 'An unexpected error occurred');
    }
  }

}

export const bookService = new BookService();
export default bookService;;