namespace HandyTool.Stock.StockInfo
{
    internal class Stock : IStock
    {
        //################################################################################
        #region Fields

        private readonly string m_Name;
        private readonly string m_Service;
        private readonly string m_Tag;
        private readonly string m_Url;

        #endregion

        //################################################################################
        #region Constructor

        public Stock(string name, string service, string tag, string url)
        {
            m_Name = name;
            m_Service = service;
            m_Tag = tag;
            m_Url = url;
        }

        #endregion

        //################################################################################
        #region IStock Members

        string IStock.Name => m_Name;

        string IStock.Service => m_Service;

        string IStock.Tag => m_Tag;

        string IStock.SourceUrl => m_Url;

        #endregion
    }
}
