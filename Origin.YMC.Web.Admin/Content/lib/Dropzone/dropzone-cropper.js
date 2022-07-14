//import { debug } from "util";

// transform cropper dataURI output to a Blob which Dropzone accepts
function dataURItoBlob(dataURI) {
    var byteString = atob(dataURI.split(',')[1]);
    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ab], { type: 'image/jpeg' });
}

// modal window template
var modalTemplate = '<div class="modal fade" tabindex="-1" role="dialog">' +
    '<div class="modal-dialog modal-lg" role="document">' +
    '<div class="modal-content">' +
    '<div class="modal-header"><h3>Cropping<h3/>' +
    '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times" aria-hidden="true"></i></button>' +
    '</div>' +
    '<div class="modal-body">' +
    '<div class="image-container">' +
    '</div>' +
    '</div>' +
    '<div class="modal-footer">' +
    '<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
    '<button type="button" class="btn btn-primary crop-upload">Crop</button>' +
    '</div>' +
    '</div>' +
    '</div>' +
    '</div>';


// initialize dropzone
Dropzone.autoDiscover = false;
var myDropzone = new Dropzone(
    "#my-dropzone-container",
    {
        autoProcessQueue: false,
        addRemoveLinks: true,
        dictRemoveFile: "Delete",
        dictRemoveFileConfirmation: "Are you sure delete this file?",
        removedfile: function (file) {
           
                var name = file.previewElement.querySelector("[data-dz-name]").innerHTML;
                if (file.name.toLowerCase() == name.toLowerCase() && file.isClient) {
                    file.previewElement.parentNode.removeChild(file.previewElement);
                    return;
                }
                else {
                    $.ajax({
                        type: 'POST',
                        url: '/OGS/DeleteFile',
                        data: { FileName: name },
                        sucess: function (data) {
                            console.log('success: ' + data);
                        }
                    });
                    var _ref;
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                }
            
           

        },
        init: function () {
            var thisDropzone = this;
            //Call the action method to load the images from the server
            $.getJSON("/OGS/GetFiles/").done(function (data) {

                
                $.each(data, function (index, item) {
                    //// Create the mock file:
                    var mockFile = {
                        name: item.name,
                        size: item.size,
                        url: item.path,
                        cropped:true
                    };

                    // Call the default addedfile event handler
                    myDropzone.emit("addedfile", mockFile);
                    myDropzone.emit("complete", mockFile);
                    myDropzone.emit("thumbnail", mockFile, item.path);
                    myDropzone.files.push(mockFile);
                    // And optionally show the thumbnail of the file:
                   //    thisDropzone.emit("thumbnail", mockFile, item.path);

                    // If you use the maxFiles option, make sure you adjust it to the
                    // correct amount:
                    //var existingFileCount = 1; // The number of files already uploaded
                    //myDropzone.options.maxFiles = myDropzone.options.maxFiles - existingFileCount;
                });

            });
        }
        // ..your other parameters..
    }
);

// listen to thumbnail event
myDropzone.on('thumbnail', function (file) {
    // ignore files which were already cropped and re-rendered
    // to prevent infinite loop
    if (file.cropped) {
        return;
    }
    //if (file.width < 800) {
    //    // validate width to prevent too small files to be uploaded
    //    // .. add some error message here
    //    return;
    //}
    // cache filename to re-assign it to cropped file
    var cachedFilename = file.name;
    // remove not cropped file from dropzone (we will replace it later)
    file.ByCode = true;
    //file.isClient = true;
    myDropzone.removeFile(file);

    // dynamically create modals to allow multiple files processing
    var $cropperModal = $(modalTemplate);
    // 'Crop and Upload' button in a modal
    var $uploadCrop = $cropperModal.find('.crop-upload');

    var $img = $('<img />');
    // initialize FileReader which reads uploaded file
    var reader = new FileReader();
    reader.onloadend = function () {
        // add uploaded and read image to modal
        $cropperModal.find('.image-container').html($img);
        $img.attr('src', reader.result);

        // initialize cropper for uploaded image
        $img.cropper({
            //aspectRatio: 16 / 9,
            autoCropArea: 1,
            movable: false,
            cropBoxResizable: true,
            minContainerWidth: 772,
            minContainerHeight: 386,
            //crop: function (e) {
            //    console.log(e);
            //}
        });
    };
    // read uploaded file (triggers code above)
    reader.readAsDataURL(file);

    $cropperModal.modal('show');

    // listener for 'Crop and Upload' button in modal
    $uploadCrop.on('click', function () {
        // get cropped image data
        var blob = $img.cropper('getCroppedCanvas').toDataURL();
        // transform it to Blob object
        var newFile = dataURItoBlob(blob);
        // set 'cropped to true' (so that we don't get to that listener again)
        newFile.cropped = true;
        // assign original filename
        newFile.name = cachedFilename;

        // add cropped file to dropzone
        myDropzone.addFile(newFile);
        // upload cropped file with dropzone
        // myDropzone.processQueue();
        $cropperModal.modal('hide');
    });

    myDropzone.on("success", function (file, serverResponse) {
        var fileuploded = file.previewElement.querySelector("[data-dz-name]");
        fileuploded.innerHTML = serverResponse.newfilename;
        //var btndelete = file.previewElement.querySelector(".delete");
        //btndelete.setAttribute("id", 'delete-midia-id-' + serverResponse.midia_id);
    });
});