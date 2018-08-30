using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genersoft.Platform.WebBillQuery.SPI;
using Genersoft.Platform.WebBillQuery.Common.Data;
using System.Data;
using Genersoft.Platform.AppFramework.Service;
using Genersoft.Platform.Core.DataAccess;
using System.Xml.Linq;
using Newtonsoft.Json;
using Genersoft.Platform.Core.Common;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Genersoft.GS.JTGK.CTKZ
{
    public enum ViewType
    {
        原始单据 = 0
    }

    public enum Platform
    {
        PC = 0,
        MOBILE = 1
    }

    public class MobileQueryExtension : IQueryBizAssembly
    {
        /// <summary>
        /// 根据提供的参数信息对原始的DataSet形式的业务数据进行组装整理，返回整理后格式化的展示数据
        /// </summary>
        /// <param name="funcID">可定义为功能ID，也可为业务上下文ID，无实际意义</param>
        /// <param name="bizParameter">业务参数键值对</param>
        /// <param name="bizData">原始业务数据</param>
        /// <returns>整理后可直接格式化展示的数据</returns>
        public QueryResultData GetBizFormatResult(string funcID, Dictionary<string, string> bizParameter, DataSet bizData)
        {
            GSPState gspState = GSPContext.Current.Session;
            IGSPDatabase db = GSPContext.Current.Database;
            string strSql = "";
            strSql += " SELECT CTLIST_DJNM,CTLIST_TYPEID,CTLIST_DJBH, CTTYPE.CTTYPE_NAME FROM CTDGLIST ";
            strSql += " JOIN CTTYPE ON CTDGLIST.CTLIST_TYPEID=CTTYPE.CTTYPE_CODE";
            strSql += " WHERE CTLIST_CONTRACTID = '" + bizData.Tables[0].Rows[0]["CTLIST_CONTRACTID"].ToString() + "'";

            WriteLog(strSql, "ctdglist");
            DataSet ds = db.ExecuteDataSet(strSql);

            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            string sysCode = gspState.AppCode;//GS实例编号

            string ysdjid = "";//原始单据id

            string urlBizApply = String.Empty; ;//业务申请地址
            string urlSrcAsset = String.Empty;//资产信息地址

            string strSql1 = "";
            DataSet ds1 = null;
            DataTable dt1 = null;
            DataRow dr1 = null;
            #region  根据不同合同类型获取对应的URL

            #region 种子采购
            if (dr["CTTYPE_NAME"].ToString() == "种子采购")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 毛种子采购（初始种子）
            if (dr["CTTYPE_NAME"].ToString() == "毛种子采购（初始种子）")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 亲本种子采购（KWS玉米）
            if (dr["CTTYPE_NAME"].ToString() == "亲本种子采购（KWS玉米）")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 其它类种子采购（非库存）
            if (dr["CTTYPE_NAME"].ToString() == "其它类种子采购（非库存）")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 仓储合同
            if (dr["CTTYPE_NAME"].ToString() == "仓储合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 营销、宣传类
            if (dr["CTTYPE_NAME"].ToString() == "营销、宣传类")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 机械作业合同
            if (dr["CTTYPE_NAME"].ToString() == "机械作业合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 推广展示合同
            if (dr["CTTYPE_NAME"].ToString() == "推广展示合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 技术开发合同
            if (dr["CTTYPE_NAME"].ToString() == "技术开发合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 运维服务合同
            if (dr["CTTYPE_NAME"].ToString() == "运维服务合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 咨询服务合同
            if (dr["CTTYPE_NAME"].ToString() == "咨询服务合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 服务合同
            if (dr["CTTYPE_NAME"].ToString() == "服务合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 其他
            if (dr["CTTYPE_NAME"].ToString() == "其他")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 辅助材料采购
            if (dr["CTTYPE_NAME"].ToString() == "辅助材料采购")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 运输合同
            if (dr["CTTYPE_NAME"].ToString() == "运输合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 装卸合同
            if (dr["CTTYPE_NAME"].ToString() == "装卸合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 品种权转让合同
            if (dr["CTTYPE_NAME"].ToString() == "品种权转让合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 品种实施许可合同
            if (dr["CTTYPE_NAME"].ToString() == "品种实施许可合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 合资、合作(有收付款)
            if (dr["CTTYPE_NAME"].ToString() == "合资、合作(有收付款)")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 招标代理合同
            if (dr["CTTYPE_NAME"].ToString() == "招标代理合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 委托试验合同-双向
            if (dr["CTTYPE_NAME"].ToString() == "委托试验合同-双向")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 保管合同
            if (dr["CTTYPE_NAME"].ToString() == "保管合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 委托加工合同
            if (dr["CTTYPE_NAME"].ToString() == "委托加工合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 在建工程项目
            if (dr["CTTYPE_NAME"].ToString() == "在建工程项目")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 费用类物资采购
            if (dr["CTTYPE_NAME"].ToString() == "费用类物资采购")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 技术服务合同
            if (dr["CTTYPE_NAME"].ToString() == "技术服务合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion

            #region 临时用工-散工
            if (dr["CTTYPE_NAME"].ToString() == "临时用工-散工")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            }
            #endregion


 





            //#region 营销、宣传类
            //if (dr["CTTYPE_NAME"].ToString() == "营销、宣传类")
            //{
            //    urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            //}
            //#endregion





            #region  咨询服务合同
            if (dr["CTTYPE_NAME"].ToString() == "咨询服务合同")
            {
                strSql1 += " select ROYWSQ_NM from   ROYWSQ where ROYWSQ_BH ='" + dr["CTLIST_DJBH"].ToString() + "'";
                WriteLog(strSql1, "ROYWSQ");
                ds1 = db.ExecuteDataSet(strSql1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dt1 = ds1.Tables[0];
                    dr1 = dt1.Rows[0];
                    ysdjid = Convert.ToString(dr1["ROYWSQ_NM"]);//共享单据
                    urlBizApply = this.GetUrlBizApply(ysdjid, Platform.MOBILE);//共享单据地址
                }
            }
            #endregion

            #region 资产采购合同
            if (dr["CTTYPE_NAME"].ToString() == "资产采购")
            {
                urlBizApply = this.GetUrlBizApply_other(dr["CTLIST_DJNM"].ToString(), Platform.MOBILE, "ca098f65-1512-4f78-8c8e-d552730f42f3");//资产单据地址
                WriteLog(urlBizApply,"sdf");
            }
            #endregion

            #region 资产租入合同
            if (dr["CTTYPE_NAME"].ToString() == "资产租入合同")
            {
                urlBizApply = this.GetUrlBizApply_other(dr["CTLIST_DJNM"].ToString(), Platform.MOBILE, "22d9394c-5c89-4122-a574-ca492322ab0d");//资产单据地址
            }
            #endregion

            #region 资产维修合同
            if (dr["CTTYPE_NAME"].ToString() == "资产维修")
            {
                urlBizApply = this.GetUrlBizApply_other(dr["CTLIST_DJNM"].ToString(), Platform.MOBILE, "41000824-ec02-474f-b974-55bf8f75fa10");//资产单据地址
            }
            #endregion


            #region 融资合同-内部借款
            if (dr["CTTYPE_NAME"].ToString() == "融资合同-内部借款")
            {
                urlBizApply = this.GetUrlBizApply_other(dr["CTLIST_DJNM"].ToString(), Platform.MOBILE, "11446bcf-30dc-43f7-b860-54e79ff5ce3f");
            } 
            #endregion

            #region 融资合同-外部借款
            if (dr["CTTYPE_NAME"].ToString() == "融资合同-外部借款")
            {
                urlBizApply = this.GetUrlBizApply_other(dr["CTLIST_DJNM"].ToString(), Platform.MOBILE, "11446bcf-30dc-43f7-b860-54e79ff5ce3f");
            } 
            #endregion

            #region 运输合同
            if (dr["CTTYPE_NAME"].ToString() == "运输合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            } 
            #endregion

            #region 招标代理合同
            if (dr["CTTYPE_NAME"].ToString() == "招标代理合同")
            {
            } 
            #endregion

            #region 营销、宣传类
            if (dr["CTTYPE_NAME"].ToString() == "营销、宣传类")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            } 
            #endregion

            #region 服务合同
            if (dr["CTTYPE_NAME"].ToString() == "服务合同")
            {
                urlBizApply = this.GetUrlBizApply(dt.Rows[0]["CTLIST_DJNM"].ToString(), Platform.MOBILE);
            } 
            #endregion

            #region 劳动合同
            if (dr["CTTYPE_NAME"].ToString() == "劳动合同")
            {

            } 
            #endregion

            #region 费用类物资采购
            if (dr["CTTYPE_NAME"].ToString() == "费用类物资采购")
            {
            } 
            #endregion

            #region 资产租入合同
            if (dr["CTTYPE_NAME"].ToString() == "资产租入合同")
            {
            } 
            #endregion
            #endregion
            XElement ele = new XElement("ul",
                new XAttribute("id", "navs"),
                new XAttribute("data-open", "收起"),
                new XAttribute("data-close", "联查"),

                new XElement("li",
                    new XAttribute("style", String.IsNullOrWhiteSpace(urlBizApply) ? "background-color:#c4c4c4;" : String.Empty),
                    new XElement("a", ViewType.原始单据,
                        new XAttribute("class", "hyperlink"),
                        new XAttribute("href", "javascript:void(0)"),
                        new XAttribute("onclick", String.IsNullOrWhiteSpace(urlBizApply) ? String.Empty : "gsp.rtf.func.openUrl({id:'viewBizApply',name:'原始单据',url:'" + urlBizApply + "'})"))));

            string ul = ele.ToString();
            string html = ul + @"<script type='text/javascript'>
            (function(){
	            var ul=$('#navs'),li=$('#navs li'),i=li.length,n=1,r=120;
	            ul.click(function(){
		            $(this).toggleClass('active');
                    var isActive = $(this).hasClass('active');
			        for(var a=0;a<i;a++){
                        var msDelayed = 50*a;
                        var cosDisplacement = isActive ? -r*Math.cos(90/n*a*(Math.PI/180)) : 0;
                        var sinDisplacement = isActive ? -r*Math.sin(90/n*a*(Math.PI/180)) : 0;
				        li.eq(a).css({
					        'transition-delay':''+msDelayed+'ms',
					        '-webkit-transition-delay':''+msDelayed+'ms',
					        '-o-transition-delay':''+msDelayed+'ms',
					        'transform':'translate('+cosDisplacement+'px,'+sinDisplacement+'px)',
					        '-webkit-transform':'translate('+cosDisplacement+'px,'+sinDisplacement+'px)',
					        '-o-transform':'translate('+cosDisplacement+'px,'+sinDisplacement+'px)',
					        '-ms-transform':'translate('+cosDisplacement+'px,'+sinDisplacement+'px)'
				        });
			        }
	            });
            })($);
            </script>
            <style type='text/css'>
            .hyperlink:focus{outline:none;}
            #navs{font-size:18px;position:fixed;right:30px;bottom:80px;width:60px;height:60px;line-height:60px;list-style-type:none;margin:0;padding:0;text-align:center;color:#fff;cursor:pointer;}
            #navs>li,#navs:after{position:absolute;left:0;top:0;width:100%;height:100%;border-radius:50%;-webkit-border-radius:50%;background-color:#0073c6;}
            #navs>li{transition:all .6s;-webkit-transition:all .6s;-moz-transition:.6s;}
            #navs:after{content:attr(data-close);z-index:1;border-radius:50%;-webkit-border-radius:50%;}
            #navs.active:after{content:attr(data-open);}
            #navs a{font-size:12px;width:60px;height:60px;display:inline-block;border-radius:50%;-webkit-border-radius:50%;text-decoration:none;color:#fff;}
            </style>";
            QueryResultData resultData = new QueryResultData();
            resultData.FormatID = bizParameter["WebBillQuery_Runtime_FormatID"];//获取表单定义ID
            resultData.Data = JsonConvert.SerializeObject(bizData);
            resultData.Content = html;//此处就是你根据影像数据形成的Html片段，业务单据数据信息需要从bizData参数中获取
            //resultData.Content = "<div style=\"font-size: 28px;position: fixed;right: 30px;bottom: 80px;z-index: 999;width: 60px;height: 60px;border-radius: 80%;background-color: #0073c6;text-align: center;line-height: 60px;color: #fff;-webkit-box-shadow: 0 0 1px 1px rgba(0, 0, 0, 0.24);-moz-box-shadow: 0 0 1px 1px rgba(0, 0, 0, 0.24);box-shadow: 0 0 1px 1px rgba(0, 0, 0, 0.24);-webkit-transition: -webkit-box-shadow 0.28s cubic-bezier(0.4, 0, 0.2, 1);-moz-transition: -moz-box-shadow 0.28s cubic-bezier(0.4, 0, 0.2, 1);-o-transition: -o-box-shadow 0.28s cubic-bezier(0.4, 0, 0.2, 1);transition: box-shadow 0.28s cubic-bezier(0.4, 0, 0.2, 1);cursor: pointer;\" onclick=\"gsp.rtf.func.openUrl({id:'viewgxyxxx',name:'合同信息', url:'/cwbase/webapp/gspqdp/PackPage.html?sysCode=" + sysCode + "&FormID=" + formID + "&dataid=" + dataID + "'})\">合同</div>";//此处就是你根据影像数据形成的Html片段，业务单据数据信息需要从bizData参数中获取
            WriteLog(html, "html");
            return resultData;
        }

        /// <summary>
        /// 根据提供的参数信息对原始的DataSet形式的业务数据进行组装整理，返回整理后的DataSet
        /// </summary>
        /// <param name="funcID">可定义为功能ID，也可为业务上下文ID，无实际意义</param>
        /// <param name="bizParameter">业务参数键值对</param>
        /// <param name="bizData">原始业务数据</param>
        /// <returns>整理后的DataSet</returns>
        public DataSet GetBizResult(string funcID, Dictionary<string, string> bizParameter, DataSet bizData)
        {
            if (bizData != null & bizData.Tables.Contains("HTGY") && bizData.Tables["HTGY"].Rows.Count > 0)
            {
                string _htNM = Convert.ToString(bizData.Tables["HTGY"].Rows[0]["HTGY_NM"]);
                IGSPDatabase _db = GSPContext.Current.Database;
                DataSet _ds = _db.ExecuteDataSet("select HTFJ_NM,HTFJ_FJMC,HTFJ_HTNM from HTFJ where HTFJ_HTNM={0} ", _htNM);
                if (DataValidator.IsFirstTableHasRecord(_ds))
                {
                    AppendAttachmentInfo(bizData, _ds);
                    return bizData;
                }
                else
                {
                    return bizData;
                }
            }
            else
            {
                return bizData;
            }
        }

        /// <summary>
        /// 将附件信息放入业务DataSet中
        /// </summary>
        /// <param name="bizData"></param>
        /// <param name="_ds"></param>
        private void AppendAttachmentInfo(DataSet bizData, DataSet _ds)
        {
            CreateAttachmentSchema(bizData);
            AppendAttachmentData(bizData, _ds);
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="bizData"></param>
        /// <param name="_ds"></param>
        private void AppendAttachmentData(DataSet bizData, DataSet _ds)
        {
            DataRow _newRow = null;
            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                _newRow = bizData.Tables["HTFJ"].NewRow();
                _newRow["HTFJ_NM"] = row["HTFJ_NM"];
                _newRow["HTFJ_FJMC"] = row["HTFJ_FJMC"];
                _newRow["HTFJ_HTNM"] = row["HTFJ_HTNM"];
                bizData.Tables["HTFJ"].Rows.Add(_newRow);
            }
        }

        /// <summary>
        /// 追加Schema
        /// </summary>
        /// <param name="bizData"></param>
        private void CreateAttachmentSchema(DataSet bizData)
        {
            if (bizData.Tables.Contains("HTFJ")) return;

            //建立表table
            DataTable _tbHTFJ = bizData.Tables.Add("HTFJ");

            //内码
            DataColumn _colNM = new DataColumn();
            _colNM.DataType = typeof(string);
            _colNM.AllowDBNull = true;
            _colNM.Caption = "内码";
            _colNM.ColumnName = "HTFJ_NM";
            _tbHTFJ.Columns.Add(_colNM);
            _tbHTFJ.PrimaryKey = new DataColumn[] { _colNM };

            //附件名称
            DataColumn _colFileName = new DataColumn();
            _colFileName.DataType = typeof(string);
            _colFileName.AllowDBNull = true;
            _colFileName.Caption = "附件名称";
            _colFileName.ColumnName = "HTFJ_FJMC";
            _tbHTFJ.Columns.Add(_colFileName);

            //外键
            DataColumn _colHTNM = new DataColumn();
            _colHTNM.DataType = typeof(string);
            _colHTNM.AllowDBNull = true;
            _colHTNM.Caption = "附件名称";
            _colHTNM.ColumnName = "HTFJ_HTNM";
            _tbHTFJ.Columns.Add(_colHTNM);

            if (bizData.Relations.Contains("HTFJXX")) return;

            bizData.Relations.Add("HTFJXX", bizData.Tables["HTGY"].Columns["HTGY_NM"], bizData.Tables["HTFJ"].Columns["HTFJ_HTNM"]);//HTFJ_HTNM跟HTGY_NM
        }

        /// <summary>
        /// 根据提供的参数信息对原始的DataSet形式的业务数据进行组装整理，返回整理后的DataSet，同时返回格式ID，查询中心根据提供的格式ID对整理后DataSet进行终极整理并显示
        /// </summary>
        /// <param name="funcID">可定义为功能ID，也可为业务上下文ID，无实际意义</param>
        /// <param name="bizParameter">业务参数键值对</param>
        /// <param name="bizData">原始业务数据</param>
        /// <param name="sysCode">系统编号，如果是工作流方式，对应工作流定义的扩展系统编号；如果数据模型方式，为固定的datamodel</param>
        /// <param name="formatID">格式ID</param>
        /// <returns>整理后的DataSet</returns>
        public DataSet GetBizResultWithFormatID(string funcID, Dictionary<string, string> bizParameter, DataSet bizData, ref string sysCode, ref string formatID)
        {
            DataSet _ds = bizData;
            _ds.Tables[0].Rows[0]["Name"] = "NameTestbizInvoke22";
            formatID = "aaaaaa";
            return _ds;
        }

        /// <summary>
        /// 根据原始单据内码获取原始单据的共享联查信息
        /// </summary>
        /// <param name="id">原始单据内码</param>
        /// <param name="platform">pc||mobile</param>
        /// <returns>原始单据的共享联查信息</returns>
        public string GetViewInfo(string id, Platform platform)
        {
            string url = String.Empty;
            string name = String.Empty;
            string view = Guid.NewGuid().ToString();




            if (!String.IsNullOrWhiteSpace(url = this.GetUrlBizApply(id, platform)))
            {
                name = ViewType.原始单据.ToString();
                JObject jo = new JObject();
                jo["id"] = view;
                jo["name"] = name;
                jo["url"] = url;
                return jo.ToString();
            }


            return String.Empty;
        }


        /// <summary>
        /// 根据原始单据内码获取原始单据的共享联查url
        /// </summary>
        /// <param name="id">原始单据内码</param>
        /// <param name="platform">pc||mobile</param>
        /// <returns>原始单据的共享联查url</returns>
        public string GetUrlBizApply(string id, Platform platform)
        {
            GSPState gspState = GSPContext.Current.Session;
            IGSPDatabase db = GSPContext.Current.Database;
            string gsCode = gspState.AppCode;//GS实例编号
            string formID = Convert.ToString(db.ExecuteScalar(@"WITH
            T1 AS (SELECT SRCCOL01, TARCOL FROM DEPVMVALUELIST WHERE VMID IN (SELECT ID FROM DEPVALUEMAPPING WHERE CODE = '2005')),
            T2 AS (SELECT TARCOL FROM T1 WHERE SRCCOL01 IN (SELECT ROYWSQ_SQLX FROM ROYWSQ WHERE ROYWSQ_NM = {0}))
            SELECT MAX(TARCOL) TARCOL FROM T2", id));//申请类型内码映射申请单据定义内码2005
            string page = platform.Equals(Platform.PC) ? "webbillQueryADPphone" : "PackPage";
            string viewType = ViewType.原始单据.ToString();
            string url = String.Format("/cwbase/webapp/gspqdp/{3}.html?viewType={4}&sysCode={0}&FormID={1}&dataid={2}", gsCode, formID, id, page, viewType);
            WriteLog(url, "url");
            return String.IsNullOrWhiteSpace(formID) ? String.Empty : Uri.EscapeUriString(url);
        }

        private string GetUrlBizApply_other(string id, Platform platform,string formid)
        {
            GSPState gspState = GSPContext.Current.Session;
            IGSPDatabase db = GSPContext.Current.Database;
            string gsCode = gspState.AppCode;//GS实例编号
            string page = platform.Equals(Platform.PC) ? "webbillQueryADPphone" : "PackPage";
            string viewType = ViewType.原始单据.ToString();
            string url = String.Format("/cwbase/webapp/gspqdp/{3}.html?viewType={4}&sysCode={0}&FormID={1}&dataid={2}", gsCode, formid, id, page, viewType);
            WriteLog(url, "url");
            return String.IsNullOrWhiteSpace(formid) ? String.Empty : Uri.EscapeUriString(url);
        }


        /// <summary>
        /// 除共享之外获取移动端URL
        /// </summary>
        /// <param name="id">原始单据内码</param>
        /// <param name="platform">pc||mobile</param>
        /// <returns></returns>
        private string GetUrlBizApply_NOGX(string id, Platform platform ,string  formID)
        {
            GSPState gspState = GSPContext.Current.Session;
            IGSPDatabase db = GSPContext.Current.Database;
            string gsCode = gspState.AppCode;//GS实例编号
           
            string page = platform.Equals(Platform.PC) ? "webbillQueryADPphone" : "PackPage";
            string viewType = ViewType.原始单据.ToString();
            string url = String.Format("/cwbase/webapp/gspqdp/{3}.html?viewType={4}&sysCode={0}&FormID={1}&dataid={2}", gsCode, formID, id, page, viewType);
            WriteLog(url, "url");
            return String.IsNullOrWhiteSpace(formID) ? String.Empty : Uri.EscapeUriString(url);
        }

        #region 记录文本日志
        public static void WriteLog(string txt, string type)
        {
            string strFilePath = @"c:\\RentLog\\";
            if (!Directory.Exists(strFilePath)) //return;
                Directory.CreateDirectory(strFilePath);

            strFilePath += System.DateTime.Now.ToString("yyyyMMdd") + type + ".log";
            System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + txt);
            sw.Close();
            fs.Close();
        }
        #endregion
    }
}

