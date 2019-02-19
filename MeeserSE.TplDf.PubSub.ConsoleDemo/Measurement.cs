using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeeserSE.TplDf.PubSub;

namespace MeeserSE.TplDf.PubSub.ConsoleDemo
{
    public class Measurement : A_Publisher
    {

        public async Task Run()
        {
            await Task.Delay(_initial_delay_ms);

            for (int y = 0; y < _values.GetLength(1); y++)
            {
                for (int x = 0; x < _values.GetLength(0); x++)
                {
                    await Publish(_values[x, y], new Dictionary<string, object> { { "x", x }, { "y", y } });
                    await Task.Delay(_freq_ms);
                }
            }
        }

        public Measurement(string name, double[,] values, int initial_delay_ms, int freq_ms) : base(name)
        {
            _values = values;
            _freq_ms = freq_ms;
            _initial_delay_ms = initial_delay_ms;
        }

        private readonly double[,] _values;

        private readonly int _initial_delay_ms;

        private readonly int _freq_ms;

    }

}
