using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeeserSE.TplDf.PubSub.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // ******************************************************
            // setup the operator, the publisher and the subscribers
            // ******************************************************
            int nx = 4;
            int ny = 12;

            // ******************************************************
            // operator
            PubSubOperator pubSubOperator = new PubSubOperator();

            // ******************************************************
            // publishers
            Measurement m1 = new Measurement("m1", CreateRandomMatrix(nx, ny), 100, 100);
            Measurement m2 = new Measurement("m2", CreateRandomMatrix(nx, ny), 20, 150);

            pubSubOperator.AddNode(m1);
            pubSubOperator.AddNode(m2);
            // ******************************************************
            // pubsubs
            Collector c1 = new Collector("m1_collector", m => m.PublisherName.Equals(m1.Name), nx, ny);
            pubSubOperator.AddNode(c1);


            Difference diff1 = new Difference("m1-m2", new List<IPublisher> { m1, m2 }, nx, ny);
            pubSubOperator.AddNode(diff1);

            pubSubOperator.AddSubscriber(
                name: "result",
                subscription: m => m.PublisherName.Equals("m1-m2"),
                action: m =>
                        pubSubOperator.PublishMessage("result", null, m.DoubleValue.Value < -1.0 ? "alien lifeform detected!" : "empty space")
                );

            // ******************************************************
            // subscribers
            pubSubOperator.AddConsoleLogging();

            // Task.Run(async () => await m1.Run());
            // Task.Run(async () => await m2.Run());

            Task.Run(async () => { await m1.Run(); await m2.Run(); });
            // Task.Run(async () => await m2.Run());

            Console.Read();
        }

        private static double[,] CreateRandomMatrix(int nx, int ny)
        {
            double[,] result = new double[nx, ny];
            Random random = new Random();

            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    result[i, j] = random.NextDouble();
                }
            }
            return result;
        }
    }


}
