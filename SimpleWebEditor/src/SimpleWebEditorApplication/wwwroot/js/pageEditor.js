const TEMPLATES_PATH = '../html/templates/';
const TEMPLATE_PICTURES_PATH = "../images/";

const SAVED_PAGE_MESSAGE = "Changes are successfully saved.";
const PAGE_NOT_SAVED_MESSAGE = "Failed to save changes.";
const PUBLISHED_PAGE_MESSAGE = "Page has been successfully published.";
const PAGE_NOT_PUBLISHED_MESSAGE = "Failed to publish page";

var SAVE_PAGE_PATH = myApp.Urls.savePageAction;
var PUBLISH_PAGE_PATH = myApp.Urls.publishPageAction;
var BASE_URL = myApp.Urls.baseUrl;

pageEditorSetup();

function pageEditorSetup() {
    showEditPanelHeader(document.getElementById("editPanel"));
    loadTemplates(document.getElementById("templatesPanel"));
    setLoadWorkPageButton(BASE_URL + myApp.Urls.workPagePath);
    setLoadPublishedPageButton(BASE_URL + myApp.Urls.publishedPagePath);
}

function setLoadWorkPageButton(pagePath) {
    $(document).ready(function () {
        //remove previous on-click event listeners
        $("#loadWorkPageButton").unbind("click");
        $("#loadWorkPageButton").click(function () {
            loadPage(pagePath, "WORK_PAGE");
        });
    });
}

function setLoadPublishedPageButton(pagePath) {
    $(document).ready(function () {
        //remove previous on-click event listeners
        $("#loadPublishedPageButton").unbind("click");
        $("#loadPublishedPageButton").click(function () {
            loadPage(pagePath, "PUBLISHED_PAGE");
        });
    });
}

function loadPage(pagePath, pageType) {
    loadPageToEditor(pagePath);
    setOpenInNewWindowButton(pagePath);
}

function publishPage() {
    savePage(PUBLISH_PAGE_PATH, PUBLISHED_PAGE_MESSAGE, PAGE_NOT_PUBLISHED_MESSAGE, "PUBLISHED_PAGE");
}

function saveToWorkPage() {
    savePage(SAVE_PAGE_PATH, SAVED_PAGE_MESSAGE, PAGE_NOT_SAVED_MESSAGE, "WORK_PAGE");
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
    setOpenInNewWindowButton(templatePath);
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
        addChangeOptions(child);
    }
}


function savePage(url, successMessage, errorMessage, pageType) {
    var pageContainer = document.getElementById("loadedPageFrame");
    removeContentEditableProperties(pageContainer);
    resetLocalPicturePathForHtml(pageContainer);

    var loadedPageHtml = ('<!DOCTYPE html> <html lang="en"><head>'
    + ($("div#loadedPageFrame").html()).replace('</style>', '</style></head><body style="margin: 0">')
    + '</body></html>')
        .replace(/\t/g, '    ');

    var data = { "html": loadedPageHtml };

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        traditional: true,
        //cache: false,
        success: function (response) {
            const pagePath = response.responseText;
            
            if (pageType === "WORK_PAGE") {
                setLoadWorkPageButton(pagePath);
            } else {
                setLoadPublishedPageButton(pagePath);
            }

            loadPageToEditor(pagePath);
            alert(successMessage);         
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorMessage);
            // alert(errorThrown); // check which error is thrown
            addContentEditableProperties(pageContainer);
            setLocalPicturePathForEditor(pageContainer);
        }
    });

}


function addContentEditableProperties(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        var tagName = child.tagName.toLowerCase();

        if (child.id != '' && (tagName == 'h1' || tagName == 'h3')) {
            child.contentEditable = true;
        }
        addContentEditableProperties(child);
    }
}

function removeContentEditableProperties(element) {
    for (var i = 0; i < element.children.length; i++) {
        var child = element.children[i];
        var tagName = child.tagName.toLowerCase();

        if (child.id != '' && (tagName == 'h1' || tagName == 'h3')) {
            child.contentEditable = false;
        }
        removeContentEditableProperties(child);
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
        setLocalPicturePathForEditor(child);
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
        resetLocalPicturePathForHtml(child);
    }
}
