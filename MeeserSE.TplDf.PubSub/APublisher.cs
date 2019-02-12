using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub
{

    public class APublisher : IPublisher
    {
        public ISourceBlock<Message> SourceBlock => _bufferBlock;

        public string Name { get; }

        public async Task<bool> Publish(object value, string key = null)
        {
            return await PublishMessage(key, value, null, null, null);
        }

        public async Task<bool> Publish(string value, string key = null)
        {
            return await PublishMessage(key, null, value, null, null);
        }

        public async Task<bool> Publish(double? value, string key = null)
        {
            return await PublishMessage(key, null, null, value, null);
        }

        public async Task<bool> Publish(double[,,] value, string key = null)
        {
            return await PublishMessage(key, null, null, null, value);
        }

        public APublisher(string name)
        {
            Name = name;
        }

        protected async Task<bool> PublishMessage(string key, object o, string s, double? d, double[,,] m)
        {
            return await _bufferBlock.SendAsync(new Message(this, key, o, s, d, m));
        }

        protected BufferBlock<Message> _bufferBlock = new BufferBlock<Message>();
    }

}