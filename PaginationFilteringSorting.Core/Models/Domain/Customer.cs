﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PaginationFilteringSorting.Core.Models.Domain;

public partial class Customer
{
    public string CustomerId { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string ContactTitle { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Region { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<CustomerDemographic> CustomerTypes { get; } = new List<CustomerDemographic>();
}