using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Models
{
    public class BusinessHours:ObservableObject
    {

		private DateTime _fromDate;
		private DateTime _toDate;
        private DateTime _fromHour;
        private DateTime _toHour;

       
        public DateTime FromDate
		{
			get { return _fromDate; }
			set
            {
                if (value != _fromDate)
                {
                    _fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                if (value != _toDate)
                {
                    _toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
        public DateTime FromHour
        {
            get { return _fromHour; }
            set
            {
                if (value != _fromHour)
                {
                    _fromHour = value;
                    OnPropertyChanged("FromHour");
                }
            }
        }
        public DateTime ToHour
        {
            get { return _toHour; }
            set
            {
                if (value != _toHour)
                {
                    _toHour = value;
                    OnPropertyChanged("ToHour");
                }
            }
        }
    }
}
