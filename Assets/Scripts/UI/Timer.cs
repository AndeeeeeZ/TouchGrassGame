using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Color startColor, endColor;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float totalTimeInSeconds;
    [SerializeField] private VoidEvent OnGameEnds; 
    private float currTime;
    private bool running; 

    private void Start()
    {
        running = true; 
        currTime = totalTimeInSeconds;
        timerText.color = startColor;
    }

    private void Update()
    {
        if (!running) return; 
        currTime -= Time.deltaTime;
        if (currTime < 0f)
        {
            currTime = 0f;
            OnGameEnds.Raise();
            running = false;  
        }

        UpdateText();
    }

    private void UpdateText()
    {
        int seconds = Mathf.FloorToInt(currTime);
        int milliseconds = Mathf.FloorToInt((currTime - seconds) * 100f);

        timerText.text = $"{seconds:00}:{milliseconds:00}";

        float ratio = (totalTimeInSeconds - currTime) / totalTimeInSeconds; 
        timerText.color = Color.Lerp(startColor, endColor, ratio); 
    }
}
