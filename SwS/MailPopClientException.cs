using System;

// http://codezine.jp/

namespace SwS
{
	/// <summary>
	/// PopClient の例外クラスです。
	/// </summary>
	public class MailPopClientException : Exception
	{
		/// <summary>
		/// コンストラクタです。
		/// </summary>
		public MailPopClientException()
		{
		}

		/// <summary>
		/// コンストラクタです。
		/// </summary>
		/// <param name="message"></param>
		public MailPopClientException(string message) : base(message)
		{
		}

		/// <summary>
		/// コンストラクタです。
		/// </summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		public MailPopClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
