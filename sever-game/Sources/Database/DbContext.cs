using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TienKiemV2Remastered.Application.Threading;

namespace TienKiemV2Remastered.DatabaseManager
{
    public class DbContext
    {
        private static DbContext Instance;
        private MySqlConnectionStringBuilder _stringBuilder;
        public MySqlConnection Connection;

        public DbContext()
        {
            _stringBuilder = new MySqlConnectionStringBuilder();
            _stringBuilder["Server"] = ConfigManager.gI().MySqlHost;
            _stringBuilder["Port"] = ConfigManager.gI().MySqlPort;
            _stringBuilder["User Id"] = ConfigManager.gI().MySqlUsername;
            _stringBuilder["Password"] = ConfigManager.gI().MySqlPassword;
            _stringBuilder["charset"] = "utf8mb4";
        }

        public static DbContext gI()
        {
            if (Instance == null) Instance = new DbContext();
            return Instance;
        }

        public void ConnectToData()
        {
            Connection?.Close();    
            _stringBuilder["Database"] = ConfigManager.gI().MySqlDBData;
            Connection = new MySqlConnection(_stringBuilder.ToString());
            Connection.Open();
        }
    

        public void ConnectToAccount()
        {
            Connection?.Close();
            _stringBuilder["Database"] = ConfigManager.gI().MySqlDBAccount;
            Connection = new MySqlConnection(_stringBuilder.ToString());
            Connection.Open();
        }

        public void CloseConnect()
        {
            Connection?.Close();
        }
    }
}