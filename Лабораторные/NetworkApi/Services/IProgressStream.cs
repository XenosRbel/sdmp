using System.IO;

namespace NetworkApi.Services
{
	public interface IProgressStream
	{
		bool CanRead { get; }
		bool CanSeek { get; }
		bool CanWrite { get; }
		long Length { get; }
		long Position { get; set; }

		void Flush();
		int Read(byte[] buffer, int offset, int count);
		long Seek(long offset, SeekOrigin origin);
		void SetLength(long value);
		void Write(byte[] buffer, int offset, int count);
	}
}