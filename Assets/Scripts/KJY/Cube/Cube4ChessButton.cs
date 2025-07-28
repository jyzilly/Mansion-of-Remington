using UnityEngine;

public class Cube4ChessButton : MonoBehaviour
{
    public int TheChessButton = 0;

    public bool TheCube4Result = false;

    private void Update()
    {
        TheChessButtonOnClick();
    }

    //�׽�Ʈ��, ������ �����뵵��
    //���콺 Ŭ���� �����ϰ�, Ŭ���� ������Ʈ�� ü�� ��ư���� Ȯ��
    //VR ȯ�濡���� �� ������ ���콺�� �ƴ� VR ��Ʈ�ѷ��� �����Ϳ��� Ray�� �߻��ϴ� ������� �����ؾ� ��

    private void TheChessButtonOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag == "ChessButton")
                {
                    TheChessButton = 1;
                }
            }
        }
    }


    //ü���ǿ� ��ȣ�ۿ�

}
