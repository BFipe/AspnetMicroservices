using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order is null)
            {
                _logger.LogError($"Order with id {request.Id} is not found in the database");
                throw new NotFoundException($"Order with id {request.Id} is not exists");
            }
            await _orderRepository.DeleteAsync(order);
            _logger.LogInformation($"Order with id {request.Id} sucessfully deleted");
        }
    }
}
