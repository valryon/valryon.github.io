using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Portfolio.Utils.Log;

namespace Portfolio.Utils.Db
{
    /// <summary>
    /// Classe Dao Générique
    /// </summary>
    /// <typeparam name="T">Type d'objet retournés</typeparam>
    public abstract class GenericDao<T>
    {
        /// <summary>
        /// La chaine de connexion à utiliser
        /// </summary>
        protected string ConnectionString;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="connectionString">Chaine de connexion à utiliser</param>
        protected GenericDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Permet de récupérer un élément par son identifiant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T ReadById(int id)
        {
            return default(T);
        }


        /// <summary>
        /// Permet de créer un instance au sein de la base de données
        /// </summary>
        /// <param name="obj">Objet d'instance à créer</param>
        /// <returns>Création réussie ?</returns>
        public virtual bool Create(T obj)
        {
            return false;
        }

        /// <summary>
        /// Permet de mettre à jour un objet avec la base de données
        /// </summary>
        /// <param name="obj">Objet à mettre à jour</param>
        /// <returns>Mis à jour réussie ?</returns>
        public virtual bool Update(T obj)
        {
            return false;
        }

        /// <summary>
        /// Permet de supprimer un objet de la base de données
        /// </summary>
        /// <param name="obj">Object à supprimer</param>
        /// <returns>Suppression réussie ?</returns>
        public virtual bool Delete(T obj)
        {
            return false;
        }

        /// <summary>
        /// Permet de convertir un SqlDataRader en liste d'objet Typé
        /// </summary>
        /// <param name="reader">Reader à convertir</param>
        /// <returns>Retourne une liste d'object typé</returns>
        protected virtual List<T> MapData(SqlDataReader reader)
        {
            return null;
        }

        /// <summary>
        /// Crée un paramètre pour les requêtes
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected SqlParameter CreateParameter(string paramName, object value)
        {
            return new SqlParameter(paramName, value);
        }


        /// <summary>
        /// Exécuter une requête SQL et parser le résultat
        /// </summary>
        /// <param name="strSql">Requête Sql à exéctuer</param>
        /// <param name="parameterList">Liste de paramètre à associer à la requête</param>
        /// <param param name="result">Liste des objets trouvés et mappés</returns>
        /// <returns>Vrai si tout s'est bien passé, faux sinon</rereturns>
        protected bool ExecuteReader(string strSql, CommandType type, out List<T> result, params SqlParameter[] parameterList)
        {
            return ExecuteReader(strSql,type, out result, MapData, parameterList);
        }

        /// <summary>
        /// Exécuter une requête SQL et parser le résultat
        /// </summary>
        /// <param name="strSql">Requête Sql à exéctuer</param>
        /// <param name="parameterList">Liste de paramètre à associer à la requête</param>
        /// <param name="mapDataDelegate">Fonction de mapping des données.</param>
        /// <param param name="result">Liste des objets trouvés et mappés</returns>
        /// <returns>Vrai si tout s'est bien passé, faux sinon</rereturns>
        protected bool ExecuteReader(string strSql, CommandType type, out List<T> result, Func<SqlDataReader, List<T>> mapDataDelegate, params SqlParameter[] parameterList)
        {
            Logger.Log(LogLevel.Debug, string.Format("GenericDAO.ExecuteReader {0}", strSql));
            
            result = new List<T>();
            SqlConnection currentConnection = null;

            try
            {
                // Initialisation de la connexion
                using (currentConnection = new SqlConnection(this.ConnectionString))
                {
                    currentConnection.Open();

                    // Initialisation de la commande
                    using (SqlCommand cmd = new SqlCommand(strSql, currentConnection))
                    {
                        cmd.CommandType = type;
                        cmd.CommandTimeout = 120;

                        if (parameterList != null)
                        {
                            foreach (var item in parameterList)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }

                        // Exécution de la commande
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // récupération des données sous forme d'un datatable
                            if (mapDataDelegate != null)
                            {
                                result = mapDataDelegate(dr);
                            }

                            dr.Close();
                        }

                        return true;
                    }
                }
            }
            catch (System.Exception e)
            {
                Logger.Log(LogLevel.Error, "ExecuteSql: " + e.Message);
                LastException = e;
            }
            finally
            {
                if (currentConnection != null) currentConnection.Close();
            }

            return false;
        }

        /// <summary>
        /// Exécuter une requête SQL et parser le résultat
        /// </summary>
        /// <param name="strSql">Requête Sql à exéctuer</param>
        /// <param name="parameterList">Liste de paramètre à associer à la requête</param>
        /// <param name="callback">Fonction de mapping des données.</param>
        /// <param param name="result">Liste des objets trouvés et mappés</returns>
        /// <returns>Vrai si tout s'est bien passé, faux sinon</rereturns>
        protected bool ExecuteReader(string strSql, CommandType type, Action<SqlDataReader> callback, params SqlParameter[] parameterList)
        {
            Logger.Log(LogLevel.Debug, string.Format("GenericDAO.ExecuteReader {0}", strSql));

            SqlConnection currentConnection = null;

            try
            {
                // Initialisation de la connexion
                using (currentConnection = new SqlConnection(this.ConnectionString))
                {
                    currentConnection.Open();

                    // Initialisation de la commande
                    using (SqlCommand cmd = new SqlCommand(strSql, currentConnection))
                    {
                        cmd.CommandType = type;
                        cmd.CommandTimeout = 120;

                        if (parameterList != null)
                        {
                            foreach (var item in parameterList)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }

                        // Exécution de la commande
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // récupération des données sous forme d'un datatable
                            if (callback != null)
                            {
                                callback(dr);
                            }

                            dr.Close();
                        }

                        return true;
                    }
                }
            }
            catch (System.Exception e)
            {
                Logger.Log(LogLevel.Error, "ExecuteSql: " + e.Message);
                LastException = e;
            }
            finally
            {
                if (currentConnection != null) currentConnection.Close();
            }

            return false;
        }

        protected T2 ExecuteScalar<T2>(string strSql, CommandType type, params SqlParameter[] parameterList)
        {
            Logger.Log(LogLevel.Debug, string.Format("GenericDAO.ExecuteScalar {0}", strSql)); 

            SqlConnection currentConnection = null;
            try
            {
                // Initialisation de la connexion
                using (currentConnection = new SqlConnection(this.ConnectionString))
                {
                    // Ouverture de la connexion
                    currentConnection.Open();

                    // Initialisation de la commande
                    using (SqlCommand cmd = new SqlCommand(strSql, currentConnection))
                    {
                        cmd.CommandType = type;
                        if (parameterList != null)
                        {
                            foreach (var item in parameterList)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }

                        // Exécution de la commande
                        object tmpResult = cmd.ExecuteScalar();
                        T2 result = (T2)tmpResult;

                        return result;
                    }
                }

            }
            catch (System.Exception e)
            {
                Logger.Log(LogLevel.Error, "ExecuteSql: " + e.Message);
                LastException = e;
            }
            finally
            {
                if (currentConnection != null) currentConnection.Close();
            }

            return default(T2);
        }

        /// <summary>
        /// Exécuter une requête SQL et récupérer le nombre de lignes affectées
        /// </summary>
        /// <param name="strSql">Requête Sql à exéctuer</param>
        /// <param name="parameterList">Liste de paramètre à associer à la requête</param>
        /// <returns>Nombre de lignes affectées</returns>
        protected int ExecuteNonQuery(string strSql, CommandType type, params SqlParameter[] parameterList)
        {
            Logger.Log(LogLevel.Debug, string.Format("GenericDAO.ExecuteNonQuery {0}", strSql)); 

            SqlConnection currentConnection = null;
            try
            {
                // Initialisation de la connexion
                using (currentConnection = new SqlConnection(this.ConnectionString))
                {
                    currentConnection.Open();

                    // Initialisation de la commande
                    using (SqlCommand cmd = new SqlCommand(strSql, currentConnection))
                    {
                        cmd.CommandType = type;
                        if (parameterList != null)
                        {
                            foreach (var item in parameterList)
                            {
                                cmd.Parameters.Add(item);
                            }
                        }

                        // Exécution de la commande
                        int rows = cmd.ExecuteNonQuery();                        

                        return rows;
                    }
                }
                
            }
            catch (System.Exception e)
            {
                Logger.Log(LogLevel.Error, "ExecuteSql: " + e.Message);
                LastException = e;
            }
            finally
            {
                if (currentConnection != null) currentConnection.Close();
            }

            return -1;
        }

        /// <summary>
        /// Si une requête a retourné faux, l'exception métier sera stocké dans cette propriété
        /// </summary>
        public Exception LastException { get; protected set; }
    }
}
