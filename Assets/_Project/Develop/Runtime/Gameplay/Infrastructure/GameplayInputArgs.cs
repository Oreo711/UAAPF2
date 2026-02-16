using _Project.Develop.Runtime.Meta;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;


namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameMode gameMode)
        {
            GameMode = gameMode;
        }

        public GameMode GameMode {get;}
    }
}
