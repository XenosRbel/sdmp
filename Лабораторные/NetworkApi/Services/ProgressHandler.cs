using System;
using static NetworkApi.Services.IProgressHandler;

namespace NetworkApi.Services
{
	internal class ProgressHandler : IProgressHandler
	{
		public delegate void ProgressChangedDelegate(long? totalStepsCount, long passedStepsCount);

		public readonly ProgressChangedDelegate _progressChangedDelegate;

		public ProgressHandler(ProgressChangedDelegate progressChangedDelegate)
		{
			_progressChangedDelegate = progressChangedDelegate ?? throw new ArgumentNullException(nameof(progressChangedDelegate));
		}

		public void ProgressChanged(long? totalStepsCount, long passedStepsCount)
		{
			_progressChangedDelegate.Invoke(totalStepsCount, passedStepsCount);
		}
	}
}
