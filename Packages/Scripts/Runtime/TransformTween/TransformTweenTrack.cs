using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Enpiech.TimelineExtensions.Scripts.Runtime.TransformTween
{
    [TrackColor(0.855f, 0.8623f, 0.870f)]
    [TrackClipType(typeof(TransformTweenClip))]
    [TrackBindingType(typeof(Transform))]
    public sealed class TransformTweenTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<TransformTweenMixerBehaviour>.Create(graph, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            if (director.GetGenericBinding(this) is not Transform comp)
            {
                return;
            }

            var so = new SerializedObject(comp);
            var iter = so.GetIterator();
            while (iter.NextVisible(true))
            {
                if (iter.hasVisibleChildren)
                {
                    continue;
                }

                driver.AddFromName<Transform>(comp.gameObject, iter.propertyPath);
            }
#endif
            base.GatherProperties(director, driver);
        }
    }
}