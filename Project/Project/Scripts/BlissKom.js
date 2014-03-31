var BlissKom = BlissKom || {};

BlissKom = {
    removeDisplayNoneIfNotValid: function () {
        Page_ClientValidate();
        if (!Page_IsValid) {
            var pnlErrorBox = document.getElementById("Content_pnlErrorBox");
            pnlErrorBox.style.display = "block";
        }
    },
    enableControl: function (id) {
        document.getElementById(id).disabled = false;
    },
    disableControl: function (id) {
        document.getElementById(id).disabled = true;
    },
    hideControl: function (id) {
        document.getElementById(id).style.display = "none";
    },
    dim: function (pos) {
        var pnl = document.getElementById("Content_pnlInnerTablet");
        var dimmer = document.createElement("div");
        dimmer.id = "dimmer";
        var unclickable = document.createElement("div");
        unclickable.id = "unclickable";
        pnl.appendChild(dimmer);
        pnl.appendChild(unclickable);
        var item = document.getElementById("Content_imbUnit" + pos);
        item.className = item.className.replace("item", "itemFull");
    },
    undim: function () {
        this.showCenterImage();
        var dimmer = document.getElementById("dimmer");
        if (dimmer) dimmer.parentElement.removeChild(dimmer);
        var unclickable = document.getElementById("unclickable");
        if (unclickable) unclickable.parentElement.removeChild(unclickable);
        var itemFull = document.getElementsByClassName("itemFull")[0];
        itemFull.className = itemFull.className.replace("itemFull", "item");
    },
    toggleNavButtons: function (ok, cancel, left, right, info) {
        var imgOK = document.getElementById("Content_imbOK");
        var imgCancel = document.getElementById("Content_imbCancel");
        var imgLeft = document.getElementById("Content_imbLeft");
        var imgRight = document.getElementById("Content_imbRight");
        var imgInfo = document.getElementById("Content_imbInfo");
        ok ? imgOK.style.display = "block" : imgOK.style.display = "none";
        cancel ? imgCancel.style.display = "block" : imgCancel.style.display = "none";
        left ? imgLeft.style.display = "block" : imgLeft.style.display = "none";
        right ? imgRight.style.display = "block" : imgRight.style.display = "none";
        info ? imgInfo.style.display = "block" : imgInfo.style.display = "none";
    },
    showCenterImage: function () {
        var currentLink = document.getElementsByClassName("itemFull")[0];
        var images = currentLink.querySelectorAll("[data-type]");
        for (var i = 0; i < images.length; i++) {
            images[i].style.display = "none";
            images[i].setAttribute("active", "false");
        }
        var newImage = currentLink.querySelector("[data-type = 'ParentWordItem']");
        newImage.style.display = "";
        newImage.setAttribute("active", "true");
    },
    showLeftImage: function () {
        var currentLink = document.getElementsByClassName("itemFull")[0];

        if (currentLink.querySelector("[data-type = 'ParentWordItem']").style.display === "") {
            var currentImage = currentLink.querySelector("[data-type = 'ParentWordItem']");
            currentImage.style.display = "none";
            currentImage.setAttribute("active", "false");
            var newImage = currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '1']");
            if (!currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '2']")) {
                document.getElementById("Content_imbLeft").style.display = "none";
            }
            newImage.style.display = "";
            document.getElementById("Content_imbRight").style.display = "block";
            newImage.setAttribute("active", "true");
        }
        else if (currentLink.querySelector("[data-type = 'ChildLeftWordItem'][active = 'true']")) {

            var currentImage = currentLink.querySelector("[data-type = 'ChildLeftWordItem'][active = 'true']");

            currentImage.style.display = "none";
            currentImage.setAttribute("active", "false");
            var newImage = currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '" + (parseInt(currentImage.getAttribute("data-pos")) + 1).toString() + "']");

            if (!currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '" + (parseInt(newImage.getAttribute("data-pos")) + 1).toString() + "']")) {
                document.getElementById("Content_imbLeft").style.display = "none";
            }
            if (!currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '" + (parseInt(newImage.getAttribute("data-pos")) - 1).toString() + "']")) {
                document.getElementById("Content_imbRight").style.display = "none";
            } else {
                document.getElementById("Content_imbRight").style.display = "block";
            }
            newImage.style.display = "";
            newImage.setAttribute("active", "true");
        } else if (currentLink.querySelector("[data-type = 'ChildRightWordItem'][active = 'true']")) {
            var currentImage = currentLink.querySelector("[data-type = 'ChildRightWordItem'][active = 'true']");

            currentImage.style.display = "none";
            currentImage.setAttribute("active", "false");
            if (currentImage.getAttribute("data-pos") === "1") {
                var newImage = currentLink.querySelector("[data-type = 'ParentWordItem']");

                if (!currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '1']")) {
                    document.getElementById("Content_imbLeft").style.display = "none";
                }
                document.getElementById("Content_imbRight").style.display = "block";
            } else {
                var newImage = currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '" + (parseInt(currentImage.getAttribute("data-pos")) - 1).toString() + "']");

                if (!currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '" + (parseInt(newImage.getAttribute("data-pos")) - 1).toString() + "']")) {
                    document.getElementById("Content_imbRight").style.display = "none";
                }
                if (!currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '" + (parseInt(newImage.getAttribute("data-pos")) + 1).toString() + "']")) {
                    document.getElementById("Content_imbLeft").style.display = "none";
                } else {
                    document.getElementById("Content_imbLeft").style.display = "block";
                }
            }
            newImage.style.display = "";
            newImage.setAttribute("active", "true");
        }
    },
    showRightImage: function () {
        var currentLink = document.getElementsByClassName("itemFull")[0];

        if (currentLink.querySelector("[data-type = 'ParentWordItem']").style.display === "") {
            var currentImage = currentLink.querySelector("[data-type = 'ParentWordItem']");
            currentImage.style.display = "none";
            currentImage.setAttribute("active", "false");
            var newImage = currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '1']");
            if (!currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '2']")) {
                document.getElementById("Content_imbRight").style.display = "none";
            }
            newImage.style.display = "";
            document.getElementById("Content_imbLeft").style.display = "block";
            newImage.setAttribute("active", "true");
        }
        else if (currentLink.querySelector("[data-type = 'ChildRightWordItem'][active = 'true']")) {

            var currentImage = currentLink.querySelector("[data-type = 'ChildRightWordItem'][active = 'true']");

            currentImage.style.display = "none";
            currentImage.setAttribute("active", "false");
            var newImage = currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '" + (parseInt(currentImage.getAttribute("data-pos")) + 1).toString() + "']");

            if (!currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '" + (parseInt(newImage.getAttribute("data-pos")) + 1).toString() + "']")) {
                document.getElementById("Content_imbRight").style.display = "none";
            }
            if (!currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '" + (parseInt(newImage.getAttribute("data-pos")) - 1).toString() + "']")) {
                document.getElementById("Content_imbLeft").style.display = "none";
            } else {
                document.getElementById("Content_imbLeft").style.display = "block";
            }
            newImage.style.display = "";
            newImage.setAttribute("active", "true");
        } else if (currentLink.querySelector("[data-type = 'ChildLeftWordItem'][active = 'true']")) {
            var currentImage = currentLink.querySelector("[data-type = 'ChildLeftWordItem'][active = 'true']");

            currentImage.style.display = "none";
            currentImage.setAttribute("active", "false");
            if (currentImage.getAttribute("data-pos") === "1") {
                var newImage = currentLink.querySelector("[data-type = 'ParentWordItem']");

                if (!currentLink.querySelector("[data-type = 'ChildRightWordItem'][data-pos = '1']")) {
                    document.getElementById("Content_imbRight").style.display = "none";
                }
                document.getElementById("Content_imbLeft").style.display = "block";
            } else {
                var newImage = currentLink.querySelector("[data-type = 'ChildLeftWordItem'][data-pos = '" + (parseInt(currentImage.getAttribute("data-pos")) - 1).toString() + "']");

                document.getElementById("Content_imbLeft").style.display = "block";
            }
            newImage.style.display = "";
            newImage.setAttribute("active", "true");
        }
    }
};