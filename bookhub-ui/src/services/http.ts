// services/http.ts - Axios configuration
import axios from 'axios';

// Create axios instance with base configuration
const httpClient = axios.create({
  baseURL: process.env.VUE_APP_API_URL || 'http://localhost:5200/api',
  timeout: 3,
  headers: {
    'Content-Type': 'application/json',
  },
});

export default httpClient;