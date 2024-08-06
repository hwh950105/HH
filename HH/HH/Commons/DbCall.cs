using HH.Models;
using HH.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using MyApp;
namespace HH.Commons
{
   static public class DbCall
    {
        

        static public bool GetLogin(LoginModel user)
        {
            try
            {
                using (SQLiteDbHelper db = new SQLiteDbHelper())
                {
                    string Qurey = $"select * from TB_User where id = '{user.id}' and password = '{user.password}'";
                    var item = db.ExecuteList<LoginModel>(Qurey);

                    if (item.Count > 0)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex) 
            {
                LogHelper.Error(ex);
                throw;
            }

        }





    }
}
