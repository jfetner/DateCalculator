using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSharp.Date
{
    public class DateRangeValue : IEquatable<DateRangeValue>
    {
        private DateTime? beginDate;
        private DateTime? endDate;
        private TimeSpan? offsetToUTC;

        public DateRangeValue()
        {
            beginDate = DateTime.MinValue;
            endDate = DateTime.MaxValue;
        }

        public DateRangeValue(DateTime? beginDate, DateTime? endDate, TimeSpan? offsetToUTC = null)
        {
            this.beginDate = beginDate;
            this.endDate = endDate;
            this.offsetToUTC = offsetToUTC;
        }

        public DateTime? BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; }
        }

        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public TimeSpan? OffsetToUTC
        {
            get { return offsetToUTC; }
            set { offsetToUTC = value; }
        }

        public TimeSpan Difference
        {
            get { return EndDate.Value.Subtract(BeginDate.Value); }
        }


        public DateRangeValue ToLocal()
        {
            if (!offsetToUTC.HasValue)
            {
                return this;
            }
            return new DateRangeValue(BeginDate.Value.Add(offsetToUTC.Value), EndDate.Value.Add(offsetToUTC.Value));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DateRangeValue);
        }

        public override int GetHashCode()
        {
            int hashCode1 = beginDate.GetHashCode();
            int hashCode2 = endDate.GetHashCode();
            return (((hashCode1 << 3) + hashCode1) ^ hashCode2);
        }

        #region IEquatable<DateRangeValue> Members

        public bool Equals(DateRangeValue other)
        {
            //Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }
            return beginDate.Equals(other.beginDate) &&
                   endDate.Equals(other.endDate);
        }

        #endregion
    }
}


