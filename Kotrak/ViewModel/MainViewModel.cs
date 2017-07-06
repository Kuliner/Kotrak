using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewManagement;

namespace Kotrak.ViewModel
{
    public class MainViewModel : ViewModelBaseWrapper
    {
        private DatabaseManager _dbManager;

        public MainViewModel(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged();
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand
                    ?? (_addCommand = new RelayCommand(
                    () =>
                    {

                        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(City) || string.IsNullOrWhiteSpace(Phone))
                            return;

                        _dbManager.CustomerService.Add(new Customer() { Name = Name, Phone = Phone, City = City });

                        Name = "";
                        City = "";
                        Phone = "";

                        Customers = new ObservableCollection<Customer>(_dbManager.CustomerService.Get());
                    }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand
                    ?? (_removeCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedCustomer == null)
                            return;

                        _dbManager.CustomerService.Remove(SelectedCustomer);

                        Name = "";
                        City = "";
                        Phone = "";

                        Customers = new ObservableCollection<Customer>(_dbManager.CustomerService.Get());
                    }));
            }
        }

        public override void NavigatedTo()
        {
            base.NavigatedTo();

            Customers = new ObservableCollection<Customer>(_dbManager.CustomerService.Get());
        }
    }
}
