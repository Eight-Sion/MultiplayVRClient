using System.IO;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack.Resolvers;

namespace Apis.Converter
{
    public class JsonConverter
    {
        public static string ToJson<T>(T source) {
            return MessagePackSerializer.SerializeToJson(source);
        }
        public static T FromJson<T>(string source ) => MessagePackSerializer.Deserialize<T>(MessagePackSerializer.ConvertFromJson(source));
    }
}
