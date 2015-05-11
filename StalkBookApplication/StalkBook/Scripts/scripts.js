$(document).ready(function ()
{
    $(".stalk-button").click(function (event)
    {
        if ($(this).text() == "Stalk")
        {
            $.post('/Stalk/Stalk/', { stalkId: $(this).closest('.search-body').data('id') },
            function ()
            {

            })
            .fail(function ()
            {
                alert("Sorry there was a problem with stalking this user.");
            });
            $(this).text("Stalking").addClass('btn-primary').removeClass('btn-default');
        }
        else
        {
            $.post('/Stalk/stopStalkingUser/', { stalkId: $(this).closest('.search-body').attr('data-id') },
            function ()
            {

            })
            .fail(function ()
            {
                alert("Sorry there was a problem with stalking this user.");
            });
            $(this).text("Stalk").addClass('btn-default').removeClass('btn-primary');
        }
    });

    $(".rating-button").click(function (event)
    {
        alert("im here1");
        var direction = "up";
        if($(this).hasClass("neutral-down") || $(this).hasClass("downvoted"))
        {
            direction = "down";
        }
        alert("ok");
        alert($(this).text());
        alert(direction);
        alert($(this).closest(".content").data('id'));
            $.post('/NewsFeed/RateStatus/', { currRating: $(this).text(), arrowDirection: direction, statusId: $(this).closest(".content").data('id') },
            function () 
            {

            })
            .fail(function () {
                alert("Sorry there was a problem with rating this status.");
            });
            /*$(this).text("Stalking").addClass('btn-primary').removeClass('btn-default');*/
    });

});