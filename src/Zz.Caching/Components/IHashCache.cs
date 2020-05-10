using System;
using System.Collections.Generic;
using System.Text;

namespace Zz.Caching.Components
{
    public interface IHashCache //: IKeyCache
    {
        #region Increment or Decrement
        long Decrement(string key, string field, long value = 1);

        double Decrement(string key, string field, double value = 1);

        long Increment(string key, string field, long value = 1);

        double Increment(string key, string field, double value = 1);
        #endregion


        /// <summary>
        /// 删除 key中的field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        bool Delete(string key, string field);

        /// <summary>
        /// 删除 key中多个field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        long Delete(string key, params string[] fields);

        #region Get & Exists & Keys & Scan
        /// <summary>
        /// 检查 key中的field 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        bool Exists(string key, string field);

        /// <summary>
        /// 获取 key中field的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/hget</remarks>
        string Get(string key, string field);

        /// <summary>
        /// 获取 key中多个field的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/hmget</remarks>
        string[] Get(string key, string[] field);

        /// <summary>
        /// 获取 key中所有field和value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/hgetall</remarks>
        HashEntry[] GetAll(string key);

        /// 获取 key中所有的Field,
        /// 作用同  <see cref="Keys"/>（仅命名不同）
        /// <remarks>https://redis.io/commands/hkeys</remarks>
        string[] GetFields(string key);

        /// <summary>
        /// 获取 key中所有的Field,
        /// 作用同 <see cref="GetFields"/>（仅命名不同）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/hkeys</remarks>
        [Obsolete("建议使用 string[] GetFields(string key)", false)]
        string[] Keys(string key);

        /// <summary>
        /// 获取 key中field的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/hlen</remarks>
        long Length(string key);

        /// <summary>
        /// 迭代查找 匹配的field和value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fieldPattern"></param>
        /// <param name="pageSize">每次迭代请求的个数（Redis中该参数不够精确，每次返回数量的可能或多或少）</param>
        /// <returns></returns>
        IEnumerable<HashEntry> ScanFields(string key, string fieldPattern, int pageSize);
        #endregion

        #region Set
        /// <summary>
        /// 设置 hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileds"></param>
        /// <remarks>https://redis.io/commands/hset</remarks>
        bool Set(string key, string field, string value);

        /// <summary>
        /// 设置 hash值，且仅当field不存在时，才能设置成功
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        bool SetIfNotExists(string key, string field, string value);

        /// <summary>
        /// 设置 hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileds"></param>
        /// <remarks>https://redis.io/commands/hmset</remarks>
        bool Set(string key, params HashEntry[] fileds);

        /// <summary>
        /// 获取 hash中所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string[] GetValues(string key);
        #endregion
    }
}
