(function () {
    'use strict';

    var comments = $.connection.commentSignalRController;

    comments.client.sendMessage = function (msg) {
        $('.panel-footer').first().append($('<h2/>').text(msg));
    };

    $.connection.hub.start();

    $('#btn-signalr-comment').on('click', function (ev) {
        var $area = $('#textarea-signalr-comment');
        var articleId = $area.attr("data-action-articleId");
        var userId = $area.attr("data-action-userId");
        var comment = $area.val();

        comments.server.getComment(articleId, userId, comment);
        var $panel = $('<div/>').attr(
            {
                'class': 'panel panel-success'
            });

        var $container = $('<div/>').attr({
            'id': 'comments-container'
        })

        var $div = $('<div/>');
        var $contentP = $('<p/>');
        var $authorP = $('<p/>');

        $contentP.append($('<span/>').attr('class', 'glyphicon glyphicon-comment')).html(' ' + comment);
        $authorP.append($('<span/>').attr('class', 'glyphicon glyphicon-user')).html('Your comment is added! Will popup after 5 minute.');

        $div.append($contentP)
        .append($('<br/>'))
        .append($authorP);

        $container.append($div);
        $panel.append($container);
        $('.panel-footer').first().append($panel);
    });

    
})();