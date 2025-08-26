// services/http.ts - Axios configuration
import axios from 'axios';

// Create axios instance with base configuration
const httpClient = axios.create({
  baseURL: 'http://localhost:5228/api',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

export default httpClient;