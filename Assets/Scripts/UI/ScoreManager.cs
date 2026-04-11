using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, comboText, comboTextText, comboMultiText; 
    [SerializeField] private Vector2[] levels; // (requirement, multi)
    [SerializeField] private Color[] levelColors; 
    [SerializeField] private VoidEvent OnLevelUp; 
    
    private int hit, miss, combo, maxComboRecord; 
    private float score, currMulti; 

    private void Start()
    {
        score = 0f; 
        currMulti = 0f; 
        hit = 0; 
        miss = 0; 
        combo = 0; 
        maxComboRecord = 0; 
        UpdateScores(); 
    }

    public void OnPlayerMiss()
    {
        miss++; 
        OnComboBroke();  
        UpdateScores(); 
    }

    public void OnPlayerHit()
    {
        hit++; 
        combo++; 
        float newMulti = GetCurrMulti(); 
        if (newMulti > currMulti)
        {
            currMulti = newMulti;
            OnLevelUp.Raise();  
        }
        score += 1f * currMulti; // 1f is score value
        UpdateScores();
    }

    private void OnComboBroke()
    {
        CheckComboRecord();
        combo = 0;
        currMulti = GetCurrMulti(); 
    }

    private void UpdateScores()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString("D5"); 
        comboText.text = combo.ToString("D3"); 
        comboMultiText.text = "x" + GetCurrMulti().ToString("F1");

        Color c = levelColors[GetCurrLevel()]; 
        scoreText.color = c;
        comboText.color = c; 
        comboTextText.color = c; 
        comboMultiText.color = c; 
    }

    private float GetCurrMulti()
    {
        for (int i = levels.Length - 1; i >= 0; i--)
        {
            if (combo >= levels[i].x)
                return levels[i].y; 
        }
        return 1f; 
    }

    private int GetCurrLevel()
    {
        for (int i = levels.Length - 1; i >= 0; i--)
        {
            if (combo >= levels[i].x)
                return i; 
        }
        return 0; 
    }

    private void CheckComboRecord()
    {
        maxComboRecord = Mathf.Max(maxComboRecord, combo); 
    }
}
