using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub
{

    public interface INode
    {
        string Name { get; }
    }

    public interface IPublisher : INode
    {
        Task<bool> Publish(Object value, string key);

        Task<bool> Publish(string value, string key);

        Task<bool> Publish(double? value, string key);

        Task<bool> Publish(Double[,,] value, string key);

        ISourceBlock<Message> SourceBlock { get; }
    }

    public interface ISubscriber : INode
    {
        Predicate<Message> Subscribtion { get; }

        ITargetBlock<Message> TargetBlock { get; }
    }

    public class Operator
    {

        public void AddNode(INode toAdd)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException(nameof(toAdd));
            }

            if (toAdd is IPublisher)
            {
                AddPublisher(toAdd as IPublisher);
            }

            if (toAdd is ISubscriber)
            {
                AddSubscriber(toAdd as ISubscriber);
            }
        }

        public Operator()
        {
            _processBlock = new TransformBlock<Message, Message>(m => ProcessMessage(m));

            _in_bufferBlock.LinkTo(_processBlock, new DataflowLinkOptions { PropagateCompletion = true });
            _processBlock.LinkTo(_out_broadcastBlock, new DataflowLinkOptions { PropagateCompletion = true });
        }

        private void AddPublisher(IPublisher toAdd)
        {
            if (_publishers.ContainsKey(toAdd.Name))
            {
                // TODO message to caller
            }
            else
            {
                toAdd.SourceBlock.LinkTo(_in_bufferBlock);
                _publishers.Add(toAdd.Name, toAdd);
            }
        }

        private void AddSubscriber(ISubscriber toAdd)
        {
            if (_subscribers.ContainsKey(toAdd.Name))
            {
                // TODO message to caller
            }
            else
            {
                if (toAdd.Subscribtion == null)
                {
                    _out_broadcastBlock.LinkTo(toAdd.TargetBlock);
                }
                else
                {
                    _out_broadcastBlock.LinkTo(toAdd.TargetBlock, toAdd.Subscribtion);
                }
                _subscribers.Add(toAdd.Name, toAdd);
            }
        }

        private Message ProcessMessage(Message toProcess)
        {
            toProcess.Attributes.Add("Publish-Time", DateTime.Now);
            return toProcess;
        }

        private BufferBlock<Message> _in_bufferBlock = new BufferBlock<Message>();

        private TransformBlock<Message, Message> _processBlock;

        private BroadcastBlock<Message> _out_broadcastBlock = new BroadcastBlock<Message>(Message.FastCopy);
        private Dictionary<string, IPublisher> _publishers = new Dictionary<string, IPublisher>();
        private Dictionary<string, ISubscriber> _subscribers = new Dictionary<string, ISubscriber>();

    }

}