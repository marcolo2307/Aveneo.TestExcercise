window.onload = function () {
    const upload = document.getElementById("upload_photos");
    const objectId = $("#object_id").val();

    function send_photos(e) {
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
                alert("Files Uploaded!");
            },
            error: function (data) {
                alert("Error!");
            }
        });

        upload.value = "";
    }

    $("#upload_photos").bind("input", send_photos);
}