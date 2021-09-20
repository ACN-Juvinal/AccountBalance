using AccountBalance.Database;
using AccountBalance.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AccountBalance.Features
{
    [ApiController]
    public class GetBalanceAndPaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetBalanceAndPaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("/api/getbalanceandpayments")]
        public async Task<IActionResult> GetBalanceAndPayments()
        {
            var userId = User.Claims.First(s => s.Type == ClaimTypes.NameIdentifier).Value;
            var response = await _mediator.Send(new GetBalanceandPayments.Query(userId));
            return response == null ? NotFound() : Ok(response);
        }
    }

    public static class GetBalanceandPayments
    {
        //Query
        public record Query(string userId) : IRequest<Response>;

        //Handler
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly AppDBContext _db;

            public Handler(AppDBContext db)
            {
                _db = db;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                if (_db.UserBalances.Any(s => s.User.Id == request.userId))
                {
                    var balance = _db.UserBalances.Where(s => s.User.Id == request.userId).Sum(s => s.Amount);
                    var payments = _db.Payments.Where(s => s.User.Id == request.userId).OrderByDescending(s => s.Date);
                    var response = new Response()
                    {
                        Balance = balance,
                        Payments = payments
                    };
                }

                return null;
            }
        }

        public record Response
        {
            public decimal Balance { get; init; }
            public IEnumerable<Payment> Payments { get; init; }
        }

    }
}
