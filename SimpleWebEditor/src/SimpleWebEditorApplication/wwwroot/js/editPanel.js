function showEditPanel(elementToChange) {
    var editPanel = document.getElementById("editPanel");

    //remove child nodes
    if (editPanel.childElementCount != 0) {
        editPanel.innerHTML = '';
    }

    showEditPanelHeader(editPanel);
    createTitleChanger(editPanel);

    var elementTag = elementToChange.tagName.toLowerCase();
    if (elementTag == "div") {
        createBackgroundColorSelector(editPanel, elementToChange);
    }

    else if (elementTag == "h1" || elementTag == "h3") {
        createBackgroundColorSelector(editPanel, elementToChange.parentNode);
        createFontColorSelector(editPanel, elementToChange);
        createFontSelector(editPanel, elementToChange);
        createFontSizeSelector(editPanel, elementToChange);

    } /*else if (elementTag == "img") {
        createImageFromURLSelector(editPanel, elementToChange);
        createImageFromFileSelector(editPanel, elementToChange);
    }*/
    else {
        createBackgroundColorSelector(editPanel, elementToChange.parentNode);
        createFontColorSelector(editPanel, elementToChange);
        createFontSelector(editPanel, elementToChange);
        createFontSizeSelector(editPanel, elementToChange);
    }
}


function showEditPanelHeader(parentElement) {
    var optionsLabel = document.createElement("h1");
    optionsLabel.innerHTML = "Options";
    optionsLabel.className = "editPanelHeader";
    parentElement.appendChild(optionsLabel);
}


function createTitleChanger(parentElement) {
    var changeTitleLabel = document.createElement("p");
    changeTitleLabel.innerHTML = "Change web page title:";
    changeTitleLabel.className = "editPanelLabel";
    parentElement.appendChild(changeTitleLabel);

    var titleInputGroup = document.createElement("div");
    titleInputGroup.className = "input-group";
    titleInputGroup.style = "margin-bottom: 1em";
    parentElement.appendChild(titleInputGroup);

    var titleInput = document.createElement("input");
    titleInput.type = "text";
    titleInput.className = "uploadFile form-control input-sm";
    titleInput.id = "titleInput";
    titleInput.value = document.getElementsByTagName("title")[1].innerHTML;
    titleInputGroup.appendChild(titleInput);

    var titleButtonSpan = document.createElement("span");
    titleButtonSpan.className = "input-group-btn";
    titleInputGroup.appendChild(titleButtonSpan);

    var titleInputButton = document.createElement("button");
    titleInputButton.innerHTML = "Change";
    titleInputButton.id = "titleInputButton";
    titleInputButton.className = "btn-primary btn-sm";
    titleButtonSpan.appendChild(titleInputButton);

    $(document).ready(function () {
        $("#titleInputButton").click(function () {
            alert("Web page title changed to: " + titleInput.value + ".");
            document.getElementsByTagName("title")[1].innerHTML = titleInput.value;
        });
    });
}


function createImageFromURLSelector(parentElement, elementToChange) {
    var imageFromURLSelectorLabel = document.createElement("p");
    imageFromURLSelectorLabel.innerHTML = "Choose image via URL:";
    imageFromURLSelectorLabel.className = "imageURLPanelLabel";
    parentElement.appendChild(imageFromURLSelectorLabel);

    var imageFromURLSelectorInputGroup = document.createElement("div");
    imageFromURLSelectorInputGroup.className = "input-group";

    var imageURLText = document.createElement("input");
    imageURLText.className = "form-control input-sm";
    imageFromURLSelectorInputGroup.appendChild(imageURLText);

    var urlUploadButtonSpan = document.createElement("span");
    urlUploadButtonSpan.className = "input-group-btn";

    var urlUploadButton = document.createElement("button");
    urlUploadButton.innerHTML = "Load";
    urlUploadButton.className = "btn-primary btn-sm";
    urlUploadButtonSpan.appendChild(urlUploadButton);

    imageFromURLSelectorInputGroup.appendChild(urlUploadButtonSpan);

    parentElement.appendChild(imageFromURLSelectorInputGroup);

    urlUploadButton.onclick = function (event) {
        if (imageURLText.value == null || imageURLText.value == "") {
            window.alert("Url slike nije unesen");
        } else {
            //TODO add url to constants
            var url = HTML_FILES_PATH + '/uploadPicture';
            var data = JSON.stringify({ "imageURL": imageURLText.value });
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                success: function (data, textStatus, jqXHR) {
                    elementToChange.src = data;
                    alert("Slika je uspješno učitana.");
                },

                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Greška kod učitavanja slike.");
                    // alert(errorThrown); // check which error is thrown
                }
            });
        }
    };
}


function createImageFromFileSelector(parentElement, elementToChange) {
    var imageFromFileSelectorLabel = document.createElement("p");
    imageFromFileSelectorLabel.innerHTML = "<br />Choose image from computer:";
    imageFromFileSelectorLabel.className = "imageFilePanelLabel";
    parentElement.appendChild(imageFromFileSelectorLabel);

    var fileSelectorInputGroup = document.createElement("div");
    fileSelectorInputGroup.className = "input-group";

    var fileSelector = document.createElement("input");
    fileSelector.type = "file";
    fileSelector.className = "uploadFile form-control input-sm";
    fileSelector.accept = 'image/*';
    fileSelector.style = "margin-bottom: 0.1em";

    var fileInput;
    var dataURL;
    fileSelector.onchange = function (event) {
        fileInput = event.target;
        var reader = new FileReader();
        reader.onload = function () {
            dataURL = reader.result;
        };
        reader.readAsDataURL(fileInput.files[0]);
    };

    fileSelectorInputGroup.appendChild(fileSelector);

    var fileUploadButtonSpan = document.createElement("span");
    fileUploadButtonSpan.className = "input-group-btn";

    var fileUploadButton = document.createElement("button");
    fileUploadButton.innerHTML = "Učitaj";
    fileUploadButton.className = "btn-primary btn-sm";
    fileUploadButtonSpan.appendChild(fileUploadButton);

    fileSelectorInputGroup.appendChild(fileUploadButtonSpan);

    parentElement.appendChild(fileSelectorInputGroup);


    fileUploadButton.onclick = function () {
        if (fileInput == null) {
            window.alert("Image is not chosen.");
        } else {
            //TODO add url to constants
            var url = HTML_FILES_PATH + '/uploadPicture';
            var data = JSON.stringify({ "image": dataURL });
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                success: function (data, textStatus, jqXHR) {
                    elementToChange.src = data;
                    alert("Slika je uspješno učitana.");
                },

                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Greška u učitavanju slike.");
                    // alert(errorThrown); // check which error is thrown
                }
            });
        }
    };
}


function createBackgroundColorSelector(parentElement, elementToChange) {
    var backgroundColorSelectorLabel = document.createElement("p");
    backgroundColorSelectorLabel.innerHTML = "Change background color:";
    backgroundColorSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(backgroundColorSelectorLabel);

    var backgroundColorSelector = document.createElement("button");
    backgroundColorSelector.id = "backgroundColorSelector";
    backgroundColorSelector.className = "changeColorButton";
    backgroundColorSelector.style.backgroundColor = elementToChange.style.backgroundColor;
    backgroundColorSelector.style.marginBottom = "1em";

    parentElement.appendChild(backgroundColorSelector);
    addColorPickerEventListener(backgroundColorSelector, elementToChange, 'backgroundColor');
}


function createFontColorSelector(parentElement, elementToChange) {
    var fontColorSelectorLabel = document.createElement("p");
    fontColorSelectorLabel.innerHTML = "Change text color:";
    fontColorSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(fontColorSelectorLabel);

    var fontColorSelector = document.createElement("button");
    fontColorSelector.id = "fontColorSelector";
    fontColorSelector.className = "changeColorButton";
    fontColorSelector.style.backgroundColor = elementToChange.style.color;
    fontColorSelector.style.marginBottom = "1em";

    parentElement.appendChild(fontColorSelector);
    addColorPickerEventListener(fontColorSelector, elementToChange, 'color');
}


function addColorPickerEventListener(colorPicker, elementToChange, propertyToChange) {
    $('#' + colorPicker.id).ColorPicker({
        color: '#0000ff',
        onShow: function (colpkr) {
            $(colpkr).fadeIn(500);
            return false;
        },
        onHide: function (colpkr) {
            $(colpkr).fadeOut(500);
            return false;
        },
        onChange: function (hsb, hex, rgb) {
            $('#' + colorPicker.id).css('backgroundColor', '#' + hex);
            $('#' + elementToChange.id).css(propertyToChange, '#' + hex);
        }
    });
}


function createFontSelector(parentElement, elementToChange) {
    var fontSelectorLabel = document.createElement("p");
    fontSelectorLabel.innerHTML = "Choose font:";
    fontSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(fontSelectorLabel);

    var fontSelector = document.createElement("select");
    fontSelector.className = "selector form-control";
    fontSelector.style.marginBottom = "1em";

    var fontFamilies = [
        '"Times New Roman", Times, serif',
        'Georgia, serif',
        '"Palatino Linotype", "Book Antiqua", Palatino, serif',
        'Arial, Helvetica, sans-serif',
        'Arial Black", Gadget, sans-serif',
        '"Comic Sans MS", cursive, sans-serif',
        'Impact, Charcoal, sans-serif',
        '"Lucida Sans Unicode", "Lucida Grande", sans-serif',
        'Tahoma, Geneva, sans-serif',
        '"Trebuchet MS", Helvetica, sans-serif',
        'Verdana, Geneva, sans-serif',
        '"Courier New", Courier, monospace',
        '"Lucida Console", Monaco, monospace'
    ];

    var currentFontFamily = elementToChange.style.fontFamily;

    for (var i in fontFamilies) {
        var option = document.createElement("option");
        option.value = fontFamilies[i];
        option.innerHTML = fontFamilies[i].split(',')[0];
        option.style.fontFamily = fontFamilies[i];

        if (option.value == currentFontFamily) {
            option.selected = "selected";
        }
        fontSelector.appendChild(option);
    }

    parentElement.appendChild(fontSelector);

    fontSelector.onchange = function () {
        elementToChange.style.fontFamily = this.value;
    };
}


function createFontSizeSelector(parentElement, elementToChange) {
    var fontSizeSelectorLabel = document.createElement("p");
    fontSizeSelectorLabel.innerHTML = "Choose font size:";
    fontSizeSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(fontSizeSelectorLabel);

    var fontSizeSelector = document.createElement("select");
    fontSizeSelector.className = "selector form-control";
    fontSizeSelector.style.marginBottom = "1em";

    var currentFontSize = elementToChange.style.fontSize;

    for (var i = 1; i <= 40; i++) {
        var option = document.createElement("option");
        option.value = i + 'px';
        option.innerHTML = i;

        if (option.value == currentFontSize) {
            option.selected = "selected";
        }
        fontSizeSelector.appendChild(option);
    }

    parentElement.appendChild(fontSizeSelector);

    fontSizeSelector.onchange = function () {
        elementToChange.style.fontSize = this.value;
    };
}