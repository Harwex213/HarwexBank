﻿using System.Diagnostics;

namespace HarwexBank
{
    public class CreditTypeModel : ObservableObject
    {
        private int _id;
        private string _name;
        private decimal _ratio;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        
        public decimal Ratio
        {
            get => _ratio;
            set
            {
                _ratio = value;
                OnPropertyChanged("Ratio");
            }
        }
    }
}