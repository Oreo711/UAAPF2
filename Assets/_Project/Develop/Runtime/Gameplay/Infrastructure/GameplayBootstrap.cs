using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.Config_Management.Configs.Scripts;
using _Project.Develop.Runtime.Utilities.ConfigsManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;


namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayBootstrap : SceneBootstrap
	{
		[SerializeField] private Gameplay _gameplay;

		private DIContainer       _container;
		private GameplayInputArgs _inputArgs;

		public override void ProcessRegistrations (DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
				throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

			_inputArgs = gameplayInputArgs;

			GameplayContextRegistrations.Process(_container, _inputArgs);
		}

		public override IEnumerator Initialize ()
		{
			ConfigProviderService configProvider = _container.Resolve<ConfigProviderService>();

			GameplayConfig config = configProvider.GetConfig<GameplayConfig>();

			_gameplay.Initialize(
				_inputArgs.GameMode,
				config,
				_container.Resolve<SceneSwitcherService>(),
				_container.Resolve<ICoroutinePerformer>(),
				_container.Resolve<WalletService>(),
				_container.Resolve<PlayerDataProvider>());
			_gameplay.Setup();

			yield break;
		}

		public override void Run () {}
	}
}
