using System;

// http://codezine.jp/

namespace SwS
{
	/// <summary>
	/// PopClient �̗�O�N���X�ł��B
	/// </summary>
	public class MailPopClientException : Exception
	{
		/// <summary>
		/// �R���X�g���N�^�ł��B
		/// </summary>
		public MailPopClientException()
		{
		}

		/// <summary>
		/// �R���X�g���N�^�ł��B
		/// </summary>
		/// <param name="message"></param>
		public MailPopClientException(string message) : base(message)
		{
		}

		/// <summary>
		/// �R���X�g���N�^�ł��B
		/// </summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		public MailPopClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
