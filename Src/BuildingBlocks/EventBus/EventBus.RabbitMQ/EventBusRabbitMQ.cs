using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;


namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistentConnection _persistentConnection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IModel _consumerChannel;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            if (config.Connection != null)
            {
                _connectionFactory = (ConnectionFactory)EventBusConfig.Connection;
            }
            else
            {
                _connectionFactory = new ConnectionFactory();
            }
            _persistentConnection = new RabbitMQPersistentConnection(_connectionFactory, config.ConnectionRetryCount);

            _consumerChannel = CreateConsumerChannel();

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProcessEventName(eventName);
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            _consumerChannel.QueueUnbind(queue: eventName,
                exchange: EventBusConfig.DefaultTopicName,
                routingKey: eventName);
            if (SubsManager.IsEmpty)
            {
                _consumerChannel.Close();
            }
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {


                    //loglama yap

                });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);
            //Ensure exchange exists while publishing
            _consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");



            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = _consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                _consumerChannel.BasicPublish(
                    exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if (!SubsManager.HasSubscriptionForEvent(eventName))
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }
                //emin olmak que consume için
                _consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                _consumerChannel.QueueBind(queue: GetSubName(eventName),
                    exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName);
            }

            SubsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);

        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscription<T, TH>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName,
                type: "direct");
            return channel;

        }
        private void StartBasicConsume(string eventName)
        {
            if (_consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(_consumerChannel);
                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: GetSubName(eventName),
                    autoAck: false,
                    consumer: consumer);
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception)
            {
                //logging
            }
            _consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }
    }
}
