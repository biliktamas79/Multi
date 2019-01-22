namespace Multi.Data
{
    /// <summary>
    /// Class describing an ordering represented by a name, direction and character casing.
    /// </summary>
    public class OrderBy
    {
        public OrderBy()
        { }

        public OrderBy(string name, OrderEnum order, CharacterCasingEnum casing = CharacterCasingEnum.Unchanged)
        {
            this.Name = name;
            this.Order = order;
            this.Casing = casing;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public OrderEnum Order { get; set; }
        /// <summary>
        /// Gets or sets the casing.
        /// </summary>
        /// <value>
        /// The casing.
        /// </value>
        public CharacterCasingEnum Casing { get; set; }
    }
}
