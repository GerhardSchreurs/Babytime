using System;
namespace babytime.Utility
{
    public static class EventController
    {
        public static event EventHandler<EventArgs> AlarmIsRinging;

        public static void RaiseAlarmIsRinging(object sender, EventArgs e = null)
        {
            AlarmIsRinging?.Invoke(sender, e);
        }
    }
}
