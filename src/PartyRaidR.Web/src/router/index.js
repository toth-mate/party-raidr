import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import EventDetailsView from '@/views/EventDetailsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { title: 'Home' }
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
      meta: { title: 'Login' }
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
      meta: { title: 'Register' }
    },
    {
      path: '/events/:id',
      name: 'event-details',
      component: EventDetailsView,
      meta: { title: 'Event Details' }
    }
  ]
})

export default router
