window.onload = function () {

    const currentId = $("#object_id").val();

    reloadPhotos(currentId);

    $("#reset-button").click(function () {
        reloadPhotos(currentId);
    });

    $("#save-button").click(function () {
        savePhotos();
    });

    function reloadPhotos(objectId) {
        
        $("#photo-container").empty();

        $.get({
            url: "/api/photos/" + objectId,
            datatype: "JSON"
        }).done(function (response) {
            response.forEach(e => {
                addPhoto(e.id, e.photo);
            })
        }).fail(function () {
            alert("Failed to load photos!");
        });

        $("#photo-container").sortable();
    }

    function addPhoto(id, photo) {
        const container = document.getElementById("photo-container");

        const li = document.createElement("li");
        li.id = "photo-id-" + id;

        const img = document.createElement("img");
        img.src = "data:image/gif;base64," + photo;
        img.width = "100";
        li.appendChild(img);

        const deleteButton = document.createElement("input");
        deleteButton.type = "button";
        deleteButton.onclick = function () {
            $("#photo-id-" + id).remove();
        };
        deleteButton.value = "Delete";
        deleteButton.className = "btn btn-danger";
        li.appendChild(deleteButton);

        container.appendChild(li);
    }

    function savePhotos() {
        console.log("saved!");
    }
}