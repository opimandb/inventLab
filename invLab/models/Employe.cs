using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace invLab
{
    public class Employe : INotifyPropertyChanged
    {
        private string fio;
        private string vacansy;
        private string faculty;
        public int id { get; set; }

        public string Fio
        {
            get { return fio; }
            set
            {
                fio = value;
                OnPropertyChanged("Fio");
            }
        }
        public string Vacansy
        {
            get { return vacansy; }
            set
            {
                vacansy = value;
                OnPropertyChanged("Vacansy");
            }
        }
        public string Faculty
        {
            get { return faculty; }
            set
            {
                faculty = value;
                OnPropertyChanged("Price");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
