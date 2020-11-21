using Newtonsoft.Json;
using risersoft.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risersoft.API.GSTN
{
    public class GSTNConstants
    {

        public static GSPCredentials GenerateCredential(string code, string API, string env )
        {
            GSPCredentials selected = null;
            string filename = System.IO.Path.Combine(GSTNConstants.base_path, "gsp.json");
            if (System.IO.File.Exists(filename))
            {
                string str1 = System.IO.File.ReadAllText(filename);
                var lst = JsonConvert.DeserializeObject<List<GSPCredentials>>(str1);
                selected = lst.Where(item => (myUtils.IsInList(item.code, code) && myUtils.IsInList(API, item.API) && myUtils.IsInList(env, item.Env))).FirstOrDefault();
            }
            return selected;
        }

        public static string base_path = ".";
        public static string publicip = "";

       
    }

}