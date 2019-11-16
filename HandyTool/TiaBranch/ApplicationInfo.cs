
namespace HandyTool.TiaBranch
{
    internal struct ApplicationInfo
    {
        //################################################################################
        #region Constructor

        public ApplicationInfo(string name, string path)
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
