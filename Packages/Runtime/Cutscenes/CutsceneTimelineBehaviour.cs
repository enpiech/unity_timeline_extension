using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace CricketsWorld
{
    public class CutsceneTimelineBehaviour : MonoBehaviour
    {
        [Header("Timeline")]
        [SerializeField]
        private PlayableDirector _cutsceneTimeline = default!;

        [Header("Marker Events")]
        [SerializeField]
        private UnityEvent _cutsceneTimelineFinished = default!;

        public void StartTimeline()
        {
            _cutsceneTimeline.Play();
        }

        public void TimelineFinished()
        {
            _cutsceneTimelineFinished.Invoke();
        }
    }
}