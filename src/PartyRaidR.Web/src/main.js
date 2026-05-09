import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import './assets/main.scss'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'

import Vue3Toastify from 'vue3-toastify'

const app = createApp(App)

app.use(createPinia())
app.use(router)

// Vue3Toastify configuration
app.use(Vue3Toastify, {
    autoClose: 3000
})

app.mount('#app')
