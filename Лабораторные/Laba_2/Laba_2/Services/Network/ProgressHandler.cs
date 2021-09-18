using System;

namespace Laba_2.Services.Network
{
	public class ProgressHandler
	{
		private readonly ProgressChangedDelegate progressChangedDelegate;

		public delegate void ProgressChangedDelegate(long? totalStepsCount, long passedStepsCount);

		public ProgressHandler(ProgressChangedDelegate progressChangedDelegate)
		{
			this.progressChangedDelegate = progressChangedDelegate ?? throw new ArgumentNullException(nameof(progressChangedDelegate));
		}

		public void ProgressChanged(long? totalStepsCount, long passedStepsCount)
		{
			progressChangedDelegate.Invoke(totalStepsCount, passedStepsCount);
		}
	}
}