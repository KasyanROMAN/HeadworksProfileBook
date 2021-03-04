using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}
