<script setup>
  import Header from './components/Header.vue'
  import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div class="container">
    <Header />
    <RouterView />
  </div>
</template>