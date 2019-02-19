using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub
{

    public class A_Publisher : IPublisher
    {
        public ISourceBlock<Message> SourceBlock => _bufferBlock;

        public string Name { get; }

        protected async Task<bool> Publish(object value, string key = null, Dictionary<string, object> headers = null)
        {
            return await PublishMessage(key, value, null, null, null, headers);
        }

        protected async Task<bool> Publish(string value, string key = null, Dictionary<string, object> headers = null)
        {
            return await PublishMessage(key, null, value, null, null, headers);
        }

        protected async Task<bool> Publish(double? value, Dictionary<string, object> headers = null)
        {
            return await PublishMessage(null, null, null, value, null, headers);
        }

        protected async Task<bool> Publish(double? value, string key = null, Dictionary<string, object> headers = null)
        {
            return await PublishMessage(key, null, null, value, null, headers);
        }

        protected async Task<bool> Publish(double[,,] value, string key = null, Dictionary<string, object> headers = null)
        {
            return await PublishMessage(key, null, null, null, value, headers);
        }

        protected A_Publisher(string name)
        {
            Name = name;
        }

        protected async Task<bool> PublishMessage(string key, object o, string s, double? d, double[,,] m, Dictionary<string, object> headers = null)
        {
            return await _bufferBlock.SendAsync(new Message(Name, key, o, s, d, m, headers));
        }

        protected BufferBlock<Message> _bufferBlock = new BufferBlock<Message>();
    }

}