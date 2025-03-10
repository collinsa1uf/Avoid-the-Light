using UnityEngine;

public class TitleScreenAnimation : MonoBehaviour
{
    public RectTransform characterImage;  
    public Vector2 startPos;  
    public Vector2 endPos;  
    public float slideSpeed = 2f;

    [SerializeField] private float t = 0f;  
    [SerializeField] private bool hasSlidIn = false;

    void Start()
    {
        ResetAnimation();   
        characterImage.anchoredPosition = startPos;
    }

    void Update()
    {
        RunAnimation();
    }

    void ResetAnimation()
    {
        t = 0f;
        hasSlidIn = false;
        characterImage.anchoredPosition = startPos;

        RunAnimation();
    }

    void RunAnimation()
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