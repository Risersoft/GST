
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Risersoft.API.GSTN;
using Risersoft.API.GSTN.Ledger;

namespace Risersoft.API.GSTN
{
    public class LedgerApiClient : GSTNReturnsClient
    {

        //action_required=“Y|N“
        public LedgerApiClient(IGSTNAuthProvider provider, string gstin, string userid, string ret_period) : base(provider, "/ledgers",gstin,userid,ret_period, "LEDGER")
        {
        }

        public GSTNResult<LedgerCashBalanceITC> GetLedgerCashBalanceITC()
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("action", "BAL");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<LedgerCashBalanceITC>(info.Data);
            var model = this.BuildResult<LedgerCashBalanceITC>(info, output);
            return model;
        }
        public GSTNResult<RetLibBalance> GetRetLibBalance()
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("ret_type", "R3");
            dic.Add("action", "TAXPAYABLE");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<RetLibBalance>(info.Data);
            var model = this.BuildResult<RetLibBalance>(info, output);
            return model;
        }
        public GSTNResult<LibDetails> GetLibDetails()
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("ret_period", this.ret_period);
            dic.Add("action", "TAX");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<LibDetails>(info.Data);
            var model = this.BuildResult<LibDetails>(info, output);
            return model;
        }
        //This API call is for getting cash ledger details for the specified period
        public GSTNResult<CashLedgerDetails> GetCashDtl(string fr_dt, string to_dt)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("fr_dt", fr_dt);
            dic.Add("to_dt", to_dt);
            dic.Add("action", "CASH");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<CashLedgerDetails>(info.Data);
            var model = this.BuildResult<CashLedgerDetails>(info, output);
            return model;

        }
        //This API call is for getting ITC ledger details for the specified period
        public GSTNResult<ITCLedgerDetails> GetItcDtl(string fr_dt, string to_dt)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("fr_dt", fr_dt);
            dic.Add("to_dt", to_dt);
            dic.Add("action", "ITC");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<ITCLedgerDetails>(info.Data);
            var model = this.BuildResult<ITCLedgerDetails>(info, output);
            return model;
        }
        //This API call is for getting Tax Liability ledger details for the specified period
        public GSTNResult<TaxLedgerDetails> GetTaxDtl(string fr_dt, string to_dt)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("fr_dt", fr_dt);
            dic.Add("to_dt", to_dt);
            dic.Add("action", "TAX");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<TaxLedgerDetails>(info.Data);
            var model = this.BuildResult<TaxLedgerDetails>(info, output);
            return model;
        }
        //This API call is for getting summary of cash ledger details for the specified period
        public GSTNResult<LedgerSummary> GetCashSum(string fr_dt, string to_dt)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("fr_dt", fr_dt);
            dic.Add("to_dt", to_dt);
            dic.Add("action", "CASHSUM");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<LedgerSummary>(info.Data);
            var model = this.BuildResult<LedgerSummary>(info, output);
            return model;
        }
        //This API call is for getting summary of ITC ledger details for the specified period
        public GSTNResult<LedgerSummary> GetItcSum(string fr_dt, string to_dt)
        {
            dicParams.Clear();
            var dic = new Dictionary<string, string>();
            dic.Add("gstin", this.gstin);
            dic.Add("fr_dt", fr_dt);
            dic.Add("to_dt", to_dt);
            dic.Add("action", "ITCSUM");
            this.PrepareQueryString(dic);
            var info = this.Get<ResponseDataInfo>();
            var output = this.Decrypt<LedgerSummary>(info.Data);
            var model = this.BuildResult<LedgerSummary>(info, output);
            return model;
        }

        //This API call is to utilize the remaining cash for paying the liability
        public GSTNResult<SaveInfo> UtilizeCash(UtilizeCashModel data)
        {
            dicParams.Clear();
            var model = this.Encrypt(data);
            model.action = "UTLCSH";
            var info = this.Post<UnsignedDataInfo, ResponseDataInfo>(model);
            var output = this.Decrypt<SaveInfo>(info.Data);
            var model2 = this.BuildResult<SaveInfo>(info, output);
            return model2;
        }


        //This API call is to utilize ITC for paying the liability
        public GSTNResult<SaveInfo> UtilizeITC(UtilizeITCModel data)
        {
            dicParams.Clear();
            var model = this.Encrypt(data);
            model.action = "UTLITC";
            var info = this.Post<UnsignedDataInfo, ResponseDataInfo>(model);
            var output = this.Decrypt<SaveInfo>(info.Data);
            var model2 = this.BuildResult<SaveInfo>(info, output);
            return model2;
        }

    }
}