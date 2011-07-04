YouGrade = {};
YouGrade.question = {};
YouGrade.ytplayer = {};
YouGrade.getQuestionUrl = "";
YouGrade.moveToPreviousQuestionUrl = "";
YouGrade.moveToNextQuestionUrl = "";
YouGrade.saveAnswerUrl = "";
YouGrade.endExamUrl = "";

// The video to load.
YouGrade.videoID = "iapcKVn7DdY";

YouGrade.configVideo = function () {
    // Lets Flash from another domain call JavaScript
    var params = { allowScriptAccess: "always", wmode: "transparent" };
    // The element id of the Flash embed
    var atts = { id: "ytPlayer" };
    // All of the magic handled by SWFObject (http://code.google.com/p/swfobject/)
    swfobject.embedSWF("http://www.youtube.com/v/" + YouGrade.videoID + "&enablejsapi=1&playerapiid=ytPlayer&wmode=opaque",
                   "videoDiv", "480", "295", "8", null, null, params, atts);
}

YouGrade.configEndExamDialog = function () {
    // Dialog			
    $('#endExamDialog').dialog({
        autoOpen: false,
        modal: true,
        width: 600,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");
            }
        }
    });
}

YouGrade.configNavigation = function () {
    $('.button').hover(
                    function () {
                        $(this).removeClass('ui-state-default');
                        $(this).addClass('ui-state-hover');
                    },
                    function () {
                        $(this).addClass('ui-state-default');
                        $(this).removeClass('ui-state-hover');
                    });
                                                                                                                                                                                                                                                    $('.button next').click(YouGrade.play);
    $('.button.play a').click(YouGrade.play);
    $('.button.previous a').click(YouGrade.moveToPreviousQuestion);
    $('.button.next a').click(YouGrade.moveToNextQuestion);
    $('.button.end a').click(YouGrade.endExam);

}

YouGrade.init = function () {
    YouGrade.configEndExamDialog();
    YouGrade.configNavigation();
}

function onYouTubePlayerReady(playerId) {
    YouGrade.ytplayer = document.getElementById(playerId);
    YouGrade.getQuestion();
}

YouGrade.play = function () {
    YouGrade.renderQuestion({ Question: YouGrade.question });
}
YouGrade.renderQuestion = function (result) {
    var ytplayer = YouGrade.ytplayer;
    var question = result.Question;
    YouGrade.question = question;
    ytplayer.loadVideoById(question.Url, question.StartSeconds);
    ytplayer.playVideo();

    $('.questionTitle').html('Question ' + question.Id);
    $('.questionText').html(question.Text);

    $('.alternatives').html('');
    for (var i = 0; i < question.Alternatives.length; i++) {
        var checked = question.Alternatives[i].IsChecked ? 'checked="true"' : '';
        var type = question.IsMultiSelect ? 'checkbox' : 'radio';
        $('.alternatives').append('<input id="alt' + question.Alternatives[i].Id + '" name="alternatives" type="' + type + '" ' + checked + ' />' + question.Alternatives[i].Id + '. ' + question.Alternatives[i].Text + '<br />');
    }
}
YouGrade.getQuestion = function () {
    $.ajax({
        url: YouGrade.getQuestionUrl,
        type: 'GET',
        cache: false,
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (json) {
            YouGrade.question = json;
            YouGrade.renderQuestion(json);
        }
    });
}


YouGrade.moveToPreviousQuestion = function () {
    YouGrade.saveAnswer();
    $.ajax({
        url: YouGrade.moveToPreviousQuestionUrl,
        type: 'GET',
        cache: false,
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (json) {
            YouGrade.renderQuestion(json);
        }
    });
}

YouGrade.moveToNextQuestion = function(){
    YouGrade.saveAnswer();
    $.ajax({
        url: YouGrade.moveToNextQuestionUrl,
        type: 'GET',
        cache: false,
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (json) {
            YouGrade.renderQuestion(json);
        }
    });
}

YouGrade.saveAnswer = function (afterSaveAnswer) {
    var answers = '';
    var question = YouGrade.question;
    $.each($('.alternatives input'), function (key, value) {
        if ($(value).is(":checked")) {
            answers = answers + String.fromCharCode(65 + key);
        }
    });
    $.ajax({
        url: YouGrade.saveAnswerUrl,
        type: 'POST',
        cache: false,
        dataType: 'json',
        data: ({
            questionId: question.Id,
            answers: answers
        }),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (json) {
            if (afterSaveAnswer != null) {
                afterSaveAnswer();
            }
        }
    });
}

YouGrade.endExam = function() {
    YouGrade.saveAnswer(YouGrade.endExamCallback);
}

YouGrade.endExamCallback = function () {
    $('#endExamDialog').dialog('open');
    $.ajax({
        url: YouGrade.endExamUrl,
        type: 'GET',
        cache: false,
        dataType: 'json',
        data: ({}),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (json) {
            $("#progressbar").progressbar({
                value: json.Result
            });
            $('#yourResult').html(json.Result);
        }
    });
}

YouGrade.renderQuestion = function (result) {
    var question = result.Question;
    var ytplayer = YouGrade.ytplayer;
    YouGrade.question = question;

    ytplayer.stopVideo();
    ytplayer.loadVideoById(question.Url, question.StartSeconds);
    ytplayer.playVideo();

    $('.questionTitle').html('Question ' + question.Id);
    $('.questionText').html(question.Text);

    $('.alternatives').html('');
    for (var i = 0; i < question.Alternatives.length; i++) {
        var checked = question.Alternatives[i].IsChecked ? 'checked="true"' : '';
        var type = question.IsMultiSelect ? 'checkbox' : 'radio';
        $('.alternatives').append('<input id="alt' + question.Alternatives[i].Id + '" name="alternatives" type="' + type + '" ' + checked + ' />' + question.Alternatives[i].Id + '. ' + question.Alternatives[i].Text + '<br />');
    }
}