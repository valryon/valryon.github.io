using System;
using Portfolio.Utils.Log;

namespace Portfolio.Utils.Data
{
    /// <summary>
    /// Interface pour les données qui peuvent être mises en cache
    /// </summary>
    public interface ICachedData
    {
        void ForceLoadData();
        void CleanData();
        DateTimeOffset AddedToCacheDate { get; set; }
        DateTimeOffset ExpirationDate { get; set; }
        TimeSpan UpdateFrequency { get; set; }
        bool HasExpired { get; set; }
        bool DeleteAfterExpiration { get; set; }
    }

    /// <summary>
    /// Données cachable avec leur méthode de chargement
    /// </summary>
    /// <typeparam name="D"></typeparam>
    public class CachedData<D> : ICachedData
    {
        private D m_data;
        private object m_lock = new object();

        public CachedData()
        {
            HasExpired = true;
            DeleteAfterExpiration = false;
        }

        /// <summary>
        /// Données stockées
        /// </summary>
        public D Data
        {
            get
            {
                //if (EqualsDefaultValue(m_data))
                //{
                //    lock (m_lock)
                //    {
                //        // On charge au besoin les données
                //        if (EqualsDefaultValue(m_data))
                //        {
                //            m_data = LoadData();
                //        }
                //    }
                //}

                return m_data;
            }
        }



        /// <summary>
        /// Chargement spécifique des données
        /// </summary>
        /// <returns></returns>
        public event Func<D> LoadData;

        /// <summary>
        /// Date de mise en cache
        /// </summary>
        public DateTimeOffset AddedToCacheDate { get; set; }

        /// <summary>
        /// Date d'expiration
        /// </summary>
        public DateTimeOffset ExpirationDate { get; set; }

        /// <summary>
        /// Fréquence de mise à jour
        /// </summary>
        public TimeSpan UpdateFrequency { get; set; }

        /// <summary>
        /// Le cache a expiré
        /// </summary>
        public bool HasExpired { get; set; }

        /// <summary>
        /// Définit si le moteur de cache doit laisser la donnée se supprimer après expiration (par défaut elle est conservée mais non rechargée tant qu'elle n'est pas réutilisée)
        /// </summary>
        public bool DeleteAfterExpiration { get; set; }

        /// <summary>
        /// Force le (re)chargement des données
        /// </summary>
        public void ForceLoadData()
        {
            D oldData = m_data;
            try
            {
                m_data = LoadData();
                HasExpired = false;
            }
            catch (Exception e)
            {
                Logger.LogException(LogLevel.Error, "CacheData<" + typeof(D).FullName + ">.ForceLoadData", e);
                m_data = oldData;
            }
        }

        public void CleanData()
        {
            m_data = default(D);
        }
    }
}
