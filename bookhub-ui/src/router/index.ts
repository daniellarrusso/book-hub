import { createRouter, createWebHistory } from 'vue-router';
import BooksList from '../pages/BooksList.vue';
import Settings from '../pages/Settings.vue';
import Analysis from '../pages/Analytics.vue';
import BookView from '../pages/BookView.vue';

const routes = [
  { path: '/', name: 'Books', component: BooksList },
  { path: '/settings', name: 'Settings', component: Settings },
  { path: '/analytics', name: 'Analysis', component: Analysis },
  { path: '/book/:id', name: 'Book', component: BookView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
