using System;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes
{
    public class RegionAttribute : Attribute
    {
        /// <summary>
        /// Region name
        /// </summary>
        public string Name { get; private set; }

        public RegionAttribute(string name)
        {
            Name = name;
        }        
    }
}
