using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));
            return instance;
        }
    }

    [SerializeField]
    private AudioClip[] BGMClip;
    [SerializeField]
    private AudioClip[] EffectClip;

    private AudioSource BGMSource;
    private AudioSource[] EffectSource = new AudioSource[10];
    private int EffectSourceNum = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.playOnAwake = false;
        BGMSource.loop = true;

        for (int i = 0; i < 10; ++i)
        {
            EffectSource[i] = gameObject.AddComponent<AudioSource>();
            EffectSource[i].playOnAwake = false;
        }

        SetBGM("Title");
    }

    public void SetBGM(string name)
    {
        if (name == "")
            BGMSource.Stop();

        for (int i = 0; i < BGMClip.Length; ++i)
            if (BGMClip[i].name == name)
            {
                BGMSource.clip = BGMClip[i];
                BGMSource.Play();
                break;
            }
    }

    public void SetEffect(string name)
    {
        for (int i = 0; i < EffectClip.Length; ++i)
            if (EffectClip[i].name == name)
            {
                EffectSource[EffectSourceNum].clip = EffectClip[i];
                EffectSource[EffectSourceNum++].Play();

                if (EffectSourceNum == EffectSource.Length)
                    EffectSourceNum = 0;

                return;
            }
    }

    public void SetVolume(float volume)
    {
        BGMSource.volume = volume;

        for (int i = 0; i < EffectSource.Length; ++i)
            EffectSource[i].volume = volume;
    }
}