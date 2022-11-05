using MyBox;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace AssemblyCSharp {
    [CreateAssetMenu]
    sealed class LoadSceneEffect : EffectBase {
        [SerializeField]
        SceneReference scene = new();
        [SerializeField]
        LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        public override void Invoke(GameObject context) {
            Assert.IsTrue(scene.IsAssigned, $"Scene has not been assigned to asset {this}!");
            scene.LoadScene(loadSceneMode);
        }
    }
}
