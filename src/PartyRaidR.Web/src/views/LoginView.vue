<script setup>
    import { RouterLink } from 'vue-router'
</script>
<template>
    <h1>Login</h1>
    <form class="w-75 mx-auto mt-4 px-3 pt-3 border">
        <div class="form-floating mb-2">
            <input type="email" class="form-control" id="email" placeholder="example@mail.org">
            <label for="email">Email address</label>
        </div>
        <div class="form-floating mb-2">
            <input type="password" class="form-control" id="password" placeholder="Password">
            <label for="password">Password</label>
        </div>
        <button type="submit" class="btn btn-primary w-100">Login</button>
        <p class="mt-3 text-center">
            Don't have an account?
            <RouterLink to="/register">Register here!</RouterLink>
        </p>
    </form>
</template>