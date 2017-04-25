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
using Android.Support.V7.App;

namespace JorjeiaAndroidApp
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //if (1==3)
            //{
            //    context.StopService(intent);
            //}

            //When user click the notification, start new activity
            Intent newIntent = new Intent(context, typeof(MainActivity));

            Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(newIntent);

            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context);
            builder.SetAutoCancel(true)
            .SetContentIntent(resultPendingIntent)
            .SetDefaults((int)NotificationDefaults.All)
            .SetSmallIcon(Resource.Drawable.Icon)
            .SetContentTitle("Jorjeia")
            .SetContentText("Изпълнихте ли препоръчаните мазания за днес?");
            //.SetContentInfo("Ñ ãðèæà çà âàñ!");


            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            manager.Notify(1, builder.Build());
        }
    }
}