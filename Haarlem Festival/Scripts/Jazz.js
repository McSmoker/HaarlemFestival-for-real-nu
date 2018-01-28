$(document).ready(function () {

    // voeg een verborgen input veld toe aan ieder element met artist-block als class
    var i = 1;
    $(".artist-block").each(function () {
        var hiddenValue = "<input type=\"hidden\" value=" + i + " />";
        $(this).append(hiddenValue);
        i++;
    });

    // toon alleen de eerste 6 artiesten bij het laden van de pagina
    $(".artist-block").each(function () {
        if ($(this).find("input[type=hidden]").val() >= 7)
            $(this).hide();
        else
            $(this).show();
    });

    // voeg een click event handler aan de dag-selectie knoppen toe die de weergave van de juiste artiesten regelt
    $(".btn-day-selection").click(function () {
        $(".btn-day-selection").removeClass("btn-day-selection-clicked");
        $(this).addClass("btn-day-selection-clicked");

        // kijk aan de hand van het ID attribuut op welke knop er geklikt is en toon de bijbehorende artiesten en verberg de rest
        // er wordt gecontroleerd op de waarde van het op regel 7 toegevoegde verborgen input element
        switch ($(this).attr("id")) {
            case "btn-day-thursday":
                $(".artist-block").each(function () {
                    if ($(this).find("input[type=hidden]").val() >= 7)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "btn-day-friday":
                $(".artist-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value <= 6 || value >= 13)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "btn-day-saturday":
                $(".artist-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value <= 12 || value >= 19)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
            case "btn-day-sunday":
                $(".artist-block").each(function () {
                    var value = $(this).find("input[type=hidden]").val();

                    if (value <= 18 || value >= 25)
                        $(this).hide();
                    else
                        $(this).show();
                });
                break;
        }
    });

    $(".added-to-cart-modal").click(function () {
        $(this).hide();
    });
});