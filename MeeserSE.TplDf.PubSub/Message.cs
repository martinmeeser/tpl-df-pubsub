using System;
using System.Collections.Generic;
using System.Text;

namespace MeeserSE.TplDf.PubSub
{
    public class Message
    {
        public static Message FastCopy(Message toCopy)
        {
            Message result = new Message();
            result.PublisherName = toCopy.PublisherName;
            result.Key = toCopy.Key;
            result._headers = new Dictionary<string, object>(toCopy.Headers);
            result.ObjectValue = toCopy.ObjectValue;
            result.StringValue = toCopy.StringValue;
            result.DoubleValue = toCopy.DoubleValue;
            result.MatrixValue = toCopy.MatrixValue;

            return result;
        }

        public string PublisherName { get; private set; }
        public string Key { get; private set; }

        public Object ObjectValue { get; private set; }
        public string StringValue { get; private set; }
        public double? DoubleValue { get; private set; }
        public double[,,] MatrixValue { get; private set; }


        public void AddHeader(string key, object value)
        {
            _headers.Add(key, value);
        }

        public object GetHeader(string key)
        {
            return _headers[key];
        }

        public Dictionary<string, object> Headers => _headers == null ? null : new Dictionary<string, object>(_headers);

        public override string ToString()
        {

            StringBuilder resultBuilder = new StringBuilder(PublisherName);
            resultBuilder.Append(" -- ");

            if (ObjectValue != null)
            {
                resultBuilder.Append("obj=").Append(ObjectValue.ToString());
            }
            if (StringValue != null)
            {
                resultBuilder.Append("str=").Append(StringValue.ToString());
            }
            if (DoubleValue != null)
            {
                resultBuilder.Append("dbl=").Append(DoubleValue.ToString());
            }
            if (MatrixValue != null)
            {
                resultBuilder.Append("mtx").Append(MatrixValue.ToString());
            }

            if (_headers != null)
            {
                resultBuilder.Append("\t{");
                foreach (KeyValuePair<string, object> header in _headers)
                {
                    // TODO null-checks
                    // TODO recursivly dive into headers ??? possibly change to string,string
                    resultBuilder.Append(" {").Append(header.Key).Append(" ").Append(header.Value.ToString()).Append("}");
                }
                resultBuilder.Append(" }");
            }

            return resultBuilder.ToString();
        }

        public Message(string publisherName, string key, object objectValue, string stringValue, double? doubleValue, Double[,,] doubleMatrix)
        {
            PublisherName = publisherName;
            Key = key;
            ObjectValue = objectValue;
            StringValue = stringValue;
            DoubleValue = doubleValue;
            MatrixValue = doubleMatrix;
        }

        public Message(string publisherName, string key, object objectValue, string stringValue, double? doubleValue, Double[,,] doubleMatrix, Dictionary<string, object> headers)
        {
            PublisherName = publisherName;
            Key = key;
            ObjectValue = objectValue;
            StringValue = stringValue;
            DoubleValue = doubleValue;
            MatrixValue = doubleMatrix;
            _headers = headers;
        }

        private Message() { }

        private Dictionary<string, object> _headers = new Dictionary<string, object>();
    }


}