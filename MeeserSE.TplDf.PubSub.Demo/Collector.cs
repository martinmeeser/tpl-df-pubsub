using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub.Demo
{
    public class Collector : IPublisher, ISubscriber
    {
        public string Name { get; }
        public Predicate<Message> Subscribtion { get; }
        public ITargetBlock<Message> TargetBlock => _in_actionBlock;
        public ISourceBlock<Message> SourceBlock => _out_bufferBlock;

        public Collector(string name, Predicate<Message> subscription, int nx, int ny)
        {
            Name = name;
            Subscribtion = subscription;
            _nx = nx;
            _ny = ny;
            _curr_vals = new double[nx, ny, 1];
            _in_actionBlock = new ActionBlock<Message>(m => ProcessMessage(m));
        }

        private ActionBlock<Message> _in_actionBlock;
        private BufferBlock<Message> _out_bufferBlock = new BufferBlock<Message>();

        private int _nx, _ny, _curr_x, _curr_y;

        private double[,,] _curr_vals;

        private int count = 0;
        private void ProcessMessage(Message m)
        {
            try
            {
                int x = (int)m.GetHeader("x");
                int y = (int)m.GetHeader("y");

                _curr_vals[x, y, 0] = m.DoubleValue.Value;

                count++;
                if (count == (_nx * _ny))
                {
                    _out_bufferBlock.Post(new Message(Name, null, null, null, null, _curr_vals));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }




        }

    }
}
