using System.Collections.Generic;
using System.IO;


namespace StylusQualificationSelector
{
    class PcmWriter
    {
        private string fileName = @"\stylusQualificationPCM.txt";
        private string path = string.Empty;

        public List<string> Styluses { get; set; }
        public string Master { get; set; } = string.Empty;

        public PcmWriter(string path)
        {
            this.path = path + fileName;
        }


        public void Abort()
        {
            string data = "setCF(" + '"' + '"' + ")";
            File.WriteAllText(path, data);
        }

        public void WritePcm()
        {
            string data = "probeConfList = list(" + FormatData() + ")";
            File.WriteAllText(path, data);
        }

        public string FormatData()
        {
            string returnString = string.Empty;
            if (Master != string.Empty)
                returnString +=  '"' + Master + '"';
            foreach (string stylus in Styluses)
                if (returnString == string.Empty)
                    returnString +='"' + stylus + '"';
                else
                    returnString += ", " + '"' + stylus + '"';
            return returnString;
        }



    }
}
