using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Services
{
    public interface ISQLitePlatform
    {
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetConnectionAsync();
    }
}
