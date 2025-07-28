using UnityEngine;

public class BookCheckPoint : MonoBehaviour
{
    public string targetTag = string.Empty;
    public bool isChecked = false;

    public delegate void OnCheckedDelegate();
    public OnCheckedDelegate onCheckedCallback = null;
        
    private RaycastHit hit;
    private float rayDistance = 0.5f;


    private void Update()
    {
        bool isHit = false;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, 1 << LayerMask.NameToLayer("Interaction")))
        {
            isHit = true;

            if (hit.transform.gameObject.tag == targetTag)
            {
                Debug.Log("책 "+ targetTag + "들어감");
                isChecked = true;

                onCheckedCallback?.Invoke();
                
            }
            else
            {
                isChecked = false;
            }


        }

        if (!isHit)
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
        else
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

    }

   

  
}
