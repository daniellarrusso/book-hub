import { describe, it, expect, vi, beforeEach } from 'vitest';
import bookService from '../../../services/api/books';
import type { Book } from '../../../types/book';
import type { PagedResponse } from '../../../types/paged-response';
import { mockedHttpClient } from '../../../setupTests';


describe('BookService', () => {

  const mockBooks: PagedResponse<Book> = {
    items: [{ id: 1, title: '1984', author: 'George Orwell' } as Book],
    totalCount: 1,
    page: 1,
    pageSize: 10,
    totalPages: 1
  };

  beforeEach(() => {
    bookService.error.value = null;
    bookService.isLoading.value = false;
  });

  it('should fetch books successfully', async () => {
    mockedHttpClient.get.mockResolvedValueOnce({ data: mockBooks });

    const result = await bookService.getBooks(1, 10);

    expect(result).toEqual(mockBooks);
    expect(mockedHttpClient.get).toHaveBeenCalledWith('/books', { params: { search: undefined, sort: undefined, page: 1, pageSize: 10 } });
    expect(bookService.error.value).toBeNull();
    expect(bookService.isLoading.value).toBe(false);
  });

  it('should fetch with sort parameter set successfully', async () => {
    mockedHttpClient.get.mockResolvedValueOnce({ data: mockBooks });

    const result = await bookService.getBooks(1, 10, '', 'desc');

    expect(result).toEqual(mockBooks);
    expect(mockedHttpClient.get).toHaveBeenCalledWith('/books', { params: { search: undefined, sort: 'desc', page: 1, pageSize: 10 } });
    expect(bookService.error.value).toBeNull();
    expect(bookService.isLoading.value).toBe(false);
  });

  it('should fetch the correct page of books', async () => {
    const mockPage2: PagedResponse<Book> = {
      items: [
        { id: 11, title: 'Book 11', author: 'Author 11' } as Book,
        { id: 12, title: 'Book 12', author: 'Author 12' } as Book,
      ],
      totalCount: 12,
      page: 2,
      pageSize: 10,
      totalPages: 2
    };

    mockedHttpClient.get.mockResolvedValueOnce({ data: mockPage2 });

    const result = await bookService.getBooks(2, 10); // page 2

    expect(result).toEqual(mockPage2);
    expect(mockedHttpClient.get).toHaveBeenCalledWith('/books', { params: { search: undefined, sort: undefined, page: 2, pageSize: 10 } });
    expect(bookService.isLoading.value).toBe(false);
    expect(bookService.error.value).toBeNull();
  });


  it('should handle server error response', async () => {

    mockedHttpClient.get.mockRejectedValueOnce({
      response: { status: 500, data: { message: 'Server crashed' } }
    });

    await expect(bookService.getBooks(1, 10)).rejects.toThrow('Server crashed');
    expect(bookService.error.value).toBe('Server crashed');
    expect(bookService.isLoading.value).toBe(false);
  });

  it('should handle network error', async () => {
    mockedHttpClient.get.mockRejectedValueOnce({ request: {} });

    await expect(bookService.getBooks(1, 10)).rejects.toThrow('Network error - please check your connection');
    expect(bookService.error.value).toBe('Network error - please check your connection');
    expect(bookService.isLoading.value).toBe(false);
  });

  it('should handle unexpected error', async () => {
    mockedHttpClient.get.mockRejectedValueOnce(new Error('Boom!'));

    await expect(bookService.getBooks(1, 10)).rejects.toThrow('Boom!');
    expect(bookService.error.value).toBe('Boom!');
    expect(bookService.isLoading.value).toBe(false);
  });

  it('should fetch with search query', async () => {
    mockedHttpClient.get.mockResolvedValueOnce({ data: mockBooks });

    const result = await bookService.getBooks(1, 10, 'orwell');

    expect(result).toEqual(mockBooks);
    expect(mockedHttpClient.get).toHaveBeenCalledWith('/books', {
      params: { search: 'orwell', sort: undefined, page: 1, pageSize: 10 }
    });
  });

  describe("createBook", () => {
    it("should POST a book and return the created book", async () => {
      const book: Book = { id: 1, title: "Test", author: "Nathan" } as Book;
      mockedHttpClient.post.mockResolvedValue({ data: book });

      const result = await bookService.createBook(book);

      expect(mockedHttpClient.post).toHaveBeenCalledWith("/books", book);
      expect(result).toEqual(book);
    });

    it("should handle errors", async () => {
      mockedHttpClient.post.mockRejectedValue(new Error("fail"));

      await expect(bookService.createBook({ id: 1, title: "Err", author: "A" } as Book))
        .rejects.toThrow("fail");
    });
  });

  describe("updateBook", () => {
    it("should PUT and return updated book", async () => {
      const book: Book = { id: 2, title: "Updated", author: "Nathan" } as Book;
      mockedHttpClient.put.mockResolvedValue({ data: book });

      const result = await bookService.updateBook(book);

      expect(mockedHttpClient.put).toHaveBeenCalledWith("/books/2", book);
      expect(result).toEqual(book);
    });

    it("should handle errors", async () => {
      mockedHttpClient.put.mockRejectedValue(new Error("update fail"));

      await expect(bookService.updateBook({ id: 2, title: "Bad", author: "B" } as Book))
        .rejects.toThrow("update fail");
    });
  });

  describe("deleteBook", () => {
    it("should call DELETE", async () => {
      mockedHttpClient.delete.mockResolvedValue({});

      await bookService.deleteBook(3);

      expect(mockedHttpClient.delete).toHaveBeenCalledWith("/books/3");
    });

    it("should handle errors", async () => {
      mockedHttpClient.delete.mockRejectedValue(new Error("delete fail"));

      await expect(bookService.deleteBook(3)).rejects.toThrow("delete fail");
    });
  });

});
