# Demo em C# console ServiceBus do Azure 

## Descrição
Projeto de demonstração de um Entregador e um Consumidor de mensagens de fila do Azure Service Bus.
Possui a estrutura básica para envio e consumo de mensagens, que podem ser usadas em aplicações
que necessitam de serviço de mensageria.

## Requisitos necessários
* Ter acesso ao Portal do Azure (conta pessoal, corporativa ou estudante).
* Ter um Service Bus criado no Azure.
* Ter uma fila criada no service Bus

## Comandos para criar um service bus
Links: https://portal.azure.com/#cloudshell/
Logado no portal Azure, abrir o azure shell e fazer esses comandos abaixo
~~~ 
# Criar grupo de recursos. Location de exemplo é 'eastus'
az group create --name meu_grupo1 --location eastus

# Criar service-bus
az servicebus namespace create --resource-group meu_grupo1 --name namespace_nome --location eastus

# Criar a fila
az servicebus queue create --resource-group meu_grupo1 --namespace-name namespace_nome --name myQueue1

~~~
