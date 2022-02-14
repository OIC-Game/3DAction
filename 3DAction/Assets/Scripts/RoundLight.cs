using UnityEngine;

public class RoundLight : MonoBehaviour
{
    private void Update()
    {
        // Y軸に対して、1秒間に-4度回転させる
        transform.Rotate(new Vector3(0, -4) * Time.deltaTime);
    }
}