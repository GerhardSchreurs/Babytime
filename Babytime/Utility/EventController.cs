using System;
namespace babytime.Utility
{
    public class AlarmEventArgs : EventArgs
    {
        public bool FromMommy = false;

        public AlarmEventArgs(bool fromMommy = false)
        {
            FromMommy = fromMommy;
        }
    }


    public static class EventController
    {
        public static event EventHandler<AlarmEventArgs> AlarmIsRinging;

        public static void RaiseAlarmIsRinging(object sender, bool fromMommy = false)
        {
            AlarmIsRinging?.Invoke(sender, new AlarmEventArgs(fromMommy));
        }
    }
}
