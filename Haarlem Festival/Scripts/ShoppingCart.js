$(document).ready(function () {

    $(".action-btn").click(function (e) {
        if ($(this).val() !== "No ticket required for this venue" && $(this).val() !== "Jump to Checkout") {
            var ticketEvent = {
                EventId: $(this).attr("id")
            }
            $.ajax({
                type: "POST",
                url: "/Jazz/AJAXTest",
                data: JSON.stringify(ticketEvent),
                dataType: "json",  
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert(data.msg);
                },
                error: function () {
                    alert("Error occured!!")
                }
            });  
        }
    });

});