using System;
using System.Collections.Generic;

using I18N.West;
using MySql.Data.MySqlClient;
using Rocket.API;
using Steamworks;

using static Rocket.Unturned.Logging.Logger;

namespace Rocket.Mash.GearUp
{
    public class DatabaseMgr
    {
        internal DatabaseMgr()
        {
            CP1250 cP1250 = new CP1250();
            this.CheckSchema();
        }

        internal void CheckSchema()
        {
            try
            {
                MySqlConnection mySqlConnection = this.createConnection();
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = string.Concat("show tables like '", GearUp.Instance.Configuration.TableName, "'");
                mySqlConnection.Open();
                if (mySqlCommand.ExecuteScalar() == null)
                {
                    mySqlCommand.CommandText = string.Concat("CREATE TABLE `", GearUp.Instance.Configuration.TableName, "` (`id` int(11) NOT NULL AUTO_INCREMENT, `steamId` varchar(32) NOT NULL DEFAULT \"1\", `kitName` varchar(32) NOT NULL, `cooldownTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, PRIMARY KEY (`id`)) ");
                    mySqlCommand.ExecuteNonQuery();
                }
                mySqlConnection.Close();
            }
            catch (Exception exception)
            {
                LogException(exception);
            }
        }

        private MySqlConnection createConnection()
        {
            MySqlConnection mySqlConnection = null;
            try
            {
                if (GearUp.Instance.Configuration.DatabasePort == 0)
                {
                    GearUp.Instance.Configuration.DatabasePort = 3306;
                }
                mySqlConnection = new MySqlConnection(string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};PORT={4};", new object[] { 
                    GearUp.Instance.Configuration.DatabaseAddress, 
                    GearUp.Instance.Configuration.DatabaseName, 
                    GearUp.Instance.Configuration.DatabaseUsername, 
                    GearUp.Instance.Configuration.DatabasePassword,
                    GearUp.Instance.Configuration.DatabasePort}));
            }
            catch (Exception exception)
            {
                LogException(exception);
            }
            return mySqlConnection;
        }

        internal int AddCooldown(DateTime time, string steamId="1", string kitname="Global")
        {
        	string mysqlTime = time.ToString("yyyy-MM-dd HH:mm:ss");
        	int result = 0;
        	try
        	{
        		MySqlConnection mySqlConnection = this.createConnection();
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = string.Concat("select count(*) from `", 
            	    GearUp.Instance.Configuration.TableName, 
            	    "` where steamId='",
            	    steamId,
            	    "' and kitname='",
            	    kitname,
            	    "'");
                mySqlConnection.Open();
                result = int.Parse(mySqlCommand.ExecuteScalar());
                string sql = "";
                if (result <= 0)
                {
            	    sql = string.Concat("update `",
            	        GearUp.Instance.Configuration.TableName,
            	        "` set cooldownTime = mysqlTime where steamId='",
            	        steamId,
            	        "' and kitName='",
            	        kitname,
            	        "'");
                } 
                else
                {
            	    sql = string.Concat("insert into `",
            	        GearUp.Instance.Configuration.TableName,
            	        "` (steamId, kitName, cooldownTime) VALUES ('",
            	        steamId,
            	        "', '",
            	        kitname,
            	        "', TIMESTAMP('",
            	        mysqlTime,
            	        "'))");
                }
                mySqlCommand.CommandText = sql;
                result = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception exception)
            {
          	    LogException(exception);
            }
            return result;
        }

        internal DateTime? GetCooldown(string steamId, string kitname="Global")
        {
        	DateTime? result = null;
        	try
        	{
        		MySqlConnection mySqlConnection = this.createConnection();
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = string.Concat("select cooldownTime from `", 
            	    GearUp.Instance.Configuration.TableName, 
            	    "` where steamId='",
            	    steamId,
            	    "' and kitname='",
            	    kitname,
            	    "'");
                mySqlConnection.Open();
                result = DateTime.Parse(mySqlCommand.ExecuteScalar());
                mySqlConnection.Close();
            }
            catch (Exception exception)
            {
          	    LogException(exception);
            }
            return result;
        }
        
        internal int DeleteCooldown(string steamId, string kitname="Global")
        {
        	int result = 0;
        	try
        	{
        		MySqlConnection mySqlConnection = this.createConnection();
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = string.Concat("delete from `", 
            	    GearUp.Instance.Configuration.TableName, 
            	    "` where steamId='",
            	    steamId,
            	    "' and kitname='",
            	    kitname,
            	    "'");
                mySqlConnection.Open();
                result = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception exception)
            {
          	    LogException(exception);
            }
            return result;
        }
        
        internal void DeleteAllCooldowns(string steamId, string kitname="Global")
        {
        	try
        	{
        		MySqlConnection mySqlConnection = this.createConnection();
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = string.Concat("truncate `", 
            	    GearUp.Instance.Configuration.TableName, 
            	    "`");
                mySqlConnection.Open();
                result = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception exception)
            {
          	    LogException(exception);
            }
        }
    }
}
