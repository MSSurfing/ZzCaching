using System.Collections.Generic;
using Zz.Caching.Enums;

namespace Zz.Caching.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 不提供 毫秒过期时间
    /// 
    /// Todo：
    ///     1、 实现 <see cref="SetIfNotExists(string, string)"/> 设置 过期时间，参考 StackExchange.Redis
    ///     
    /// 注：原生命令中 在使用 StrLen 与 NX, NotExists 方法，要注意 有可能会有 key存在但无值 的情况。
    ///     场景如：
    ///         set key1 ''  ，即设key1 为 空值
    ///         strlen key1 得到 0，可能会被当作 key1 不存在
    ///         setnx key1 'value' ，结果 会一直失败，因为 key1 是存在的
    /// 
    ///     因此 在实现封装时，应避免 set 空值，这一块 StackExchange.Redis 做得很好
    /// 
    /// </remarks>
    public interface IStringCache
    {
        #region Get or Set
        /// <summary>
        /// 设置 Key值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间，为 -1 时，代表永不过期，为 0 时，代表删除该key</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/set 当 expiry = -1 时</remarks>
        /// <remarks>https://redis.io/commands/setex 当 expiry > 0 时</remarks>
        /// <remarks> ?? https://redis.io/commands/del 当 value == null 时</remarks>
        bool Set(string key, string value, int expiry = -1);

        /// <summary>
        /// 设置 Key值，且仅当该 key 不存在时，才能设置成功
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/setnx</remarks>
        bool SetIfNotExists(string key, string value); // , int expiry = -1，怎么实现支持同时设置过期时间

        /// <summary>
        /// 设置多个 key / value 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/mset</remarks>
        bool Set(KeyValuePair<string, string>[] keyValues);

        /// <summary>
        /// 设置多个 key / value, 且仅所有key都不存在时，才能设置成功
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/msetnx</remarks>
        bool SetIfNotExists(KeyValuePair<string, string>[] keyValues);

        /// <summary>
        /// 获取 Key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取 多个key 值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        string[] Get(string[] keys);

        /// <summary>
        /// 获取 部分值（子字符串值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">起始位置</param>
        /// <param name="end">截止位置</param>
        /// <returns></returns>
        string GetRange(string key, long start, long end);

        /// <summary>
        /// 获取 值的长度（key 不存在时 返回0）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>
        /// 不能 用此方法来验证 key 是否存在
        /// </remarks>
        long Length(string key);
        #endregion

        #region Get or Set bit
        /// <summary>
        /// 设置 特定位(bit) 的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        bool SetBit(string key, long offset, bool bit);

        /// <summary>
        /// 获取 特定位(bit) 的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        bool Getbit(string key, long offset);

        /// <summary>
        /// 统计 设置为1的位(bit) 的数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">起始统计位置</param>
        /// <param name="end">截止统计位置</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/bitcount</remarks>
        long BitCount(string key, long start = 0, long end = -1);

        /// <summary>
        /// 二进制操作 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="destinationKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        long BitOperation(Bitwise operation, string destinationKey, params string[] keys);

        /// <summary>
        /// 查找第一个 1 的位置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bit"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        long BitPosition(string key, bool bit, long start = 0, long end = -1);

        // long BitField();
        #endregion

        #region Increment or Decrement or Append
        /// <summary>
        /// 递增 相应数值（仅整数值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">增量（默认为1）</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/incrby</remarks>
        /// <remarks>https://redis.io/commands/incr</remarks>
        /// <remarks>
        /// 注：当 旧值 带有小数 时，使用整型 递增会异常
        ///     如：key1 值为 1.5 ，则 incr key1 时会异常，但 值为 1.0 时，不会异常
        ///     1.0 或 1.5 可能 由 incrbyfloat 操作增加的，因此 incrbyfloat 不应与 incr 共用在一个key上
        /// </remarks>
        long Increment(string key, long value = 1);

        /// <summary>
        /// 递增 相应数值（可小数值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">增量（浮点类型）</param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/incrbyfloat</remarks>
        double Increment(string key, double value = 1);

        /// <summary>
        /// 递减 相应数值（仅整数值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long Decrement(string key, long value = 1);

        /// <summary>
        /// 递减 相应数值（可小数值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double Decrement(string key, double value = 1);

        /// <summary>
        /// 追加 值到默认，并返回追加后值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="appendValue"></param>
        /// <returns></returns>
        long Append(string key, string appendValue);
        #endregion
    }
}
