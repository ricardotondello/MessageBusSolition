using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using MassTransit;
using MyStoreSale.Service.Interface;
using SharedKernel;

namespace MyStoreSale
{
    public class ProductConsumer : IConsumer<ProductMessage>
    {
        private readonly IStore _store;
        public ProductConsumer(IStore store)
        {
            _store = store;
        }
        public async Task Consume(ConsumeContext<ProductMessage> context)
        {
            await Console.Out.WriteLineAsync("cleaning cache");
            await _store.ClearProductsCacheAsync();
        }
    }
}