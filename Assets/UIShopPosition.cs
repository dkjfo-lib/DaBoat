using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopPosition : MonoBehaviour
{
    public Bool isInPort;
    [Space]
    public float speed = 1;
    public Vector2 positionX = new Vector2(-180, 0);

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var targetPosX = isInPort.value ? positionX[0] : positionX[1];
        var posX = Mathf.Lerp(rectTransform.anchoredPosition.x, targetPosX, speed * Time.deltaTime);

        rectTransform.anchoredPosition = new Vector3(posX, 0, 0);
    }
}
