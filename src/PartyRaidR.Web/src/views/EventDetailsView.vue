<template>
    <div class="container w-75 border rounded p-4">
        <h3>Event {{ $route.params.id }}</h3>
        <p class="text-body-secondary">Event description...</p>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-md-6">
                    <p>Starting date: 2024.06.30.</p>
                    <p>Ending date: 2024.07.01.</p>
                    <p>Place: Place name</p>
                    <p>City: City name</p>
                </div>
                <div class="col-12 col-md-6">
                    <p>Category: Party</p>
                    <p>Room: Not limited</p>
                    <p>Ticket price: Not paid</p>
                </div>
            </div>
        </div>
        <button class="btn btn-success w-100 mt-2">Apply</button>
    </div>
</template>