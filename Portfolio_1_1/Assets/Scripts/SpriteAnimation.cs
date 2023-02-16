using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField][Range(1, 30)] private int _frameRate = 10;
    [SerializeField] private AnimationClip[] _clips;
    [SerializeField] private UnityEvent<string> _onComplete;

    private SpriteRenderer _renderer;
    private float _secondsPerFrame;
    private int _currentFrame;
    private float _nextFrameTime;

    private bool _isPlaying = true;

    private int _currentClip;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;

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
        for (var i = 0; i < _clips.Length; i++)
        {
            if (_clips[i].Name == clipName)
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
        _nextFrameTime = Time.time + _secondsPerFrame;
        enabled = _isPlaying = true;
        _currentFrame = 0;
    }

    private void OnEnable()
    {
        _nextFrameTime = Time.time + _secondsPerFrame;
    }

    private void Update()
    {
        if (_nextFrameTime > Time.time) // проигрывается ли анимация и наступила ли смена кадра
            return;
        var clip = _clips[_currentClip];
        if (_currentFrame >= clip.Sprites.Length)
        {
            if (clip.Loop)
            {
                _currentFrame = 0;
            }
            else
            {
                enabled = _isPlaying = clip.AllowNextClip;
                clip.OnComplete?.Invoke();
                _onComplete?.Invoke(clip.Name);
                if (clip.AllowNextClip)
                {
                    _currentFrame = 0;
                    _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                }
            }
            return;
        }
        _renderer.sprite = clip.Sprites[_currentFrame];

        _nextFrameTime += _secondsPerFrame;
        _currentFrame++;
    }
}


[System.Serializable]
public class AnimationClip
{
    [SerializeField] private string name;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _loop;
    [SerializeField] private bool _allowNextClip;
    [SerializeField] private UnityEvent _onComplete;

    public string Name => name;
    public Sprite[] Sprites => _sprites;
    public bool Loop => _loop;
    public bool AllowNextClip => _allowNextClip;
    public UnityEvent OnComplete => _onComplete;
}
