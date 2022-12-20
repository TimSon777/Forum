$(document).ready(() => {
    const form = $('#form-search');
    $(form).submit((e) => {
        e.preventDefault();
        const formData = new FormData(e.target);
        const params = new URLSearchParams(formData);
        const url = $(form).attr('action') + '?' + params.toString();
        $.get({
            url: url,
            success: (html) => {
                if (html.indexOf('search-item') < 0) {
                    $('#btn-search-show-more').prop('disabled', true);
                    return;
                }
                $('#search-items').append(html);
                const inputPageSearch = $('#input-page-search');
                const currentNumberPage = parseInt(inputPageSearch.val())
                inputPageSearch.val(currentNumberPage + 1);
            }
        });
        $('#btn-search-show-more').prop('disabled', false);
    });
    
    $('#btn-search-search').click(() => {
        $('#search-items').empty();
        $('#input-page-search').val(1);
    });
    
    $('#input-search').on('input', () => {
       $('#btn-search-show-more').prop('disabled', true); 
    });
});