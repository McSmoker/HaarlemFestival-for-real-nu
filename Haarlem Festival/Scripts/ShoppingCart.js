$(document).ready(function () {

    $(".action-btn").click(function () {
        if ($(this).val() === "Add ticket to cart") {
            var clickedTicket = {
                EventId: $(this).attr("id")
            };
            $.ajax({
                type: "POST",
                url: "/Orders/AddJazzTicketToCart",
                data: JSON.stringify(clickedTicket),
                dataType: "json",  
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (!data.alreadyAdded && !data.eventDoesNotExist) {
                        $("#success-ticket-add").slideDown();
                        var jazzCartRow = "<div id=\"event-" + data.EventId + "\" class=\"jazz-cart-row\">";
                        jazzCartRow += "<span class=\"jc-artist\">" + data.PerformerName + "</span>";
                        jazzCartRow += "<span class=\"jc-date\">" + data.EventDate + "</span>";
                        jazzCartRow += "<span class=\"jc-time\">" + data.EventTime + "</span>";
                        jazzCartRow += "<span class=\"jc-location\">" + data.LocationHall + "</span>";
                        jazzCartRow += "<span class=\"jc-amount\"><input type=\"number\" value=\"" + data.Amount + "\" min=\"0\" oninput=\"validity.valid||(value='');\" class=\"jc-amount-input\" /></span>";
                        jazzCartRow += "<span class=\"jc-price\">€" + data.Price + ",00</span>";
                        jazzCartRow += "</div>";
                        $("#jazz-shopping-cart-rows").append(jazzCartRow);
                    }
                    else if (data.eventDoesNotExist)
                        alert("Event ID not found. This is not possible without editing the HTML, so stop messing around!");
                    else
                        $("#failed-ticket-add").slideDown();
                },
                error: function () {
                    alert("Error occured!!");
                }
            });  
        }
    });

    // Functionaliteit voor Passe Partout tickets, werkt wel maar passe partout kan niet goed aan sessie worden toegevoegd
    //$(".pp-checkbox").click(function (e) {
    //    if ($(this.checked)){
    //        var checkedPP = {
    //            passePartoutType: $(this).attr("id")
    //        };
    //        alert($(this).attr("id"));
    //        $.ajax({
    //            type: "POST",
    //            url: "/Orders/AddPassePartoutTicketToCart",
    //            data: JSON.stringify(checkedPP),
    //            dataType: "json",
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (data) {
    //                if (!data.fail) {
    //                    $("#success-ticket-add").slideDown();
    //                    var jazzCartRow = "<div id=\"event-" + data.TicketType + "\" class=\"jazz-cart-row\">";
    //                    jazzCartRow += "<span class=\"jc-artist\">Passe partout ticket</span>";
    //                    jazzCartRow += "<span class=\"jc-date\">" + data.PassePartoutDate + "</span>";
    //                    jazzCartRow += "<span class=\"jc-time\">" + data.PassePartoutTime + "</span>";
    //                    jazzCartRow += "<span class=\"jc-location\">Patronaat</span>";
    //                    jazzCartRow += "<span class=\"jc-amount\"><input type=\"number\" value=\"" + data.Amount + "\" min=\"0\" oninput=\"validity.valid||(value='');\" class=\"jc-amount-input\" /></span>";
    //                    jazzCartRow += "<span class=\"jc-price\">€" + data.Price + ",00</span>";
    //                    jazzCartRow += "</div>";
    //                    $("#jazz-shopping-cart-rows").append(jazzCartRow);
    //                }
    //                else {
    //                    $("#failed-ticket-add").slideDown();
    //                }
    //            },
    //            error: function () {
    //                alert("Error occured!!");
    //            }
    //        });
    //    }
    //});

    $(document).on("change", ".jc-amount-input", function (e) {
        var changedTicket = {
            Amount: $(this).val(),
            JazzEvent: {
                EventId: $(this).closest(".jazz-cart-row").attr("id").substring(6)
            }
        };

        $.ajax({
            type: "POST",
            url: "/Orders/ChangeTicketAmount",
            data: JSON.stringify(changedTicket),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.fail)
                    console.log("Ticket amount change failed.");
                if (data.removed)
                    $("#" + data.removedId).remove();
            },
            error: function () {
                alert("Error occured!!");
            }
        });
    });
});