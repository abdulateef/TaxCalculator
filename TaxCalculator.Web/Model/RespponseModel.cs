﻿using System;
namespace TaxCalculator.Web.Model
{
    public class RespponseModel<T>
    {
        public string? Message { get; set; }
        public T? ResponseData { get; set; }
        public bool RequestStatus { get; set; }
        public string? ResponseCode { get; set; }
    }
}

