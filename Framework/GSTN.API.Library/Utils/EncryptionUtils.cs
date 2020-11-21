using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Numerics;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Macs;
using risersoft.shared;
using Org.BouncyCastle.X509;

namespace Risersoft.API.GSTN
{


	public class EncryptionUtils
	{
        //https://www.limilabs.com/blog/import-certificate-private-public-keys-pem-cer-pfx

        public static X509Certificate2 getPublicKey(string cert_file)
		{
			X509Certificate2 cert2 = new X509Certificate2(System.IO.Path.Combine(GSTNConstants.base_path, "Resources\\" + cert_file));
			return cert2;
		}

      
        public static string GenerateHMAC(string message, string secret)
		{
			secret = secret ?? "";
			byte[] keyByte = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            return GenerateHMAC(messageBytes, keyByte);


		}
    
        public static string GenerateHMAC(byte[] data, byte[] EK)
        {
            using (var HMACSHA256 = new HMACSHA256(EK))
            {
                byte[] hashmessage = HMACSHA256.ComputeHash(data);
                return Convert.ToBase64String(hashmessage);
            }
        }


		public static string AesEncrypt(byte[] dataToEncrypt, byte[] keyBytes)
		{
            AesManaged tdes = new AesManaged();

            tdes.KeySize = 256;
            tdes.BlockSize = 128;
            tdes.Key = keyBytes;
            // Encoding.ASCII.GetBytes(key);
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform crypt = tdes.CreateEncryptor();
            byte[] cipher = crypt.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            tdes.Clear();
            return Convert.ToBase64String(cipher, 0, cipher.Length);
        }


		public static byte[] AesDecrypt(string encryptedText, byte[] keys)
		{
			byte[] dataToDecrypt = Convert.FromBase64String(encryptedText);
			return AesDecrypt(dataToDecrypt, keys);
		}



		public static byte[] AesDecrypt(byte[] dataToDecrypt, byte[] keyBytes)
		{

			AesManaged tdes = new AesManaged();
			tdes.KeySize = 256;
			tdes.BlockSize = 128;
			tdes.Key = keyBytes;
			tdes.Mode = CipherMode.ECB;
			tdes.Padding = PaddingMode.PKCS7;

			ICryptoTransform decrypt__1 = tdes.CreateDecryptor();
			byte[] deCipher = decrypt__1.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
			tdes.Clear();

			return deCipher;
		}




		public static string RSAEncrypt(string input, string cert_file)
		{
			byte[] bytesToBeEncrypted = Encoding.ASCII.GetBytes(input);
			return RsaEncrypt(bytesToBeEncrypted,cert_file);
		}

		private static readonly byte[] Salt = new byte[] {
			10,
			20,
			30,
			40,
			50,
			60,
			70,
			80
		};
		public static byte[] CreateAesKey()
		{

			System.Security.Cryptography.AesCryptoServiceProvider crypto = new System.Security.Cryptography.AesCryptoServiceProvider();
			crypto.KeySize = 256;
			crypto.GenerateKey();
			byte[] key = crypto.Key;
			return key;
		}

        public static byte[] sha256_hash(string value)
        {

            using (SHA256 hash = SHA256Managed.Create())
            {
                Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                return result;
            }

        }


        public static string convertByteArrayToString(byte[] result)
        {
            string str1 = String.Concat(result.Select(item => item.ToString("x2")));
            return str1;

        }


        public static byte[] convertStringToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
        public static string RsaEncrypt(byte[] bytesToBeEncrypted, string cert_file)
        {

            if (myUtils.EndsWith(cert_file, ".pem"))
            {
                return RsaEncryptPEM(bytesToBeEncrypted, cert_file);
            }
            else
            {
                return RsaEncryptCER(bytesToBeEncrypted, cert_file);
            }
        }
        public static string RsaEncryptCER(byte[] bytesToBeEncrypted, string cert_file)
        {
            X509Certificate2 certificate = getPublicKey(cert_file);
            RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)certificate.PublicKey.Key;


            byte[] bytesEncrypted = RSA.Encrypt(bytesToBeEncrypted, false);

            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }

        public static string RsaEncryptPEM(byte[] bytesToEncrypt, string cert_file)
        {
            string certificateLocation = System.IO.Path.Combine(GSTNConstants.base_path, "Resources\\" + cert_file);
            string publicKey = File.ReadAllText(certificateLocation);
            publicKey=publicKey.Replace("-----BEGIN PUBLIC KEY-----","").Replace("-----END PUBLIC KEY-----","").Trim().Replace("\n","").Replace("\r","");
            byte[] keyBytes = Convert.FromBase64String(publicKey);
            
            AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            RSAParameters rsaParameters = new RSAParameters();
            rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
            rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);
            byte[] ciphertext = rsa.Encrypt(bytesToEncrypt, false);
            string cipherresult = Convert.ToBase64String(ciphertext);
            return cipherresult;
        }

        public static string RsaBCEncryptPEM(byte[] bytesToEncrypt, string cert_file)
        {

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            string certificateLocation = System.IO.Path.Combine(GSTNConstants.base_path, "Resources\\" + cert_file);
            string publicKey = File.ReadAllText(certificateLocation).Replace("RSA PUBLIC","PUBLIC");


            using (var txtreader = new StringReader(publicKey))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();
                
                encryptEngine.Init(true, keyParameter);
            }

            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
            return encrypted;

        }

        public static byte[] CreateAesKeyBC()
        {
            SecureRandom random = new SecureRandom();
            byte[] keyBytes = new byte[32]; //32 Bytes = 256 Bits
            random.NextBytes(keyBytes);

            var key = ParameterUtilities.CreateKeyParameter("AES", keyBytes);
            return key.GetKey();
        }
        public byte[] Hash(string text, string key)
        {
            var hmac = new HMac(new Sha256Digest());
            hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(key)));
            byte[] result = new byte[hmac.GetMacSize()];
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            hmac.BlockUpdate(bytes, 0, bytes.Length);
            hmac.DoFinal(result, 0);

            return result;
        }
   
        public static string GenerateHMACBC(byte[] data, byte[] EK)
        {
            var hmac = new HMac(new Sha256Digest());
            hmac.Init(new KeyParameter(EK));
            byte[] hashMessage = new byte[hmac.GetMacSize()];
            hmac.BlockUpdate(data, 0, data.Length);
            hmac.DoFinal(hashMessage, 0);
            return Convert.ToBase64String(hashMessage);
        }
    }

}



