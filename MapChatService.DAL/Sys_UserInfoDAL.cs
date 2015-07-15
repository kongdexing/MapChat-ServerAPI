
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MapChatService;
using MapChatService.DBUtility;
using MapChatService.Model;//Please add references
namespace MapChatService.DAL
{
    /// <summary>
    /// 数据访问类:Sys_UserInfo
    /// </summary>
    public partial class Sys_UserInfoDAL
    {
        public Sys_UserInfoDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 登录
        /// </summary>
        public int Login(string username, string password, ref string id)
        {
            SqlConnection sqlConn = new SqlConnection(DbHelperSQL.connectionString);
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand("Login", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
            sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = password;
            sqlCmd.Parameters.Add("ReturnValue", SqlDbType.Int, 4);
            sqlCmd.Parameters["ReturnValue"].Direction = ParameterDirection.ReturnValue;

            sqlCmd.Parameters.Add("@ID", SqlDbType.VarChar, 50); //添加参数
            sqlCmd.Parameters["@ID"].Direction = ParameterDirection.Output; //指定是输出参数

            sqlCmd.ExecuteNonQuery();

            int result = Convert.ToInt32(sqlCmd.Parameters["ReturnValue"].Value);
            id = sqlCmd.Parameters["@ID"].Value.ToString();
            return result;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys_UserInfo model)
        {
            SqlConnection sqlConn = new SqlConnection(DbHelperSQL.connectionString);
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand("AddUserInfo", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = model.ID;
            sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = model.UserName;
            sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = model.Password;
            sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = model.Email;
            sqlCmd.Parameters.Add("@Phone", SqlDbType.VarChar, 250).Value = model.Phone;
            sqlCmd.Parameters.Add("ReturnValue", SqlDbType.Int, 4);
            sqlCmd.Parameters["ReturnValue"].Direction = ParameterDirection.ReturnValue;

            sqlCmd.ExecuteNonQuery();

            int result = Convert.ToInt32(sqlCmd.Parameters["ReturnValue"].Value);
            return result;

            // SqlParameter[] parameters = {
            //         new SqlParameter("@ID", SqlDbType.VarChar,38),
            //         new SqlParameter("@UserName", SqlDbType.VarChar,50),
            //         new SqlParameter("@Password", SqlDbType.VarChar,50),
            //         //new SqlParameter("@NiName", SqlDbType.NVarChar,50),
            //         new SqlParameter("@Email", SqlDbType.VarChar,50),
            //         new SqlParameter("@Phone", SqlDbType.VarChar,12),
            //         //new SqlParameter("@Sex", SqlDbType.Bit,1),
            //         //new SqlParameter("@Birthday", SqlDbType.DateTime),
            //         //new SqlParameter("@ImageUrl", SqlDbType.VarChar,100)
            //         //,
            //         //new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
            //         //new SqlParameter("@CreateTime", SqlDbType.DateTime),
            //         //new SqlParameter("@DeleteTime", SqlDbType.DateTime)
            //                             };
            // parameters[0].Value = model.ID;
            // parameters[1].Value = model.UserName;
            // parameters[2].Value = model.Password;
            //// parameters[3].Value = model.NiName;
            //// parameters[4].Value = model.Sex;
            //// parameters[5].Value = model.Birthday;
            // parameters[3].Value = model.Email;
            // parameters[4].Value = model.Phone;
            //// parameters[8].Value = model.ImageUrl;
            // //parameters[9].Value = model.IsDeleted;
            // //parameters[10].Value = model.CreateTime;
            // //parameters[11].Value = model.DeleteTime;
            // int rows = 0;
            // int result = DbHelperSQL.RunProcedure("AddUserInfo", parameters, out rows);
            // return result;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Password=@Password,");
            strSql.Append("NiName=@NiName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("Email=@Email,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("IsDeleted=@IsDeleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("DeleteTime=@DeleteTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@Password", SqlDbType.VarChar,50),
					new SqlParameter("@NiName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.Bit,1),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@Phone", SqlDbType.VarChar,12),
					new SqlParameter("@ImageUrl", SqlDbType.VarChar,100),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@DeleteTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.VarChar,38)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.NiName;
            parameters[3].Value = model.Sex;
            parameters[4].Value = model.Birthday;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.IsDeleted;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.DeleteTime;
            parameters[11].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sys_UserInfo ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.VarChar,38)			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sys_UserInfo ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys_UserInfo GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserName,Password,NiName,Sex,Birthday,Email,Phone,ImageUrl,IsDeleted,CreateTime,DeleteTime from Sys_UserInfo ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.VarChar,38)			};
            parameters[0].Value = ID;

            Sys_UserInfo model = new Sys_UserInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys_UserInfo DataRowToModel(DataRow row)
        {
            Sys_UserInfo model = new Sys_UserInfo();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["NiName"] != null)
                {
                    model.NiName = row["NiName"].ToString();
                }
                if (row["Sex"] != null && row["Sex"].ToString() != "")
                {
                    if ((row["Sex"].ToString() == "1") || (row["Sex"].ToString().ToLower() == "true"))
                    {
                        model.Sex = true;
                    }
                    else
                    {
                        model.Sex = false;
                    }
                }
                if (row["Birthday"] != null && row["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(row["Birthday"].ToString());
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Phone"] != null)
                {
                    model.Phone = row["Phone"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    model.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["IsDeleted"] != null && row["IsDeleted"].ToString() != "")
                {
                    if ((row["IsDeleted"].ToString() == "1") || (row["IsDeleted"].ToString().ToLower() == "true"))
                    {
                        model.IsDeleted = true;
                    }
                    else
                    {
                        model.IsDeleted = false;
                    }
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["DeleteTime"] != null && row["DeleteTime"].ToString() != "")
                {
                    model.DeleteTime = DateTime.Parse(row["DeleteTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,UserName,Password,NiName,Sex,Birthday,Email,Phone,ImageUrl,IsDeleted,CreateTime,DeleteTime ");
            strSql.Append(" FROM Sys_UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,UserName,Password,NiName,Sex,Birthday,Email,Phone,ImageUrl,IsDeleted,CreateTime,DeleteTime ");
            strSql.Append(" FROM Sys_UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Sys_UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Sys_UserInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Sys_UserInfo";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

