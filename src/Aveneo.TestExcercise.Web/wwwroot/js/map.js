const map = L.map("map").setView([0, 0], 1);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);


function createPoint(coords, name, image) {
    const popup = "<p>" + name +
        "</p><img src=\"data:image/gif;base64," + image +
        "\" style='height: 100px;' alt='No photo!' />";

    L.marker(coords).addTo(map)
        .bindPopup(popup);
}