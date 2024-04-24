using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLeafBehaviour : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        //myTransform = this.transform;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //面回転を実行する関数
    public void Rotate(Vector3 rotateAxis, float degree, List<GameObject> cubeList)
    {
        //cubeList内の全キューブに対して回転を実行
        foreach(GameObject cube in cubeList)
        {
            cube.transform.RotateAround(Vector3.zero, rotateAxis, degree);
        }
    }

    public void Fix(List<GameObject> cubeList)
    {
        foreach(GameObject cube in cubeList)
        {
            float x = DiscreteCoordinate(cube.transform.localPosition.x);
            float y = DiscreteCoordinate(cube.transform.localPosition.y);
            float z = DiscreteCoordinate(cube.transform.localPosition.z);
            
            cube.transform.localPosition = new Vector3(x, y, z);

            float rotX = DiscreteAngle(cube.transform.localEulerAngles.x);
            float rotY = DiscreteAngle(cube.transform.localEulerAngles.y);
            float rotZ = DiscreteAngle(cube.transform.localEulerAngles.z);
            
            cube.transform.localEulerAngles = new Vector3(rotX, rotY, rotZ);
        }      
    }

    float DiscreteCoordinate(float value)
    {
        if(value < -0.5f)
        {
            return -1.0f;
        }
        else if(value > 0.5f)
        {
            return 1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    float DiscreteAngle(float value)
    {
        if(value < 135.0f && value >= 45.0f)
        {
            return 90.0f;
        }
        else if(value < 225.0f && value >= 135.0f)
        {
            return 180.0f;
        }
        else if(value < 315.0f && value >= 225.0f)
        {
            return 270.0f;
        }
        else
        {
            return 0.0f;
        }
    }
}
