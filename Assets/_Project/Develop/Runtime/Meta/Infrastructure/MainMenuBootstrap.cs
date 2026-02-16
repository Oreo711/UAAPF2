using System.Collections;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;


namespace _Project.Develop.Runtime.Utilities.CoroutinesManagment.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private WalletService _walletService;

        private PlayerDataProvider       _playerDataProvider;
        private ICoroutinePerformer      _coroutinePerformer;
        private GameModeSelectionService _gameModeSelectionService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _walletService = _container.Resolve<WalletService>();

            _playerDataProvider       = _container.Resolve<PlayerDataProvider>();
            _coroutinePerformer       = _container.Resolve<ICoroutinePerformer>();
            _gameModeSelectionService = _container.Resolve<GameModeSelectionService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Choose game mode. (1 - numbers, 2 - letters)");

            _gameModeSelectionService.Run();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinePerformer.StartCoroutine(_playerDataProvider.Save());
                Debug.Log("Saved!");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _coroutinePerformer.StartCoroutine(_playerDataProvider.Load());
                Debug.Log("Loaded!");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"Current balance: {_walletService.GetCurrency(CurrencyTypes.Gold).Value.ToString()}");
            }
        }
    }
}
