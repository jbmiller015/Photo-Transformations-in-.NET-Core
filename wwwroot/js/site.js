// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const uri = '/api/Photo';

function showRotBox(cbox) {
    if (cbox.checked) {
        var input = document.createElement("input");
        input.id = "rotateInput"
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
        inputX.id = "xInput"
        inputX.type = "number";
        inputX.min = "0";
        inputX.max = "9999";
        var inputY = document.createElement("input");
        inputY.id = "yInput"
        inputY.type = "number";
        inputY.min = "0";
        inputY.max = "9999";
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
        if ($(this).val() === "Rotate")
            return $(this).val().concat(" :" + document.getElementById("rotateInput").value.toString());
        if ($(this).val() === "Resize")
            return $(this).val().concat(" :" + document.getElementById("xInput").value.toString() +"|"+
                    document.getElementById("yInput").value.toString());
        else
            return $(this).val();
        }).get();
        console.log('SearchId:',searchIDs);

        var formFields = { Instructions: searchIDs, Image: image };

        console.log('FormFields:', formFields);

    fetch(uri, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(formFields)
    })
    .then((response) => response.json())
    .then((data) => {
        console.log('Success:', data);  
        document.getElementById("ItemPreview").src = data;
        document.getElementById("image").style.display = 'block';
        })
}
