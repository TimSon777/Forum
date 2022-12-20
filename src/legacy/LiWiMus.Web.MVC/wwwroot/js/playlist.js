$(document).ready(() => {
    const form = $('#search-tracks-to-playlist');
    $(form).submit((e) => {
        e.preventDefault();
        const formData = new FormData(e.target);
        const params = new URLSearchParams(formData);
        const url = $(form).attr('action') + '?' + params.toString();
        $.get({
            url: url,
            success: (html) => {
                const div = $('#tracks-to-playlist');
                div.empty();
                div.html(html);
            }
        });
    });

    $(document).on('submit','.form-add-track-to-playlist', function (e) {
        e.preventDefault();
        $.ajax({
            method: 'POST',
            url: '/User/Playlist/AddOrRemoveTrack',
            data: {
                TrackId: $(this).find('input[name="TrackId"]')[0].value,
                PlaylistId: $(this).find('input[name="PlaylistId"]')[0].value,
            },
            success: () => {
                const btn = $(this).find('input[type="submit"]')[0];
                if ($(btn).val() === 'Add') {
                    $(btn).val('Remove');
                } else {
                    $(btn).val('Add');
                }
            }
        })
    });
});