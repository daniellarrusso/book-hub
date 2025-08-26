/// <reference types="vitest" />
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  test: {
    globals: true,           // so you don’t need to import describe/it/expect
    environment: 'jsdom',    // simulate browser environment
    setupFiles: './src/setupTests.ts', // optional, for global test setup
  },
});
