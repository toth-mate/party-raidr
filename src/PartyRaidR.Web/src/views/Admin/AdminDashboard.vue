<template>
    <h1>Dashboard</h1>

    <section class="container mt-5">
        <div class="row g-4">
            <div class="col-12 col-md-6 col-lg-2" v-on:click="navigateTo('/admin-dashboard/users')">
                <div class="dashboard-card p-3 rounded h-100">
                    <h2 class="h3">Users</h2>
                    <p>Manage users here.</p>
                </div>
            </div>
            <div class="col-12 col-md-6 col-lg-2" v-on:click="navigateTo('/admin-dashboard/events')">
                <div class="dashboard-card p-3 rounded h-100">
                    <h2 class="h3">Events</h2>
                    <p>Manage events here.</p>
                </div>
            </div>
            <div class="col-12 col-md-6 col-lg-2" v-on:click="navigateTo('/admin-dashboard/cities')">
                <div class="dashboard-card p-3 rounded h-100">
                    <h2 class="h3">Cities</h2>
                    <p>Manage cities here.</p>
                </div>
            </div>
            <div class="col-12 col-md-6 col-lg-2" v-on:click="navigateTo('/admin-dashboard/places')">
                <div class="dashboard-card p-3 rounded h-100">
                    <h2 class="h3">Places</h2>
                    <p>Manage places here.</p>
                </div>
            </div>
            <div class="col-12 col-md-6 col-lg-2" v-on:click="navigateTo('/admin-dashboard/applications')">
                <div class="dashboard-card p-3 rounded h-100">
                    <h2 class="h3">Applications</h2>
                    <p>Manage applications here.</p>
                </div>
            </div>
        </div>
    </section>
</template>
<script setup>
import { useRouter } from 'vue-router'

const router = useRouter()

const navigateTo = (path) => router.push(path)
</script>
<style scoped>
.dashboard-card {
    color: whitesmoke;
    cursor: pointer
}
.col-12:first-of-type .dashboard-card {
    background-color: #198754
}
.col-12:nth-of-type(2) .dashboard-card {
    background-color: #0d6efd
}
.col-12:nth-of-type(3) .dashboard-card {
    background-color: #ffc107
}
.col-12:nth-of-type(4) .dashboard-card {
    background-color: #dc3545
}
.col-12:nth-of-type(5) .dashboard-card {
    background-color: #6c757d
}
.col-12:last-of-type .dashboard-card {
    background-color: #20c997
}
</style>