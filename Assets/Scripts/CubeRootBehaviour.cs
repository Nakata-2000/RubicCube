using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRootBehaviour : MonoBehaviour
{
    private Transform myTransform;
    public bool rootRotating;
    [SerializeField] List<GameObject> allCubeList;//ルービックキューブを構成する全キューブを格納する配列
    public List<GameObject> cubeList_Z = new List<GameObject>();//z座標（ローカル）が-1.00のキューブを格納する配列
    public List<GameObject> cubeListZ = new List<GameObject>();//z座標（ローカル）が0のキューブを格納する配列
    public List<GameObject> cubeListZ_ = new List<GameObject>();//z座標（ローカル）が1.00のキューブを格納する配列
    public List<GameObject> cubeList_X = new List<GameObject>();//x座標（ローカル）が-1.00のキューブを格納する配列
    public List<GameObject> cubeListX = new List<GameObject>();//x座標（ローカル）が0のキューブを格納する配列
    public List<GameObject> cubeListX_ = new List<GameObject>();//x座標（ローカル）が1.00のキューブを格納する配列
    public List<GameObject> cubeList_Y = new List<GameObject>();//y座標（ローカル）が-1.00のキューブを格納する配列
    public List<GameObject> cubeListY = new List<GameObject>();//y座標（ローカル）が0のキューブを格納する配列
    public List<GameObject> cubeListY_ = new List<GameObject>();//y座標（ローカル）が1.00のキューブを格納する配列
    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform;//CubeRootのtransform取得
        rootRotating = true;//rootRotatingはtrueで初期化
        UpdateCubelist();//全キューブを各キューブリストへ振り分ける
    }

    // Update is called once per frame
    void Update()
    {
        //CubeRootの回転が許可されている間マウスドラッグで回転
        if(rootRotating == true)
        {
            if(Input.GetMouseButton(0))
            {
                transform.Rotate(Input.GetAxis("Mouse Y") * 10, -Input.GetAxis("Mouse X") * 10, 0, Space.World);
            }
        }
    }

    public void UpdateCubelist()//各キューブリストに該当キューブを格納
    {
        //はじめにすべて初期化
        cubeList_Z.Clear();
        cubeListZ.Clear();
        cubeListZ_.Clear();
        cubeList_X.Clear();
        cubeListX.Clear();
        cubeListX_.Clear();
        cubeList_Y.Clear();
        cubeListY.Clear();
        cubeListY_.Clear();
        
        foreach(GameObject cube in allCubeList)
        {
            if(Mathf.Abs(cube.transform.localPosition.z + 1.00f) < 0.02f)
            {
                //キューブのz座標が-1.00（誤差含む）ならばcubeList_Zにキューブを追加
                cubeList_Z.Add(cube);
            }
            else if(Mathf.Abs(cube.transform.localPosition.z) < 0.02f)
            {
                //キューブのz座標が0（誤差含む）ならばcubeListZにキューブを追加
                cubeListZ.Add(cube);
            }
            else if(Mathf.Abs(cube.transform.localPosition.z - 1.00f) < 0.02f)
            {
                //キューブのz座標が1.00（誤差含む）ならばcubeListZ_にキューブを追加
                cubeListZ_.Add(cube);
            }

            if(Mathf.Abs(cube.transform.localPosition.x + 1.00f) < 0.02f)
            {
                cubeList_X.Add(cube);
            }
            else if(Mathf.Abs(cube.transform.localPosition.x) < 0.02f)
            {
                cubeListX.Add(cube);
            }
            else if(Mathf.Abs(cube.transform.localPosition.x - 1.00f) < 0.02f)
            {
                cubeListX_.Add(cube);
            }

            if(Mathf.Abs(cube.transform.localPosition.y + 1.00f) < 0.02f)
            {
                cubeList_Y.Add(cube);
            }
            else if(Mathf.Abs(cube.transform.localPosition.y) < 0.02f)
            {
                cubeListY.Add(cube);
            }
            else if(Mathf.Abs(cube.transform.localPosition.y - 1.00f) < 0.02f)
            {
                cubeListY_.Add(cube);
            }
        }
    }
}
