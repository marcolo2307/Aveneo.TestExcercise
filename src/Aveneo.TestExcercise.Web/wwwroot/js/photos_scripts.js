$("document").ready(function () {
    
    function uploadPhotos(e) {
        const upload = document.getElementById("upload_photos");
        let form = new FormData();
        for (let i = 0; i < upload.files.length; i++)
            form.append("photos", upload.files[i]);

        $.ajax({
            url: "/api/photos/" + objectId,
            data: form,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                reloadPhotos(objectId);
            },
            error: function (data) {
                alert("Error uploading photos!");
            }
        });

        upload.value = "";
    }

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

    function savePhotos(objectId) {
        const photos = photosListToArray();
        sendPhotos(objectId, photos);
    }

    function photosListToArray() {
        array = [];
        const container = document.getElementById("photo-container");
        let i = 0;
        for (let child = container.firstChild; child != null; child = child.nextSibling) {
            array[i] = {
                id: parseInt(child.id.substr(9)),
                sequence: i
            };
            i++;
        }
        return array;
    }

    function sendPhotos(objectId, photos) {
        $.ajax({
            url: "/api/photos/" + objectId,
            type: "put",
            contentType: "application/json",
            data: JSON.stringify({
                Photos: photos
            })
        }).done(function () {
            alert("Updated!");
        }).fail(function () {
            alert("Failed to save photos!");
        });
    }

    const objectId = $("#object_id").val();

    $("#upload_photos").bind("input", uploadPhotos);

    const currentId = $("#object_id").val();

    reloadPhotos(currentId);

    $("#reset-button").click(function () {
        reloadPhotos(currentId);
    });

    $("#save-button").click(function () {
        savePhotos(currentId);
    });
});