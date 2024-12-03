using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	public class CertificateRequest
	{
		protected readonly byte[] mCertificateTypes;

		protected readonly IList mSupportedSignatureAlgorithms;

		protected readonly IList mCertificateAuthorities;

		public virtual byte[] CertificateTypes => mCertificateTypes;

		public virtual IList SupportedSignatureAlgorithms => mSupportedSignatureAlgorithms;

		public virtual IList CertificateAuthorities => mCertificateAuthorities;

		public CertificateRequest(byte[] certificateTypes, IList supportedSignatureAlgorithms, IList certificateAuthorities)
		{
			mCertificateTypes = certificateTypes;
			mSupportedSignatureAlgorithms = supportedSignatureAlgorithms;
			mCertificateAuthorities = certificateAuthorities;
		}

		public virtual void Encode(Stream output)
		{
			if (mCertificateTypes == null || mCertificateTypes.Length == 0)
			{
				TlsUtilities.WriteUint8(0, output);
			}
			else
			{
				TlsUtilities.WriteUint8ArrayWithUint8Length(mCertificateTypes, output);
			}
			if (mSupportedSignatureAlgorithms != null)
			{
				TlsUtilities.EncodeSupportedSignatureAlgorithms(mSupportedSignatureAlgorithms, allowAnonymous: false, output);
			}
			if (mCertificateAuthorities == null || mCertificateAuthorities.Count < 1)
			{
				TlsUtilities.WriteUint16(0, output);
				return;
			}
			IList list = Platform.CreateArrayList(mCertificateAuthorities.Count);
			int num = 0;
			IEnumerator enumerator = mCertificateAuthorities.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
					byte[] encoded = asn1Encodable.GetEncoded("DER");
					list.Add(encoded);
					num += encoded.Length + 2;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
			TlsUtilities.CheckUint16(num);
			TlsUtilities.WriteUint16(num, output);
			IEnumerator enumerator2 = list.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					byte[] buf = (byte[])enumerator2.Current;
					TlsUtilities.WriteOpaque16(buf, output);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = enumerator2 as IDisposable) != null)
				{
					disposable2.Dispose();
				}
			}
		}

		public static CertificateRequest Parse(TlsContext context, Stream input)
		{
			int num = TlsUtilities.ReadUint8(input);
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = TlsUtilities.ReadUint8(input);
			}
			IList supportedSignatureAlgorithms = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				supportedSignatureAlgorithms = TlsUtilities.ParseSupportedSignatureAlgorithms(allowAnonymous: false, input);
			}
			IList list = Platform.CreateArrayList();
			byte[] buffer = TlsUtilities.ReadOpaque16(input);
			MemoryStream memoryStream = new MemoryStream(buffer, writable: false);
			while (memoryStream.Position < memoryStream.Length)
			{
				byte[] encoding = TlsUtilities.ReadOpaque16(memoryStream);
				Asn1Object obj = TlsUtilities.ReadDerObject(encoding);
				list.Add(X509Name.GetInstance(obj));
			}
			return new CertificateRequest(array, supportedSignatureAlgorithms, list);
		}
	}
}
