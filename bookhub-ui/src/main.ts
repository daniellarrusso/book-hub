import { createApp } from 'vue';
import App from './App.vue';
import ElementPlus from 'element-plus';
import 'element-plus/dist/index.css';
import 'element-plus/theme-chalk/display.css';
import './style.css';
import router from './router';

createApp(App).use(router).use(ElementPlus).mount('#app');

