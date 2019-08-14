﻿using System.Runtime.InteropServices;
using Mql.Telegram.IntegrationTests.Framework;
using Mql.Telegram.IntegrationTests.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using StEn.MMM.Mql.Common.Services.InApi.Entities;
using Telegram.Bot.Types;

namespace Mql.Telegram.IntegrationTests
{
	[TestFixture]
	public class TelegramDllImportTests
	{
#if !DEBUG

		[SetUp]
		public void PerTestSetup()
		{
			// Method intentionally left empty.
		}

		[Test]
		[Category(Constants.TelegramBotApiMethods.GetMe)]
		public void GetMeReturnsBotUser()
		{
			Initialize(Secrets.BOT_API_KEY, 100);
			SetDebugOutput(true);
			var result = GetMe();
			var successResponse = JsonConvert.DeserializeObject<Response<User>>(result);
			Assert.True(successResponse.Content.IsBot);
		}

		#region DllImport

		[DllImport(Constants.AssemblyUnderTestName)]
		private static extern void SetDebugOutput([MarshalAs(UnmanagedType.Bool)] bool enabled);

		[DllImport(Constants.AssemblyUnderTestName)]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		private static extern string Initialize(
			[MarshalAs(UnmanagedType.LPWStr)] string apiKey,
			int timeout);

		[DllImport(Constants.AssemblyUnderTestName)]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		private static extern string GetMe();

		[DllImport(Constants.AssemblyUnderTestName)]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		private static extern string StartGetMe();

		#endregion

#endif
	}
}
