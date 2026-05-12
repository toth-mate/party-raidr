<template>
    <h1>Dashboard</h1>

    <section class="container mt-5">
        <div class="row g-4">
            <div class="col-12 col-md-6 col-lg-2 p-3 rounded">
                <h2 class="h3">Users</h2>
                <p>Manage users here.</p>
            </div>
            <div class="col-12 col-md-6 col-lg-2 p-3 rounded">
                <h2 class="h3">Events</h2>
                <p>Manage events here.</p>
            </div>
            <div class="col-12 col-md-6 col-lg-2 p-3 rounded">
                <h2 class="h3">Cities</h2>
                <p>Manage cities here.</p>
            </div>
            <div class="col-12 col-md-6 col-lg-2 p-3 rounded">
                <h2 class="h3">Places</h2>
                <p>Manage places here.</p>
            </div>
            <div class="col-12 col-md-6 col-lg-2 p-3 rounded">
                <h2 class="h3">Applications</h2>
                <p>Manage applications here.</p>
            </div>
        </div>
    </section>
</template>
<style scoped>
.col-12 { color: whitesmoke }
.col-12:first-of-type {
    background-color: #198754
}
.col-12:nth-of-type(2) {
    background-color: #0d6efd
}
.col-12:nth-of-type(3) {
    background-color: #ffc107
}
.col-12:nth-of-type(4) {
    background-color: #dc3545
}
.col-12:last-of-type {
    background-color: #20c997
}
</style>