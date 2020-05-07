namespace Zz.Caching
{
    /// <summary>
    /// 数据类型
    /// </summary>
    /// <remarks>https://redis.io/topics/data-types</remarks>
    public enum DataType
    {
        /// <summary>
        /// 键不存在
        /// </summary>
        None,
        /// <summary>
        /// String 类型
        /// </summary>
        String,
        /// <summary>
        /// List
        /// </summary>
        List,
        /// <summary>
        /// Set
        /// </summary>
        Set,
        /// <summary>
        /// Sorted Set
        /// </summary>
        ZSet,
        /// <summary>
        /// Hash
        /// </summary>
        Hash,
        /// <summary>
        /// Stream
        /// </summary>
        Stream,
        /// <summary>
        /// 未知类型
        /// </summary>
        Unknown,
    }
}
