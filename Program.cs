using System;
using Azure.Messaging.ServiceBus;


//Console.WriteLine("Hello, World!");

string connectionString = "<primary_connection_string do servicebus. Esta em Settings > shared access policies > RootManageSharedAccessKey>";
string queueName = "<nome da fila que voce criou no servicebus>";

//Inicio envio de mensagens para a Fila - queue
QueueHelper helper = new QueueHelper();
ServiceBusClient? client = helper.GetServiceBusClient(connectionString, queueName);
ServiceBusSender? sender = helper.GetServiceBusSender(connectionString, queueName);
ServiceBusProcessor processor = helper.GetProcessor(connectionString, queueName);

ProducerService producer = new ProducerService(client,sender);

await producer.SendMessageAsync("Hello, Bruno!");

//Fim processamento/consumo de mensagens da fila
ConsumerService consumerService = new ConsumerService(client, processor);
await consumerService.ConsumeMessagesAsync();

await sender.DisposeAsync();
await processor.DisposeAsync();
await client.DisposeAsync();
helper.Dispose();

/* 

//Inicio envio de mensagens para a Fila - queue
var options = new ServiceBusClientOptions
{
    TransportType = ServiceBusTransportType.AmqpWebSockets,
    //WebProxy = new WebProxy("https://proxyserver:80", true)
};


ServiceBusClient client = new ServiceBusClient(connectionString, options);
ServiceBusSender sender = client.CreateSender(queueName);

ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

for (int i = 0; i <= 6; i++)
{
    if(!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}"))){
        throw new Exception($"The exception: {i}.");
    }
}

try
{
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of messages has been published");
}
catch (Exception e)
{    
    Console.WriteLine($"An error occurred while sending the message: {e.Message}");    
}
finally {
    await sender.DisposeAsync();
    //await client.DisposeAsync();
} */

//Fim envio de mensagens para a Fila - queue


//Inicio processamento/consumo de mensagens da fila

/* 
ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
client = new ServiceBusClient(connectionString, options);
try
{
    processor.ProcessMessageAsync += MessageHandler;

    processor.ProcessErrorAsync += ErrorHandler;

    await processor.StartProcessingAsync();

    Console.WriteLine($"Press any key to exit");
    Console.ReadKey();
}
catch (Exception e)
{    
    Console.WriteLine($"An error occurred while sending the message: {e.Message}");    
}
finally {
    await processor.DisposeAsync();
    await client.DisposeAsync();
} 
*/

//Fim processamento/consumo de mensagens da fila

//metodo auxiliar
/* async Task MessageHandler(ProcessMessageEventArgs args)
{
    try
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine($"Received message: {body}");

        // Processar a mensagem aqui
        // Simular processamento
        await Task.Delay(1000);

        // Marcar mensagem como completada (remove da fila)
        await args.CompleteMessageAsync(args.Message);
        Console.WriteLine("Message processed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing message: {ex.Message}");

        // Opções de tratamento:
        // 1. Abandon (volta para a fila para retry)
        await args.AbandonMessageAsync(args.Message);

        // 2. Dead letter (envia para dead letter queue)
        // await args.DeadLetterMessageAsync(args.Message, "ProcessingError", ex.Message);
    }
}

//metodo auxiliar
Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine($"Message handler encountered an exception {args.Exception.ToString()}.");
    
    return Task.CompletedTask;
}
 */