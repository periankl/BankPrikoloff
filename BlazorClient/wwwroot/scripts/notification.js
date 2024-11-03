window.notification = {
    showNotification: function (title, options) {
        if (!("Notification" in window)) {
            console.error("Браузер не поддерживает уведомления.");
        } else if (Notification.permission === "granted") {
            new Notification(title, options);
        } else if (Notification.permission !== "denied") {
            Notification.requestPermission().then(function (permission) {
                if (permission === "granted") {
                    new Notification(title, options);
                }
            });
        }
    }
};