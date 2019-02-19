using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub.Demo
{

    public class Difference : ISubscriber, IPublisher
    {
        public Predicate<Message> Subscribtion { get; }

        public ITargetBlock<Message> TargetBlock => _in_actionBlock;

        public ISourceBlock<Message> SourceBlock => _out_bufferBlock;

        public string Name { get; }

        public Difference(string name, List<ISubscriber> sources, int nx, int ny)
        {
            Subscribtion =
            (m) =>
                sources.Any(sourceSubscriber => sourceSubscriber.Name.Equals(m.PublisherName));

            _in_actionBlock = new ActionBlock<Message>(m => ProcessMessage(m));

            sources.ForEach(source => _values.Add(source.Name, new double[nx, ny]));
        }

        private ActionBlock<Message> _in_actionBlock;

        private BufferBlock<Message> _out_bufferBlock;

        private List<ISubscriber> _sources;

        private Dictionary<string, double[,]> _values;

        private void ProcessMessage(Message m)
        {
            int x = (int)m.GetHeader("x");
            int y = (int)m.GetHeader("y");

            _values[m.PublisherName][x, y] = m.DoubleValue.Value;

            bool areAllSet = true;
            double sum = 0;
            foreach (KeyValuePair<string, double[,]> kvp in _values)
            {
                areAllSet = areAllSet && kvp.Value[x, y] != 0;
                sum -= kvp.Value[x, y];
            }

            if (areAllSet)
            {
                _out_bufferBlock.Post(new Message(Name, null, null, null, sum, null, new Dictionary<string, object> { { "x", x }, { "y", y } }));
            }

        }

    }

}