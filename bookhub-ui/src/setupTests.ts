import { vi, beforeEach } from 'vitest';
import httpClient from './services/http';

// Cast httpClient so TypeScript knows about mocks
export const mockedHttpClient = httpClient as unknown as {
  get: ReturnType<typeof vi.fn>;
};

// Set a default mock for all tests
mockedHttpClient.get = vi.fn();

// Optional: reset mocks before each test automatically
beforeEach(() => {
  vi.clearAllMocks();
});
