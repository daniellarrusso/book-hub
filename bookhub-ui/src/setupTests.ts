import { vi, beforeEach } from 'vitest';
import httpClient from './services/http';

export const mockedHttpClient = httpClient as unknown as {
  get: ReturnType<typeof vi.fn>;
  post: ReturnType<typeof vi.fn>;
  put: ReturnType<typeof vi.fn>;
  delete: ReturnType<typeof vi.fn>;
};

mockedHttpClient.get = vi.fn();
mockedHttpClient.post = vi.fn();
mockedHttpClient.delete = vi.fn();
mockedHttpClient.put = vi.fn();

beforeEach(() => {
  vi.clearAllMocks();
});
