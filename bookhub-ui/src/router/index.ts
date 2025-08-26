import { createRouter, createWebHistory } from 'vue-router';
import BookList from '../components/BookList.vue';
import Settings from '../pages/Settings.vue';
import Analysis from '../pages/Analytics.vue';

const routes = [
  { path: '/', name: 'Books', component: BookList },
  { path: '/settings', name: 'Settings', component: Settings },
  { path: '/analytics', name: 'Analysis', component: Analysis },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
