namespace NetworkApi.Services
{
	public interface IProgressHandler
	{
		void ProgressChanged(long? totalStepsCount, long passedStepsCount);
	}
}