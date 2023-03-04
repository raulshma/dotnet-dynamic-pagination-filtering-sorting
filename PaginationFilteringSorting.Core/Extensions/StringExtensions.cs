using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationFilteringSorting.Core.Extensions;
public static class StringExtensions
{
    public static bool IsNullEmpty(this string? value) => string.IsNullOrEmpty(value);
    public static bool Not(this bool value) => !value;
}
