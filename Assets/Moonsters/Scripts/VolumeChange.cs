using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace Moonsters
{
    public class VolumeChange : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private AudioMixer audioGroup;
        [SerializeField] private VolumeGroup volumeGroup;
        [SerializeField][Range(0, 1)] private float startValue;

        private void Start()
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            audioGroup.SetFloat(volumeGroup.ToString(), FloatToMixer(startValue));
        }

        public void OnVolumeChanged(float value)
        {
            audioGroup.SetFloat(volumeGroup.ToString(), FloatToMixer(value));
        }

        public static float MixerToFloat(float value)
        {
            return Mathf.Pow(10, value / 20);
        }

        public static float FloatToMixer(float value)
        {
            return Mathf.Log10(value) * 20;
        }
    }

    public enum VolumeGroup
    {
        SFX,
        Music
    }
}