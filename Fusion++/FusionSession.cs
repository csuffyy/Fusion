﻿using FusionPlusPlus.Model;
using FusionPlusPlus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionPlusPlus
{
	internal class FusionSession
	{
		private bool _priorSettingImmerisve;
		private string _priorSettingLogPath;
		private LogMode _priorSettingMode;

		public FusionSession(IFusionService fusionService)
			: this(fusionService, new TemporaryLogStore())
		{
		}

		public FusionSession(IFusionService fusionService, ILogStore store)
		{
			FusionService = fusionService ?? throw new ArgumentNullException(nameof(fusionService));
			Store = store ?? throw new ArgumentNullException(nameof(store));
		}

		public void Start()
		{
			_priorSettingImmerisve = FusionService.Immersive;
			_priorSettingLogPath = FusionService.LogPath;
			_priorSettingMode = FusionService.Mode;

			Store.Prepare();

			FusionService.LogPath = Store.Path;
			FusionService.Immersive = true;
			FusionService.Mode = LogMode.All;
		}

		public void End()
		{
			FusionService.Immersive = _priorSettingImmerisve;
			FusionService.LogPath = _priorSettingLogPath;
			FusionService.Mode = _priorSettingMode;
		}

		public IFusionService FusionService { get; }

		public ILogStore Store { get; }
	}
}
