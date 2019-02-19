using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeeserSE.TplDf.PubSub;
using MeeserSE.TplDf.PubSub.Demo;

namespace MeeserSE.TplDf.Demo
{
    public class Demo
    {

        public async Task Execute()
        {
            // ******************************************************
            // setup the operator, the publisher and the subscribers
            // ******************************************************

            int nx = 2;
            int ny = 1;

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

            // pubSubOperator.AddSubscriber(
            //     name: "eval_median_m1",
            //     subscription: m => m.PublisherName.Equals("median_m1"),
            //     action: m =>
            //     {

            //     });

            // ******************************************************
            // subscribers


            pubSubOperator.AddConsoleLogging();

            await Task.Run(async () => await m1.Run());
            await Task.Run(async () => await m2.Run());

            await Task.Delay(20000);
        }


        private double[,] CreateRandomMatrix(int nx, int ny)
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
