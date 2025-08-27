using Azure.Messaging.ServiceBus;


public class ProducerService
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;

    public ProducerService(ServiceBusClient client, ServiceBusSender sender)
    {
        _client = client;
        _sender = sender;
    }


    public async Task SendMessageAsync(string customMessage)
    {
        //Inicio envio de mensagens para a Fila - queue

        //ServiceBusClient client = new ServiceBusClient(connectionString, options);
        //ServiceBusSender sender = client.CreateSender(queueName);

        ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();

        for (int i = 0; i <= 6; i++)
        {
            if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {customMessage} - {i}")))
            {
                throw new Exception($"The exception: {i}.");
            }
        }

        try
        {
            await _sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"A batch of messages has been published");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while sending the message: {e.Message}");
        }
        finally
        {
            await _sender.DisposeAsync();
        }
        //Fim envio de mensagens para a Fila - queue
    }
}
