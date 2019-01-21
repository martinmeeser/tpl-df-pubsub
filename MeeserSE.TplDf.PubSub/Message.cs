using System;
using System.Collections.Generic;

namespace MeeserSE.TplDf.PubSub
{
    public class Message
    {
        public static Message FastCopy(Message toCopy)
        {
            Message result = new Message();
            result.Publisher = toCopy.Publisher;
            result.Key = toCopy.Key;
            result.Attributes = new Dictionary<string, object>(toCopy.Attributes);
            result.ObjectValue = toCopy.ObjectValue;
            result.StringValue = toCopy.StringValue;
            result.DoubleValue = toCopy.DoubleValue;
            result.DoubleMatrix = toCopy.DoubleMatrix;

            return result;
        }

        public I_Publisher Publisher { get; private set; }
        public string Key { get; private set; }
        public Dictionary<string, object> Attributes { get; private set; }
        public Object ObjectValue { get; private set; }
        public string StringValue { get; private set; }
        public Double DoubleValue { get; private set; }
        public Double[,,] DoubleMatrix { get; private set; }


        public Message(I_Publisher publisher, string key, Dictionary<string, object> attributes, object objectValue, string stringValue, Double doubleValue, Double[,,] doubleMatrix)
        {
            Publisher = publisher;
            Key = key;
            Attributes = attributes;
            ObjectValue = objectValue;
            StringValue = stringValue;
            DoubleValue = doubleValue;
            DoubleMatrix = doubleMatrix;
        }

        private Message() { }
    }


}