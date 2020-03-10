// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const uri = '/api/Photo';

function showRotBox(cbox) {
    if (cbox.checked) {
        var input = document.createElement("input");
        input.type = "number";
        input.min = "-360";
        input.max = "360";
        var div = document.createElement("div");
        div.id = cbox.name;
        div.innerHTML = "Rotate (Degrees)";
        div.appendChild(input);
        document.getElementById("insertinputs1").appendChild(div);
    } else {
        document.getElementById(cbox.name).remove();
    }
};

function showResBox(cbox) {
    if (cbox.checked) {
        var inputX = document.createElement("input");
        inputX.type = "number";
        inputX.min = "0";
        inputX.max = "999";
        var inputY = document.createElement("input");
        inputY.type = "number";
        inputY.min = "0";
        inputY.max = "999";
        var div = document.createElement("div");
        div.id = cbox.name;
        div.innerHTML += "Resize X Y (Pixels)" + inputX.outerHTML + inputY.outerHTML;
        document.getElementById("insertinputs2").appendChild(div);
    } else {
        document.getElementById(cbox.name).remove();
    }
};

const reader = new FileReader();

function encodeImageFileAsURL(element) {
    var file = element.files[0];
    reader.onloadend = function () {
        console.log('RESULT', reader.result)
    };
    reader.readAsDataURL(file);
};

function submitForm() {
    const image = reader.result;
        var searchIDs = $("input:checkbox:checked").map(function () {
            return $(this).val();
        }).get();
        console.log(searchIDs);

        var formFields = { Instructions: searchIDs, Image: image };

        console.log('FormFields:', formFields);

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formFields)
        })
            .then(response => response.json())
            .then(data => _display(data))
            .catch(error => console.error('Failed to uplaod', error));

    };
function _display(data) {
    document.getElementById("ItemPreview").src = data;
    };