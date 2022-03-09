using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondPerFrame;
        private int _currentFrameIndex;
        private float _nextFrameTime;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        private void OnEnable()
        {
            _secondPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secondPerFrame;
            _currentFrameIndex = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            if (_currentFrameIndex >= _sprites.Length)
            {
                if (_loop)
                { 
                    _currentFrameIndex = 0;
                }
                else
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return;
                }
            }
            _renderer.sprite = _sprites[_currentFrameIndex];
            _nextFrameTime += _secondPerFrame;
            _currentFrameIndex++;
        }
    }
}

