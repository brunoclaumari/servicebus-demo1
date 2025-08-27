using Azure.Messaging.ServiceBus;


public class ConsumerService
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusProcessor _processor;

    public ConsumerService(ServiceBusClient client, ServiceBusProcessor processor)
    {
        _client = client;
        _processor = processor;
    }


    public async Task ConsumeMessagesAsync()
    {
        try
        {
            _processor.ProcessMessageAsync += MessageHandler;

            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync();

            Console.WriteLine($"Press any key to exit");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while sending the message: {e.Message}");
        }
        finally
        {
            await CloseAsync();
        }

        //Fim processamento/consumo de mensagens da fila
    }

    //metodo auxiliar
    async Task MessageHandler(ProcessMessageEventArgs args)
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


    public async Task CloseAsync()
    {
        await _processor.DisposeAsync();
        await _client.DisposeAsync();
    }
}
