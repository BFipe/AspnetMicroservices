using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var storedOrder = await _orderRepository.GetByIdAsync(request.Id);

            if (storedOrder is null)
            {
                _logger.LogError($"Request to database with order id {request.Id} returns null");
                //throw new NotFoundException($"Order with id {request.Id} is not exists");
                throw new Exception();
            }

            _mapper.Map(request, storedOrder);

            await _orderRepository.UpdateAsync(storedOrder);

            _logger.LogInformation($"Order {storedOrder.Id} sucessfully updated");
        }
    }
}
