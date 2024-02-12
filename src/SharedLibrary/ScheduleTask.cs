using System;
using System.Threading;

namespace SharedLibrary
{
	public static class ScheduleTask
	{
		public static Timer Schedule(int hour, int min, Action task)
		{
			DateTime now = DateTime.Now;
			DateTime firstRun = new(now.Year, now.Month, now.Day, hour, min, 0, 0);
			if (now > firstRun)
			{
				firstRun = firstRun.AddDays(1);
			}

			TimeSpan timeToGo = firstRun - now;
			if (timeToGo <= TimeSpan.Zero)
			{
				timeToGo = TimeSpan.Zero;
			}

			Timer timer = new(x =>
			{
				task.Invoke();
			}, null, timeToGo, TimeSpan.FromHours(24));

			return timer;
		}
	}
}
