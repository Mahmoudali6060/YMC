$(document).ready(function () {
    window.Cropper;

    Dropzone.autoDiscover = false;
    var c = 0;

    var cropped = false;
    var myDropzone = new Dropzone('#my-dropzone', {
        url: "/my/url",
        addRemoveLinks: true,
        parallelUploads: 1,
        uploadMultiple: false,
        createImageThumbnails: true,
        autoProcessQueue: false,
        maxFiles: 10
    });

    myDropzone.on('addedfile', function (file) {
        if (!cropped) {
            myDropzone.removeFile(file);
            cropper(file);
        } else {
            cropped = false;
            var previewURL = URL.createObjectURL(file);
            var dzPreview = $(file.previewElement).find('img');
            dzPreview.attr("src", previewURL);
        }
    });


    var cropper = function (file) {
        var fileName = file.name;
        var loadedFilePath = getSrcImageFromBlob(file);
        // @formatter:off
        var modalTemplate =
            '<div class="modal fade" tabindex="-1" role="dialog">' +
            '<div class="modal-dialog" role="document">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times" aria-hidden="true"></i></button>' +
            '</div>' +
            '<div class="modal-body">' +
            '<div class="cropper-container">' +
            '<img id="img-' + ++c + '" src="' + loadedFilePath + '" data-vertical-flip="false" data-horizontal-flip="false">' +
            '</div>' +
            '</div>' +
            '<div class="modal-footer">' +
            '<button type="button" class="btn btn-warning rotate-left"><span class="fa fa-rotate-left"></span></button>' +
            '<button type="button" class="btn btn-warning rotate-right"><span class="fa fa-rotate-right"></span></button>' +
            '<button type="button" class="btn btn-warning scale-x" data-value="-1"><span class="fa fa-arrows-h"></span></button>' +
            '<button type="button" class="btn btn-warning scale-y" data-value="-1"><span class="fa fa-arrows-v"></span></button>' +
            '<button type="button" class="btn btn-warning reset"><span class="fa fa-refresh"></span></button>' +
            '<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
            '<button type="button" class="btn btn-primary crop-upload">Crop & upload</button>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';
        // @formatter:on

        var $cropperModal = $(modalTemplate);

        $cropperModal.modal('show').on("shown.bs.modal", function () {
            var $image = $('#img-' + c);
            console.log($image);
            var cropper = $image.cropper({
                autoCropArea: 1,
                aspectRatio: 9 / 16,
                cropBoxResizable: false,
                movable: true,
                rotatable: true,
                scalable: true,
                viewMode: 2,
                minContainerWidth: 250,
                maxContainerWidth: 250
            })
                .on('hidden.bs.modal', function () {
                    $image.cropper('destroy');
                });

            $cropperModal.on('click', '.crop-upload', function () {
                // get cropped image data
                $image.cropper('getCroppedCanvas', {
                    width: 160,
                    height: 90,
                    minWidth: 256,
                    minHeight: 256,
                    maxWidth: 4096,
                    maxHeight: 4096,
                    fillColor: '#fff',
                    imageSmoothingEnabled: false,
                    imageSmoothingQuality: 'high'
                }).toBlob(function (blob) {
                    var croppedFile = blobToFile(blob, fileName);
                    croppedFile.accepted = true;
                    var files = myDropzone.getAcceptedFiles();
                    for (var i = 0; i < files.length; i++) {
                        var file = files[i];
                        if (file.name === fileName) {
                            myDropzone.removeFile(file);
                        }
                    }
                    cropped = true;

                    myDropzone.files.push(croppedFile);
                    myDropzone.emit('addedfile', croppedFile);
                    myDropzone.createThumbnail(croppedFile); //, width, height, resizeMethod, fixOrientation, callback)
                    $cropperModal.modal('hide');
                });
            })
                .on('click', '.rotate-right', function () {
                    $image.cropper('rotate', 90);
                })
                .on('click', '.rotate-left', function () {
                    $image.cropper('rotate', -90);
                })
                .on('click', '.reset', function () {
                    $image.cropper('reset');
                })
                .on('click', '.scale-x', function () {
                    if (!$image.data('horizontal-flip')) {
                        $image.cropper('scale', -1, 1);
                        $image.data('horizontal-flip', true);
                    } else {
                        $image.cropper('scale', 1, 1);
                        $image.data('horizontal-flip', false);
                    }
                })
                .on('click', '.scale-y', function () {
                    if (!$image.data('vertical-flip')) {
                        $image.cropper('scale', 1, -1);
                        $image.data('vertical-flip', true);
                    } else {
                        $image.cropper('scale', 1, 1);
                        $image.data('vertical-flip', false);
                    }
                });
        });
    };


    function getSrcImageFromBlob(blob) {
        var urlCreator = window.URL || window.webkitURL;
        return urlCreator.createObjectURL(blob);
    }

    function blobToFile(theBlob, fileName) {
        theBlob.lastModifiedDate = new Date();
        theBlob.name = fileName;
        return theBlob;
    }
});
