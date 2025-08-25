import { createApp } from 'vue';
import App from './App.vue';
import ElementPlus from 'element-plus';
import 'element-plus/dist/index.css';
import 'element-plus/theme-chalk/display.css';
import './style.css';

createApp(App).use(ElementPlus).mount('#app');

createApp(App).mount('#app');
