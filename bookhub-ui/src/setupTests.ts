import { vi, beforeEach } from 'vitest';
import httpClient from './services/http';

// Cast httpClient so TypeScript knows about mocks
export const mockedHttpClient = httpClient as unknown as {
  get: ReturnType<typeof vi.fn>;
  post: ReturnType<typeof vi.fn>;
  put: ReturnType<typeof vi.fn>;
  delete: ReturnType<typeof vi.fn>;
};

// Set a default mock for all tests
mockedHttpClient.get = vi.fn();
mockedHttpClient.post = vi.fn();
mockedHttpClient.delete = vi.fn();
mockedHttpClient.put = vi.fn();

// Optional: reset mocks before each test automatically
beforeEach(() => {
  vi.clearAllMocks();
});
