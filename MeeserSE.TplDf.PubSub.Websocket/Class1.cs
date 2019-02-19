using System;
using System.Threading.Tasks.Dataflow;

namespace MeeserSE.TplDf.PubSub.Websocket
{
    public class WebsocketSubscriber : ISubscriber
    {
        public Predicate<Message> Subscribtion => throw new NotImplementedException();

        public ITargetBlock<Message> TargetBlock => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();
    }
}
