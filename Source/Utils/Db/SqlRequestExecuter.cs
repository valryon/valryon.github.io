using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Portfolio.Utils.Log;

namespace Portfolio.Utils.Db
{
    /// <summary>
    /// Classe simplifiant l'éxécution d'une requête SQL
    /// </summary>
    public class SqlRequestExecuter
    {
        /// <summary>
        /// Exécute la requête pour la connexion
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="reqSql"></param>
        public static void Execute(string connection, string reqSql, Action<SqlDataReader> callback)
        {
            Logger.Log(LogLevel.Debug, "SqlRequestExecuter: " + reqSql);

            using (SqlConnection currentConnection = new SqlConnection(connection))
            {
                currentConnection.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(reqSql, currentConnection))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (callback != null)
                        {
                            callback(dr);
                        }

                        dr.Close();
                        dr.Dispose();
                    }
                }
                catch (System.Exception)
                {
                    if (currentConnection != null)
                    {
                        currentConnection.Close();
                    }

                    throw;
                }
                finally
                {
                    if (currentConnection != null)
                    {
                        currentConnection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Exécute la préocdure stockée pour la connexion
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="procStockName"></param>
        public static void ExecutePS(string connection, string procStockName, Action<SqlCommand> setParameters, Action<SqlDataReader> callback)
        {
            Logger.Log(LogLevel.Debug, "SqlRequestExecuter: " + procStockName);

            using (SqlConnection currentConnection = new SqlConnection(connection))
            {
                currentConnection.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(procStockName, currentConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (setParameters != null) setParameters(cmd);

                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (callback != null)
                        {
                            callback(dr);
                        }

                        dr.Close();
                        dr.Dispose();
                    }
                }
                catch (System.Exception)
                {
                    if (currentConnection != null)
                    {
                        currentConnection.Close();
                    }

                    throw;
                }
                finally
                {
                    if (currentConnection != null)
                    {
                        currentConnection.Close();
                    }
                }
            }
        }

        public static int ExecutePSNonQuery(string connection, string procStockName, Action<SqlCommand> setParameters)
        {
            Logger.Log(LogLevel.Debug, "SqlRequestExecuter: " + procStockName);

            int result = -1;

            using (SqlConnection currentConnection = new SqlConnection(connection))
            {
                currentConnection.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(procStockName, currentConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (setParameters != null) setParameters(cmd);

                        result = cmd.ExecuteNonQuery();
                    }
                }
                catch (System.Exception)
                {
                    if (currentConnection != null)
                    {
                        currentConnection.Close();
                    }

                    throw;
                }
                finally
                {
                    if (currentConnection != null)
                    {
                        currentConnection.Close();
                    }
                }
            }

            return result;
        }
    }
}
