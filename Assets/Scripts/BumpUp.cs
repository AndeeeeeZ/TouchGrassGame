using UnityEngine;

public class BumpUp : MonoBehaviour
{
    [SerializeField] private Vector2 targetScale;
    [SerializeField] private float scaleDownSpeed;  
    private Vector2 normalScale;

    private void Awake()
    {
        normalScale = transform.localScale; 
    }

    private void Update()
    {
        float diff = Vector2.Distance(normalScale, transform.localScale);
        if (diff < 0.001f)
            transform.localScale = normalScale; 
        else
            transform.localScale = Vector2.Lerp(transform.localScale, normalScale, Time.deltaTime * scaleDownSpeed); 
    }

    public void ScaleUp()
    {
        transform.localScale = targetScale; 
    }
}
