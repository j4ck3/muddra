try {
    const toggleBtn = document.querySelector('[data-option="toggle"]')
    const menuLinks = document.getElementById("menu")

    toggleBtn.addEventListener("click", function () {
        const element = document.getElementById("icon")

        if (element.classList.contains("fa-bars")) {
            element.classList.add("fa-xmark")
            element.classList.remove("fa-bars")
            menuLinks.classList.remove("d-none")
        }

        else {
            element.classList.add("fa-bars")
            element.classList.remove("fa-xmark")
            menuLinks.classList.add("d-none")
        }
    })
} catch { }