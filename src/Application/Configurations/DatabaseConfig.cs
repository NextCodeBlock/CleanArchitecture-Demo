using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.Configurations;

public class DatabaseConfig
{
    public string? DatabaseName { get; set; }
    public string? DatabaseConnection { get; set; }
    public bool? EnsureDatabaseCreated { get; set; }
    public bool? ApplyDataSeeder { get; set; }
}