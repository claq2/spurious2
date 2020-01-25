using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core
{
    public static class StringExtensionMethods
    {
        public static byte[] Compress(this string input)
        {
            byte[] result;
            using (var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var compressorStream = new DeflateStream(outputStream, CompressionLevel.Fastest, true))
                    {
                        inputStream.CopyTo(compressorStream);
                    }

                    result = outputStream.ToArray();
                }
            }

            return result;
        }

        public static string Decompress(this byte[] input)
        {
            byte[] outputBytes;
            var inputStream = new MemoryStream(input);
            using (var decompressorStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                using (var outputStream = new MemoryStream())
                {
                    decompressorStream.CopyTo(outputStream);
                    outputBytes = outputStream.ToArray();
                }
            }

            return Encoding.UTF8.GetString(outputBytes);
        }
    }
}
