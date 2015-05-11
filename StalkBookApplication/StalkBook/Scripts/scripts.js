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
        var currRating = $(this).text();
        if($(this).hasClass("neutral-down") || $(this).hasClass("downvoted"))
        {
            direction = "down";
        }
            $.post('/NewsFeed/RateStatus/', { currRating: currRating, arrowDirection: direction, statusId: $(this).closest(".content").data('id') },
            function () 
            {

            })
            .fail(function () {
                alert("Sorry there was a problem with rating this status.");
            });
            var statusRating = $(this).siblings(".status-rating").first().text();
            statusRating = parseInt(statusRating, 0);
            if (direction == "up" && currRating == 0)
            {
                $(this).text("1").addClass("upvoted").removeClass("neutral-up");
                $(this).next().text("1");
                $(this).siblings(".status-rating").first().text(++statusRating);
            }
            else if (direction == "up" && currRating == 1)
            {
                $(this).text("0").addClass("neutral-up").removeClass("upvoted");
                $(this).next().text("0");
                $(this).siblings(".status-rating").first().text(--statusRating);
            }
            else if (direction == "up" && currRating == 2)
            {
                $(this).text("1").addClass("upvoted").removeClass("neutral-up");
                $(this).next().text("1").addClass("neutral-down").removeClass("downvoted");
                $(this).siblings(".status-rating").first().text(statusRating + 2);
            }
            else if (direction == "down" && currRating == 0)
            {
                $(this).text("2").addClass("downvoted").removeClass("neutral-down");
                $(this).prev().text("2");
                $(this).siblings(".status-rating").first().text(--statusRating);
            }
            else if (direction == "down" && currRating == 1)
            {
                $(this).text("2").addClass("downvoted").removeClass("neutral-down");
                $(this).prev().text("2").addClass("neutral-up").removeClass("upvoted");
                $(this).siblings(".status-rating").first().text(statusRating - 2);
            }
            else
            {
                $(this).text("0").addClass("neutral-down").removeClass("downvoted");
                $(this).prev().text("0");
                $(this).siblings(".status-rating").first().text(++statusRating);
            }
    });

});