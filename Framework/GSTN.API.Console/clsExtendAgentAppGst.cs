using risersoft.shared;
using risersoft.shared.portable.Models.Nav;
using risersoft.shared.portable.Models.Publisher;
using System;
using System.Collections.Generic;
using System.Data;

namespace Risersoft.API.GSTN.Console
{
    public class clsExtendAgentAppGst : clsAppExtendBase
        {
    

        public override IForm AboutBox()
        {
            throw new NotImplementedException();
        }

        public override List<string> AppCodesHandled()
        {
            throw new NotImplementedException();
        }

        public override IAppDataProvider CreateDataProvider(clsAppController context, IAsyncWCFCallBack cb)
        {
            throw new NotImplementedException();
        }

        public override ICosmosDbContext CreateObjNoSqlContext(IAppDataProvider provider)
        {
            throw new NotImplementedException();
        }

        public override string DBScriptAppCode()
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicFormModelTypes()
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicReportProviderTypes(clsAppController myContext)
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicTaskProviderTypes()
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicWOInfoTypes()
        {
            throw new NotImplementedException();
        }

        public override string FileProviderCode(IProviderContext context)
        {
            return "blob";
        }

        public override bool FileServerRequired()
        {
            throw new NotImplementedException();
        }

        public override string frmAdvanceName()
        {
            throw new NotImplementedException();
        }

        public override IForm frmDel(clsView pView, string IDX, string sysentXML)
        {
            throw new NotImplementedException();
        }

        public override DataTable GenerateDataTable(clsViewModel pView, clsBandConf conf, string pdclientview, string strFilter)
        {
            throw new NotImplementedException();
        }

        public override string GenerateParamValue(IProviderContext context, string str1)
        {
            throw new NotImplementedException();
        }

        public override string GetSystemOption(IProviderContext context, string code)
        {
            throw new NotImplementedException();
        }

        public override clsProcOutput LoadAppData(IProviderContext context, string dataKey, List<clsSQLParam> Params, bool forcefresh)
        {
            throw new NotImplementedException();
        }

        public override decimal MinDBVersion()
        {
            throw new NotImplementedException();
        }

        public override string NewDBName()
        {
            throw new NotImplementedException();
        }

        public override ISQLHelper2 oSQLHelper(IAppDataProvider provider)
        {
            throw new NotImplementedException();
        }

        public override string ProgramCode()
        {
            return "";
        }
    

    public override string ProgramName()
        {
            return "";
        }

        public override PublisherInfo Publisher()
        {
            return new PublisherInfo();
        }

        public override string StartupAppCode()
        {
            throw new NotImplementedException();
        }


        public override string UpdateURL(clsCoreApp oApp)
        {
            throw new NotImplementedException();
        }
    }
}
