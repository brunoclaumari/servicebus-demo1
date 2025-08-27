using Azure.Messaging.ServiceBus;


public class QueueHelper : IDisposable
{
    private ServiceBusClient? _client;
    private ServiceBusSender? _sender;
    private ServiceBusProcessor _processor;


    public ServiceBusClient GetServiceBusClient(string connectionString, string queueName)
    {
        if (_client == null)
        {
            var options = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets,
            };
            _client = new ServiceBusClient(connectionString, options);
        }

        return _client;
    }

    public ServiceBusSender GetServiceBusSender(string connectionString, string queueName)
    {
        _client = GetServiceBusClient(connectionString, queueName);

        if (_sender == null)
        {
            _sender = _client.CreateSender(queueName);
        }

        return _sender;
    }

    public ServiceBusProcessor GetProcessor(string connectionString,string queueName)
    {
        _client = GetServiceBusClient(connectionString, queueName);
        if (_processor == null)
            _processor = _client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
        return _processor;
    }


    public void Dispose()
    {
        _sender?.DisposeAsync();
        _processor.DisposeAsync();
        _client?.DisposeAsync();        
    } 
    
}
