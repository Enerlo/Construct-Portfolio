using UnityEngine;

public class GivenPlatform : MonoBehaviour
{
    public GameObject[] VariableObject;
    private bool _isObject = false;
    public GameObject Shoose;

    public void Awake()
    {
        IsObject();
    }

    public void VarObject()
    {
        if(Shoose != null)
        {
            if(_isObject != true)
                _isObject = true;

            Shoose.transform.Rotate(0, 2.0f, 0);
        }
        else
        {
            if (_isObject != false)
            {
                _isObject = false;
                Invoke("IsObject", 5);
            };
        }
    }

    void IsObject()
    {
        var obj = Random.Range(0, VariableObject.Length - 1);
        Shoose = Instantiate(VariableObject[obj], new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
    }

}
