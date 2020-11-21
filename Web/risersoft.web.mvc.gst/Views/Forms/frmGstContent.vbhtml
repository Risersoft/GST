@imports AuthorizationFramework.Proxies
@imports risersoft.shared.portable
@imports risersoft.app.mxform.gst
@imports risersoft.shared.portable.Proxies
@imports risersoft.shared.web.Extensions
@imports Newtonsoft.Json
@ModelType frmGstContentModel


@Code
    ViewData("Title") = "Gst Content"
    Layout = "~/Views/Shared/_FrameworkLayout.vbhtml"

    Dim modelJson = JsonConvert.SerializeObject(Model, Formatting.Indented,
                    New JsonSerializerSettings With {.StringEscapeHandling = StringEscapeHandling.EscapeHtml, .NullValueHandling = NullValueHandling.Ignore})

End Code

<link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />

<link href="~/Content/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="~/Content/font-awesome.css" rel="stylesheet" />
<script src="~/Scripts/bootstrap-datepicker.min.js"></script>
<link href="~/Content/font-awesome.css" rel="stylesheet" />
<style>
    #progressc {
        display: none;
    }

    .progress {
        height: 25px !important;
    }

    .progressdis {
        display: block !important;
        width: 750px
    }

    .cresult {
        display: none;
    }

    .cresultdis {
        display: block !important;
    }

    .cprocess {
        display: none;
    }

    .cLink {
        display: none;
    }

    .cfailure {
        display: none;
        width: 750px
    }

    .percent {
        position: absolute;
        /*color: #fff;*/
        margin-left: 325px;
    }
</style>
<div class="container cbackground">


    <form action="" method="post" name="userform" id="userform" ng-controller="FormController">
        @Html.AntiForgeryToken()

        <input type="hidden" id="model_json" value='@Html.Raw(modelJson)' />
        <div Class="form-horizontal">









            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Category</Label>
                    <select name="Cdat" ng-model="ContentInfo.CategorySelected" ng-options="itemtax.DisplayText for itemtax in ValueLists.Category.ValueListItems track by itemtax.DataValue" Class="form-control" required ng-class="{true: 'error'}[submitted() && userform.Cdat.$invalid]"></select>

                </div>


            </div>







            <div Class="row">
                <div Class="col-md-1"></div>

                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Date</Label>
                    <input type="text" name="invoicedate" id="invoicedate" ng-model="ContentInfo.Dated" class="form-control my-datepicker" required ng-class="{true: 'error'}[submitted() && userform.invoicedate.$invalid]" />
                </div>

            </div>

            <div Class="row">
                <div Class="col-md-1"></div>


                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Title</Label>
                    <input name="title" type="text" id="title" ng-model="ContentInfo.Title" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.title.$invalid]" />
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>


                <div Class="col-md-10">
                    <Label Class="control-label labeltext">Notification No.</Label>
                    <input name="title" type="text" id="title" ng-model="ContentInfo.NoteNum" class="form-control" required ng-class="{true: 'error'}[submitted() && userform.title.$invalid]" />
                </div>
            </div>
            <div Class="row">
                <div Class="col-md-1"></div>
                <div Class="col-md-4">
                    <Label Class="control-label labeltext">File</Label>
                    <input type="file" id="filename" name="file" Class="form-control" autocomplete="off" />
                </div>


                <div Class="col-md-3">
                    <Label Class="control-label labeltext"> </Label>
                    <input type="button" value="Upload" class="btn btn-primary" style="margin-top:35px;" id="btnUpload" name="btnUpload" />
                </div>
            </div>

            <div Class="form-group">
                <div Class="col-md-offset-2 col-md-10 " style="margin-top:8px">
                    <div class="progress" id="progressc" style="text-align:center">
                        <div class="progress-bar"></div>
                        <div class="percent">0%</div>
                    </div>
                </div>
            </div>
            <div class="row"><hr /></div>




            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10"> </div>
            </div>
            @Html.Partial("_SavePanel")
        </div>

    </form>



</div>

@section BotScripts
    <script src="~/Scripts/jquery.form.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setInterval(function () {
                $(".my-datepicker").removeClass("my-datepicker").each(function () {
                    $(this).datepicker({ dateFormat: 'yy-mm-dd' });
                });
            }, 10);
        });


    </script>
    <script type="text/javascript">




        var submitUri = null;
        var _fileName = "";
        rsApp.controller('FormController', function ($scope, $http) {
            $scope.model = JSON.parse($("#model_json").val());
            $scope.status = 'loaded';
            var loadmodel = function (result) {

                $scope.model = result;
                $scope.ContentInfo = $scope.model.dsForm.DT[0];

                //$scope.ChallanItem = $scope.model.dsForm.ChallanItem;
                $('#invoicedate').datepicker({ dateFormat: 'yy-mm-dd' });





                $scope.ValueLists = $scope.model.ValueLists;


            @Html.RenderLookup("ContentInfo", Model, Model.dsForm.Tables(0))
            $('.progress').removeClass('progressdis');





            };






            loadmodel($scope.model);


            $scope.GenerateDelPayload = function () {

            };
            $scope.cleanupmodel = function (datamodel) {
                //empty function
            };


            $scope.calculatemodel = function () {
                $scope.ContentInfo.FileUrl = _fileName;


            };



            $scope.visdel = false;@Html.RenderFile("~/scripts/rsforms.js")
        });
        $(document).ready(function () {
            
            var bar = $('.progress-bar');
            var percent = $('.percent');
          
            var fileSize = "";
            var pFileId = "";
            var iFileId = "";
            var lstModifiedDateTime = "";
            $('#btnUpload').on('click', function () {
                //debugger;
                addDisable();
                addJsonDisable();

                var filc = '', filec = '', docType = '';
                //var action_type = $(this).attr("id") == "btnUpload" ? "import" : "convertjson";
                var action_type = getUrlParameter("ActionType");

                // For Progress baar
                var percentVal = '0%';
                bar.width(percentVal);
                percent.html(percentVal);
                $('.cprocess').removeClass('progressdis');
                $('.cfailure').removeClass('progressdis');
                $('.cprocess').html('');
                $('.cfailure').html('');
                filc = $('#filename');
                filec = $('#filename').val();
                var url = '/frmGstContent/ParamsOutput' + location.search;
                var payload = { filename: filec };
                payload = JSON.stringify(payload);
                var payloaddata = { key: 'sas', Params: payload };



                $.post(url, payloaddata, function (result) {
                    //debugger;
                    if (result.success) {
                        // create submit uri
                        submitUri = result.Data;
                        // If one file has been selected in the HTML file input element
                        var file = filc.get(0).files[0];
                        fileSize = file.size;
                        _fileName = result.flName;
                        debugger;
                        if ($('#impFileId').attr("data-import-fileId")) {
                            pFileId = $('#impFileId').attr("data-import-fileId")
                        }
                        else {
                            pFileId = result.previousFileID = "" ? null : result.previousFileID;
                        }


                        iFileId = result.ImportFileID;
                        lstModifiedDateTime = file.lastModified ? file.lastModified : new Date(file.lastModifiedDate).getTime();

                        // calculate the start and end byte index for each blocks(chunks)
                        // with the index, file name and index list for future using

                        // define the function array and push all chunk upload operation into this array
                        var blockIdPrefix = "block-";
                        var blockSizeInKB = 500;
                        var blockSize = blockSizeInKB * 1024;
                        var blocks = [];
                        var offset = 0;
                        var index = 0;
                        var blockList = "";

                        while (offset < fileSize) {
                            var start = offset;
                            var end = Math.min(offset + blockSize, fileSize);
                            var blockId = blockIdPrefix + pad(index, 6);
                            blockId = btoa(blockId);

                            blocks.push({
                                name: _fileName,
                                blockId: blockId,
                                index: index,
                                start: start,
                                end: end
                            });

                            blockList += '<Latest>' + blockId + '</Latest>';
                            offset = end;
                            index++;
                        }

                        // define the function array and push all chunk upload operation into this array
                        var putBlocks = [];
                        var uploadedChunks = 0;
                        blocks.forEach(function (block) {
                            putBlocks.push(function (callback) {
                                //debugger;
                                // load blob based on the start and end index for each chunks
                                var blob = file.slice(block.start, block.end);
                                var uri = submitUri + '&comp=block&blockid=' + block.blockId;

                                $.ajax({
                                    url: uri,
                                    type: "PUT",
                                    data: blob,
                                    processData: false,
                                    beforeSend: function (xhr) {
                                        //debugger;
                                        xhr.setRequestHeader('x-ms-blob-type', 'BlockBlob');
                                        xhr.setRequestHeader('Content-Type', file.type);
                                    },
                                    success: function (data, status) {
                                        //debugger;
                                        if (status == "success") {
                                            // upload files in chunks
                                            if ((uploadedChunks++) === putBlocks.length - 1)
                                                callback(null, block.index);
                                        }
                                        else {
                                            console.log(data);
                                            removeDisable();
                                            removeJsonDisable();
                                        }
                                    },
                                    error: function (xhr, desc, err) {
                                        $('.cprocess').removeClass('progressdis');
                                        $('.cfailure').addClass('progressdis');
                                        $('.cfailure').html("Unable to connect server. Please check your internet connection!!!");
                                        removeDisable();
                                        removeJsonDisable();
                                    }
                                });
                            });
                        });
                        var commitCallback = function (error, result) {
                            var params = location.search.split('?')[1];
                            var uri = submitUri + '&comp=blocklist';
                            var requestBody = '<?xml version="1.0" encoding="utf-8"?><BlockList>';
                            requestBody += blockList
                            requestBody += '</BlockList>';

                            $.ajax({
                                url: uri,
                                type: "PUT",
                                data: requestBody,
                                beforeSend: function (xhr) {
                                    //debugger;
                                    xhr.setRequestHeader('x-ms-blob-content-type', file.type);
                                },
                                success: function (data, status) {
                                    debugger;
                                    // For Progress baar
                                    var percentVal = '100%';
                                    bar.width(percentVal);
                                    percent.html(percentVal);
                                    removeDisable();
                                    removeJsonDisable();

                                },
                                error: function (xhr, desc, err) {
                                    console.log(result.message);
                                    //TODO
                                    removeDisable();
                                    removeJsonDisable();
                                }
                            });
                        }
                        $('#progressc').addClass('progressdis');
                        putBlocks.forEach(function (putBlock, index) {
                            putBlock.call(this, commitCallback);
                            // For Progress baar
                            var percentVal = (index * 100 / putBlocks.length).toFixed(0) + '%';
                            bar.width(percentVal);
                            percent.html(percentVal);
                        });
                    }
                    else {
                        //TODO
                        console.log(result.message);
                        removeDisable();
                        removeJsonDisable();
                    }
                });

            });








            var getUrlParameter = function getUrlParameter(sParam) {
                var sPageURL = window.location.search.substring(1),
                    sURLVariables = sPageURL.split('&'),
                    sParameterName,
                    i;

                for (i = 0; i < sURLVariables.length; i++) {
                    sParameterName = sURLVariables[i].split('=');

                    if (sParameterName[0] === sParam) {
                        return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                    }
                }
            }


            function addDisable() {
                $('#DocType').attr("disabled", true);
                $('#filename').attr("disabled", true);
                $('#btnUpload').attr("disabled", true);
                $('#btnsave').attr("disabled", true);
            }
            function addJsonDisable() {
                $('#DocTypeC').attr("disabled", true);
                $('#filenamec').attr("disabled", true);
                $('#btnJsonUpload').attr("disabled", true);


            }

            function removeDisable() {
                //$('#DocType').removeAttr("disabled");
                $('#filename').val('');
                $('#filename').removeAttr("disabled");
                $('#btnUpload').removeAttr("disabled");

                $('#btnsave').removeAttr("disabled");
            }
            function removeJsonDisable() {
                $('#DocTypeC').removeAttr("disabled");
                $('#filenamec').removeAttr("disabled");
                $('#filenamec').val('');
                $('#btnJsonUpload').removeAttr("disabled");


            }
            function pad(number, length) {
                var str = '' + number;
                while (str.length < length) {
                    str = '0' + str;
                }



                return str;
            }
           
        });

    </script>
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @Scripts.Render("~/bundles/jqueryui")
end section

