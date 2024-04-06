﻿using System.Text.Json;

namespace InventoryManagerAPI
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
