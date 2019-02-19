using System;
using System.Threading.Tasks;

namespace MeeserSE.TplDf.PubSub
{
    public static class OperatorTools
    {

        public static void AddConsoleLogging(this PubSubOperator pubSubOperator)
        {
            pubSubOperator.AddSubscriber(
                "Console Logger",
                m => true,
                m =>
                {
                    Console.WriteLine($"{DateTime.UtcNow} -- {m.ToString()}");
                });
        }

        public static ISubscriber AddSubscriber(
            this PubSubOperator pubSubOperator
            , string name
            , Predicate<Message> subscription
            , Action<Message> action)
        {
            ActionSubscriber result = new ActionSubscriber(name, subscription, action);

            pubSubOperator.AddNode(result);

            return result;
        }

        public static ISubscriber AddSubscriber(
            this PubSubOperator pubSubOperator
            , string name
            , Predicate<Message> subscription
            , Func<Message, Task> action)
        {
            //TODO
            return null;
        }

    }
}
