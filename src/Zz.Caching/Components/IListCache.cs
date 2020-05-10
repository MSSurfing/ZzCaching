namespace Zz.Caching.Components
{
    public interface IListCache
    {
        string GetByIndex(string key, long index);

        /// <summary>
        /// 插入 值，在<paramref name="pivotValue"/>值之后
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pivotValue"></param>
        /// <param name="addValue"></param>
        /// <returns></returns>
        long AddAfter(string key, string pivotValue, string addValue);

        /// <summary>
        /// 插入 值，在<paramref name="pivotValue"/>值之前
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pivotValue"></param>
        /// <param name="addValue"></param>
        /// <returns></returns>
        long AddBefore(string key, string pivotValue, string addValue);

        #region Left Operation
        /// <summary>
        /// 移除并获取 第一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string LeftPop(string key);

        /// <summary>
        /// 插入 值 到列表头部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/lpush</remarks>
        long LeftPush(string key, string value);

        /// <summary>
        /// 插入 值 到列表头部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/lpush</remarks>
        long LeftPush(string key, string[] values);


        /// <summary>
        /// 插入 值 到列表头部，仅当列表已经存在时；如果列表不存在，则插入失败
        /// </summary>
        long LeftPushIfExists(string key, string value);
        #endregion

        /// <summary>
        /// 获取 列表长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>https://redis.io/commands/llen</remarks>
        long Length(string key);

        /// <summary>
        /// 获取 列表指定范围内的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        string[] GetRange(string key, long start = 0, long stop = -1);

        /// <summary>
        /// 移除 列表元素，根据参数 <paramref name="count"/> 的值，移除列表中与参数 <paramref name="value"/> 相等的元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="count">
        /// count > 0 : 从表头开始向表尾搜索，移除与 VALUE 相等的元素，数量为 COUNT 。
        /// count< 0 : 从表尾开始向表头搜索，移除与 VALUE 相等的元素，数量为 COUNT 的绝对值。
        /// count = 0 : 移除表中所有与 VALUE 相等的值。
        /// </param>
        /// <returns></returns>
        long Remove(string key, string value, long count = 0);

        #region Right Operation
        /// <summary>
        /// 移除并获取 最后一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string RightPop(string key);

        /// <summary>
        /// 插入 值 到列表尾部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long RightPush(string key, string value);

        /// <summary>
        /// 插入 值 到列表尾部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        long RightPush(string key, string[] values);

        /// <summary>
        /// 插入 值 到列表尾部，仅当列表已经存在时；如果列表不存在，则插入失败
        /// </summary>
        long RightPushIfExists(string key, string value);

        /// <summary>
        /// 转移元素，移除<paramref name="popSource"/>列表的最后一个元素，并将该元素添加到另一个列表<paramref name="pushDestination"/>的表头，并返回
        /// </summary>
        /// <param name="popSource"></param>
        /// <param name="pushDestination"></param>
        /// <returns></returns>
        string RightPopLeftPush(string popSource, string pushDestination);
        #endregion
    }
}
