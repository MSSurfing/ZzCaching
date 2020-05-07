namespace Zz.Caching.Keys
{
    public interface IKeyCache
    {
        /// <summary>
        /// 删除 key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string key);

        /// <summary>
        /// 删除 多个key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>被删除 key 的数量</returns>
        long DeleteKeys(params string[] keys);

        /// <summary>
        /// 序列化 Key 的值 并返回
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Dump(string key);

        /// <summary>
        /// 检查 Key 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 检查 多个key 是否存在
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>被 key 的数量</returns>
        long ExistsKeys(params string[] keys);

        /// <summary>
        /// 设置 key 的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">过期时间（秒）</param>
        /// <returns></returns>
        bool Expire(string key, int seconds);

        /// <summary>
        /// 设置 key 的过期时间（毫秒）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">过期时间（毫秒）</param>
        /// <returns></returns>
        bool PExpire(string key, int milliseconds);

        /* 使用 PExpire 实现以下扩展 */
        // Todo Extension Expire(string key, TimeSpan? expiry);
        // Todo Extension Expire(string key, DateTime? expiry);

        /// <summary>
        /// 移动 key 到另一个数据库db中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Move(string key, int database);

        /// <summary>
        /// 移除 key 的过期时间, 移除后 key 将永不过期
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Persist(string key);

        /// <summary>
        /// 返回 一个随机key（从当前数据库中返回）
        /// </summary>
        /// <returns></returns>
        string RandomKey();

        /// <summary>
        /// 查找 匹配的key
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        string[] Keys(string pattern);

        /// <summary>
        /// 查找 匹配的key
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        string[] Keys(int database, string pattern);

        // 不提供 PTTL 方法

        /// <summary>
        /// 返回 key 的剩余过期时间（秒）
        /// TTT, Time to live
        /// </summary>
        /// <returns>剩余时间（key 不存在 返回 -2, key存在且永不过期 返回 -1, 其他均返回 剩余时间, 单位：秒）</returns>
        int TTL(string key);

        /// <summary>
        /// 修改 key名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        bool Rename(string key, string newKey);

        /// <summary>
        /// 修改 key名称（仅当 newKey 不存在时）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        bool RenameNotExists(string key, string newKey);

        /// <summary>
        /// 获取 key数据类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        DataType Type(string key);

        /// <summary>
        /// 获取 key的数据类型（以原文本返回）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string TypeString(string key);
    }
}
