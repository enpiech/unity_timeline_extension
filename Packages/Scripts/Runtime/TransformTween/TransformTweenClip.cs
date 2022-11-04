using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Enpiech.TimelineExtensions.Runtime.TransformTween
{
    [Serializable]
    public sealed class TransformTweenClip : PlayableAsset, ITimelineClipAsset
    {
        [SerializeField]
        private ExposedReference<Transform> _startLocation;

        [SerializeField]
        private ExposedReference<Transform> _endLocation;

        [SerializeField]
        private readonly TransformTweenBehaviour _template = new();

        public ClipCaps clipCaps => ClipCaps.Blending;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TransformTweenBehaviour>.Create(graph, _template);
            var clone = playable.GetBehaviour();
            clone.InitLocations(_startLocation.Resolve(graph.GetResolver()), _endLocation.Resolve(graph.GetResolver()));
            return playable;
        }
    }
}