const subBtn = document.querySelector(".subscribe-button");

function subscribe(){
    subBtn.classList.add("subscribed");
    subBtn.innerHTML = "Unsubscribe";
}

function unsubscribe(){
    subBtn.classList.remove("subscribed");
    subBtn.innerHTML = "Subscribe";
}

$(document).on('click', '.subscribe-button', function() {
        const isSubscribed = subBtn.classList.contains("subscribed");
        isSubscribed ?  unsubscribe() : subscribe();
})