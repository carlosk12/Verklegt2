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
        var direction = "up";
        if($(this).hasClass("neutral-down") || $(this).hasClass("downvoted"))
        {
            direction = "down";
        }
        if ($(this).text("0"))
        {
            alert("neatral");
            $.post('/NewsFeed/RateStatus/', { currRating: $(this).text(), arrowDirection: direction },
            function () 
            {

            })
            .fail(function () {
                alert("Sorry there was a problem with rating this status.");
            });
            /*$(this).text("Stalking").addClass('btn-primary').removeClass('btn-default');*/
        }
        else if($(this).text("1"))
        {
            alert("upvoted");
            /*$.post('/Stalk/stopStalkingUser/', { stalkId: $(this).closest('.search-body').attr('data-id') },
            function () {

            })
            .fail(function () {
                alert("Sorry there was a problem with stalking this user.");
            });
            $(this).text("Stalk").addClass('btn-default').removeClass('btn-primary');*/
        }
        else
        {
            alert("downvoted");
            $.post('/Stalk/stopStalkingUser/', { stalkId: $(this).closest('.search-body').attr('data-id') },
            function () {

            })
            .fail(function () {
                alert("Sorry there was a problem with stalking this user.");
            });
            $(this).text("Stalk").addClass('btn-default').removeClass('btn-primary');
        }
    });

});