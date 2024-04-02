using UnityEngine;

public class MovePos : MonoBehaviour
{
    [SerializeField] GameObject endPoint;

    private void Awake()
    {
        GetComponent<Transform>();
    }

    void Update()
    {
        this.transform.position = Vector2.Lerp(this.transform.position, endPoint.transform.position, .8f * Time.deltaTime);
        this.transform.localScale = Vector2.Lerp(this.transform.localScale, endPoint.transform.localScale, .5f * Time.deltaTime);
    }
}
