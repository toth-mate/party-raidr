import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import CreateEventView from '@/views/CreateEventView.vue'
import EventDetailsView from '@/views/EventDetailsView.vue'
import ProfileView from '@/views/ProfileView.vue'
import AdminDashboard from '@/views/Admin/AdminDashboard.vue'
import AdminLogin from '@/views/Admin/AdminLogin.vue'

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
      path: '/event/new',
      name: 'new-event',
      component: CreateEventView,
      meta: { title: 'Create event', requiresAuthentication: true }
    },
    {
      path: '/event/:id',
      name: 'event-details',
      component: EventDetailsView,
      meta: { title: 'Event Details', requiresAuthentication: false }
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView,
      meta: { title: 'Profile', requiresAuthentication: true }
    },
    {
      path: '/admin-dashboard',
      name: 'admin-dashboard',
      component: AdminDashboard,
      meta: { title: 'Admin Dashboard', requiresAuthentication: true, authRedirect: '/admin-login' }
    },
    {
      path: '/admin-login',
      name: 'admin-login',
      component: AdminLogin,
      meta: { title: 'Admin Login', requiresAuthentication: false }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  if(to.meta.requiresAuthentication && !authStore.isAuthenticated) {
      next(to.meta.authRedirect || '/login')
  } else {
    next()
  }
})

export default router
