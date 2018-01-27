$(document).ready(function () {

    $(".action-btn").click(function (e) {
        if ($(this).val() === "Add ticket to cart") {
            var clickedTicket = {
                EventId: $(this).attr("id")
            };
            $.ajax({
                type: "POST",
                url: "/Orders/AddOrderItem",
                data: JSON.stringify(clickedTicket),
                dataType: "json",  
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert(data.msg);
                },
                error: function () {
                    alert("Error occured!!");
                }
            });  
        }
    });

});