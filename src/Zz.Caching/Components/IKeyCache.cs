using System;
using System.Collections.Generic;

namespace Zz.Caching.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 1、不提供 keys 操作
    /// </remarks>
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
        /// <remarks>https://redis.io/commands/keys</remarks>
        [Obsolete("大数据量时，建议使用 IEnumerable<string> ScanKeys(string pattern, int pageSize)", false)]
        string[] Keys(string pattern);

        /// <summary>
        /// 查找 匹配的key
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        [Obsolete("大数据量时，建议使用 IEnumerable<string> ScanKeys(int database, string pattern, int pageSize)", false)]
        string[] Keys(int database, string pattern);

        /// <summary>
        /// 迭代查找 匹配的key
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="pageSize">每次迭代请求的个数（Redis中该参数不够精确，每次返回数量的可能或多或少）</param>
        /// <returns></returns>
        /// <remarks>
        /// Scan方法会分多次请求，每次请求 <paramref name="pageSize"/> 个数据分多次请求直到请求完所有数据。
        /// 请求是内部实现的，开发者不感知，只需要：ScanKeys().ToList(); 
        /// 数据量较大时 建议用方式
        /// foreach(var key in ScanKeys()) 
        /// {  
        ///     /* code */ 
        /// }
        /// </remarks>
        IEnumerable<string> ScanKeys(string pattern, int pageSize);

        /// <summary>
        /// 迭代查找 匹配的key
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize">每次迭代请求的个数（Redis中该参数不够精确，每次返回数量的可能或多或少）</param>
        /// <returns></returns>
        /// <remarks>
        /// Scan方法会分多次请求，每次请求 <paramref name="pageSize"/> 个数据分多次请求直到请求完所有数据。
        /// 请求是内部实现的，开发者不感知，只需要：ScanKeys().ToList(); 
        /// 数据量较大时 建议用方式
        /// foreach(var key in ScanKeys()) 
        /// {  
        ///     /* code */ 
        /// }
        /// </remarks>
        IEnumerable<string> ScanKeys(int database, string pattern, int pageSize);

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
        bool RenameIfNotExists(string key, string newKey);

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
