using Unity.XR.CoreUtils;
using UnityEngine;

public class toyBlockPuzzle : MonoBehaviour
{
    [SerializeField]
    private Camera cm;
    public GameObject toyBlockGo;
    public float threshold = 0.83f;

    private GCondition conTrigger;
    private bool isActive = false;
    private void Start()
    {
        conTrigger = GetComponent<GCondition>();
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("����Ʈ ����");
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("����Ʈ Ż��");
            isActive = false;
        }
    }

    private bool isChacksi()
    {
        float dotProduct = Vector3.Dot(cm.transform.forward, (toyBlockGo.transform.position - cm.transform.position).normalized);

        if (dotProduct >= threshold)
        {
            //Debug.Log("On dotProduct : "+ dotProduct);
            return true;
        }
        else
        {
            //Debug.Log("OFF dotProduct : "+ dotProduct);
            return false;
        }
    }

    public void OnPhoto()
    {
        if(isActive && isChacksi())
        {
            Debug.Log("���� ����");
            conTrigger.OnSolved(true);
        }
        else
        {
            Debug.Log("���� �ȸ���");
        }
    }
}
