using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.ComponentModel;

// http://codezine.jp/

namespace SwS
{
	/// <summary>
	/// POP ��胁�[������M����N���X�ł��B
	/// </summary>
	public class PopClient : IDisposable
	{
		/// <summary>TCP �ڑ�</summary>
		private TcpClient tcp = null;

		/// <summary>TCP �ڑ�����̃��[�_�[</summary>
		private StreamReader reader = null;

		/// <summary>
		/// �R���X�g���N�^�ł��BPOP�T�[�o�Ɛڑ����܂��B
		/// </summary>
		/// <param name="hostname">POP�T�[�o�̃z�X�g���B</param>
		/// <param name="port">POP�T�[�o�̃|�[�g�ԍ��i�ʏ��110�j�B</param>
		public PopClient(string hostname, int port)
		{
			// �T�[�o�Ɛڑ�
			this.tcp = new TcpClient(hostname, port);
			this.reader = new StreamReader(this.tcp.GetStream(), Encoding.ASCII);

			// �I�[�v�j���O��M
			string s = ReadLine();
			if (!s.StartsWith("+OK"))
			{
				throw new MailPopClientException("�ڑ����� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}
		}

		/// <summary>
		/// ����������s���܂��B
		/// </summary>
		public void Dispose()
		{
			if (this.reader != null)
			{
				((IDisposable)this.reader).Dispose();
				this.reader = null;
			}
			if (this.tcp != null)
			{
				((IDisposable)this.tcp).Dispose();
				this.tcp = null;
			}
		}

		/// <summary>
		/// POP �T�[�o�Ƀ��O�C�����܂��B
		/// </summary>
		/// <param name="username">���[�U���B</param>
		/// <param name="password">�p�X���[�h�B</param>
		public void Login(string username, string password)
		{
			// ���[�U�����M
			SendLine("USER " + username);
			string s = ReadLine();
            if (s == null || !s.StartsWith("+OK"))
			{
				throw new MailPopClientException("USER ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}

			// �p�X���[�h���M
			SendLine("PASS " + password);
			s = ReadLine();
			if (!s.StartsWith("+OK"))
			{
				throw new MailPopClientException("PASS ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}
		}

		/// <summary>
		/// POP �T�[�o�ɗ��܂��Ă��郁�[���̃��X�g���擾���܂��B
		/// </summary>
		/// <returns>System.String ���i�[���� ArrayList�B</returns>
		public ArrayList GetList()
		{
			// LIST ���M
			SendLine("LIST");
			string s = ReadLine();
            if (s == null || !s.StartsWith("+OK"))
			{
				throw new MailPopClientException("LIST ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}

			// �T�[�o�ɂ��܂��Ă��郁�[���̐����擾
			ArrayList list = new ArrayList();
			while (true)
			{
				s = ReadLine();
				if (s == ".")
				{
					// �I�[�ɓ��B
					break;
				}
				// ���[���ԍ������݂̂����o���i�[
				int p = s.IndexOf(' ');
				if (p > 0)
				{
					s = s.Substring(0, p);
				}
				list.Add(s);
			}
			return list;
		}

        /// <summary>
        /// POP �T�[�o�ɗ��܂��Ă��郁�[����UIDL���X�g���擾���܂��B
        /// </summary>
        /// <returns>System.String ���i�[���� ArrayList�B</returns>
        public ArrayList GetUidlList(BackgroundWorker worker, DoWorkEventArgs e)
        {
            // LIST ���M
            SendLine("UIDL");
            string s = ReadLine();
            if (s == null || !s.StartsWith("+OK"))
            {
                throw new MailPopClientException("UIDL ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
            }

            // �T�[�o�ɂ��܂��Ă��郁�[���̐����擾
            ArrayList list = new ArrayList();
            while (true)
            {
                System.Threading.Thread.Sleep(1);

                // �L�����Z������ĂȂ�������I�Ƀ`�F�b�N
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return list;
                }

                s = ReadLine();
                if (s == ".")
                {
                    // �I�[�ɓ��B
                    break;
                }
                // ���[��UIDL�����݂̂����o���i�[
                int p = s.IndexOf(' ');
                if (p > 0)
                {
                    s = s.Substring(p + 1, s.Length - (p + 1));
                }
                list.Add(s);
            }
            return list;
        }

		/// <summary>
		/// POP �T�[�o���烁�[���� 1�擾���܂��B
		/// </summary>
		/// <param name="num">GetList() ���\�b�h�Ŏ擾�������[���̔ԍ��B</param>
		/// <returns>���[���̖{�́B</returns>
		public string GetMail(string num)
		{
			// RETR ���M
			SendLine("RETR " + num);
			string s = ReadLine();
            if (s == null || !s.StartsWith("+OK"))
			{
				throw new MailPopClientException("RETR ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}

			// ���[���擾
			StringBuilder sb = new StringBuilder();
			while (true)
			{
				s = ReadLine();
				if (s == ".")
				{
					// "." �݂̂̏ꍇ�̓��[���̏I�[��\��
					break;
				}
				sb.Append(s);
				sb.Append("\r\n");
			}
			return sb.ToString();
		}

		/// <summary>
		/// POP �T�[�o�̃��[���� 1�폜���܂��B
		/// </summary>
		/// <param name="num">GetList() ���\�b�h�Ŏ擾�������[���̔ԍ��B</param>
		public void DeleteMail(string num)
		{
			// DELE ���M
			SendLine("DELE " + num);
			string s = ReadLine();
			if (s == null || !s.StartsWith("+OK"))
			{
				throw new MailPopClientException("DELE ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}
		}

		/// <summary>
		/// POP �T�[�o�Ɛؒf���܂��B
		/// </summary>
		public void Close()
		{
			// QUIT ���M
			SendLine("QUIT");
			string s = ReadLine();
            if (s == null || !s.StartsWith("+OK"))
			{
				throw new MailPopClientException("QUIT ���M���� POP �T�[�o�� \"" + s + "\" ��Ԃ��܂����B");
			}

			((IDisposable)this.reader).Dispose();
			this.reader = null;
			((IDisposable)this.tcp).Dispose();
			this.tcp = null;
		}

		/// <summary>
		/// POP �T�[�o�ɃR�}���h�𑗐M���܂��B
		/// </summary>
		/// <param name="s">���M���镶����B</param>
		private void Send(string s)
		{
			Print("���M: " + s);
			byte[] b = Encoding.ASCII.GetBytes(s);
			this.tcp.GetStream().Write(b, 0, b.Length);
		}

		/// <summary>
		/// POP �T�[�o�ɃR�}���h�𑗐M���܂��B�����ɉ��s��t�����܂��B
		/// </summary>
		/// <param name="s">���M���镶����B</param>
		private void SendLine(string s)
		{
			Print("���M: " + s + "\\r\\n");
			byte[] b = Encoding.ASCII.GetBytes(s + "\r\n");
			this.tcp.GetStream().Write(b, 0, b.Length);
		}

		/// <summary>
		/// POP �T�[�o���� 1�s�ǂݍ��݂܂��B
		/// </summary>
		/// <returns>�ǂݍ��񂾕�����B</returns>
		private string ReadLine()
		{
			string s = this.reader.ReadLine();
			Print("��M: " + s + "\\r\\n");
			return s;
		}

		/// <summary>
		/// �`�F�b�N�p�ɃR���\�[���ɏo�͂��܂��B
		/// </summary>
		/// <param name="msg">�o�͂��镶����B</param>
		private void Print(string msg)
		{
			Console.WriteLine(msg);
		}
	}
}
