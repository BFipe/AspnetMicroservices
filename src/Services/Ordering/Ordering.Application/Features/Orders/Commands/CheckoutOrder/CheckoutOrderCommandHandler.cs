using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IMapper mapper, 
            IOrderRepository orderRepository, 
            IEmailService emailService, 
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} was created");

            await SendMail(newOrder);

            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email() { To = order.EmailAddress, Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Sending email for order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
