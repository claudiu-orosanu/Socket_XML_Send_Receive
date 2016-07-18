using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Schema;

namespace Socket_XML_Send_Receive_Console_App
{
    public class StringConverter
    {
        public static byte[] GetBytesToSend(string encoding
                                    , string content
                                    , bool shouldAddLengthPrefix)
        {
            byte[] bytesToSend = GetContentBytes(encoding, content);
            if (shouldAddLengthPrefix)
            {
                int contentLength = bytesToSend.Length;
                int reqLenH2N = IPAddress.HostToNetworkOrder(contentLength);
                byte[] reqLenArray = BitConverter.GetBytes(reqLenH2N);

                byte[] buff_intermediar = new byte[contentLength + 4];
                reqLenArray.CopyTo(buff_intermediar, 0);
                bytesToSend.CopyTo(buff_intermediar, 4);
                bytesToSend = buff_intermediar;
            }

            return bytesToSend;
        }

        private static byte[] GetContentBytes(string enconding, string content)
        {
            byte[] contentBytes = null;
            switch (enconding)
            {
                case "ASCII":
                    contentBytes = Encoding.ASCII.GetBytes(content);
                    break;
                case "UTF7":
                    contentBytes = Encoding.UTF7.GetBytes(content);
                    break;
                case "UTF8":
                    contentBytes = Encoding.UTF8.GetBytes(content);
                    break;
                case "Unicode":
                    contentBytes = Encoding.Unicode.GetBytes(content);
                    break;
                default:
                    //
                    break;
            };
            return contentBytes;
        }

        public static bool TryGetStringFromBytes(byte[] rcvBuffer_partial, int bytesReceived, bool shouldValidateSchema,
                                           out string result, string encoding, string schemaValidationText, IForm form)
        {
            result = null;
            if (shouldValidateSchema && !form.Validation(schemaValidationText))
            {
                return false;
            }
            result = GetEncoding(encoding).GetString(rcvBuffer_partial, 0, bytesReceived);
            return true;
        }

        private static Encoding GetEncoding(string encodingType)
        {
            switch (encodingType)
            {
                case "ASCII":
                    return Encoding.ASCII;
                case "UTF7":
                    return Encoding.UTF7;
                case "UTF8":
                    return Encoding.UTF8;
                case "Unicode":
                    return Encoding.Unicode;
                default:
                    throw new ArgumentException("Parametrul encodingType nu este bun", nameof(encodingType));
            }
        }

    }

    public interface IForm
    {
        void MyValidationEventHandler(object sender, ValidationEventArgs args);
        bool Validation(string file);
    }
}
