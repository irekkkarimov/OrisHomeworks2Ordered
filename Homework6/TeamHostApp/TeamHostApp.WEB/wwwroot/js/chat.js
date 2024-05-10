const currentUser = document.getElementById('current-user-id').value;

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5279/chat", {
        accessTokenFactory: () => currentUser
    })
    .build()

hubConnection.on("OnUserConnection", function(data) {
    console.log(data)
    for (let i = 0; i < data.onlineUsers.length; i++){
        let parentNode = document.getElementById(`friend-id=${data.onlineUsers[i]}`);

        console.log(`parent node: ${parentNode}`)

        if (parentNode !== null && parentNode !== undefined){
            let childNode = parentNode.querySelector('.user-item');
            childNode.classList.add('--active')
            childNode.offsetWidth;
        }
    }
})

hubConnection.on("OnUserDisconnected", function (data)  {
    console.log(data)

    let parentNode = document.getElementById(`friend-id=${data.userId}`);

    if (parentNode !== undefined){
        let childNode = parentNode.querySelector('.user-item');
        childNode.classList.remove('--active')
        childNode.offsetWidth;
    }
});

hubConnection.on("ReceiveMessage", function (data) {
    console.log(data)
    let currentUserId = document.getElementById('current-user-id').value;

    let isMyMessage = currentUserId === data.senderId;
    let senderName = data.senderName;

    let img = data.image
    
    let senderMessage = `<div class="sender-name" style=${isMyMessage ? "margin-left:5px" : "margin-right:5px"}>${senderName}</div>`
    let avatar = `<div class="messages-item__avatar"><img src="~/${img}" alt="user"></div>`;
    let isYourMessage = isMyMessage ? '--friend-message' : '--your-message'

    console.log(avatar)
    // Создаем новый элемент сообщения
    let newMessage = `
        <div class="messages-item ${isYourMessage}" id="your-message"">
            ${isMyMessage ? "" : avatar}
            ${isMyMessage ? "" : senderMessage}
            <div class="messages-item__text">${data.content}</div>
            ${isMyMessage ? senderMessage : ""}
        </div>
    `;

    // Добавляем новое сообщение в конец списка
    $('.chat-messages-body').append(newMessage);

    // Обновляем высоту контейнера чата и прокручиваем вниз
    var chatMessagesBody = $('.chat-messages-body')[0];
    chatMessagesBody.scrollTop = chatMessagesBody.scrollHeight;
});


hubConnection.start();

// Функция для загрузки частичного представления чата
function loadChats() {
    $('.chat-user-list__body').load('/Chats/GetChats', function() {
        // После загрузки чатов, привязываем обработчик клика к элементам чата
        $('.user-item').off('click').on('click', function (event) {
            event.preventDefault(); // Предотвращаем стандартное действие ссылки
            let id = $(this).data('message-id'); // Получаем значение data-message-id
            console.log('q')
            $.ajax({
                url: `/Chats/GetDetailed/${id}`,
                type: 'GET',
                dataType: 'text',
                success: function (data) {
                    $('#chat-place').html(data);
                    attachFormSubmitHandler()

                    // Прокручиваем чат вниз после загрузки сообщений
                    var chatMessagesBody = $('.chat-messages-body')[0];
                    chatMessagesBody.scrollTop = chatMessagesBody.scrollHeight;
                },
                error: function () {
                    alert('Ошибка при загрузке данных');
                }
            });
        });
    });
}

// Загрузка чатов при загрузке страницы
$(document).ready(function () {
    loadChats();
});
// Функция для привязки обработчика отправки формы
function attachFormSubmitHandler() {
    // Находим форму внутри #chat-place и назначаем обработчик отправки
    $('#chat-place').on('submit', '#form-chat', function(event) {
        event.preventDefault(); // Предотвращаем стандартное действие отправки формы
        console.log(document.querySelector('.chat-messages-input').value)
        
        // Получаем данные из формы
        let body = {
            chatId: document.getElementById("chatIdInput").value,
            content: document.querySelector('.chat-messages-input').value
        };

        const chatId = $('#chatIdInput').val();

        // Выполняем AJAX-запрос на сервер
        fetch(`Chats/SendMessage`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        })
            .then()

        document.querySelector('.chat-messages-input').value = "";
    });
}