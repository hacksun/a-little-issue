using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Genersoft.Platform.BizComponent.BasicLib;
using Genersoft.Platform.Resource.Metadata.Component.Attributes;
using Genersoft.Platform.Core.DataAccess;
using Genersoft.Platform.AppFramework.Service;
using System.Data;

namespace Genersoft.GS.JTGK.CTKZ
{
    public class CTWebSelect : BaseBizComponent
    {
        #region 联查共享
        /// <summary>
        /// 获取共享申请单的查询配置{id:id,name:name,url:url}
        /// </summary>
        /// <param name="id">申请单单据内码</param>
        /// <returns></returns>
        [BizComponentMethodAttribute]

        public string ctdgSelect_all(string id, string code)
        {
            Log("all方法执行成功");
            Log("id="+id+",code="+code);
            string tmp;
            if (code.Length >= 4)
            {
                tmp = code.Substring(0, 4).ToUpper();
                Log("code="+ code);
            }
            
            else 
            {
                return string.Empty;
            }
            IGSPDatabase db = GSPContext.Current.Database;
            //tarcol:两个字母  例如：ZC GX ZJ
            //srccol02: ca098f65-1512-4f78-8c8e-d552730f42f3 41000824-ec02-474f-b974-55bf8f75fa10 22d9394c-5c89-4122-a574-ca492322ab0d
            string tarcol = Convert.ToString(db.ExecuteScalar("SELECT TARCOL FROM DEPVMVALUELIST WHERE VMID IN (SELECT ID FROM DEPVALUEMAPPING WHERE CODE = 'CT_LC' AND SRCCOL01={0})", tmp));
            //string srccol02 = Convert.ToString(db.ExecuteScalar("SELECT SRCCOL02 FROM DEPVMVALUELIST WHERE VMID IN (SELECT ID FROM DEPVALUEMAPPING WHERE CODE = 'CT_LC' AND SRCCOL01={0})", tmp));

            switch (tarcol)
            {
                case "GX": return ctdgSelect_gx(id, code); break;
                case "ZC":
                    {
                        string str = String.Empty;
                        str = this.ctdgSelect_zccg(id, code, "ca098f65-1512-4f78-8c8e-d552730f42f3");//调用资产采购
                        if (!String.IsNullOrWhiteSpace(str))
                        {
                            return str;
                        }
                        str = this.ctdgSelect_zcwx(id, code, "41000824-ec02-474f-b974-55bf8f75fa10");//调用资产维修
                        if (!String.IsNullOrWhiteSpace(str))
                        {
                            return str;
                        }
                        str = this.ctdgSelect_zczr(id, code, "22d9394c-5c89-4122-a574-ca492322ab0d");//调用资产租入
                        if (!String.IsNullOrWhiteSpace(str))
                        {
                            return str;
                        }
                        else 
                        {
                            return string.Empty;
                        }
                        
                        #region
                        //    if (srccol02 == "ca098f65-1512-4f78-8c8e-d552730f42f3")
                        //    {
                        //        return ctdgSelect_zccg(id, code, "ca098f65-1512-4f78-8c8e-d552730f42f3"); 
                        //    }
                        //    else if (srccol02 == "41000824-ec02-474f-b974-55bf8f75fa10")
                        //    {
                        //        return ctdgSelect_zcwx(id, code, "41000824-ec02-474f-b974-55bf8f75fa10");
                        //    }
                        //    else if (srccol02 == "22d9394c-5c89-4122-a574-ca492322ab0d")
                        //    {
                        //        return ctdgSelect_zczr(id, code, "22d9394c-5c89-4122-a574-ca492322ab0d");
                        //    }

                        //    else
                        //    {
                        //        return string.Empty;
                        //    }

                        //}; break;
                        #endregion

                    }break;
                case "ZJ":
                    {
                        Log("进入case ZJ");
                        string str = String.Empty;
                        str = this.ctdgSelect_zjnb(id, code, "11446bcf-30dc-43f7-b860-54e79ff5ce3f");//调用资金内部
                        if (!String.IsNullOrWhiteSpace(str))
                        {
                            return str;
                        }
                        //str = this.ctdgSelect_zjwb(id, code, "41000824-ec02-474f-b974-55bf8f75fa10");//调用资金外部
                        //if (!String.IsNullOrWhiteSpace(str))
                        //{
                        //    return str;
                        //}
                        else
                        {
                            return string.Empty;
                        }
                    }break;
                default: return string.Empty;
            }
        }


        public string ctdgSelect_gx(string id, string code)
        {
            IGSPDatabase db = GSPContext.Current.Database;
            string billType = Convert.ToString(db.ExecuteScalar("SELECT MAX(FSYXDJ_DJLX) FROM FSYXDJ WHERE FSYXDJ_DJNM = {0}", id));
            JObject jo = new JObject();
            jo["id"] = id;
            jo["name"] = code;
            string url = String.Format("/cwbase/web/ro/ywcl/2fc63df3-0b0c-42c4-96c3-2bdee5aa0763/Index.html?isruntime=1&operation=VIEW&mode=CX&secondlatitude=SQ&firstlatitude={1}&nm={0}", id, billType);
            jo["url"] = String.Format("/cwbase/web/session/gsprtf/adpdebug.aspx?adpurl={0}", Uri.EscapeDataString(url));
            string result = String.IsNullOrWhiteSpace(billType) ? String.Empty : jo.ToString();
            return result;
        }
        #endregion

        #region 联查资产采购
        /// <summary>
        /// 获取资产采购查询配置{id:id,code:code,formid:formid}
        /// </summary>
        /// <param name="id">申请单单据内码</param>
        /// <returns></returns>
        [BizComponentMethodAttribute]
        public string ctdgSelect_zccg(string id, string code, string formid)
        {
            IGSPDatabase db = GSPContext.Current.Database;
            int count = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(1) COUNT FROM AMCGSQ WHERE AMCGSQ_DJID = {0}", id));
            JObject jo = new JObject();
            jo["id"] = id;
            jo["name"] = code;
            string url = String.Format("/cwbase/web/session/gspadp/AdpRuntime.aspx?formID={1}&dataID={0}&actionID=view", id, formid);
            jo["url"] = String.Format("/cwbase/web/session/gsprtf/adpdebug.aspx?adpurl={0}", Uri.EscapeDataString(url));
            //Log(jo["url"]);
            string result = count <= 0 ? String.Empty : jo.ToString();
            return result;
        }
        #endregion

        #region 联查资产租入
        [BizComponentMethodAttribute]
        public string ctdgSelect_zczr(string id, string code, string formid)
        {
            //Log(id);
            //formid = "ca098f65-1512-4f78-8c8e-d552730f42f3";
            IGSPDatabase db = GSPContext.Current.Database;
            int count = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(1) COUNT FROM AMZCZL WHERE AMZCZL_DJID = {0}", id));
            JObject jo = new JObject();
            jo["id"] = id;
            jo["name"] = code;
            string url = String.Format("/cwbase/web/session/gspadp/AdpRuntime.aspx?formID={1}&dataID={0}&actionID=view", id, formid);
            jo["url"] = String.Format("/cwbase/web/session/gsprtf/adpdebug.aspx?adpurl={0}", Uri.EscapeDataString(url));
            //Log(jo["url"]);
            string result = count <= 0 ? String.Empty : jo.ToString();
            return result;
        }
        #endregion

        #region 联查资产维修
        [BizComponentMethodAttribute]
        public string ctdgSelect_zcwx(string id, string code, string formid)
        {
            //Log(id);
            //formid = "ca098f65-1512-4f78-8c8e-d552730f42f3";
            IGSPDatabase db = GSPContext.Current.Database;
            int count = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(1) COUNT FROM AMWXSQ WHERE AMWXSQ_DJID = {0}", id));
            JObject jo = new JObject();
            jo["id"] = id;
            jo["name"] = code;
            string url = String.Format("/cwbase/web/session/gspadp/AdpRuntime.aspx?formID={1}&dataID={0}&actionID=view", id, formid);
            jo["url"] = String.Format("/cwbase/web/session/gsprtf/adpdebug.aspx?adpurl={0}", Uri.EscapeDataString(url));
            //Log(jo["url"]);
            string result = count <= 0 ? String.Empty : jo.ToString();
            return result;
        }
        #endregion

        #region 联查融资
        [BizComponentMethodAttribute]
        public string ctdgSelect_zjnb(string id,string code,string formid)
        {

            MobileQueryExtension aa = new MobileQueryExtension();
            
            Log("资金内部方法执行成功");
            IGSPDatabase db = GSPContext.Current.Database;
            int count = Convert.ToInt32(db.ExecuteScalar("SELECT COUNT(1) COUNT FROM XDDKHT WHERE XDDKHT_HTNM = {0}", id));
            Log(count);
            JObject jo = new JObject();
            jo["id"] = id;
            jo["name"] = code;
            //string url = String.Format("/cwbase/web/session/gspadp/AdpRuntime.aspx?formID={1}&dataID={0}&actionID=view", id, formid);
            //jo["url"] = String.Format("/cwbase/web/session/gsprtf/adpdebug.aspx?adpurl={0}", Uri.EscapeDataString(url));
            //string url = aa.GetUrlBizApply(id,Platform.MOBILE);
            GSPState gspState = GSPContext.Current.Session;
            string gsCode = gspState.AppCode;//GS实例编号
            string viewType = ViewType.原始单据.ToString();

            string url = String.Format("/cwbase/webapp/gspqdp/{3}.html?viewType={4}&sysCode={0}&FormID={1}&dataid={2}", gsCode, formid, id, "PackPage", viewType);
            jo["url"] = String.Format("/cwbase/web/session/gsprtf/adpdebug.aspx?adpurl={0}", Uri.EscapeDataString(url));
            string result = count <= 0 ? String.Empty : jo.ToString();
            Log(result);
            return result;
        }
        #endregion

        #region log
        public void Log(object obj)
        {
            string path = String.Format("C:\\AAAA\\{0}\\{1}\\", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, DateTime.Now.ToString("yyyy-MM-dd-HH"));
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path = String.Format("{0}{1}.log", path, System.Threading.Thread.CurrentThread.ManagedThreadId);

            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            try
            {
                sw.WriteLine(String.Format("{0}  ↓↓↓", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                sw.WriteLine(obj);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
        #endregion
    }
}
