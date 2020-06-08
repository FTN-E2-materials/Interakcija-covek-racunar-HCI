using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Models
{
    public class BusinessHours:ObservableObject
    {

		private string _fromDate;
		private string _toDate;
        private string _fromHour;
        private string _toHour;

       
        public string FromDate
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
        public string ToDate
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
        public string FromHour
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
        public string ToHour
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
