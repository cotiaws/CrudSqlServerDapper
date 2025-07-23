﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSqlServerDapper.Entities
{
    /// <summary>
    /// Modelo de dados para a entidade Cliente.
    /// </summary>
    public class Client
    {
        #region Properties

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        #endregion
    }
}
