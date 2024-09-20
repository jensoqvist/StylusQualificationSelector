using System;
using System.Collections.Generic;

namespace StylusQualificationSelector
{
    public class StylusList
    {
        #region Propertys
        /// <summary>
        /// Lists styluses user can choose to qualify
        /// </summary>
        public List<Stylus> Styluses { get; set; } = new List<Stylus>();
        /// <summary>
        /// Name of MasterProbe
        /// </summary>
        public string Master { get; set; }
        #endregion

        #region Constructor
        public StylusList()
        {


        }

        public StylusList(string master, string[] stylusListIn)
        {
            Master = master.Replace("'", "");
            foreach (string stylus in stylusListIn)
            {
                if(stylus.Replace("'", "") != Master)
                    Styluses.Add(new Stylus
                    {
                        Name = stylus.Replace("'", "")
                    });
            };
            Styluses.Sort((x, y) => x.Name.CompareTo(y.Name)); // Sorts list of styluses by name property
        }

        #endregion

        public override string ToString()
        {
            string description = string.Format("Master: {0}\n\n", Master);
            foreach (Stylus stylus in Styluses)
                description += string.Format("{0}\n", stylus.Name);
            return description;
        }
    }
}
