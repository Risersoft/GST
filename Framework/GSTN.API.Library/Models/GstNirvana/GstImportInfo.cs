using risersoft.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTN.API.Library.Models.GstNirvana
{

    public class GstImportInfo
    {
        public List<GstImportInfoGSTIN> GSTIN_List { get; set; } = new List<GstImportInfoGSTIN>();
        public virtual GstImportInfoGSTIN AddRecord(string GSTIN, string ret_pd, int RowCount, bool IsSuccess, decimal txval, decimal iamt, decimal camt, decimal samt, decimal csamt)
        {
            var found = GSTIN_List.Where(a => myUtils.IsInList(a.GSTIN, GSTIN) && myUtils.IsInList(a.ret_pd, ret_pd)).FirstOrDefault();
            if (found == null)
            {
                found = new GstImportInfoGSTIN { GSTIN = GSTIN, ret_pd = ret_pd };
                GSTIN_List.Add(found);
            }
            found.AddRecord(RowCount, IsSuccess, txval, iamt, camt, samt, csamt);
            return found;
        }

        public virtual GstImportInfoGSTIN AddRecord(string GSTIN, string ret_pd, int RowCount, bool IsSuccess)
        {
            var found = GSTIN_List.Where(a => myUtils.IsInList(a.GSTIN, GSTIN) && myUtils.IsInList(a.ret_pd, ret_pd)).FirstOrDefault();
            if (found == null)
            {
                found = new GstImportInfoGSTIN { GSTIN = GSTIN, ret_pd = ret_pd };
                GSTIN_List.Add(found);
            }
            found.AddRecord(RowCount, IsSuccess);
            return found;
        }
        public virtual GstImportInfoGSTIN AddInfo(GstImportInfoGSTIN info)
        {
            var found = GSTIN_List.Where(a => myUtils.IsInList(a.GSTIN, info.GSTIN) && myUtils.IsInList(a.ret_pd, info.ret_pd)).FirstOrDefault();
            if (found == null)
            {
                found = info;
                GSTIN_List.Add(info);
            }
            else
            {
                found.AddInfo(info);
            }
            return found;
        }
    }


    public class GstImportInfoGSTIN
    {
        public string State { get; set; }
        public string GSTIN { get; set; }
        public string ret_pd { get; set; }
        public int RowCount { get; set; } = 0;
        public int RecordCount { get; set; } = 0;
        public int ErrorCount { get; set; } = 0;
        public decimal txval { get; set; } = 0;
        public decimal iamt { get; set; } = 0;
        public decimal camt { get; set; } = 0;
        public decimal samt { get; set; } = 0;
        public decimal csamt { get; set; } = 0;
        public virtual void AddRecord(int RowCount, bool IsSuccess)
        {
            this.RowCount = this.RowCount + RowCount;
            this.RecordCount = this.RecordCount + 1;
            if (!IsSuccess) this.ErrorCount = this.ErrorCount + 1;

        }
        public virtual void AddInfo(GstImportInfoGSTIN info)
        {

            this.RowCount = this.RowCount + info.RowCount;
            this.RecordCount = this.RecordCount + info.RecordCount;
            this.ErrorCount = this.ErrorCount + info.ErrorCount;
            this.txval = this.txval + Math.Round(info.txval, 2);
            this.iamt = this.iamt + Math.Round(info.iamt, 2);
            this.camt = this.camt + Math.Round(info.camt, 2);
            this.samt = this.samt + Math.Round(info.samt, 2);
            this.csamt = this.csamt + Math.Round(info.csamt, 2);

        }

        public virtual void AddRecord(int RowCount, bool IsSuccess, decimal txval, decimal iamt, decimal camt, decimal samt, decimal csamt)
        {
            this.AddRecord(RowCount, IsSuccess);
            this.txval = this.txval + Math.Round(txval, 2);
            this.iamt = this.iamt + Math.Round(iamt, 2);
            this.camt = this.camt + Math.Round(camt, 2);
            this.samt = this.samt + Math.Round(samt, 2);
            this.csamt = this.csamt + Math.Round(csamt, 2);

        }
    }
}
