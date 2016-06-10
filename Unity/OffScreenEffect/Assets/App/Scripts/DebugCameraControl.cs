using UnityEngine;

public class DebugCameraControl : MonoBehaviour
{
#if UNITY_EDITOR
    private Vector3 oldPos;

    void Update()
    {
        //if (Input.GetMouseButton(1))
        {
            // 右クリック中にWASDでカメラ前後左右移動
            transform.position += transform.forward * Input.GetAxis("Vertical") / 30f;
            transform.position += transform.right * Input.GetAxis("Horizontal") / 30f;

            // 右クリック中にQEでカメラ上下移動
            float uppos = 0.0f;
            if (Input.GetKey(KeyCode.Q)) uppos = -0.01f;
            if (Input.GetKey(KeyCode.E)) uppos = 0.01f;
            transform.position += transform.up * uppos;

            // 右ドラッグでカメラ向き回転
            var diff = Input.mousePosition - oldPos;
            if (Mathf.Abs(diff.x) < 10f && Mathf.Abs(diff.y) < 10f)
            {
                transform.RotateAround(Vector3.up, diff.x / 100f);
                transform.RotateAround(transform.right, -diff.y / 100f);
            }
            oldPos = Input.mousePosition;
        }

        // スクロールでカメラ前後移動
        transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel");
    }
#endif

}
