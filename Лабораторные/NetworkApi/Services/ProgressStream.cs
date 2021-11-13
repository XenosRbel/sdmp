using System;
using System.IO;
using System.Threading;
using static NetworkApi.Services.IProgressStream;

namespace NetworkApi.Services
{
	public sealed class ProgressStream : Stream, IProgressStream
	{
		private readonly Stream _stream;
		private readonly long? _totalFileSize;
		private readonly CancellationToken _token;
		private long _currentPosition = 0;

		public event ProgressChangedHandler ProgressChanged;
		public delegate void ProgressChangedHandler(long? totalStepsCount, long passedStepsCount);

		internal ProgressStream(Stream stream, long? totalFileSize, CancellationToken token)
		{
			_totalFileSize = totalFileSize;
			_token = token;
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
		}
		public override bool CanRead => _stream.CanRead;
		public override bool CanSeek => _stream.CanSeek;
		public override bool CanWrite => _stream.CanWrite;
		public override long Length => _stream.Length;

		public override long Position
		{
			get => _stream.Position;
			set => _stream.Position = value;
		}

		public override void Flush()
		{
			_stream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var downloadedBytesCount = _stream.Read(buffer, offset, count);

			_token.ThrowIfCancellationRequested();

			_currentPosition += downloadedBytesCount;

			OnProgressChanged(_totalFileSize, _currentPosition);

			return downloadedBytesCount;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			_stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			_stream.Write(buffer, offset, count);
		}

		private void OnProgressChanged(long? totalContentSize, long totalBytesLoaded)
		{
			ProgressChanged?.Invoke(totalContentSize, totalBytesLoaded);
		}
	}
}
