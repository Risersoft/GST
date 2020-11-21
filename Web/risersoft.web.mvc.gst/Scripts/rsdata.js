/*
$scope.Datacn = {
    "gstin": "29HJKPS9689A8Z4",
    "ret_period": "072016",
    "sup_details": {
        "osup_det": {
            "txval": 250,
            "iamt": 100,
            "camt": 200,
            "samt": 300,
            "csamt": 400
        },
        "osup_zero": {
            "txval": 250,
            "iamt": 100,
            "csamt": 400
        },
        "osup_nil_exmp": {
            "txval": 250
        },
        "isup_rev": {
            "txval": 250,
            "iamt": 100,
            "camt": 200,
            "samt": 300,
            "csamt": 400
        },
        "osup_nongst": {
            "txval": 250
        }
    },
    "inter_sup": {
        "unreg_details": [
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            },
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            },
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            }
        ],
        "comp_details": [
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            },
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            },
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            }
        ],
        "uin_details": [
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            },
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            },
            {
                "pos": "07",
                "txval": 250,
                "iamt": 200
            }
        ]
    },
    "itc_elg": {
        "itc_avl": [
            {
                "ty": "IMPG",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            },
            {
                "ty": "IMPS",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            },
            {
                "ty": "ISRC",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            },
            {
                "ty": "ISD",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            },
            {
                "ty": "OTH",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            }
        ],
        "itc_rev": [
            {
                "ty": "RUL",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            },
            {
                "ty": "OTH",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            }
        ],
        "itc_net": {
            "iamt": 136.53,
            "camt": 274,
            "samt": 162.99,
            "csamt": 103
        },
        " itc_inelg": [
            {
                "ty": "RUL",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            },
            {
                "ty": "OTH",
                "iamt": 136.53,
                "camt": 274,
                "samt": 162.99,
                "csamt": 103
            }
        ]
    },
    "inward_sup": {
        "isup_details": [
            {
                "ty": "GST",
                "inter": 100,
                "intra": 200
            },
            {
                "ty": "NONGST",
                "inter": 300,
                "intra": 400
            }
        ]
    },
    "tx_pmt": {
        "tx_py": [
            {
                "trans_typ": 30002,
                "trans_desc": "Other than reverse charge",
                "liab_ldg_id": 11191,
                "sgst": {
                    "intr": 100,
                    "tx": 100,
                    "fee": 100
                },
                "cgst": {
                    "intr": 100,
                    "tx": 100,
                    "fee": 100
                },
                "cess": {
                    "intr": 100,
                    "tx": 100
                },
                "igst": {
                    "intr": 100,
                    "tx": 100
                }
            },
            {
                "trans_typ": 30003,
                "trans_desc": "Reverse charge",
                "liab_ldg_id": 11191,
                "sgst": {
                    "intr": 100,
                    "tx": 100,
                    "fee": 100
                },
                "cgst": {
                    "intr": 100,
                    "tx": 100,
                    "fee": 100
                },
                "cess": {
                    "intr": 100,
                    "tx": 100
                },
                "igst": {
                    "intr": 100,
                    "tx": 100
                }
            }
        ],
        "pdcash": [
            {
                "liab_ldg_id": 1233,
                "trans_typ": 30002,
                "ipd": 3517817,
                "cpd": 3517817,
                "spd": 3517817,
                "cspd": 3517817,
                "i_intrpd": 20,
                "c_intrpd": 30,
                "s_intrpd": 10,
                "cs_intrpd": 15,
                "c_lfeepd": 13,
                "s_lfeepd": 13
            },
            {
                "liab_ldg_id": 1233,
                "trans_typ": 30002,
                "ipd": 3517817,
                "cpd": 3517817,
                "spd": 3517817,
                "cspd": 3517817,
                "i_intrpd": 20,
                "c_intrpd": 30,
                "s_intrpd": 10,
                "cs_intrpd": 15,
                "c_lfeepd": 13,
                "s_lfeepd": 13
            }
        ],
        "pditc": {
            "liab_ldg_id": 12321,
            "trans_typ": 30002,
            "i_pdi": 3517817,
            "i_pdc": 2290459,
            "i_pds": 2290459,
            "c_pdi": 3517817,
            "c_pdc": 2290459,
            "s_pdi": 3517817,
            "s_pds": 2290459,
            "cs_pdcs": 2290459
        }
    },
    "intr_ltfee": {
        "intr_details": {
            "iamt": 10,
            "camt": 20,
            "samt": 30,
            "csamt": 40
        },
        "ltfee_details": {
            "camt": 50,
            "samt": 60
        }
    }
};

$scope.liabilityLedger = {
    "gstin": "06AAAAA0000A1Z6",
    "ret_period": "032017",
    "tr": [
        {
            "dt": "09/03/2017",
            "ref_no": "AM051216747183N",
            "tot_rng_bal": 48,
            "tot_tr_amt": 48,
            "tr_typ": "Dr",
            "desc": "Other than reverse charge",
            "dschrg_typ": "",
            "igst": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "sgst": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "cgst": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "cess": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "igstbal": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "sgstbal": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "cgstbal": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            },
            "cessbal": {
                "intr": 2,
                "oth": 2,
                "tx": 2,
                "fee": 2,
                "pen": 2,
                "tot": 12
            }
        },
        {
            "dt": "10/03/2017",
            "ref_no": "DI0506170000004",
            "tot_rng_bal": 24,
            "tot_tr_amt": 24,
            "tr_typ": "Cr",
            "desc": "Other than reverse charge",
            "dschrg_typ": "credit",
            "igst": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "sgst": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "cgst": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "cess": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "igstbal": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "sgstbal": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "cgstbal": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            },
            "cessbal": {
                "intr": 1,
                "oth": 1,
                "tx": 1,
                "fee": 1,
                "pen": 1,
                "tot": 6
            }
        }
    ],
    "cl_bal": {
        "tot_rng_bal": 24,
        "desc": "Closing Balance",
        "igstbal": {
            "intr": 1,
            "oth": 1,
            "tx": 1,
            "fee": 1,
            "pen": 1,
            "tot": 6
        },
        "sgstbal": {
            "intr": 1,
            "oth": 1,
            "tx": 1,
            "fee": 1,
            "pen": 1,
            "tot": 6
        },
        "cgstbal": {
            "intr": 1,
            "oth": 1,
            "tx": 1,
            "fee": 1,
            "pen": 1,
            "tot": 6
        },
        "cessbal": {
            "intr": 1,
            "oth": 1,
            "tx": 1,
            "fee": 1,
            "pen": 1,
            "tot": 6
        }
    }
};

$scope.opliability = {
  "gstin": "06AAAAA0000A1Z6",
  "ret_period": "032017",
  "tr": [
    {
      "dt": "09/03/2017",
      "ref_no": "AM051216747183N",
      "tot_rng_bal": 48,
      "tot_tr_amt": 48,
      "tr_typ": "Dr",
      "desc": "Other than reverse charge",
      "dschrg_typ": "",
      "igst": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "sgst": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "cgst": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "cess": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "igstbal": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "sgstbal": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "cgstbal": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      },
      "cessbal": {
        "intr": 2,
        "oth": 2,
        "tx": 2,
        "fee": 2,
        "pen": 2,
        "tot": 12
      }
    },
    {
      "dt": "10/03/2017",
      "ref_no": "DI0506170000004",
      "tot_rng_bal": 24,
      "tot_tr_amt": 24,
      "tr_typ": "Cr",
      "desc": "Other than reverse charge",
      "dschrg_typ": "credit",
      "igst": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "sgst": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "cgst": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "cess": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "igstbal": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "sgstbal": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "cgstbal": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      },
      "cessbal": {
        "intr": 1,
        "oth": 1,
        "tx": 1,
        "fee": 1,
        "pen": 1,
        "tot": 6
      }
    }
  ],
  "cl_bal": {
    "tot_rng_bal": 24,
    "desc": "Closing Balance",
    "igstbal": {
      "intr": 1,
      "oth": 1,
      "tx": 1,
      "fee": 1,
      "pen": 1,
      "tot": 6
    },
    "sgstbal": {
      "intr": 1,
      "oth": 1,
      "tx": 1,
      "fee": 1,
      "pen": 1,
      "tot": 6
    },
    "cgstbal": {
      "intr": 1,
      "oth": 1,
      "tx": 1,
      "fee": 1,
      "pen": 1,
      "tot": 6
    },
    "cessbal": {
      "intr": 1,
      "oth": 1,
      "tx": 1,
      "fee": 1,
      "pen": 1,
      "tot": 6
    }
  }
};

$scope.cashledgerbalance = {
    "gstin": "05AVHPB7348G1ZP",
    "cash_bal": {
        "igst_tot_bal": 50000,
        "igst": {
            "tx": 10000,
            "intr": 10000,
            "pen": 10000,
            "fee": 10000,
            "oth": 10000
        },
        "cgst_tot_bal": 50000,
        "cgst": {
            "tx": 10000,
            "intr": 10000,
            "pen": 10000,
            "fee": 10000,
            "oth": 10000
        },
        "sgst_tot_bal": 50000,
        "sgst": {
            "tx": 10000,
            "intr": 10000,
            "pen": 10000,
            "fee": 10000,
            "oth": 10000
        },
        "cess_tot_bal": 50000,
        "cess": {
            "tx": 10000,
            "intr": 10000,
            "pen": 10000,
            "fee": 10000,
            "oth": 10000
        }
    },
    "itc_bal": {
        "cgst_bal": 10000,
        "sgst_bal": 20000,
        "igst_bal": 15000,
        "cess_bal": 5000
    }
};

$scope.cashledgerDetails = {
    "gstin": "05AVHPB7348G1ZP",
    "fr_dt": "30/05/2017",
    "to_dt": "01/06/2017",
    "op_bal": {
        "desc": "Opening Balance",
        "igstbal": {
            "tx": 9032,
            "intr": 1306,
            "pen": 1217,
            "fee": 1126,
            "oth": 1044,
            "tot": 13725
        },
        "cgstbal": {
            "tx": 78,
            "intr": 4018,
            "pen": 2929,
            "fee": 1835,
            "oth": 4954,
            "tot": 13814
        },
        "sgstbal": {
            "tx": 2045,
            "intr": 1027,
            "pen": 940,
            "fee": 847,
            "oth": 859,
            "tot": 5718
        },
        "cessbal": {
            "tx": 48,
            "intr": 938,
            "pen": 850,
            "fee": 756,
            "oth": 769,
            "tot": 3361
        },
        "tot_rng_bal": 36618
    },
    "cl_bal": {
        "desc": "Closing Balance",
        "igstbal": {
            "tx": 9136,
            "intr": 1306,
            "pen": 1217,
            "fee": 1126,
            "oth": 1044,
            "tot": 13829
        },
        "cgstbal": {
            "tx": 79,
            "intr": 4118,
            "pen": 3029,
            "fee": 1935,
            "oth": 4954,
            "tot": 14115
        },
        "sgstbal": {
            "tx": 2142,
            "intr": 1027,
            "pen": 940,
            "fee": 847,
            "oth": 859,
            "tot": 5815
        },
        "cessbal": {
            "tx": 48,
            "intr": 938,
            "pen": 850,
            "fee": 756,
            "oth": 769,
            "tot": 3361
        },
        "tot_rng_bal": 37120
    },
    "tr": [
        {
            "dpt_dt": "30/05/2017",
            "rpt_dt": "30/05/2017",
            "dpt_time": "11:20:10",
            "desc": "Other than reverse charge",
            "igst": {
                "tx": 110,
                "intr": 140,
                "pen": 130,
                "fee": 120,
                "oth": 150,
                "tot": 650
            },
            "cgst": {
                "tx": 10,
                "intr": 40,
                "pen": 30,
                "fee": 20,
                "oth": 50,
                "tot": 150
            },
            "sgst": {
                "tx": 60,
                "intr": 90,
                "pen": 80,
                "fee": 70,
                "oth": 100,
                "tot": 400
            },
            "cess": {
                "tx": 160,
                "intr": 190,
                "pen": 180,
                "fee": 170,
                "oth": 200,
                "tot": 900
            },
            "igstbal": {
                "tx": 110,
                "intr": 140,
                "pen": 130,
                "fee": 120,
                "oth": 150,
                "tot": 0
            },
            "cgstbal": {
                "tx": 10,
                "intr": 40,
                "pen": 30,
                "fee": 20,
                "oth": 50,
                "tot": 0
            },
            "sgstbal": {
                "tx": 60,
                "intr": 90,
                "pen": 80,
                "fee": 70,
                "oth": 100,
                "tot": 0
            },
            "cessbal": {
                "tx": 160,
                "intr": 190,
                "pen": 180,
                "fee": 170,
                "oth": 200,
                "tot": 0
            },
            "tr_typ": "Cr",
            "tot_tr_amt": 2100,
            "tot_rng_bal": 2100,
            "ret_period": "122016",
            "refNo": "AA010123456789A"
        },
        {
            "dpt_dt": "01/06/2017",
            "desc": "Other than reverse charge",
            "igst": {
                "tx": 1,
                "intr": 0,
                "pen": 0,
                "fee": 0,
                "oth": 0,
                "tot": 1
            },
            "cgst": {
                "tx": 0,
                "intr": 0,
                "pen": 0,
                "fee": 0,
                "oth": 0,
                "tot": 0
            },
            "sgst": {
                "tx": 0,
                "intr": 0,
                "pen": 0,
                "fee": 0,
                "oth": 0,
                "tot": 0
            },
            "cess": {
                "tx": 0,
                "intr": 0,
                "pen": 0,
                "fee": 0,
                "oth": 0,
                "tot": 0
            },
            "igstbal": {
                "tx": 9137,
                "intr": 1306,
                "pen": 1217,
                "fee": 1126,
                "oth": 1044,
                "tot": 0
            },
            "cgstbal": {
                "tx": 80,
                "intr": 4218,
                "pen": 3129,
                "fee": 2035,
                "oth": 4954,
                "tot": 0
            },
            "sgstbal": {
                "tx": 2238,
                "intr": 1127,
                "pen": 1040,
                "fee": 947,
                "oth": 859,
                "tot": 0
            },
            "cessbal": {
                "tx": 120,
                "intr": 1038,
                "pen": 950,
                "fee": 856,
                "oth": 769,
                "tot": 0
            },
            "tr_typ": "Dr",
            "tot_tr_amt": 1,
            "tot_rng_bal": 38190,
            "ret_period": "122016",
            "refNo": "DC0506170000004"
        },
        {
            "dpt_dt": "01/06/2017",
            "desc": "Other than reverse charge",
            "igst": {
                "tx": 1,
                "intr": 0,
                "pen": 0,
                "fee": 0,
                "oth": 0,
                "tot": 1
            },
            "cgst": {
                "tx": 1,
                "intr": 100,
                "pen": 100,
                "fee": 100,
                "oth": 0,
                "tot": 301
            },
            "sgst": {
                "tx": 96,
                "intr": 100,
                "pen": 100,
                "fee": 100,
                "oth": 0,
                "tot": 396
            },
            "cess": {
                "tx": 72,
                "intr": 100,
                "pen": 100,
                "fee": 100,
                "oth": 0,
                "tot": 372
            },
            "igstbal": {
                "tx": 9136,
                "intr": 1306,
                "pen": 1217,
                "fee": 1126,
                "oth": 1044,
                "tot": 0
            },
            "cgstbal": {
                "tx": 79,
                "intr": 4118,
                "pen": 3029,
                "fee": 1935,
                "oth": 4954,
                "tot": 0
            },
            "sgstbal": {
                "tx": 2142,
                "intr": 1027,
                "pen": 940,
                "fee": 847,
                "oth": 859,
                "tot": 0
            },
            "cessbal": {
                "tx": 48,
                "intr": 938,
                "pen": 850,
                "fee": 756,
                "oth": 769,
                "tot": 0
            },
            "tr_typ": "Dr",
            "tot_tr_amt": 1070,
            "tot_rng_bal": 37120,
            "ret_period": "122016",
            "refNo": "DC0506170000007"
        }
    ]
};

$scope.ledgerBalance = {
    "itcLdgDtls": {
        "gstin": "05AVHPB7348G1ZP",
        "fr_dt": "31/05/2017",
        "to_dt": "01/06/2017",
        "op_bal": {
            "desc": "Opening Balance",
            "igstTaxBal": 395,
            "cgstTaxBal": 50,
            "sgstTaxBal": 497,
            "cessTaxBal": 4997,
            "tot_rng_bal": 5939
        },
        "tr": [
            {
                "dt": "31/05/2017",
                "desc": "Transitional  Cenvat Credit/ VAT credit ",
                "ref_no": "AA0520-1000008D",
                "ret_period": "122016",
                "sgstTaxAmt": 100,
                "cgstTaxAmt": 100,
                "igstTaxAmt": 500,
                "cessTaxAmt": 0,
                "igstTaxBal": 98,
                "cgstTaxBal": 99,
                "sgstTaxBal": 0,
                "cessTaxBal": 4990,
                "tot_rng_bal": 5187,
                "tot_tr_amt": 700,
                "tr_typ": "Cr"
            },
            {
                "dt": "01/06/2017",
                "desc": "Other than reverse charge",
                "ref_no": "DI0506170000005",
                "ret_period": "122016",
                "sgstTaxAmt": 1,
                "cgstTaxAmt": 0,
                "igstTaxAmt": 0,
                "cessTaxAmt": 0,
                "igstTaxBal": 94,
                "cgstTaxBal": 0,
                "sgstTaxBal": 96,
                "cessTaxBal": 4983,
                "tot_rng_bal": 5173,
                "tot_tr_amt": 1,
                "tr_typ": "Dr"
            }
        ],
        "cl_bal": {
            "desc": "Closing Balance",
            "igstTaxBal": 94,
            "cgstTaxBal": 0,
            "sgstTaxBal": 96,
            "cessTaxBal": 4983,
            "tot_rng_bal": 5173
        }
    },
    "provCrdBalList": {
        "provCrdBal": [
            {
                "ret_period": "122016",
                "cgstProCrBal": 0,
                "igstProCrBal": 34,
                "sgstProCrBal": 46,
                "cessProCrBal": 82,
                "totProCrBal": 162
            }
        ]
    }
};
*/
