namespace PblExporter
{
    /// <summary>
    /// デリミタを提供します。
    /// </summary>
    class Delimiter
    {
        /// <summary>
        /// デリミタ名を取得します。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// デリミタの実際の値を取得します。
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 新しい Delimiter インスタンスを生成します。
        /// </summary>
        /// <param name="name">デリミタ名。</param>
        /// <param name="value">デリミタの実際の値。</param>
        public Delimiter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
