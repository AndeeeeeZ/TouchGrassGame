using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hitCountText, missCountText, totalCountText; 
    private int hit, miss, totalScore; 
    

    private void Start()
    {
        hit = 0; 
        miss = 0; 
        // totalScore = 0; 
        UpdateScores(); 
    }

    public void OnPlayerMiss()
    {
        miss++; 
        UpdateText(missCountText, miss); 
    }

    public void OnPlayerHit()
    {
        hit++; 
        UpdateText(hitCountText, hit); 
    }

    private void UpdateScores()
    {
        UpdateText(hitCountText, hit);
        UpdateText(missCountText, miss);  
    }

    private void UpdateText(TextMeshProUGUI text, int score)
    {
        // Score length 5
        text.text = score.ToString("D5"); 
    }
}
