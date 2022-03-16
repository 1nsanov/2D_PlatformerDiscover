using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] [Range(1,30)] private int _frameRate = 10;
        [SerializeField] private UnityEvent<string> _onComplete;
        [SerializeField] private AnimationClip[] _sprites;

        private SpriteRenderer _renderer;
        private float _secondPerFrame;
        private float _nextFrameTime;
        private int _currentFrameIndex;
        private bool _isPlaying = true;
        private int _currentClip;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondPerFrame = 1f / _frameRate;

            StartAnimation();
        }
        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }
        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (int i = 0; i < _sprites.Length; i++)
            {
                if (_sprites[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }
            enabled = _isPlaying = false;
        }
        private void StartAnimation()
        {
            _nextFrameTime = Time.time + _secondPerFrame;
            _isPlaying = true;
            _currentFrameIndex = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;
            var clip = _sprites[_currentClip];
            if (_currentFrameIndex >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrameIndex = 0;
                }
                else
                {
                    clip.OnComplete?.Invoke();
                    _onComplete?.Invoke(clip.Name);
                    enabled = _isPlaying = clip.AllowNextClip;
                    if (clip.AllowNextClip)
                    {
                        _currentFrameIndex = 0; 
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _sprites.Length);
                    }
                    return;
                }
            }
            _renderer.sprite = clip.Sprites[_currentFrameIndex];
            _nextFrameTime += _secondPerFrame;
            _currentFrameIndex++;
        }
    }

    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onComplete;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool Loop => _loop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnComplete => _onComplete;
    }
}

