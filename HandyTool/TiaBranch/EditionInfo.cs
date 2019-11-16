
namespace HandyTool.TiaBranch
{
    internal struct EditionInfo
    {
        //################################################################################
        #region Constructor

        public EditionInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }

        #endregion

        //################################################################################
        #region Properties

        public string Name { get; }

        public string Path { get; }

        #endregion

        //################################################################################
        #region Overrides

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
