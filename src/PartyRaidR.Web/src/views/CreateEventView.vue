<template>
    <h1>Create event</h1>
    <div class="w-75 mx-auto">
        <div class="mb-2">
            <label for="title" class="form-label">Title</label>
            <input type="text" class="form-control" id="title" placeholder="Event title">
        </div>
        <div class="mb-2">
            <label for="description" class="form-label">Description</label>
            <textarea class="form-control" id="description" placeholder="Event description"></textarea>
        </div>
        <div class="mb-2 d-flex justify-content-between">
            <div class="w-50 me-3">
                <label for="start-date" class="form-label">Start date</label>
                <input type="date" class="form-control" id="start-date">
            </div>
            <div class="w-50">
                <label for="end-date" class="form-label">End date</label>
                <input type="date" class="form-control" id="end-date">
            </div>
        </div>
        <div class="mb-2 d-flex justify-content-evenly">
            <div class="w-50 me-3">
                <label for="place" class="form-label">Place</label>
                <select id="place" class="form-control">
                    <option value="1">place1</option>
                    <option value="2">place2</option>
                    <option value="3">place3</option>
                </select>
            </div>
            <div class="w-50 me-3">
                <label for="category" class="form-label">Category</label>
                <select id="category" class="form-control">
                    <option value="1">cat1</option>
                    <option value="2">cat2</option>
                    <option value="3">cat3</option>
                </select>
            </div>
        </div>
        <div class="mb-2">
            <label for="room" class="form-label">Max room</label>
            <input type="number" class="form-control" id="room" placeholder="250">
        </div>
        <div class="mb-2">
            <label for="price" class="form-label">Ticket price</label>
            <input type="number" class="form-control" id="price" placeholder="250">
            <div class="form-text">Set ticket price '0' if there is no fee.</div>
        </div>
        <button class="btn btn-primary w-100">Create</button>
    </div>
</template>