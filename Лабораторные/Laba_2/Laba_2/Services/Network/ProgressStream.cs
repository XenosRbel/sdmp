using System;
using System.IO;
using System.Threading;

namespace Laba_2.Services.Network
{
	internal sealed class ProgressStream : Stream
	{
		private readonly Stream stream;
		private readonly long? totalFileSize;
		private CancellationToken token;
		private long currentPosition = 0;

		public delegate void ProgressChangedHandler(long? totalSteps, long passedSteps);

		public event ProgressChangedHandler ProgressChanged;

		internal ProgressStream(Stream stream, long? totalFileSize, CancellationToken token)
		{
			this.totalFileSize = totalFileSize;
			this.token = token;
			this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
		}
		public override bool CanRead => stream.CanRead;
		public override bool CanSeek => stream.CanSeek;
		public override bool CanWrite => stream.CanWrite;
		public override long Length => stream.Length;

		public override long Position
		{
			get => stream.Position;
			set => stream.Position = value;
		}

		public override void Flush()
		{
			stream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var downloadedBytesCount = stream.Read(buffer, offset, count);

			token.ThrowIfCancellationRequested();

			currentPosition += downloadedBytesCount;

			OnProgressChanged(totalFileSize, currentPosition);

			return downloadedBytesCount;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			stream.Write(buffer, offset, count);
		}



		private void OnProgressChanged(long? totalContentSize, long totalBytesLoaded)
		{
			ProgressChanged?.Invoke(totalContentSize, totalBytesLoaded);
		}
	}
}