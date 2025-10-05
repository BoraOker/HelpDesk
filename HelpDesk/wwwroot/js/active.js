
    document.addEventListener("DOMContentLoaded", function() {
        // sidebar içindeki tüm a etiketlerini seçin
        var menuLinks = document.querySelectorAll(".sidebar a");

        // Şu anki sayfanın yolunu alın
        var currentPath = window.location.pathname;

        // Menü bağlantılarını döngü ile kontrol edin
        menuLinks.forEach(function(link) {
            // Eğer bağlantının href'i şu anki sayfanın yolu ile eşleşiyorsa
            if (link.getAttribute("href") === currentPath) {
                // "active" sınıfını ekleyin
                link.classList.add("active");
            } else {
                // Diğer bağlantılardan "active" sınıfını kaldırın
                link.classList.remove("active");
            }
        });
    });
