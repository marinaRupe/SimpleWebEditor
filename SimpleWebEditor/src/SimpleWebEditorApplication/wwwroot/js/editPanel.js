//TODO: check tag name
function showEditPanel (elementToChange) {
    var editPanel = document.getElementById("editPanel");

    //remove child nodes
    if (editPanel.childElementCount != 0 ) {
        editPanel.innerHTML = '';
    }

    showEditPanelHeader(editPanel);

    var elementTag = elementToChange.tagName.toLowerCase();

    if (elementTag == "div") {
        createBackgroundColorSelector(editPanel, elementToChange);
    }

    else if (elementTag == "h1" || elementTag == "h3") {
        createBackgroundColorSelector(editPanel, elementToChange.parentNode);
        createFontColorSelector(editPanel, elementToChange);
        createFontSelector(editPanel, elementToChange);
        createFontSizeSelector(editPanel, elementToChange);

    }
    else {
        createBackgroundColorSelector(editPanel, elementToChange.parentNode);
        createFontColorSelector(editPanel, elementToChange);
        createFontSelector(editPanel, elementToChange);
        createFontSizeSelector(editPanel, elementToChange);
    }
}


function showEditPanelHeader (parentElement) {
    var optionsLabel = document.createElement("h1");
    optionsLabel.innerHTML = "Opcije";
    optionsLabel.className = "editPanelHeader";
    parentElement.appendChild(optionsLabel);
}


function createBackgroundColorSelector(parentElement, elementToChange) {
    var backgroundColorSelectorLabel = document.createElement("p");
    backgroundColorSelectorLabel.innerHTML = "Promijeni boju pozadine:";
    backgroundColorSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(backgroundColorSelectorLabel);

    var backgroundColorSelector = document.createElement("button");
    backgroundColorSelector.id = "backgroundColorSelector";
    backgroundColorSelector.className = "changeColorButton";
    backgroundColorSelector.style.backgroundColor = elementToChange.style.backgroundColor;

    parentElement.appendChild(backgroundColorSelector);
    addColorPickerEventListener(backgroundColorSelector, elementToChange, 'backgroundColor');
}


function createFontColorSelector(parentElement, elementToChange) {
    var fontColorSelectorLabel = document.createElement("p");
    fontColorSelectorLabel.innerHTML = "Promijeni boju teksta:";
    fontColorSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(fontColorSelectorLabel);

    var fontColorSelector = document.createElement("button");
    fontColorSelector.id = "fontColorSelector";
    fontColorSelector.className = "changeColorButton";
    fontColorSelector.style.backgroundColor = elementToChange.style.color;

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


function createFontSelector (parentElement, elementToChange) {
    var fontSelectorLabel = document.createElement("p");
    fontSelectorLabel.innerHTML = "Izaberi font:";
    fontSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(fontSelectorLabel);

    var fontSelector = document.createElement("select");
    fontSelector.className = "selector";

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

    fontSelector.onchange = function() {
        elementToChange.style.fontFamily = this.value;
    };
}


function createFontSizeSelector(parentElement, elementToChange) {
    var fontSizeSelectorLabel = document.createElement("p");
    fontSizeSelectorLabel.innerHTML = "Izaberi veličinu fonta:";
    fontSizeSelectorLabel.className = "editPanelLabel";
    parentElement.appendChild(fontSizeSelectorLabel);

    var fontSizeSelector = document.createElement("select");
    fontSizeSelector.className = "selector";

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

    fontSizeSelector.onchange = function() {
        elementToChange.style.fontSize = this.value;
    };
}