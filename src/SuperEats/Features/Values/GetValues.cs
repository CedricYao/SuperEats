﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using SuperEats.Extensions;

namespace SuperEats.Features.Values
{
    public class GetValues
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                //Task.Run() is used here just to demonstrate the usage of async/await method
                return await Task.Run(() => new Response { Result = JsonConvert.SerializeObject(request) }, cancellationToken);
            }
        }

        public class Request : IRequest, IRequest<Response>
        {
            public string Param1 { get; set; }
            public string Param2 { get; set; }
        }

        public class Response
        {
            public string Result { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(p => p.Param1)
                    .IsNonEmptyEightDigitNumber();

                RuleFor(p => p.Param2)
                    .NotEmpty();
            }
        }
    }
}