<script setup>
    import { ref } from 'vue'
    import { RouterLink } from 'vue-router'

    const today = new Date()
    const month = today.getMonth() + 1
    const displayMonth = month < 10 ? `0${month}` : month
    const date = ref(`${today.getFullYear()}-${displayMonth}-${today.getDate()}`)
</script>
<template>
    <h1>Register</h1>
    <form class="w-75 mx-auto mt-4 px-3 pt-3 border">
        <div class="form-floating mb-2">
            <input type="text" class="form-control" id="username" placeholder="Username">
            <label for="username">Username</label>
        </div>
        <div class="form-floating mb-2">
            <input type="email" class="form-control" id="email" placeholder="example@mail.org">
            <label for="email">Email address</label>
        </div>
        <div class="form-floating mb-2">
            <input type="password" class="form-control" id="password" placeholder="Password">
            <label for="password">Password</label>
        </div>
        <div class="form-floating mb-2">
            <input type="password" class="form-control" id="password-again" placeholder="Password">
            <label for="password-again">Password again</label>
        </div>
        <div class="form-floating mb-2">
            <input type="date" class="form-control" id="birthdate" placeholder="Birthdate" :value="date">
            <label for="birthdate">Birthdate</label>
        </div>
        <button type="submit" class="btn btn-success w-100">Register</button>
        <p class="mt-3 text-center">
            Already have an account?
            <RouterLink to="/login">Login here!</RouterLink>
        </p>
    </form>
</template>