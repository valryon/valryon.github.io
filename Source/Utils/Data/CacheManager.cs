using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Portfolio.Utils.Log;

namespace Portfolio.Utils.Data
{
    /// <summary>
    /// Informations pour le cache
    /// </summary>
    public struct CacheInfo
    {
        /// <summary>
        /// Taille de la mémoire que le cache peut utiliser (en Mb)
        /// </summary>
        public long CacheMemoryLimit;

        /// <summary>
        /// Pourcentage de la mémoire que le cache peut utiliser
        /// </summary>
        public long PhysicalMemoryLimit;

        /// <summary>
        /// Trucs stockés dans le cache par le système
        /// </summary>
        public List<string> SystemCacheInfos;

        /// <summary>
        /// Trucs stockés dans le cache par le site 
        /// </summary>
        public Dictionary<string, ICachedData> CachedDataInfos;
    }

    /// <summary>
    /// Gestionnaire de cache (singleton)
    /// </summary>
    public sealed class CacheManager
    {
        public static TimeSpan DefaultCacheDuration = new TimeSpan(1, 0, 0);

        private static CacheManager m_instance;
        private static object m_creationLock = new object();
        private InternalCache m_cache;
        

        private CacheManager()
        {
            m_cache = new InternalCache();
        }

        private static CacheManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_creationLock)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new CacheManager();
                        }
                    }
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Récupération de données typées mise en cache (elles seront chargées au besoin)
        /// </summary>
        /// <param name="cacheKey">Identifiant de l'élément en cache</param>
        /// <returns></returns>
        public static D Get<D>(string cacheKey)
        {
            return Instance.m_cache.Get<D>(cacheKey);
        }

        /// <summary>
        /// Ajout de données à mettre en cache
        /// </summary>
        /// <param name="cacheKey">Identifiant de l'élément en cache</param>
        /// <param name="loadData">Fonction permettant de charger les éléments</param>
        /// <param name="duration">(Optionnel) durée de conservation dans le cache</param>
        /// <param name="deleteAfterExpiration">(Optionnel) supprime la donnée quand elle expire. Par défaut, la donnée est conservée et est rechargée sur demande</param>
        public static void Set<D>(string cacheKey, Func<D> loadData, TimeSpan? duration, bool deleteAfterExpiration = false)
        {
            Instance.m_cache.Add(cacheKey, loadData, duration, deleteAfterExpiration);
        }

        /// <summary>
        /// Force le rechargement des données en cache
        /// </summary>
        /// <param name="cachekey"></param>
        public static void ReloadAll()
        {
            Instance.m_cache.ReloadAll();
        }

        /// <summary>
        /// Force le rechargement de données en cache
        /// </summary>
        /// <param name="cachekey"></param>
        public static void Reload(string cachekey)
        {
            Instance.m_cache.Reload(cachekey);
        }

        /// <summary>
        /// Teste l'existence d'une clé dans le cache
        /// </summary>
        /// <param name="cachekey"></param>
        /// <returns></returns>
        public static bool TestKey(string cachekey)
        {
            return Instance.m_cache.ContainsKey(cachekey);
        }

        /// <summary>
        /// Supprime l'élément à la clé donnée
        /// </summary>
        /// <param name="cachekey"></param>
        /// <returns></returns>
        public static void Remove(string cachekey)
        {
            Instance.m_cache.Remove(cachekey);
        }

        /// <summary>
        /// Récupération des informations relatives au cache
        /// </summary>
        /// <returns></returns>
        public static CacheInfo GetInfo()
        {
            return Instance.m_cache.GetInfo();
        }
    }

    /// <summary>
    /// Moteur interne du cache
    /// </summary>
    internal sealed class InternalCache
    {
        // Récupération de l'instance du MemoryCache
        private MemoryCache cache;
        private Dictionary<string, object> m_locks = new Dictionary<string, object>();
        private object m_lockLockGetter = new object();

        public InternalCache()
        {
            // On récupère le cache mémoire du framework par défaut
            cache = MemoryCache.Default;
        }

        private object getLock(string key)
        {
            object lockObject = null;

            if (m_locks.TryGetValue(key, out lockObject) == false)
            {
                lock (m_lockLockGetter)
                {
                    if (m_locks.TryGetValue(key, out lockObject) == false)
                    {
                        lockObject = new object();
                        m_locks.Add(key, lockObject);
                    }
                }
            }

            return lockObject;
        }

        /// <summary>
        /// Récupération d'un objet dans le cache
        /// </summary>
        /// <param name="CacheKeyName"></param>
        /// <returns></returns>
        internal D Get<D>(String cacheKeyName)
        {
            // On récupère la structure de données mise en cache
            if (cache.Contains(cacheKeyName) == false)
            {
                // Si le cache est en train de lever l'évènement d'expiration, alors il ne contient plus la clé pendant quelques instants
                // Mais il lock le process, donc si c'est lui qui a le sémaphore alors quand il le débloquera le cache contiendra cette clé (sauf rechargement trop rapide)
                lock (getLock(cacheKeyName))
                {
                    if (cache.Contains(cacheKeyName) == false)
                    {
                        throw new ArgumentException("Unknow cache key: " + cacheKeyName);
                    }
                }
            }

            object data = cache[cacheKeyName];

            if (data is CachedData<D>)
            {
                CachedData<D> cachedData = data as CachedData<D>;

                // Si les données ont expiré, il faut les recharger
                if (cachedData.HasExpired)
                {
                    lock (getLock(cacheKeyName))
                    {
                        if (cachedData.HasExpired)
                        {
                            Update(cacheKeyName, cachedData);
                        }
                    }
                }

                // Si après rechargement les données sont vides, il peut s'agir d'une erreur
                if (equalsDefaultValue<D>(cachedData.Data))
                {
                    Logger.Log(LogLevel.Debug, "Aucune donnée récupérée dans le cache pour la clé \"" + cacheKeyName + "\".");
                }

                return cachedData.Data;
            }
            else
            {
                throw new ArgumentException("Invalid cache object type. Parameter:" + typeof(D) + " but Cache object: " + data.GetType());
            }
        }

        /// <summary>
        /// Teste l'égalité entre un valeur template et sa valeur null (ex : int et 0, objet et null)
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool equalsDefaultValue<D>(D value)
        {
            return EqualityComparer<D>.Default.Equals(value, default(D));
        }

        /// <summary>
        /// Ajout d'un objet dans le cache
        /// </summary>
        internal void Add<D>(String cacheKey, Func<D> loadData, TimeSpan? duration, bool deleteAfterExpiration)
        {
            // Ajouter des données au cache
            // Seulement si la clé n'existe pas déjà
            if (cache.Contains(cacheKey) == false)
            {
                Logger.Log(LogLevel.Debug, string.Format("Demande Ajout Cache {0}", cacheKey));

                // ATTENTION : On est peut-être en train de faire la mise à jour de l'élément -> lock
                lock (getLock(cacheKey))
                {
                    if (cache.Contains(cacheKey) == false)
                    {
                        // Paramètres de gestion du cache
                        // -- Expiration
                        DateTimeOffset expirationDate = DateTimeOffset.Now;
                        if (duration.HasValue)
                        {
                            expirationDate = expirationDate.Add(duration.Value);
                        }
                        else
                        {
                            expirationDate = expirationDate.Add(CacheManager.DefaultCacheDuration);
                        }

                        CacheItemPolicy policy = createPolicy(expirationDate, CacheItemPriority.Default);

                        // Préparation des données à stocker
                        CachedData<D> cachedData = new CachedData<D>();
                        cachedData.LoadData += loadData;
                        cachedData.AddedToCacheDate = DateTimeOffset.Now;
                        cachedData.ExpirationDate = expirationDate;
                        cachedData.UpdateFrequency = duration.HasValue ? duration.Value : CacheManager.DefaultCacheDuration;
                        cachedData.DeleteAfterExpiration = deleteAfterExpiration;

                        // Stockage de la structure de données
                        cache.Set(cacheKey, cachedData, policy);

                        Logger.Log(LogLevel.Info, "Cache : Ajout élément \"" + cacheKey + "\"");
                    }
                    else
                        Logger.Log(LogLevel.Debug, string.Format("Demande Ajout Cache {0} déjà faite", cacheKey));
                }
            }
        }

        /// <summary>
        /// Suppression d'une données en cache
        /// </summary>
        /// <param name="cacheKeyName"></param>
        internal void Remove(string cacheKeyName)
        {
            lock (getLock(cacheKeyName))
            {
                cache.Remove(cacheKeyName);
            }
        }

        /// <summary>
        /// Rechargement des données en cache
        /// </summary>
        /// <param name="cacheKeyName"></param>
        internal void ReloadAll()
        {
            foreach (var entry in cache)
            {
                Reload(entry.Key);
            }
        }

        /// <summary>
        /// Rechargement d'une donnée
        /// </summary>
        /// <param name="cacheKeyName"></param>
        internal void Reload(string cacheKeyName)
        {
            if (cache.Contains(cacheKeyName))
            {
                lock (getLock(cacheKeyName))
                {
                    if (cache.Contains(cacheKeyName))
                    {
                        if (cache[cacheKeyName] is ICachedData)
                        {
                            ICachedData data = (ICachedData)cache[cacheKeyName];
                            Update(cacheKeyName, data);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Mise à jour d'une donnée. Elle doit être présente dans le cache !
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        internal void Update(string key, ICachedData data)
        {
            Logger.Log(LogLevel.Info, "Cache : élement \"" + key + "\" -> mise à jour");

            data.ForceLoadData();

            DateTimeOffset expirationDate = DateTimeOffset.Now + data.UpdateFrequency;
            CacheItemPolicy policy = createPolicy(expirationDate, CacheItemPriority.Default);

            // On remet en cache après avoir rechargé les données
            data.ExpirationDate = expirationDate;
            cache.Set(key, data, policy);

            Logger.Log(LogLevel.Info, "Cache : élement \"" + key + "\" -> mise à jour terminée !");
        }

        internal bool ContainsKey(string cachekey)
        {
            return cache.Contains(cachekey);
        }

        private CacheItemPolicy createPolicy(DateTimeOffset expirationDate, CacheItemPriority priority)
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            // -- Priorité
            policy.Priority = priority;

            // -- Mise à jour
            //policy.UpdateCallback = new CacheEntryUpdateCallback(cacheItemUpdateCallback);
            policy.RemovedCallback = new CacheEntryRemovedCallback(cacheItemRemoveCallback);

            policy.AbsoluteExpiration = expirationDate;

            return policy;
        }

        private void cacheItemUpdateCallback(CacheEntryUpdateArguments arguments)
        {
            // ATTENTION : L'élément de cache est supprimé après l'appel à cet évènement !!!
            Logger.Log(LogLevel.Info, "Cache updatecallback : élement \"" + arguments.Key + "\" " + arguments.RemovedReason.ToString());
        }

        private void cacheItemRemoveCallback(CacheEntryRemovedArguments arguments)
        {
            // L'élément vient d'être supprimé
            if (arguments.RemovedReason == CacheEntryRemovedReason.Expired)
            {
                if (arguments.CacheItem != null && arguments.CacheItem.Value is ICachedData)
                {
                    Logger.Log(LogLevel.Info, "Cache : élement \"" + arguments.CacheItem.Key + "\" expiré.");

                    ICachedData data = arguments.CacheItem.Value as ICachedData;

                    if (data.DeleteAfterExpiration == false)
                    {
                        lock (getLock(arguments.CacheItem.Key))
                        {
                            // On remet l'élément de cache mais sans charger les données
                            // Comme cela on garde en mémoire la fonction de rechargement
                            data.HasExpired = true;
                            data.CleanData();

                            CacheItemPolicy policy = new CacheItemPolicy();
                            policy.Priority = CacheItemPriority.NotRemovable; // Force le côté non effaçable + pas de date d'expiration

                            cache.Set(arguments.CacheItem.Key, data, policy);
                        }
                    }
                    else
                    {
                        if (m_locks.ContainsKey(arguments.CacheItem.Key))
                        {
                            lock (m_lockLockGetter)
                            {
                                if (m_locks.ContainsKey(arguments.CacheItem.Key))
                                {
                                    m_locks.Remove(arguments.CacheItem.Key);
                                }
                            }
                        }

                        Logger.Log(LogLevel.Info, "Cache : élement \"" + arguments.CacheItem.Key + "\" supprimé définitivement du cache.");
                    }
                }
                else if (arguments.RemovedReason != CacheEntryRemovedReason.Removed) // Removed == écrasé par de nouvelles valeurs, comme avec l'update
                {
                    // Cas non géré donc erreur
                    Logger.Log(LogLevel.Warning, "Cache : élement \"" + arguments.CacheItem.Key + "\" " + arguments.RemovedReason.ToString());
                }
            }
        }

        /// <summary>
        /// Récupération des informations relatives au cache
        /// </summary>
        /// <returns></returns>
        internal CacheInfo GetInfo()
        {
            CacheInfo info = new CacheInfo();

            info.PhysicalMemoryLimit = cache.PhysicalMemoryLimit;
            info.CacheMemoryLimit = cache.CacheMemoryLimit;
            info.SystemCacheInfos = new List<string>();
            info.CachedDataInfos = new Dictionary<string, ICachedData>();

            foreach (var entry in cache)
            {
                if (entry.Value is ICachedData)
                {
                    ICachedData data = entry.Value as ICachedData;
                    info.CachedDataInfos.Add(entry.Key, data);
                }
                else
                {
                    info.SystemCacheInfos.Add(entry.Key);
                }
            }

            return info;
        }
    }
}
