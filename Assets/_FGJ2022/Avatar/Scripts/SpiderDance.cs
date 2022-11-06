using Game.Avatar.SpiderImpl;
using UnityEngine;

namespace Game.Avatar
{
    public class SpiderDance: MonoBehaviour
    {
        public void StartDance()
        {
            var spider = FindObjectOfType<Spider>();
            spider.breathing = true;
            spider.breathePeriod = 0.7f;
            spider.breatheMagnitude = Random.Range(0.7f, 1f);
        }
    }
}