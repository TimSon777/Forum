$(document).ready(() => {
    $(document).find("textarea").on( "input change propertychange", function ()  {
            const patternRegex = new RegExp("^[^<>]*$", "g");

            let hasError = !$(this).val().match(patternRegex);

            if (hasError) {
                formHelperToastr.warning("Dont enter < and >");
                const wrongValue = $(this).val();
                const validVal = wrongValue.replace('<', "").replace('>', "");
                $(this).val(validVal);
            }
    });
});