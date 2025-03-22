function buttonShowHandbook(button) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));
    const parent = button.parentElement;
    const inputKeyReturnSelectValue = parent.querySelectorAll('input[type="hidden"]')[0];

    var isReturnSelectValue = Boolean(button.getAttribute('isReturnSelectValue'));

    if (isReturnSelectValue === true) {
        if (!window.inputReturnIds) {
            window.inputReturnIds = [];
        }

        if (inputKeyReturnSelectValue.id.length > 0) {
            window.inputReturnIds.push(inputKeyReturnSelectValue.id);
        }
        else {
            isReturnSelectValue = false;
        }
    }

    const callback = (data) => {
        $('body').append(data);

        getMainBodyHandbook(idHandbookButton);

        var modalHandbook = $('#Handbook' + idHandbookButton);

        modalHandbook.modal('show');

        modalHandbook.on('hidden.bs.modal', function () {
            modalHandbook.remove();

            if (isReturnSelectValue === true && window.inputReturnIds) {
                window.inputReturnIds.pop();
            }
        });
    }

    sendAjaxRequest('/Handbook/GetHandbookHTML', 'GET', { idHandbook: idHandbookButton, isReturnSelectValue: isReturnSelectValue }, callback);
}

function sendAjaxRequest(urlRequest, typeUrlRequest, dataRequest, callback) {
    setDisplayLoadingIndicator('block');

    $.ajax({
        url: urlRequest,
        type: typeUrlRequest,
        data: dataRequest,
        success: function (data) {
            setDisplayLoadingIndicator('none');

            if (typeof callback === 'function') {
                callback(data);
            }

            updaetTooltips();
        },
        error: function (xhr, status, error) {
            setDisplayLoadingIndicator('none');

            console.error('Ошибка:', error);

            updaetTooltips();
        }
    });
}

function setDisplayLoadingIndicator(style) {
    const loadingIndicator = document.getElementById('loadingIndicator');
    const overlay = document.getElementById('overlay');

    loadingIndicator.style.display = style;
    overlay.style.display = style;
}

function getMainBodyHandbook(idHandbook, page = 0, inputField = null) {
    const callback = (data) => {
        var handbookBody = $('#HandbookBody' + idHandbook);

        handbookBody.append(data);

        showFooterHandbook(handbookBody[0].offsetParent);
    }

    sendAjaxRequest('/Handbook/GetMainBodyHandbookHTML', 'GET', { idHandbook: idHandbook, page: page, inputField: inputField, selectValue: null }, callback);
}

function showFooterHandbook(handbookContent) {
    var footer = handbookContent.querySelectorAll('.modal-footer')[0];

    if (footer.classList.contains('visually-hidden')) {
        footer.classList.remove('visually-hidden');
    }
}

function buttonInsertValuesHandbook(button) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));

    const callback = (data) => {
        var handbookBody = $('#HandbookBody' + idHandbookButton);

        handbookBody.empty();

        handbookBody.append(data);

        hideFooterHandbook(handbookBody[0].offsetParent);
    }

    sendAjaxRequest('/Handbook/GetInsertBodyHTML', 'GET', { idHandbook: idHandbookButton }, callback);
}

function buttonMainBodyHandbook(button, page, inputField, selectValue) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));

    setSelectValueToButtonReturn(button.closest('[id="Handbook' + idHandbookButton + '"]'), selectValue);

    const callback = (data) => {
        var handbookBody = $('#HandbookBody' + idHandbookButton);

        handbookBody.empty();

        handbookBody.append(data);

        showFooterHandbook(handbookBody[0].offsetParent);
    }

    sendAjaxRequest('/Handbook/GetMainBodyHandbookHTML', 'GET', { idHandbook: idHandbookButton, page: page, inputField: inputField, selectValue: selectValue }, callback);
}

function setSelectValueToButtonReturn(handbookTarget, selectValue) {
    buttonReturnSelect = handbookTarget.querySelectorAll('button[ReturnSelectValue="true"]');

    buttonReturnSelect.forEach(button => {
        if (button && selectValue && selectValue["key"] != "null" && selectValue["value"] != "null") {
            button.setAttribute('key', selectValue["key"]);
            button.setAttribute('value', selectValue["value"]);
        }
    });
}

function buttonSaveValuesInsertHandbook(button, type) {

    const url = type == "insert" ? '/Handbook/Insert' : '/Handbook/Update'

    const idHandbookButton = Number(button.getAttribute('idHandbook'));

    var target = button.closest('[id ^= "HandbookBody"]');

    var formData = getFormDataTarget(target);

    const callback = (data) => {
        $('body').append(data);

        openToals(idHandbookButton);
    }

    sendAjaxRequest(url, 'POST', formData, callback);
}

function getFormDataTarget(target) {
    var formData = {};

    if (target) {
        var inputs = target.querySelectorAll('input, select, textarea');

        inputs.forEach(function (input) {
            if (input.name) {
                formData[input.name] = input.value;
            }
        });
    }

    return formData;
}

function openToals(idHandbook) {
    var toastEl = document.getElementById('HandbookToast' + idHandbook);

    var toast = new bootstrap.Toast(toastEl);

    toast.show();

    setTimeout(function () {
        toast.hide();
    }, 5000);

    toastEl.addEventListener('hide.bs.toast', function () {
        toastEl.remove();
    });
}

function updaetTooltips() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
}

function buttonRetrievalValuesHandbook(button, selectValue) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));

    var parent = button.parentElement;

    var inputForm = parent.querySelectorAll('input')[0];

    const callback = (data) => {
        var handbookBody = $('#HandbookBody' + idHandbookButton);
        handbookBody.empty();
        handbookBody.append(data);
    }

    sendAjaxRequest('/Handbook/GetMainBodyHandbookHTML', 'Get', { idHandbook: idHandbookButton, page: 0, inputField: inputForm.value, selectValue: selectValue }, callback);
}

function buttonUpdateValue(button, selectValue) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));

    const callback = (data) => {
        var handbookBody = $('#HandbookBody' + idHandbookButton);

        handbookBody.empty();

        handbookBody.append(data);

        hideFooterHandbook(handbookBody[0].offsetParent);
    }

    sendAjaxRequest('/Handbook/GetUpdateBodyHTML', 'Get', { idHandbook: idHandbookButton, selectValue: selectValue }, callback);
}

function hideFooterHandbook(handbookContent) {
    var footer = handbookContent.querySelectorAll('.modal-footer')[0];

    if (!footer.classList.contains('visually-hidden')) {
        footer.classList.add('visually-hidden');
    }
}

function buttonDeleteValue(button, selectValue) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));
    const page = button.getAttribute('page') ?? 0;
    const inputField = button.getAttribute('inputField') ?? null;

    var formData = {
        'Handbook.IdHandbook': idHandbookButton,
        'KeyFieldValue': selectValue['key']
    };

    const callback = (data) => {
        $('body').append(data);

        var handbookBody = $('#HandbookBody' + idHandbookButton);

        handbookBody.empty();

        getMainBodyHandbook(idHandbookButton, page, inputField);

        openToals(idHandbookButton);
    }

    sendAjaxRequest('/Handbook/Delete', 'POST', formData, callback);
}

function buttonReturnSelectValue(button) {
    const idHandbookButton = Number(button.getAttribute('idHandbook'));
    const selectKey = button.getAttribute('key');
    const selectValue = button.getAttribute('value');

    var modalHandbook = $('#Handbook' + idHandbookButton);

    var idReturn = window.inputReturnIds[window.inputReturnIds.length - 1];

    var inputKeyReturn = document.getElementById(idReturn);

    var parentInput = inputKeyReturn.parentElement;

    var inputValueReturn = parentInput.querySelectorAll('input[type="text"]')[0];

    inputKeyReturn.value = selectKey;

    inputValueReturn.value = selectValue;

    modalHandbook.modal('hide');
}

function buttonClearSelectValue(button) {
    var parent = button.parentElement;

    var inputForm = parent.querySelectorAll('input');

    inputForm.forEach(function (input) {
        if (input) {
            input.value = null;
        }
    });

}