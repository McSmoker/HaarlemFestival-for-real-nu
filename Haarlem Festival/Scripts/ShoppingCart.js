$(document).ready(function () {

    $(".action-btn").click(function (e) {
        if ($(this).val() == "Add ticket to cart") {
            var ticketEventId = {
                EventId: $(this).attr("id")
            }
            $.ajax({
                type: "POST",
                url: "/Jazz/AJAXTest",
                data: JSON.stringify(ticketEvent),
                dataType: "json",  
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert(data.EventId);
                },
                error: function () {
                    alert("Error occured!!")
                }
            });  
        }
    });

});