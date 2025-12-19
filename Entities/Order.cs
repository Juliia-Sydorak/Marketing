using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MarketingDAL.Entities
{
    // Клас для деталей замовлення (окремо від Order)
    public class OrderItemExtended
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Sum => Quantity * Price;
    }

    public class Order : INotifyPropertyChanged
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }

        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set { _totalAmount = value; OnPropertyChanged(); OnPropertyChanged(nameof(FinalAmount)); }
        }

        private decimal _appliedDiscount;
        public decimal AppliedDiscount
        {
            get => _appliedDiscount;
            set
            {
                // ВАЛІДАЦІЯ: Обмежуємо значення діапазоном від 0 до 100
                if (value < 0)
                    _appliedDiscount = 0;
                else if (value > 100)
                    _appliedDiscount = 100;
                else
                    _appliedDiscount = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalAmount));
            }
        }
        // Колекція для відображення складу замовлення
        public ObservableCollection<OrderItemExtended> Items { get; set; } = new ObservableCollection<OrderItemExtended>();

        public decimal FinalAmount => TotalAmount * (1 - AppliedDiscount / 100);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
