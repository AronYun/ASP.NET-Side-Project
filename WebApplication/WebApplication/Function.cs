using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Data.SqlClient;

namespace WebApplication
{
    public class Function
    {
        public static string MD5(string value)
        {
            using (var md5Object = System.Security.Cryptography.MD5.Create())
            {
                //將字串編碼成 UTF8 位元組陣列
                var bytes = Encoding.UTF8.GetBytes(value);

                //取得雜湊值位元組陣列
                var hash = md5Object.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash).Replace("-", String.Empty).ToUpper();

                return md5;
            }
        }

        public static DataTable SQLServer(string ConnectionString, string SQL)
        {
            try
            {
                //Connection
                SqlConnection connection = new SqlConnection(ConnectionString);

                //啟動Connection
                connection.Open();

                //向SQL Server下SQL語法
                SqlDataAdapter adapter = new SqlDataAdapter(SQL, connection);

                //逾時時間
                adapter.SelectCommand.CommandTimeout = 300;

                //宣告DataSet
                DataSet dataSet = new DataSet();

                //將查詢結果Table放入DataSet
                adapter.Fill(dataSet);

                //查詢權限Table
                DataTable dataTable = dataSet.Tables[0];

                //關閉connection
                connection.Close();

                //釋放資源
                dataSet.Dispose();
                adapter.Dispose();
                connection.Dispose();

                return dataTable;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static string ClientIP()
        {
            //宣告字串
            string IP = "0.0.0.0";

            //判斷是否使用代理伺服器
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)
            {
                //REMOTE_ADDR = 客戶端真實IP
                IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                //客戶端真實IP，後以逗點串接多個經過的代理伺服器IP
                string realIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                //判斷HTTP_X_FORWARDED_FOR是否有值
                if (!string.IsNullOrEmpty(realIP))
                {
                    //使用逗號將字串分為陣列
                    string[] addresses = realIP.Split(',');

                    //陣列長度不等於0
                    if (addresses.Length != 0)
                    {
                        //取第0個元素
                        IP = addresses[0];
                    }
                }
            }

            //localhost = ::1
            IP = IP.Replace("::1", "127.0.0.1");

            return IP;
        }
    }
}