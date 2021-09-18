using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba_2.Services.Network.Api.Extensions
{
	public static class UriExtensions
	{
		public static Uri AddQueryParameter(this Uri uri, string name, string value)
		{
			if (uri == null) throw new ArgumentNullException(nameof(uri));
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (value == null) throw new ArgumentNullException(nameof(value));

			var originalUrl = uri.OriginalString;

			var questionMarkIndex = originalUrl.IndexOf('?');

			var query = questionMarkIndex != -1 ? originalUrl.Substring(questionMarkIndex + 1) : string.Empty;
			var rawUrl = questionMarkIndex != -1 ? originalUrl.Substring(0, questionMarkIndex) : originalUrl;

			var paramsDictionary = GetParamsFromQuery(query);

			paramsDictionary.Add(name, value);

			var pairedParams = paramsDictionary.Select(keyValuePair =>
			{
				return $"{Uri.EscapeUriString(keyValuePair.Key)}={Uri.EscapeUriString(keyValuePair.Value)}";
			});

			string joined = string.Join("&", pairedParams.ToArray());

			var isAbsolute = uri.IsAbsoluteUri;
			var fullUrl = $"{rawUrl}?{joined}";

			return new Uri(fullUrl, isAbsolute ? UriKind.Absolute : UriKind.Relative);
		}

		private static Dictionary<string, string> GetParamsFromQuery(string query)
		{
			var pairs = query.Split('&');
			return pairs
			   .Select(o => o.Split('='))
			   .Where(items => items.Length == 2)
			   .ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
				   pair => Uri.UnescapeDataString(pair[1]));
		}
	}
}
