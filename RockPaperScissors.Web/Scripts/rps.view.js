window.onload = loadScript;
function loadScript() {
    $('button[name=play]').attr('disabled', 'disabled');
    $('#computerMove').hide();
    $('#playerMove').hide();
    $('#currentGame').hide();

    $('#rbPersonVsComputer').click(function() {
        $('#playerMove').show();
        $('#computerMove').hide();
        newGame($(this).attr("value"));
    });
    
    $('#rbComputerVsComputer').click(function () {
        $('#computerMove').show();
        $('#playerMove').hide();
        newGame($(this).attr("value"));
    });
}

function newGame(selectedType) {
    $('#currentGame').show();
    $('#currentGameType').text(selectedType);
    var list = $('#gameResults');
    list.empty();

    $('button[name=play]').removeAttr("disabled");
    
    var gameId = $('#gameId');
    $.ajax({
        url: '/api/Games/' + selectedType,
        success: function (data) {
            gameId.text(data);
        },
        statusCode: {
            404: function () {
                gameId.text('Error creating game!');
                $('button[name=play]').attr('disabled', 'disabled');
            }
        }
    });
}

function play(selectedMove) {
    var gameId = $('#gameId').text();
    $.ajax({
        url: '/api/Games',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({"Id":gameId, "move": selectedMove}),
        success: function (result) {
            var table = $('#gameResults');
            table.empty();
            table.append('<thead><tr><th>Player 1</th><th>Player 2</th><th>Player 1 Points</th></tr></thead><tbody></tbody>');
            var total = 0;
            for (var i = 0; i < result.ResultHistory.length; i++) {
                var resultItem = result.ResultHistory[i];
                total += resultItem.Result;
                var rowType = "";
                if (resultItem.Result > 0) {
                    rowType = "success";
                }
                if (resultItem.Result < 0) {
                    rowType = "error";
                }
                
                $('#gameResults').prepend('<tr class="' + rowType + '"><td>' + resultItem.Selection.m_Item1 + '</td><td>' + resultItem.Selection.m_Item2 + '</td><td>' + total + '</td></tr>');
            }
        }
    });
}