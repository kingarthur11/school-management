using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using Shared.Controllers;
using Shared.Models.Requests;
using Shared.Models.Responses;

namespace API.Controllers.Subscribe
{
    [ApiController]
    [Route("api/payment")]
    public class PayStackPayment : BaseController
    {
        private readonly IPaymentRepository _paymentRepository;

        public PayStackPayment(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("initialize")]
        public async Task<IActionResult> InitializePayment([FromBody] PaymentRequest request)
        {
            var accessCode = await _paymentRepository.InitializeTransaction(request);
            return Ok(new { AccessCode = accessCode });
        }

        [HttpPost("submit-card")]
        public async Task<IActionResult> SubmitCard(SubmitCardRequest request)
        {
            var accessCode = await _paymentRepository.SubmitCard(request);
            return Ok(new { AccessCode = accessCode });
        }

        // [HttpGet("verify/{reference}")]
        // public async Task<IActionResult> InitializeTransaction(PaymentRequest request)
        // {
        //     var isValid = await _paymentRepository.InitializeTransaction(request);
        //     return Ok(new { IsValid = isValid });
        // }

    }
}