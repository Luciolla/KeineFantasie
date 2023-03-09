using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fantasie
{
    public class AudioHelper : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _audioSFXList;
        [SerializeField] private List<AudioClip> _musicList;

        private float _sfxVolume = 1f;
        private bool _isPlayingMusic = true;
        private bool _isPlayingSFX = true;
        private bool _isMusicPlayingNow = false;

        private AudioClip _currentLevelMusic;

        public static AudioHelper Instance { get; private set; }
        public IList<AudioClip> GetSFTList => _audioSFXList;

        public static int StageIndex = 0;

        private void Start()
        {
            Instance = this;
            _currentLevelMusic = _musicList[StageIndex];
            PlayMusic();
        }

        private void Update()
        {
            _audioSource.volume = _sfxVolume;
            CheckMusicToggle();
            CheckCurrentMusicIndex();
        }

        public void IsPlayingMusic() => _isPlayingMusic = !_isPlayingMusic;
        public void IsPlayingMusicNow() => _isMusicPlayingNow = !_isMusicPlayingNow;
        public void IsPlayingSFX() => _isPlayingSFX = !_isPlayingSFX;

        public void SetVolume(float volume) => _sfxVolume = volume;

        public void PlaySound(AudioClip clip)
        {
            if (_isPlayingSFX) _audioSource.PlayOneShot(clip);
        }

        public void PlayMusic()
        {
            if (_isPlayingMusic) _audioSource.PlayOneShot(_currentLevelMusic);
            _isMusicPlayingNow = true;
        }

        private void CheckMusicToggle() //todo fix
        {
            if (_isMusicPlayingNow && _isPlayingMusic) return;
            if (_isMusicPlayingNow)
            {
                _audioSource.Stop();
                IsPlayingMusicNow();
            }

            if (!_isMusicPlayingNow && !_isPlayingMusic) return;
            if (!_isMusicPlayingNow) PlayMusic();
        }

        private void CheckCurrentMusicIndex()
        {
            if (_audioSource.isPlaying) return;
            _currentLevelMusic = _musicList[StageIndex];
            PlayMusic();
        }
    }
}