using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment.Meta;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateGameModeSelectionService);
        }

        private static GameModeSelectionService CreateGameModeSelectionService (DIContainer c)
        {
            return new GameModeSelectionService(c.Resolve<ICoroutinePerformer>(), c.Resolve<SceneSwitcherService>());
        }
    }
}
