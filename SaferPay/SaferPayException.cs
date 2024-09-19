using System;
using System.Net;
using SaferPay.Models;

namespace SaferPay
{
    public class SaferPayException : Exception
    {
        public SaferPayException(HttpStatusCode httpStatusCode, string message) : base(
            $"{httpStatusCode}: {message}")
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; }
    }
}