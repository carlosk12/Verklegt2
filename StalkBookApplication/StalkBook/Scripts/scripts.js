$(document).ready(function ()
{
    $(".stalk-button").click(function (event)
    {
        if ($(this).text() == "Stalk")
        {
            $.post('/Stalk/Stalk/', { stalkId: $(this).closest('.search-body').attr('id') },
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
            $.post('/Stalk/stopStalkingUser/', { stalkId: $(this).closest('.search-body').attr('id') },
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
});