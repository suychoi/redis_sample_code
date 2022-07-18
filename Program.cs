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

			// 시작
			ConnectionMultiplexer redisCluster = null;

			try
			{
				string connectString = "172.30.5.186:6379,172.30.5.167:6379";  //172.30.5.167 , 172.30.5.186
				var options = ConfigurationOptions.Parse(connectString);
					options.AllowAdmin = true;
					options.ConfigCheckSeconds = 10;
					options.SyncTimeout = 1000000;
					//  options.Password = "password";
				redisCluster = ConnectionMultiplexer.Connect(options);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}


			IDatabase clusterDb = redisCluster.GetDatabase();

			/*
			string value1 = DateTime.Now.ToString("hh:mm:ss");

			clusterDb.StringSet("mykey1", value1);

			string result1 = clusterDb.StringGet("mykey1");

			Console.WriteLine(result1);
			*/

			for(int i = 0; i < 101; i++)
            {
				string key = "time" + i;				
				string when = DateTime.Now.ToString("hh:mm:ss");

				clusterDb.StringSet(key, when);

				string result = clusterDb.StringGet(key);

				Console.WriteLine(result);

				Thread.Sleep(20000);   // 20초 대기

			}
		}
	}

}


