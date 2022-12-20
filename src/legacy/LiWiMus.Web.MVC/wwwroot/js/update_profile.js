callback = (response) => {
    $("#avatar").attr("src", response.object.avatarLocation);
}

successUpdate = function (xhr) {
    alert(xhr);
};

failureUpdate = function (xhr) {
    alert(`Status code ${xhr.status}, message ${xhr.responseText}`);
}

var avatarBlob;

beforeSubmit = function (rr, $form) {
    if (avatarBlob) {
        $form.data.append('Avatar', avatarBlob, 'avatar.jpg');
    }
}

$(document).ready(() => {
    let isShowSaveBtn = false;
    let saveBtn = $("#btn-save-profile-info");
    let editInputs = $(".disabled-input");

    $("#btn-edit-profile-info").click(() => {
        if (isShowSaveBtn) {
            $(saveBtn).hide();
        } else {
            $(saveBtn).show();
        }

        $(editInputs).prop('disabled', isShowSaveBtn);
        isShowSaveBtn = !isShowSaveBtn;
    });

    $("input[name='gender']").change(function () {
        $('#is-male').val(this.id.indexOf("female") === -1);
    });

    var avatar = document.getElementById('avatar');
    var image = document.getElementById('image');
    var input = document.getElementById('input');
    var $modal = $('#modal');
    var cropper;

    input.addEventListener('change', function (e) {
        var files = e.target.files;
        var done = function (url) {
            input.value = '';
            image.src = url;
            $modal.modal('show');
        };
        var reader;
        var file;

        if (files && files.length > 0) {
            file = files[0];

            if (URL) {
                done(URL.createObjectURL(file));
            } else if (FileReader) {
                reader = new FileReader();
                reader.onload = function (e) {
                    done(reader.result);
                };
                reader.readAsDataURL(file);
            }
        }
    });

    $modal.on('shown.bs.modal', function () {
        cropper = new Cropper(image, {
            aspectRatio: 1,
            viewMode: 3,
        });
    }).on('hidden.bs.modal', function () {
        cropper.destroy();
        cropper = null;
    });

    document.getElementById('crop').addEventListener('click', function () {
        var canvas;

        $modal.modal('hide');

        if (cropper) {
            canvas = cropper.getCroppedCanvas({
                width: 300,
                height: 300,
            });
            avatar.src = canvas.toDataURL();
            canvas.toBlob(function (blob) {
                avatarBlob = blob;
            });
        }
    });
})