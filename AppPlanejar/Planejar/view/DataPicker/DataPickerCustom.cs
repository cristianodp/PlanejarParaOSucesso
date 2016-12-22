using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.dinizdesenvolve.planejar.view.DataPicker
{
    public class OnDateSelectArgs : EventArgs
    {
        private DateTime mItem;

        public DateTime item
        {

            get { return mItem; }
            set { mItem = value; }
        }

        public OnDateSelectArgs(DateTime it) : base()
        {
            item = it;
        }
    }




    public class DatePickerFragment : DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
    {
        DateTime currently;
        Context mContext;

        protected Dialog onCreateDialog(int id)
        {

            return new DatePickerDialog(mContext, mOndatepickerListiner, currently.Year, currently.Month, currently.Day);
        }

        private void mOndatepickerListiner(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OnDateSelectArgs> mOnItemSelectArgs;

        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = (d) => {

            
        };

        public DatePickerFragment(DateTime baseDate) {
            currently = baseDate;
        }

        /*
        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }
        */
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                           this,
                                                           currently.Year,
                                                           currently.Month,
                                                           currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            //Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
    
}