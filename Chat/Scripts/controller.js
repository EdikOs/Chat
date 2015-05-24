var app = angular.module('chatapp', []);

app.controller('ChatController', function ($scope) {
    $scope.messages = [{ UserId: "anon", Text: "Привет из чата!" }];
    $scope.users = [];
    $scope.chat = $.connection.chatHub;//устанавляем соединение  // Ссылка на автоматически-сгенерированный прокси хаба
    $scope.chat.client.addMessage = function (name, message) { // 
        $scope.messages.unshift({ UserId: name, Text: message });//добавляет сообщение в начало 
        $scope.$apply(); //Обновляем вид
    };
    $scope.chat.client.onNewUserConnected = function () { //добавляем нового пользователя
        $scope.updateUsers();//обновляем список пользователей
    }

    $scope.chat.client.onUserDisconnected = function () { //отключаем пользователя
        $scope.updateUsers();//обновляем список пользователей
    }

    //---------

    //  Открываем соединение
    $.connection.hub.start().done(function () {
        $scope.sndMessage = function (e) { // Вызываем у хаба метод Send
            if (e.keyCode != 13) return;//по Enter
            $scope.addMessage($scope.msg);
        }
        $scope.addMessage = function (msg) {
            $scope.chat.server.send($scope.username, msg);
            $scope.msg = "";
        }
        $scope.chat.server.connect($scope.username); // Вызов серверного метода Connect в ChatHub
        $scope.updateMessages();
    });
    // Функция, вызываемая при подключении нового пользователя
    $scope.chat.client.onConnected = function () {
        $scope.updateUsers();
        $scope.$apply();
    }
    $scope.updateMessages = function (userid) {
        if (userid == null)
            userid = "";
        $.getJSON("/api/messages/"+userid)
          .done(function (data) {
              $scope.messages = data;
              $scope.$apply(); //Обновляем вид
          });
    }

    $scope.updateUsers = function () {
        $.getJSON("/api/users")
          .done(function (data) {
              $scope.users = data;
              $scope.$apply(); //Обновляем вид
          });
    }
});
