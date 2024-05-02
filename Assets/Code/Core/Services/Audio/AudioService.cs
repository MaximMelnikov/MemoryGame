using UnityEngine;

//have no time for really good audio service so made it stupidly simple)
public class AudioService : MonoBehaviour
{
    [SerializeField]
    private AudioClip _click;
    [SerializeField]
    private AudioClip _success;
    [SerializeField]
    private AudioClip _fail;
    [SerializeField]
    private AudioClip _win;
    [SerializeField]
    private AudioClip _loose;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayClick()
    {
        Play(_click);
    }

    public void PlaySuccess()
    {
        Play(_success);
    }

    public void PlayFail()
    {
        Play(_fail);
    }

    public void PlayWin()
    {
        Play(_win);
    }

    public void PlayLoose()
    {
        Play(_loose);
    }

    private void Play(AudioClip clip)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.PlayOneShot(audioSource.clip);

        Timer.Register(clip.length, () => Destroy(audioSource));
    }
}