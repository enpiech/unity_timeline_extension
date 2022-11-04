using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Enpiech.TimelineExtensions.Runtime.Cutscenes
{
    public sealed class CutsceneTimelineBehaviour : MonoBehaviour
    {
        [Header("Marker Events")]
        [SerializeField]
        private UnityEvent _cutsceneTimelineFinished = default!;

        [Header("Timeline")]
        [SerializeField]
        private PlayableDirector _cutsceneTimeline = default!;

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