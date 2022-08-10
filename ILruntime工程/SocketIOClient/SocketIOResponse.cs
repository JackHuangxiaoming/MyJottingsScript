using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LitJson;

namespace SocketIOClient
{
    public class SocketIOResponse
    {
        public SocketIOResponse(IList<JsonElement> array, SocketIO socket)
        //public SocketIOResponse(IList<MyJson.IJsonNode> array, SocketIO socket)
        {
            _array = array;
            InComingBytes = new List<byte[]>();
            SocketIO = socket;
            PacketId = -1;
        }

        readonly IList<JsonElement> _array;
        //readonly IList<MyJson.IJsonNode> _array;

        public List<byte[]> InComingBytes { get; }
        public SocketIO SocketIO { get; }
        public int PacketId { get; set; }
        //public void Test(string msg = "{\"data\":{\"objID\":123321,\"stat\":1,\"playerName\":\"王大爷\",\"Appearance\":{\"hair\":10000,\"cloth\":20000,\"face\":30000}},\"eventName\":\"PlayerInitSuccess\"}")
        //{
        //    if (_array.Count < 2)
        //        _array.Add(JsonDocument.Parse(msg).RootElement);
        //    else
        //        _array[1] = (JsonDocument.Parse(msg).RootElement);
        //}
        public T GetValue<T>(int index = 0)
        {
            var element = GetValue(index);
            string json = element.GetRawText();
            //string json = element.AsString();
            return SocketIO.JsonSerializer.Deserialize<T>(json, InComingBytes);
        }

        public JsonElement GetValue(int index = 0) => _array[index];
        //public MyJson.IJsonNode GetValue(int index = 0) => _array[index];

        public int Count => _array.Count;

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            foreach (var item in _array)
            {
                builder.Append(item.GetRawText());
                //builder.Append(item.AsString());
                if (_array.IndexOf(item) < _array.Count - 1)
                {
                    builder.Append(',');
                }
            }
            builder.Append(']');
            return builder.ToString();
        }

        public async Task CallbackAsync(params object[] data)
        {
            await SocketIO.ClientAckAsync(PacketId, CancellationToken.None, data).ConfigureAwait(false);
        }

        public async Task CallbackAsync(CancellationToken cancellationToken, params object[] data)
        {
            await SocketIO.ClientAckAsync(PacketId, cancellationToken, data).ConfigureAwait(false);
        }
    }
}
