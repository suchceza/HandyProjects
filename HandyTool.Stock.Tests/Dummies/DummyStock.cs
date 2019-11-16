namespace HandyTool.Stock.Tests.Dummies
{
    internal class DummyStock : IStock
    {
        private readonly string m_Name;
        private readonly string m_Service;
        private readonly string m_Tag;
        private readonly string m_SourceUrl;

        public DummyStock()
        {
        }

        public DummyStock(string name, string service, string tag, string sourceUrl)
        {
            m_Name = name;
            m_Service = service;
            m_Tag = tag;
            m_SourceUrl = sourceUrl;
        }

        string IStock.Name => m_Name;

        string IStock.Service => m_Service;

        string IStock.Tag => m_Tag;

        string IStock.SourceUrl => m_SourceUrl;
    }
}