
var form = document.getElementById('uploadForm');

form.addEventListener('submit', function (event) { uploadFile(event) });

function uploadFile(e) {

    e.preventDefault();

    var form = document.getElementById('uploadForm');
    var formData = new FormData(form);
    var uploadButton = document.querySelector('#uploadForm button');
    var result = document.getElementById('result');

    uploadButton.disabled = true;
    uploadButton.classList.add('loading');

    var data = {
        method: 'POST',
        body: formData
    };

    fetch('http://localhost:2070/api/FileProcess/Upload', data).then(function (res) {
        
        if (!res.ok) {
            return res.json().then(function (err) {
                var errMsg = null;

                if (err.status === 400 && err.errors?.file) {
                    errMsg = err.errors.file[0];
                }

                return { success: false, errorMessage: errMsg };
            });
        } 

        return res.json();
               
    }).then(function (data) {
       
        if (data.success) {
            result.innerHTML = '<p class="okMsg">Success!</p>'
        } else {           
            errMsg = data.errorMessage || 'Problem has occured. Please try again later.';
            result.innerHTML = '<p class="errorMsg">' + errMsg + '</p>'
        }
    }).catch(function (err) {
        result.innerHTML = '<p class="errorMsg">Problem has occured. Please try again later.</p>'
    }).finally(function () {
        uploadButton.disabled = false;
        uploadButton.classList.remove('loading');
    });
}