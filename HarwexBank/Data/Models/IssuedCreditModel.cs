﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HarwexBank
{
    public class IssuedCreditModel : ObservableObject, ModelResourcesManager.IModel, IDataErrorInfo
    {
        public IssuedCreditModel()
        {
            CreditRepayments = new HashSet<CreditRepaymentModel>();
        }
        
        private int _id;
        private int _userId;
        private string _creditType;
        private DateTime _dateIn;
        private long _term;
        private decimal _amount;
        private decimal _repaidAmount;
        private decimal _amountToPay;
        private bool _isApproved;
        private bool _isRepaid;

        public virtual CreditTypeModel CreditTypeModelNavigation { get; set; }
        public virtual UserModel UserModelAccount { get; set; }
        public virtual ICollection<CreditRepaymentModel> CreditRepayments { get; set; }
        
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public string CreditType
        {
            get => _creditType;
            set
            {
                _creditType = value;
                OnPropertyChanged("CreditType");
            }
        }

        [NotMapped]
        public string CreditName => "Кредит номер " + Id + ", " + CreditType;

        public DateTime DateIn
        {
            get => _dateIn;
            set
            {
                _dateIn = value;
                OnPropertyChanged("DateIn");
            }
        }
        public long Term
        {
            get => _term;
            set
            {
                _term = value;
                OnPropertyChanged("Term");
            }
        }
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public decimal RepaidAmount
        {
            get => _repaidAmount;
            set
            {
                _repaidAmount = value;
                OnPropertyChanged("RepaidAmount");
            }
        }
        public decimal AmountToPay
        {
            get => _amountToPay;
            set => Set(ref _amountToPay, value);
        }
        public bool IsApproved
        {
            get => _isApproved;
            set
            {
                _isApproved = value;
                OnPropertyChanged("IsApproved");
            }
        }
        public bool IsRepaid
        {
            get => _isRepaid;
            set
            {
                _isRepaid = value;
                OnPropertyChanged("IsRepaid");
            }
        }
        
        public string Error => null;

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case nameof(Amount):
                        if (Amount < CreditTypeModelNavigation.MinimalTakingAmount || Amount > CreditTypeModelNavigation.MaximalTakingAmount)
                        {
                            result = $"Сумма должна быть от {CreditTypeModelNavigation.MinimalTakingAmount} до {CreditTypeModelNavigation.MaximalTakingAmount}";
                        }
                        break;
                    case nameof(Term):
                        if (Term < CreditTypeModelNavigation.MinimalTerm || Term > CreditTypeModelNavigation.MaximalTerm)
                        {
                            result = $"Срок должен быть от {CreditTypeModelNavigation.MinimalTerm} до {CreditTypeModelNavigation.MaximalTerm}";
                        }
                        break;
                }

                return result;
            }
        }
    }
}