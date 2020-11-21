$scope.filesign = function (gstRegID, returnPeriodID, signature) {
    $scope.result = ''; $scope.status = 'postedc';

    $.post('/' + $scope.modelUrl + '/ParamsOutput' + location.search, {
        key: 'filesign',
        Params: JSON.stringify({ GstRegID: gstRegID, ReturnPeriodID: returnPeriodID, sign: signature }),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function (result) {
        $scope.status = 'loaded';
        if (result.success) {
            $scope.status = '';
            $scope.result = 'success';
        } else {
            if (result.message === '') {
                result.message = 'Unknown Error!';
            };

            //alert(result.message);
            $scope.status = '';
            $scope.result = 'failure';
            $scope.message = result.message;
            //$scope.message = "Unable to file gst someting went wrong. please try again.";
        }

        $scope.$apply();

        return;
    });
};

$scope.getsign = function () {
    //debugger;
    $scope.result = ''; $scope.status = 'postedc';

    if ($scope.frm.$invalid) {
        return;
    }

    var gstRegID = $scope.GstRegID;
    var returnPeriodID = $scope.ReturnPeriodID;
    var modelURL = '/' + $scope.modelUrl + '/ParamsOutput' + location.search;

    $.post(modelURL, {
        key: 'signdata',
        Params: JSON.stringify({ GstRegID: gstRegID, ReturnPeriodID: returnPeriodID }),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    }, function (result) {
        $scope.status = 'loaded';

        if (result.success) {
            $scope.status = '';
            $scope.result = 'success';

            if (result.Data) {
                var connPorts = [1585, 2095, 2568, 2868, 4587];
                var connPortIndex = 0;
                var tryConnect = function (connPort) {
                    //debugger;
                    console.log(connPort);
                    if (connPort) {
                        var connection = new WebSocket('wss://127.0.0.1:' + connPort);

                        connection.onopen = function (e) {
                            //alert('Connect on port "' + connPort + '": ' + e);
                            console.log('Connect on port "' + connPort + '": ', e);

                            connection.onerror = function (error) {
                                //alert('Error "' + '": ' + error);
                                console.log('Error "' + '": ', error);
                            };

                            connection.onmessage = function (e) {
                                //debugger
                                //alert('Message recieved "' + '": ' + e);
                                console.log('Message recieved "' + '": ' + e);

                                var respData = e.data;
                                var siglast = respData.indexOf("SerialNo");
                                if (siglast >= 0) {
                                    var signature = respData.substring(10, siglast);
                                    $scope.filesign(gstRegID, returnPeriodID, signature.trim());
                                }
                            };

                            //debugger
                            var data = JSON.parse(result.Data.Description);
                            connection.send("action=sign" +
                                "\ntobesigned=" + data.DataHashKey +
                                "\npanNo=" + data.AuthPanNumber +
                                "\nsigntype=1" +
                                "\nexpirycheck=true" +
                                "\nissuername=" +
                                "\ncertclass=2|3" +
                                "\ncerttype=DSC" +
                                "\ncertdetails="
                            );
                        };

                        connection.onerror = function (error) {
                            //debugger;
                            //alert('Unable to connect on port "' + connPort + '": ' + error);
                            console.log('Unable to connect on port "' + connPort + '": ', error);

                            tryConnect(connPorts[++connPortIndex]);
                        };
                    } else {
                        //alert('All ports checked, Unable to connect');
                        console.log('All ports checked, Unable to connect');
                    }
                };

                tryConnect(connPorts[connPortIndex]);
            }
        } else {
            if (result.message === '') {
                result.message = 'Unknown Error!';
            };

            //alert(result.message);
            $scope.status = '';
            $scope.result = 'failure';
            $scope.message = "Unable to file gst someting is wrong. please try again.";
        }

        $scope.$apply();
        return;
    });
};

$scope.checkStatusInterval = function (apiTaskID, msg) {
    var stop = $interval(function () {
        var url = '/' + $scope.modelUrl + '/ParamsOutput' + location.search;
        var datainfo = { ApiTaskID: apiTaskID, GstRegID: $scope.GstRegID };
        datainfo = JSON.stringify(datainfo);
        var payloadDataInfo = { key: 'payloadStatus', Params: datainfo, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() };

        $http({
            method: 'POST',
            url: url,
            data: payloadDataInfo
        }).then(function successCallback(result) {
            if (result.data.Success) {
                if (result.data.DownloadUrl) {
                    msg.find("div#syncExecutedMsg").html('<i class="fa fa-2x fa-check" style="color:green"> ' + result.data.Message + '</i> <br>  <a href=' + result.data.DownloadUrl + ' style="font-size:15px"> Download File</a>');
                   
                   
                    $scope.stopFight(stop);
                    //$scope.downloadURL = result.DownloadUrl;
                    //$scope.showdownloadlink = 'downloadlink';
                } else {
                    msg.find("div#syncExecutedMsg").html('<i class="fa fa-2x fa-check" style="color:green"> ' + result.data.Message + '</i>');
                    $scope.stopFight(stop);
                    //$scope.message = result.Message;
                    //$scope.result = 'success';
                }
            }
            else {
                msg.find("div#syncExecutedMsg").html('<i class="fa fa-2x fa-check" style="color:green"> ' + result.data.Message + '</i>');
                //$scope.result = 'success';
                //$scope.message = result.Message;
            }
        })
    }, 20000);
    return;
}

$scope.stopFight = function (stop) {
    $interval.cancel(stop);
    $scope.IsInitializing = false;
};

$scope.$on('$destroy', function () {
    // Make sure that the interval is destroyed too
    $scope.stopFight();
});

$scope.getCashITCDetails = function () {
    $scope.result = '';
    $scope.status = 'postedc';

    var url = '/' + $scope.modelUrl + '/ParamsOutput' + location.search;
    var datainfo = { ReturnPeriodID: $scope.ReturnPeriodID, GstRegID: ($scope.filteredPaymentDetail && $scope.filteredPaymentDetail.length > 0 && $scope.filteredPaymentDetail[0].GSTRegId ? $scope.filteredPaymentDetail[0].GSTRegId : $scope.GstRegID) };
    datainfo = JSON.stringify(datainfo);
    var payloadDataInfo = { key: 'cashdetail', Params: datainfo, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() };

    $http({
        method: 'POST',
        url: url,
        data: payloadDataInfo
    }).then(function successCallback(result) {
        if (result.success) {
            //debugger;
            if (result.Data.Data) {
                var data = JSON.parse(result.Data.CalcList.trans[0].ResponsePayload);
                $scope.cashledgerDetails = data;
            }
            else {
                $scope.cashledgerDetails = [];
            }

            $('#ledgerDetails').modal('show');
            $('#loading').hide();
        }
        else {
            $('#loading').hide();
            $scope.result = 'success';
            $scope.message = result.Message;
        }
    })
}

$scope.getCashLedgerDetails = function () {
    $scope.result = '';
    $scope.status = 'postedc';

    var url = '/' + $scope.modelUrl + '/ParamsOutput' + location.search;
    var datainfo = { ReturnPeriodID: $scope.ReturnPeriodID, GstRegID: ($scope.filteredPaymentDetail && $scope.filteredPaymentDetail.length > 0 && $scope.filteredPaymentDetail[0].GSTRegId ? $scope.filteredPaymentDetail[0].GSTRegId : $scope.GstRegID) };
    datainfo = JSON.stringify(datainfo);
    var payloadDataInfo = { key: 'itcdetail', Params: datainfo, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() };

    $http({
        method: 'POST',
        url: url,
        data: payloadDataInfo
    }).then(function successCallback(result) {
        if (result.success) {
            //debugger;
            if (result.Data.Data) {
                var data = JSON.parse(result.Data.CalcList.trans[0].ResponsePayload);
                $scope.ledgerBalance = data;
            }
            else
                $scope.ledgerBalance = [];

            $('#ItcLedgerBalance').modal('show');
            $('#loading').hide();
        }
        else {
            $('#loading').hide();
            $scope.result = 'success';
            $scope.message = result.Message;
        }
    })
}

$scope.getLiabilityBalance = function () {
    $scope.result = '';
    $scope.status = 'postedc';

    var url = '/' + $scope.modelUrl + '/ParamsOutput' + location.search;
    var datainfo = { ReturnPeriodID: $scope.ReturnPeriodID, GstRegID: ($scope.filteredPaymentDetail && $scope.filteredPaymentDetail.length > 0 && $scope.filteredPaymentDetail[0].GSTRegId ? $scope.filteredPaymentDetail[0].GSTRegId : $scope.GstRegID) };
    datainfo = JSON.stringify(datainfo);
    var payloadDataInfo = { key: 'libdetails', Params: datainfo, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() };

    $http({
        method: 'POST',
        url: url,
        data: payloadDataInfo
    }).then(function successCallback(result) {
        $scope.opliability = [];
        if (result.success) {
            if (result.Data) {
                if (typeof (result.Data) === "string") {
                    result.Data = JSON.parse(result.Data);
                }

                if (result.Data.CalcList && result.Data.CalcList.trans && result.Data.CalcList.trans.length > 0 && result.Data.CalcList.trans[0].ResponsePayload) {
                    $scope.opliability = JSON.parse(result.Data.CalcList.trans[0].ResponsePayload);

                    $('#libBalance').modal('show');
                    $('#loading').hide();
                }
            }
        }
        else {
            $('#loading').hide();
            $scope.result = 'success';
            $scope.message = result.Message;
        }
    })
}

$scope.autoOffset = function () {
    if ($scope.cashITCDetails) {
        var i_pdi = 0;
        var i_pdc = 0;
        var i_pds = 0;
        var c_pdi = 0;
        var c_pdc = 0;
        var s_pdi = 0;
        var s_pds = 0;
        var cs_pdcs = 0;

        if ($scope.Datacn && $scope.Datacn.tx_pmt && $scope.Datacn.tx_pmt.tx_py && $scope.Datacn.tx_pmt.tx_py.length > 0) {
            var tx_py__trans_typ__30002 = null;
            $scope.Datacn.tx_pmt.tx_py.forEach(function (tx_py) {
                if (tx_py.trans_typ == 30002) {
                    tx_py__trans_typ__30002 = tx_py;
                }
            });

            if (tx_py__trans_typ__30002) {
                var tx_py__trans_typ__30002__igst = 0;
                var tx_py__trans_typ__30002__cgst = 0;
                var tx_py__trans_typ__30002__sgst = 0;
                var tx_py__trans_typ__30002__cess = 0;

                if (tx_py__trans_typ__30002.igst) {
                    tx_py__trans_typ__30002__igst = (tx_py__trans_typ__30002.igst.tx || 0) + (tx_py__trans_typ__30002.igst.intr || 0) + (tx_py__trans_typ__30002.igst.fee || 0);
                }

                if (tx_py__trans_typ__30002.cgst) {
                    tx_py__trans_typ__30002__cgst = (tx_py__trans_typ__30002.cgst.tx || 0) + (tx_py__trans_typ__30002.cgst.intr || 0) + (tx_py__trans_typ__30002.cgst.fee || 0);
                }

                if (tx_py__trans_typ__30002.sgst) {
                    tx_py__trans_typ__30002__sgst = (tx_py__trans_typ__30002.sgst.tx || 0) + (tx_py__trans_typ__30002.sgst.intr || 0) + (tx_py__trans_typ__30002.sgst.fee || 0);
                }

                if (tx_py__trans_typ__30002.cess) {
                    tx_py__trans_typ__30002__cess = (tx_py__trans_typ__30002.cess.tx || 0) + (tx_py__trans_typ__30002.cess.intr || 0) + (tx_py__trans_typ__30002.cess.fee || 0);
                }



                var igst_bal = 0;
                var cgst_bal = 0;
                var sgst_bal = 0;
                var cess_bal = 0;

                if ($scope.cashITCDetails.itc_bal) {
                    igst_bal = $scope.cashITCDetails.itc_bal.igst_bal || 0;
                    cgst_bal = $scope.cashITCDetails.itc_bal.cgst_bal || 0;
                    sgst_bal = $scope.cashITCDetails.itc_bal.sgst_bal || 0;
                    cess_bal = $scope.cashITCDetails.itc_bal.cess_bal || 0;
                }

                var section4aData = null;
                $scope.Datac.forEach(function (data) {
                    if (data.section == "4A") {
                        section4aData = data;
                    }
                });
                if (section4aData) {
                    igst_bal += section4aData.iamt || 0;
                    cgst_bal += section4aData.camt || 0;
                    sgst_bal += section4aData.samt || 0;
                    cess_bal += section4aData.csamt || 0;
                }



                //(a) IGST ITC shall first be set-off against IGST liability, then CGST liability and if any balance remaining against SGST / UGST liability.
                if (igst_bal > 0) {
                    if (tx_py__trans_typ__30002__igst > igst_bal) {
                        i_pdi += igst_bal;
                        tx_py__trans_typ__30002__igst -= igst_bal;
                        igst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__igst;
                        tx_py__trans_typ__30002__igst = 0;
                        igst_bal -= tx_py__trans_typ__30002__igst;
                    }
                }
                if (igst_bal > 0) {
                    if (tx_py__trans_typ__30002__cgst > igst_bal) {
                        i_pdi += igst_bal;
                        tx_py__trans_typ__30002__cgst -= igst_bal;
                        igst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__cgst;
                        tx_py__trans_typ__30002__cgst = 0;
                        igst_bal -= tx_py__trans_typ__30002__cgst;
                    }
                }
                if (igst_bal > 0) {
                    if (tx_py__trans_typ__30002__sgst > igst_bal) {
                        i_pdi += igst_bal;
                        tx_py__trans_typ__30002__sgst -= igst_bal;
                        igst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__sgst;
                        tx_py__trans_typ__30002__sgst = 0;
                        igst_bal -= tx_py__trans_typ__30002__sgst;
                    }
                }

                //(b) CGST ITC shall first be set-off against CGST liability and then IGST liability (if any)
                if (cgst_bal > 0) {
                    if (tx_py__trans_typ__30002__cgst > cgst_bal) {
                        i_pdi += cgst_bal;
                        tx_py__trans_typ__30002__cgst -= cgst_bal;
                        cgst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__cgst;
                        tx_py__trans_typ__30002__cgst = 0;
                        cgst_bal -= tx_py__trans_typ__30002__cgst;
                    }
                }
                if (cgst_bal > 0) {
                    if (tx_py__trans_typ__30002__igst > cgst_bal) {
                        i_pdi += cgst_bal;
                        tx_py__trans_typ__30002__igst -= cgst_bal;
                        cgst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__igst;
                        tx_py__trans_typ__30002__igst = 0;
                        cgst_bal -= tx_py__trans_typ__30002__igst;
                    }
                }

                //(c) SGST ITC shall first be set-off against SGST liability and then IGST liability (if any). However, SGST can be set-off against IGST only when Balance in CGST is NIL i.e. for payment of IGST exhaust CGST first then SGST
                //(d) UGST ITC shall first be set-off against UGST liability and then IGST liability (if any). However, UGST can be set-off against IGST only when Balance in CGST is NIL i.e. for payment of IGST exhaust CGST first then UGST
                if (sgst_bal > 0) {
                    if (tx_py__trans_typ__30002__sgst > sgst_bal) {
                        i_pdi += sgst_bal;
                        tx_py__trans_typ__30002__sgst -= sgst_bal;
                        sgst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__sgst;
                        tx_py__trans_typ__30002__sgst = 0;
                        sgst_bal -= tx_py__trans_typ__30002__sgst;
                    }
                }
                if (sgst_bal > 0) {
                    if (tx_py__trans_typ__30002__igst > sgst_bal) {
                        i_pdi += sgst_bal;
                        tx_py__trans_typ__30002__igst -= sgst_bal;
                        sgst_bal = 0;
                    } else {
                        i_pdi += tx_py__trans_typ__30002__igst;
                        tx_py__trans_typ__30002__igst = 0;
                        sgst_bal -= tx_py__trans_typ__30002__igst;
                    }
                }

                //(e) Set-off of CGST against SGST / UGST NOT ALLOWED & vice a versa.
            }
        }


        $scope.final_pditc.i_pdi = i_pdi;
        $scope.final_pditc.i_pdc = i_pdc;
        $scope.final_pditc.i_pds = i_pds;
        $scope.final_pditc.c_pdi = c_pdi;
        $scope.final_pditc.c_pdc = c_pdc;
        $scope.final_pditc.s_pdi = s_pdi;
        $scope.final_pditc.s_pds = s_pds;
        $scope.final_pditc.cs_pdcs = cs_pdcs;
    } else {
        $(`
            <div title="Invalid operation">
                <p>Please get the Cash ITC Balance, before auto-offsetting.</p>
            </div>
        `).dialog({
                modal: true,
                width: 400
            }).dialog("open");
    }
}

$scope.donwloadTaskFile = function (fileId, fltype) {
    //debugger;
    $("div.spinnerTarget").addClass("backdrop");
    $("body").css("overflow", "hidden");
    //usSpinnerService.spin('spinner-1');

    $http({
        method: 'GET',
        url: "/frmGstImportVouch/Downloads/" + fileId + "/" + fltype + location.search
    }).then(function successCallback(result) {
        if (result.status == 200) {
            window.location.href = result.data.data;
        }
    }).finally(function () {
        $("div.spinnerTarget").removeClass("backdrop");
        $("body").css("overflow", "auto");
        usSpinnerService.stop('spinner-1');
    });
}

$(document).on("click", "#removeSyncDiv", function (e) {
    e.preventDefault();
    $(this).closest('div#syncMessagePanel').hide();
    return;
});


//https://stackoverflow.com/questions/12190166/angularjs-any-way-for-http-post-to-send-request-parameters-instead-of-json