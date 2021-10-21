﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetTooth.DataAccess;
using SweetTooth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetTooth.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        OrderRepo _repo;
        PaymentMethodRepo _pmRepo;

        public OrdersController(OrderRepo repo, PaymentMethodRepo pmRepo)
        {
            _repo = repo;
            _pmRepo = pmRepo;
            // need the payment method repo for the add function
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{orderId}")]
        public IActionResult GetSingleOrder(Guid orderId)
        {
            var order = _repo.GetSingleOrder(orderId);

            if (order == null)
            {
                return NotFound("Order was not found.");
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult AddOrder(CreateOrderCommand command)
        {
            var _order = command.Order;
            var _snacks = command.SnackIdQuantityList;

            _repo.Add(_order, _snacks);

            return Created($"/api/orders/{_order.Id}", _order);
        }
    }
}
