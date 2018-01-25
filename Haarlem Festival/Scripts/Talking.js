$(document).ready(function () {

    var i = 1;
    $(".speaker-block").each(function () {
        var hiddenValue = "<input type=\"hidden\" value=" + i + " />";
        $(this).append(hiddenValue);
        i++;
    });

    $(".speaker-block").each(function () {
        if ($(this).find("input[type=hidden]").val() > 2)
            $(this).hide();
        else
            $(this).show();
    });

    $(".btn-day-selection").click(function () {
        $(".btn-day-selection").removeClass("btn-day-selection-clicked");
        $(this).addClass("btn-day-selection-clicked");

        switch ($(this).val()) {
            case "Thursday (26/07/2018)":
                $(".speaker-block").each(function () {
                    if ($(this).find("input[type=hidden]").val() > 2)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "Friday (27/07/2018)":
                $(".speaker-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value < 3 || value > 4)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "Saturday (28/07/2018)":
                $(".speaker-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value < 5)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
        }
    });
});