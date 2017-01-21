const TEMPLATES_PATH = '../html/templates/';
const PICTURES_PATH = FILES_PATH + '/pictures/';
const TEMPLATE_PICTURES_PATH = "../images/";
const SAVE_WORK_PAGE_PATH = HTML_FILES_PATH + '/saveWorkPage';
const SAVE_PUBLISHED_PAGE_PATH = HTML_FILES_PATH + '/savePublishedPage';
const LOAD_WORK_PAGE_PATH = HTML_FILES_PATH + '/getWorkPage';
const LOAD_PUBLISHED_PAGE_PATH = HTML_FILES_PATH + '/getPublishedPage';

const PAGE_TYPE_COOKIE = "ChildWeb.pageType";
const PUBLISHED_PAGE = "PUBLISHED_PAGE";
const WORK_PAGE = "WORK_PAGE";

const SAVED_PAGE_MESSAGE = "Changes are successfully saved.";
const PAGE_NOT_SAVED_MESSAGE = "Failed to save changes.";
const PUBLISHED_PAGE_MESSAGE = "Page has been successfully published.";
const PAGE_NOT_PUBLISHED_MESSAGE = "Failed to publish page";

pageEditorSetup();

function pageEditorSetup() {
    setOpenInNewWindowButton(LOAD_WORK_PAGE_PATH);
    setSavePageButton();
    setPublishPageButton();

    showEditPanelHeader(document.getElementById("editPanel"));
    loadTemplates(document.getElementById("templatesPanel"));
}


function loadPage(pagePath) {
    loadPageToEditor(pagePath);
    setOpenInNewWindowButton(pagePath);
}


function setOpenInNewWindowButton(pageLink) {
    $(document).ready(function () {
        //remove previous on-click event listeners
        $("#openInNewWindowButton").unbind("click");

        $("#openInNewWindowButton").click(function () {
            window.open(pageLink);
        });
    });
}


function setSavePageButton() {
    $(document).ready(function () {
        $("#savePageButton").click(function () {
            savePage(getCookie(PAGE_TYPE_COOKIE), SAVED_PAGE_MESSAGE, PAGE_NOT_SAVED_MESSAGE);
        });
    });
}


function setPublishPageButton() {
    $(document).ready(function () {
        $("#publishPageButton").click(function () {
            publishPage();
        });
    });
}


function loadTemplates(templatesPanel) {
    for (var i = 1; i <= 8; i++) {
        var templateImage = document.createElement("img");
        templatesPanel.appendChild(templateImage);

        templateImage.src = TEMPLATE_PICTURES_PATH + "template" + i.toString() + ".png";
        templateImage.className = "templateImage";
        templateImage.id = "templateImage" + i.toString();

        addChooseTemplateOption(templateImage, i);
    }
}


function addChooseTemplateOption(templateImage, templateIndex) {
    $("#" + templateImage.id).click(function () {
        chooseTemplate(templateIndex);
    });
}


function chooseTemplate(templateIndex) {
    var templatePath = TEMPLATES_PATH + "template" + templateIndex.toString() + ".html";

    emptyPageEditor();
    loadPageToEditor(templatePath);

    setCookie(PAGE_TYPE_COOKIE, WORK_PAGE);
    setOpenInNewWindowButton(templatePath); //TODO: change
}


function loadPageToEditor(pageLink) {
    $("#loadedPageFrame").load(pageLink, function () {
        var pageContainer = document.getElementById("loadedPageFrame");
        addChangeOptions(pageContainer);
        addContentEditableProperties(pageContainer);
        setLocalPicturePathForEditor(pageContainer);
    });
}


function emptyPageEditor() {
    var pageContainer = document.getElementById("loadedPageFrame");
    var editPanel = document.getElementById("editPanel");
    while (pageContainer.firstChild) {
        pageContainer.removeChild(pageContainer.firstChild);
    }
    while (editPanel.firstChild) {
        editPanel.removeChild(editPanel.firstChild);
    }

    showEditPanelHeader(editPanel);
}


function addChangeOptions(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        if (child.id != '') {
            $(document).ready(function () {
                $("#" + child.id).click(function (event) {
                    var event = event || window.event;
                    showEditPanel(this);
                    event.stopPropagation ? event.stopPropagation() : (event.cancelBubble = true);
                });
            });
        }
        addChangeOptions(child)
    }
}


function savePage(pageType, successMessage, errorMessage) {
    var pageContainer = document.getElementById("loadedPageFrame");
    removeContentEditableProperties(pageContainer);
    resetLocalPicturePathForHtml(pageContainer);

    var loadedPageHtml = ('<!DOCTYPE html> <html lang="en"><head>'
    + ($("div#loadedPageFrame").html()).replace('</style>', '</style></head><body style="margin: 0">')
    + '</body></html>')
        .replace(/\t/g, '    ');

    var data = JSON.stringify({ "html": loadedPageHtml });

    var url;
    if (pageType == WORK_PAGE) {
        url = SAVE_WORK_PAGE_PATH;
    }
    else if (pageType == PUBLISHED_PAGE) {
        url = SAVE_PUBLISHED_PAGE_PATH;
    } else {
        url = SAVE_WORK_PAGE_PATH;
    }

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        cache: false,
        contentType: 'application/json; charset=UTF-8',
        success: function (data, textStatus, jqXHR) {
            alert(successMessage);
            resetEditorPage(pageType);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorMessage);
            // alert(errorThrown); // check which error is thrown
            addContentEditableProperties(pageContainer);
            setLocalPicturePathForEditor(pageContainer);
        }
    });

}


function publishPage() {
    savePage(PUBLISHED_PAGE, PUBLISHED_PAGE_MESSAGE, PAGE_NOT_PUBLISHED_MESSAGE);
}


function resetEditorPage(pageType) {
    if (pageType == PUBLISHED_PAGE) {
        $("#loadPublishedPageButton").click();
    } else {
        $("#loadWorkPageButton").click();
    }
}


function addContentEditableProperties(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        var tagName = child.tagName.toLowerCase();

        if (child.id != '' && (tagName == 'h1' || tagName == 'h3')) {
            child.contentEditable = true;
        }
        addContentEditableProperties(child)
    }
}

function removeContentEditableProperties(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        var tagName = child.tagName.toLowerCase();

        if (child.id != '' && (tagName == 'h1' || tagName == 'h3')) {
            child.contentEditable = false;
        }
        removeContentEditableProperties(child)
    }
}


function setLocalPicturePathForEditor(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        var tagName = child.tagName.toLowerCase();

        if (child.id != '' && tagName === 'img') {
            var imgSrc = child.getAttribute('src');
            if (imgSrc.substring(0, 6) === '../../') {
                child.setAttribute('src', imgSrc.substring(3));
            }
        }
        setLocalPicturePathForEditor(child)
    }
}

function resetLocalPicturePathForHtml(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        var tagName = child.tagName.toLowerCase();

        if (child.id != '' && tagName === 'img') {
            var imgSrc = child.getAttribute('src');
            if (imgSrc.substring(0, 6) === '../') {
                child.setAttribute('src', '../' + imgSrc);
            }
        }
        resetLocalPicturePathForHtml(child)
    }
}




function setLoadInsidePageButton(pageLink) {
    $(document).ready(function () {
        //remove previous on-click event listeners
        $("#loadInsidePageButton").unbind("click");

        //add new on-click event listener
        $("#loadInsidePageButton").click(function () {
            loadPageToEditor(pageLink);
        });
    });
}