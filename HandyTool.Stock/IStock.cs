namespace HandyTool.Stock
{
    public interface IStock
    {
        string Name { get; }

        string Service { get; }

        string Tag { get; }

        string SourceUrl { get; }
    }
}
