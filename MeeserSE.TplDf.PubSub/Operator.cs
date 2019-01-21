using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub
{

    public interface I_Node
    {
        string Name { get; }
    }

    public interface I_Publisher : I_Node
    {
        void Publish(Object value);

        void Publish(string value);

        void Publish(Double value);

        void Publish(Double[,,] value);

        ISourceBlock<Message> SourceBlock { get; }
    }

    public interface I_Subscriber : I_Node
    {
        Predicate<Message> Subscribtion { get; }

        void Process(Object value);

        void Process(string value);

        void Process(Double value);

        void Process(Double[,,] value);

        ITargetBlock<Message> TargetBlock { get; }
    }

    public class Operator
    {

        public void AddNode(I_Node toAdd)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException(nameof(toAdd));
            }

            if (toAdd is I_Publisher)
            {
                AddPublisher(toAdd as I_Publisher);
            }

            if (toAdd is I_Subscriber)
            {
                AddSubscriber(toAdd as I_Subscriber);
            }
        }

        public Operator()
        {
            _in_bufferBlock.LinkTo(_in_bufferBlock, new DataflowLinkOptions { PropagateCompletion = true });
        }

        private void AddPublisher(I_Publisher toAdd)
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

        private void AddSubscriber(I_Subscriber toAdd)
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

        private BufferBlock<Message> _in_bufferBlock = new BufferBlock<Message>();
        private BroadcastBlock<Message> _out_broadcastBlock = new BroadcastBlock<Message>(Message.FastCopy);
        private Dictionary<string, I_Publisher> _publishers = new Dictionary<string, I_Publisher>();
        private Dictionary<string, I_Subscriber> _subscribers = new Dictionary<string, I_Subscriber>();

    }

}