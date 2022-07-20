using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;       // Stopwatch
using StackExchange.Redis;		// from nuGet "StackExchange.Redis"

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // ctrl + F5 : 실행

			// 연결 시작
			ConnectionMultiplexer redisCluster = null;

			try
			{
				string connectString = "172.30.5.186:3950,172.30.5.186:1521,172.30.5.186:6379,172.30.5.167:1521,172.30.5.167:3950,172.30.5.167:6379";
				//172.30.5.186:3950,172.30.5.186:1521,172.30.5.186:6379,172.30.5.167:1521,172.30.5.167:3950,172.30.5.167:6379
				//My ip = ?

				var options = ConfigurationOptions.Parse(connectString);
					options.AllowAdmin = true;
					options.ConfigCheckSeconds = 10;
					//options.ConnectRetry = 300;
					//options.ReconnectRetryPolicy = new ExponentialRetry(5000);
					options.SyncTimeout = 1000;
					options.Password = "foobared";
				redisCluster = ConnectionMultiplexer.Connect(options);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			/*
			string value1 = DateTime.Now.ToString("hh:mm:ss");

			clusterDb.StringSet("test_connection", value1);

			Console.WriteLine("write done");

			string result1 = clusterDb.StringGet("test_connection");

			Console.WriteLine(result1);
			*/

			
			for(int i = 0; i < 101; i++)
            {
				IDatabase clusterDb = redisCluster.GetDatabase();

				string key = "time" + i;				
				string t = DateTime.Now.ToString("hh:mm:ss");

				Console.WriteLine("start write " + key + " " + t);

				try
				{
					clusterDb.StringSet(key, t);
					string result = clusterDb.StringGet(key);

					Console.WriteLine(result);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				Thread.Sleep(2000);   // 2000 = 2초 대기
			}
		}
	}
}


