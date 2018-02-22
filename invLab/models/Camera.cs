using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace invLab
{
    public class Camera : INotifyPropertyChanged
    {
        public int id { get; set; }
        private int roomid;
        private int empid;
        private string name;
        private string camera_type;
        private string matrix_type;
        private string focus;
        private string resolution;
        private string speed;
        private string night_mode;
        private string angle;
        private string weight;
        private double cost;
        private string start_date;
        private double using_term;
        private string finish_date;
        private string status;


        public int Roomid
        {
            get { return roomid; }
            set
            {
                roomid = value;
                OnPropertyChanged("Roomid");
            }
        }

        public int Empid
        {
            get { return empid; }
            set
            {
                empid = value;
                OnPropertyChanged("Empid");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Camera_type
        {
            get { return camera_type; }
            set
            {
                camera_type = value;
                OnPropertyChanged("Camera_type");
            }
        }

        public string Matrix_type
        {
            get { return matrix_type; }
            set
            {
                matrix_type = value;
                OnPropertyChanged("Matrix_type");
            }
        }
        public string Focus
        {
            get { return focus; }
            set
            {
                focus = value;
                OnPropertyChanged("Focus");
            }
        }
        public string Resolution
        {
            get { return resolution; }
            set
            {
                resolution = value;
                OnPropertyChanged("Resolution");
            }
        }
        public string Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            }
        }
        public string Night_mode
        {
            get { return night_mode; }
            set
            {
                night_mode = value;
                OnPropertyChanged("Night_mode");
            }
        }
        public string Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged("Angle");
            }
        }
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }
        public double Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }
        public string Start_date
        {
            get { return start_date; }
            set
            {
                start_date = value;
                OnPropertyChanged("Start_date");
            }
        }
        public double Using_term
        {
            get { return using_term; }
            set
            {
                using_term = value;
                OnPropertyChanged("Using_term");
            }
        }
        public string Finish_date
        {
            get { return finish_date; }
            set
            {
                finish_date = value;
                OnPropertyChanged("Finish_date");
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
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
