using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Effects {
    [CreateAssetMenu]
    sealed class GameManagerEffect : EffectBase {
        enum GameMethod {
            ExitGame,
            ReloadCurrentScene,
        }

        [SerializeField]
        GameMethod method;

        public override void Invoke(GameObject context) {
            switch (method) {
                case GameMethod.ExitGame:
                    Application.Quit();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    break;
                case GameMethod.ReloadCurrentScene:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                default:
                    break;
            }
        }
    }
}
