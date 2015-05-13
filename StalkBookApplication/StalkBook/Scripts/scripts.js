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

    $(".join-button").click(function (event)
    {
        var groupId = parseInt($(this).closest('.search-body').data('id'), 0);
        if ($(this).text() == "Join")
        {
            $.post('/Group/Join/', { groupId: groupId },
            function () {

            })
            .fail(function () {
                alert("Sorry there was a problem with stalking this user.");
            });
            $(this).text("Joined").addClass('btn-primary').removeClass('btn-default');
        }
        else
        {
            $.post('/Group/Leave/', { groupId: groupId },
            function ()
            {

            })
            .fail(function ()
            {
                alert("Sorry there was a problem with stalking this user.");
            });
            $(this).text("Join").addClass('btn-default').removeClass('btn-primary');
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

neededkeys = [38, 38, 40, 40, 37, 39, 37, 39, 66, 65], started = false, count = 0;
$(document).keydown(function (e) {
    key = e.keyCode;
    //Set start to true only if having pressed the first key in the konami sequence.
    if (!started) {
        if (key == 38) {
            started = true;
        }
    }
    //If we've started, pay attention to key presses, looking for right sequence.
    if (started) {
        if (neededkeys[count] == key) {
            //We're good so far.
            count++;
        } else {
            //Oops, not the right sequence, lets restart from the top.
            reset();
        }
        if (count == 10) {
            window.location.replace("http://fmarcia.info/jquery/tetris/tetris.html");
            reset();
        }
    } else {
        //Oops.
        reset();
    }
});
//Function used to reset us back to starting point.
function reset() {
    started = false; count = 0;
    return;
}