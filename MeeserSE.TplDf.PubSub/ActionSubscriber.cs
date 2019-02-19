using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub
{

    public class ActionSubscriber : ISubscriber
    {
        public Predicate<Message> Subscribtion { get; }

        public ITargetBlock<Message> TargetBlock => _actionBlock;

        public string Name { get; }

        public ActionSubscriber(string name, Predicate<Message> subscribtion, Action<Message> messageAction)
        {
            Name = name;
            _messageAction = messageAction;
            Subscribtion = subscribtion;
            _actionBlock = new ActionBlock<Message>((m) => ProcessMessage(m));
        }

    

        private ActionBlock<Message> _actionBlock;

        private Action<Message> _messageAction;


        private void ProcessMessage(Message m)
        {
            if (m == null)
            {
                throw new ArgumentNullException(nameof(m));
            }

            if (_messageAction != null)
            {
                _messageAction(m);
            }
        }

    }

}