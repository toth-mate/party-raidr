<script setup>
  import { RouterView } from 'vue-router'
  import Header from './components/Header.vue'
</script>

<template>
  <div class="container-fluid">
    <Header />
    <main class="mt-3">
      <RouterView />
    </main>
  </div>
</template>
<style>
  h1 {
    text-align: center;
    margin-top: .5rem
  }
</style>