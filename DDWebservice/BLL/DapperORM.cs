using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DDWebservice.BLL
{
    public class DapperORM
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static int ExecuteInsert(string query, DynamicParameters parameters = null)
        {
            try
            {
                var affectedrows = -1;
                using (var sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    affectedrows = sqlCon.Execute(query, parameters);
                }

                return affectedrows;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:  // Unique constraint error
                    case 547:   // Constraint check violation
                    case 2601:
                        throw new Exception("User with the Same Email Already Exists");
                    default:
                        throw new Exception("There was an Error Process your request try Again Later");
                }
            }
           

        }
        /// <summary>
        /// Returns a Single object to Limit a breach of data to only one single element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T QueryGetSingle<T>(string query, DynamicParameters parameters = null)
        {
            using (var sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.QuerySingleOrDefault<T>(query, parameters, commandType: CommandType.Text);
            }
        }
    }
}