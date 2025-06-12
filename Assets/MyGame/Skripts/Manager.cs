using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DJManager : MonoBehaviour
{
    [Header("Turntable Links")]
    public AudioSource leftAudio;
    public Transform leftDisc;
    public Slider leftSpeedSlider;
    public Slider leftVolumeSlider;
    public TMP_Text leftNowPlaying;

    [Header("Turntable Rechts")]
    public AudioSource rightAudio;
    public Transform rightDisc;
    public Slider rightSpeedSlider;
    public Slider rightVolumeSlider;
    public TMP_Text rightNowPlaying;

    [Header("Mixer")]
    public Slider crossfader;

    public float rotationSpeed = 100f;

    void Start()
    {
        leftSpeedSlider.onValueChanged.AddListener(val => leftAudio.pitch = val);
        rightSpeedSlider.onValueChanged.AddListener(val => rightAudio.pitch = val);

        leftVolumeSlider.onValueChanged.AddListener(val => UpdateVolume());
        rightVolumeSlider.onValueChanged.AddListener(val => UpdateVolume());
        crossfader.onValueChanged.AddListener(val => UpdateVolume());

        UpdateVolume();
    }

    void Update()
    {
        if (leftAudio.isPlaying)
        {
            float speed = leftAudio.pitch * rotationSpeed;
            leftDisc.Rotate(Vector3.forward * speed * Time.deltaTime);
        }

        if (rightAudio.isPlaying)
        {
            float speed = rightAudio.pitch * rotationSpeed;
            rightDisc.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
    }


    void UpdateVolume()
    {
        float leftVol = leftVolumeSlider.value * (1f - crossfader.value);
        float rightVol = rightVolumeSlider.value * crossfader.value;

        leftAudio.volume = leftVol;
        rightAudio.volume = rightVol;
    }

    // Button-Funktionen
    public void PlayPauseLeft()
    {
        if (leftAudio.isPlaying) leftAudio.Pause();
        else leftAudio.Play();
    }

    public void PlayPauseRight()
    {
        if (rightAudio.isPlaying) rightAudio.Pause();
        else rightAudio.Play();
    }

    public void StopLeft()
    {
        leftAudio.Stop();
    }

    public void StopRight()
    {
        rightAudio.Stop();
    }

    public void UpdateNowPlayingLeft()
    {
        if (leftAudio.clip != null)
            leftNowPlaying.text = "Now Playing:\n" + leftAudio.clip.name;
        else
            leftNowPlaying.text = "No Track Loaded";
    }


    public void UpdateNowPlayingRight()
    {
        if (rightAudio.clip != null)
            rightNowPlaying.text = "Now Playing:\n" + rightAudio.clip.name;
        else
            rightNowPlaying.text = "No Track Loaded";
    }

}
