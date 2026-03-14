namespace ESNMatcherCreator.Models
{
    /// <summary>
    /// Модель сопоставителя
    /// </summary>
    public class MatcherModel
    {
        #region Properties
        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Тикер
        /// </summary>
        public string Ticker { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор тикера
        /// </summary>
        public int TickerId { get; set; }

        /// <summary>
        /// Поставщик ликвидности
        /// </summary>
        public string LP { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор поставщика ликвидности
        /// </summary>
        public int LpId { get; set; }
        #endregion
    }
}
