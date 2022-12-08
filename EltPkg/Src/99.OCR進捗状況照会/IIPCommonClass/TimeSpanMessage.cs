using System;
using System.Collections.Generic;
using System.Text;

namespace IIPCommonClass
{
    class TimeSpanMessage
    {
        private DateTime _dt;
        private TimeSpan _ts;
        private string _msg;

        public TimeSpanMessage(DateTime dt, TimeSpan ts, String msg)
        {
            _dt = dt;
            _ts = ts;
            _msg = msg;
        }

        public DateTime DateTime
        {
            get { return _dt; }
        }

        public TimeSpan TimeSpan
        {
            get { return _ts; }
        }

        public String Message
        {
            get { return _msg; }
        }
    }
}
