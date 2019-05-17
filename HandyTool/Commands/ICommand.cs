namespace HandyTool.Commands
{
    internal interface ICommand
    {
        string Output { get; }

        void Execute();
    }
}
