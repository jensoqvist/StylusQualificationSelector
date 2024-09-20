using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StylusQualificationSelector
{
    class MainViewModel: BaseViewModel
    {

        #region private fields
        private ObservableCollection<Stylus> styluses;
        private bool qualifyAll = false;
        private string master = string.Empty;
        private bool enableSelectionButton = false;
        private int selection = -1;
        private string path;
        private string qualifyAllText;
        private string qualifySelectedText;
        private string abortText;
        private string stylusesText;
        private string text;
        private string qualifyMaster;
        private bool qualifyMasterBool = true;
        #endregion

        #region Public Propertys
        public ObservableCollection<Stylus> Styluses { get => styluses; set { styluses = value; OnPropertyChanged(); } }
        public bool EnableSelectionButton { get => enableSelectionButton; set { enableSelectionButton = value; OnPropertyChanged(); } }

        public int Selection
        {
            get => selection;
            set
            {
                selection = value;
                if (value != -1)
                    EnableSelectionButton = true;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Button names
        /// </summary>
        public string QualifyAllText { get => qualifyAllText; set { qualifyAllText = value; OnPropertyChanged(); } } 
        public string QualifySelectedText { get => qualifySelectedText; set { qualifySelectedText = value; OnPropertyChanged(); } } 
        public string AbortText { get => abortText; set { abortText = value; OnPropertyChanged(); } } 
        public string StylusesText { get => stylusesText; set { stylusesText = value; OnPropertyChanged(); } }
        public string Text { get => text; set { text = value; OnPropertyChanged(); } }
        public string QualifyMaster { get => qualifyMaster; set { qualifyMaster = value; OnPropertyChanged(); } }
        public bool QualifyMasterBool { get => qualifyMasterBool; set { qualifyMasterBool = value; OnPropertyChanged(); } }

        #endregion

        #region Commands
        public ICommand QualifySelected { get; set; }
        public ICommand QualifyAll { get; set; }
        public ICommand Abort { get; set; }
        #endregion

        #region Constructor
        public MainViewModel(string[] args)
        {
            this.path = args[0];
            string lang = args[1];
            master = args[2];
            string[] styluses = args.Skip(3).ToArray();
            Debug.Print("-----------------");
            Debug.Print(styluses.ToString());
            foreach(string s in styluses)
                Debug.Print(s);
            Debug.Print("-----------------");

            StylusList stylusList = new StylusList(master, styluses);
            Styluses = new ObservableCollection<Stylus> (stylusList.Styluses);
            master = stylusList.Master;
            
            QualifySelected = new RelayCommand(QualifySelection);
            QualifyAll = new RelayCommand(QualifyAllStyluses);
            Abort = new RelayCommand(AbortQualification);

            setLang(lang);
            

        }
        #endregion

        #region Methods

        private void setLang(string lang)
        {
            CultureInfo cultureInfo = new CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            QualifyAllText = Resources.localization.qualifyAll;
            QualifySelectedText = Resources.localization.qualifySelected;
            AbortText = Resources.localization.abort;
            StylusesText = Resources.localization.styluses;
            Text = Resources.localization.text;
            QualifyMaster = Resources.localization.qualifyMasterText;
        }

        private string getSelection()
        {
            string selectionString = string.Empty;
            foreach (Stylus stylus in Styluses)
            {
                if (this.qualifyAll)
                    selectionString += string.Format("{0}\n", stylus.Name);
                else if (stylus.IsSelected)
                    selectionString += string.Format("{0}\n", stylus.Name);
            }
            return selectionString;
        }

        private List<string> getStylusSelection()
        {
            List<string> selection = new List<string>();
            foreach (Stylus stylus in Styluses)
            {
                if (this.qualifyAll)
                    selection.Add(stylus.Name);
                else if (stylus.IsSelected)
                    selection.Add(stylus.Name);
            }
            return selection;
        }

        private void QualifySelection()
        {
            this.qualifyAll = false;
            PcmWriter pcmWriter = new PcmWriter(path);
            if (QualifyMasterBool == true)
                pcmWriter.Master = master;
            pcmWriter.Styluses = getStylusSelection();
            pcmWriter.WritePcm();
            Environment.Exit(0);
        }

        private void QualifyAllStyluses()
        {
            this.qualifyAll = true;
            PcmWriter pcmWriter = new PcmWriter(path)
            {
                Master = master,
                Styluses = getStylusSelection()
            };
            pcmWriter.WritePcm();
            Environment.Exit(0);
        }

        private void AbortQualification()
        {
            new PcmWriter(path).Abort();
            Application.Current.MainWindow.Close();
        }

        #endregion


    }
}
