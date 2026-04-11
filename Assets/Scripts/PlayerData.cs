using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public int totalHit, totalMiss, score, longestCombo;

    public void Reset()
    {
        totalHit = totalMiss = score = longestCombo = 0; 
    } 
}
