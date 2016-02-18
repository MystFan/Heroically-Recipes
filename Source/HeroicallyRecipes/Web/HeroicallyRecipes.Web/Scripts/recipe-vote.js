(function () {
    'use strict';

    $("div[data-action='up'").click(function () {
        var id = $(this).attr("data-id");
        voteClick(id, 1);
    });
    $("div[data-action='down'").click(function () {
        var id = $(this).attr("data-id");
        voteClick(id, -1);
    });

    function voteClick(id, vote) {
        $.post("/Users/Votes/Vote", { recipeId: id, vote: vote },
            function (data) {
                var newVotesCount = data.Count;
                $("div[data-action='votesCount'][data-id='" + id + "']").html(newVotesCount)
            });
    }
})();