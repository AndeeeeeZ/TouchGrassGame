using UnityEngine.UI; 
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private float duration; 
    [SerializeField] private Image image;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    public void Start()
    {
        image.enabled = false; 
    }
    public void FlashDamageEffect()
    {
        animator.Play("ShowEffect"); 
    }


}
