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

        public ActionSubscriber(Action<object> action)
        { }

        public ActionSubscriber(Action<string> action)
        { }

        public ActionSubscriber(Action<double?> action)
        { }

        public ActionSubscriber(Action<double[,,]> action)
        { }


        private ActionBlock<Message> _actionBlock;

        private Action<object> _objectAction;

        private Action<string> _stringAction;

        private Action<double?> _doubleAction;

        private Action<double[,,]> _matrixAction;

        private void ProcessMessage(Message m)
        {
            if (m == null)
            {
                throw new ArgumentNullException(nameof(m));
            }

            if (m.ObjectValue != null && _objectAction != null)
            {
                _objectAction(m.ObjectValue);
            }

            if (m.StringValue != null && _stringAction != null)
            {
                _stringAction(m.StringValue);
            }

            if (m.DoubleValue != null && _doubleAction != null)
            {
                _doubleAction(m.DoubleValue);
            }

            if (m.DoubleMatrix != null && _matrixAction != null)
            {
                _matrixAction(m.DoubleMatrix);
            }
        }

    }

}