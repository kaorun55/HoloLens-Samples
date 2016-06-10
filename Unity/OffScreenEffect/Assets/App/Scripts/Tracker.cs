// http://tsubakit1.hateblo.jp/entry/2016/04/26/233000
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [SerializeField]
    Image icon;

    private Rect rect = new Rect(0, 0, 1, 1);

    public Rect canvasRect;

    Vector2 indRange;

    void Start()
    {
        var x = icon.rectTransform.rect.width * 3;
        var y = icon.rectTransform.rect.height * 3;

        // UIがはみ出ないようにする
        canvasRect = new Rect(0, 0, Screen.width, Screen.height);
        canvasRect.Set(
            canvasRect.x + x,
            canvasRect.y + y,
            canvasRect.width - x * 2,
            canvasRect.height - y * 2
        );
    }

    void Update()
    {
        var v3Screen = Camera.main.WorldToViewportPoint(transform.position);
        var dir = Vector3.Normalize(v3Screen);
        dir.y *= -1f;

        var indPos = new Vector2(indRange.x * dir.x, indRange.y * dir.y);
        indPos = new Vector2((Screen.width / 2) + indPos.x, (Screen.height / 2) + indPos.y);

        Vector3 pdir = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(indPos.x, indPos.y, transform.position.z));
        pdir = Vector3.Normalize(pdir);

        var angle = Mathf.Atan2(pdir.x, pdir.y) * Mathf.Rad2Deg;

        var viewport = Camera.main.WorldToViewportPoint(transform.position);
        if (rect.Contains(viewport))
        {
            icon.enabled = false;
        }
        else
        {
            icon.enabled = true;

            // 画面内で対象を追跡
            viewport.x = Mathf.Clamp01(viewport.x);
            viewport.y = Mathf.Clamp01(viewport.y);
            icon.rectTransform.anchoredPosition = Rect.NormalizedToPoint(canvasRect, viewport);
            icon.transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}
