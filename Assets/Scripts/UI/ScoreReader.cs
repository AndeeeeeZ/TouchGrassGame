using TMPro;
using UnityEngine;


public class ScoreReader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, statText; 
    [SerializeField] private PlayerData data;

    private void Start()
    {
        scoreText.text = data.score.ToString("D4"); 
        statText.text = data.totalHit.ToString("D3") + "\n" + data.totalMiss.ToString("D3") + "\n" + data.longestCombo.ToString("D3"); 
    }

}
