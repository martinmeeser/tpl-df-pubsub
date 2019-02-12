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
            result._attributes = new Dictionary<string, object>(toCopy.Attributes);
            result.ObjectValue = toCopy.ObjectValue;
            result.StringValue = toCopy.StringValue;
            result.DoubleValue = toCopy.DoubleValue;
            result.DoubleMatrix = toCopy.DoubleMatrix;

            return result;
        }

        public IPublisher Publisher { get; private set; }
        public string Key { get; private set; }

        public Object ObjectValue { get; private set; }
        public string StringValue { get; private set; }
        public double? DoubleValue { get; private set; }
        public double[,,] DoubleMatrix { get; private set; }


        public void AddAttribute(string key, object value)
        {
            if (_attributes == null)
            {
                _attributes.Add(key, value);
            }
        }

        public object GetAttribute(string key)
        {
            return _attributes[key];
        }

        public Dictionary<string, object> Attributes => _attributes == null ? null : new Dictionary<string, object>(_attributes);

        public Message(IPublisher publisher, string key, object objectValue, string stringValue, double? doubleValue, Double[,,] doubleMatrix)
        {
            Publisher = publisher;
            Key = key;
            ObjectValue = objectValue;
            StringValue = stringValue;
            DoubleValue = doubleValue;
            DoubleMatrix = doubleMatrix;
        }

        private Message() { }

        private Dictionary<string, object> _attributes;
    }


}