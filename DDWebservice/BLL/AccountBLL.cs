using DDWebservice.Models;
using System.Transactions;
using Dapper;
using static DDWebservice.BLL.Hashing;

namespace DDWebservice.BLL
{
    public class AccountBll
    {
        /// <summary>
        /// Used to Create a User Model
        /// First insert into user
        /// then insert into auth
        /// if the first insert fail dont insert the pwd
        /// if second insert fails rollback
        /// </summary>
        /// <remarks>Uses Transactions</remarks>
        /// <param name="user"></param>
        public static void CreateAccount(RegistrationUserModel user)
        {
            const string sql1 = "insert into users values(@email,@firstname,@familyname)";
            const string sql2 = "insert into auth values(@email,@pwd)";
            var par1 = new DynamicParameters();
            par1.Add("email", user.Email);
            par1.Add("firstname", user.FirstName);
            par1.Add("familyname", user.FamilyName);
            var par2 = new DynamicParameters();
            par2.Add("email", user.Email);
            par2.Add("pwd", HashPassword(user.Password).Trim());
            //Using a transaction to perform the insert into two tables
            //In case of an Error rollback
            using (var transcope = new TransactionScope())
            {
                if (DapperORM.ExecuteInsert(sql1, par1) > 0)
                {
                    if (DapperORM.ExecuteInsert(sql2, par2) < 0)
                    {
                        transcope.Dispose();
                    }
                }

                transcope.Complete();
            }
        }
        /// <summary>
        /// Gets the Users stored password using email
        /// verify the Hash
        /// <remarks>The storedhash queried from the DB should be trimmed.
        /// if the length of data type used to store the data is greater than 60 </remarks>
        /// </summary>
        /// <param name="user">User Model</param>
        /// <returns>True if Valid else False </returns>
        public static bool Login(UserLoginModel user)
        {
            const string sql = "select pwd from auth where email = @email";
            var parameters = new DynamicParameters();
            parameters.Add("email",user.Email);
            //remove the empty whitespaces
            var storedhash = DapperORM.QueryGetSingle<string>(sql, parameters).Trim();
            return VerifyPassword(user.Password, storedhash);
        }
    }
}
