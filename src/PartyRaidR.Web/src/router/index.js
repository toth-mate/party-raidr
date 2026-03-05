import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import EventDetailsView from '@/views/EventDetailsView.vue'
import ProfileView from '@/views/ProfileView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { title: 'Home', requiresAuthentication: false }
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
      meta: { title: 'Login', requiresAuthentication: false }
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
      meta: { title: 'Register', requiresAuthentication: false }
    },
    {
      path: '/events/:id',
      name: 'event-details',
      component: EventDetailsView,
      meta: { title: 'Event Details', requiresAuthentication: false }
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView,
      meta: { title: 'Profile', requiresAuthentication: true }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  if(to.meta.requiresAuthentication && !authStore.isAuthenticated) {
      next('/login')
  } else {
    next()
  }
})

export default router
