
$(document).ready(function () {

    var i = 1;
    $(".artist-block").each(function () {
        var hiddenValue = "<input type=\"hidden\" value=" + i + " />";
        $(this).append(hiddenValue);
        i++;
    });

    $(".artist-block").each(function () {
        if ($(this).find("input[type=hidden]").val() >= 7)
            $(this).hide();
        else
            $(this).show();
    });

    $(".btn-day-selection").click(function () {
        $(".btn-day-selection").removeClass("btn-day-selection-clicked");
        $(this).addClass("btn-day-selection-clicked");

        switch ($(this).val()) {
            case "Thursday (26/07/2018)":
                $(".artist-block").each(function () {
                    if ($(this).find("input[type=hidden]").val() >= 7)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "Friday (27/07/2018)":
                $(".artist-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value <= 6 || value >= 13)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "Saturday (28/07/2018)":
                $(".artist-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value <= 12 || value >= 19)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "Sunday (29/07/2018)":
                $(".artist-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value <= 18)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
        }
    });

    $(".action-btn").click(function () {
        if ($(this).val() == "Add ticket to cart")
            $(".added-to-cart-modal").slideDown();
    });

    $(".added-to-cart-modal").click(function () {
        $(this).hide();
    })
});