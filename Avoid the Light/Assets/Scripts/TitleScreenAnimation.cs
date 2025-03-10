using UnityEngine;

public class TitleScreenAnimation : MonoBehaviour
{
    public RectTransform characterImage;  
    public Vector2 startPos;  
    public Vector2 endPos;  
    public float slideSpeed = 2f;

    private float t = 0f;  
    private bool hasSlidIn = false;

    void Start()
    {
        
        characterImage.anchoredPosition = startPos;
    }

    void Update()
    {
        if (!hasSlidIn)  // Run this only once
        {
            t += Time.deltaTime * slideSpeed;
            float easedT = Mathf.SmoothStep(0, 1, t);

            characterImage.anchoredPosition = Vector2.Lerp(startPos, endPos, easedT);

            if (t >= 1)
            {
                hasSlidIn = true;
            }
        }
    }
}
