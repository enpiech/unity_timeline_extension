using System;
using UnityEngine;
using UnityEngine.Playables;

namespace TimelineExtensions.Runtime.TransformTween
{
    [Serializable]
    public sealed class TransformTweenBehaviour : PlayableBehaviour
    {
        public enum TweenType
        {
            Linear,
            Deceleration,
            Harmonic,
            Custom
        }

        private const float RIGHT_ANGLE_IN_RADS = Mathf.PI * 0.5f;

        [SerializeField]
        private Transform _startLocation = default!;

        [SerializeField]
        private Transform? _endLocation;

        [SerializeField]
        private bool _tweenPosition = true;

        [SerializeField]
        private bool _tweenRotation = true;

        [SerializeField]
        private TweenType _tweenType;

        [SerializeField]
        private AnimationCurve _customCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [SerializeField]
        private Vector3 _startingPosition;

        [SerializeField]
        private Quaternion _startingRotation = Quaternion.identity;

        private AnimationCurve _mDecelerationCurve = new(
            new Keyframe(0f, 0f, -RIGHT_ANGLE_IN_RADS, RIGHT_ANGLE_IN_RADS),
            new Keyframe(1f, 1f, 0f, 0f)
        );

        private AnimationCurve _mHarmonicCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private AnimationCurve _mLinearCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public Transform StartLocation => _startLocation;

        public Transform? EndLocation => _endLocation;

        public bool TweenPosition => _tweenPosition;

        public bool TweenRotation => _tweenRotation;

        public Vector3 StartingPosition => _startingPosition;

        public Quaternion StartingRotation => _startingRotation;

        private bool IsCustomCurveNormalised
        {
            get
            {
                var firstKey = _customCurve[0];
                if (!Mathf.Approximately(firstKey.time, 0f))
                {
                    return false;
                }

                if (!Mathf.Approximately(firstKey.value, 0f))
                {
                    return false;
                }

                var lastKey = _customCurve[_customCurve.length - 1];
                if (!Mathf.Approximately(lastKey.time, 1f))
                {
                    return false;
                }

                return Mathf.Approximately(lastKey.value, 1f);
            }
        }

        public void InitLocations(Transform startLocation, Transform endLocation)
        {
            _startLocation = startLocation;
            _endLocation = endLocation;
        }

        public void InitTransform(Vector3 startingPosition, Quaternion startingRotation)
        {
            _startingPosition = startingPosition;
            _startingRotation = startingRotation;
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (_startLocation)
            {
                _startingPosition = _startLocation.position;
                _startingRotation = _startLocation.rotation;
            }
        }

        public float EvaluateCurrentCurve(float time)
        {
            switch (_tweenType)
            {
                case TweenType.Custom when !IsCustomCurveNormalised:
                    Debug.LogError("Custom Curve is not normalised.  Curve must start at 0,0 and end at 1,1.");
                    return 0f;
                case TweenType.Linear:
                    return _mLinearCurve.Evaluate(time);
                case TweenType.Deceleration:
                    return _mDecelerationCurve.Evaluate(time);
                case TweenType.Harmonic:
                    return _mHarmonicCurve.Evaluate(time);
                default:
                    return _customCurve.Evaluate(time);
            }
        }
    }
}