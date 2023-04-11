using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{
    public static Dashboard Instance;

    public GameObject Overview;
    public GameObject Settlement;

    public TextMeshProUGUI TxtScore;
    public TextMeshProUGUI TxtTimer;
    public TextMeshProUGUI TxtTotalScore;

    public AudioClip ScoreAudioClip;
    public AudioSource ScoreAudioSource;

    private int _score;
    private int _timer = 60;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;

            TxtScore.text = $"Score: {value}";

            ScoreAudioSource.PlayOneShot(ScoreAudioClip);
        }
    }

    public int Timer
    {
        get => _timer;
        set
        {
            _timer = value;

            TxtTimer.text = $"Time {value} s";
        }
    }

    public bool IsGameOver => this.Timer <= 0;

    private void Start()
    {
        Instance = this;

        Overview.SetActive(true);
        Settlement.SetActive(false);

        this.InvokeRepeating(nameof(this.SetTime), 1, 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void SetTime()
    {
        this.Timer--;

        if (this.Timer <= 0)
        {
            this.CancelInvoke(nameof(this.SetTime));

            TxtTotalScore.text = TxtScore.text;

            Overview.SetActive(false);
            Settlement.SetActive(true);
        }
    }
}
