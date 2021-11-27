﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UnaPinta.Data.Entities
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; protected set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        public override bool Equals(object obj)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(obj, settings) == JsonConvert.SerializeObject(obj, settings);
        }
    }
}
