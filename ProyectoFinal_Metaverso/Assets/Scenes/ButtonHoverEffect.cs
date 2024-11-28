using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;
    public float scaleSpeed = 0.1f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(transform.localScale, originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(transform.localScale, originalScale));
    }

    private IEnumerator ScaleButton(Vector3 from, Vector3 to)
    {
        float timeElapsed = 0f;
        while (timeElapsed < scaleSpeed)
        {
            transform.localScale = Vector3.Lerp(from, to, timeElapsed / scaleSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = to;
    }
}
