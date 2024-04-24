using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadBehaviour : MonoBehaviour
{
    [SerializeField] CubeRootBehaviour rootCubeBehaviour;//CubeRootのスクリプト
    [SerializeField] CubeLeafBehaviour leafCubeBehaviour;//Quadの所属するCubeのスクリプト
    [SerializeField] GameObject leafCubeObject;//自身が所属するcubeのオブジェクト
    [SerializeField] GameObject rootCubeObject;//CubeRootのオブジェクト
    private Transform myTransform;//自身のtransform
    private Transform leafCubeTransform;//自身が所属するcubeのtransform
    private Transform rootCubeTransform;//CubeRootのtransform

    private Vector3 rotateAxis = new Vector3();//面回転の軸を表すベクトル
    private Vector3 orthogonalToAxis = new Vector3();//回転軸に直交するベクトル
    private bool axisIdentified;//回転軸が決定されたかを表す
    private bool axisIsRight;//回転軸がQuad座標系でright(x)軸であるかを表す

    public float degreeSum;//フレームごとに行った回転量の和を表す
    public List<GameObject> cubeList = new List<GameObject>();//面回転させるキューブを格納する配列
    // Start is called before the first frame update
    void Start()
    {
        //各オブジェクトのtransform取得
        myTransform = this.transform;
        leafCubeTransform = leafCubeObject.transform;
        rootCubeTransform = rootCubeObject.transform;
        //axisIdentifiedはfalseで初期化
        axisIdentified = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDragBegin()
    {
        //面回転時はCubeRootの回転を禁止する
        rootCubeBehaviour.rootRotating = false;
        //回転の和を0に初期化
        degreeSum = 0.0f;
        //面回転させるキューブリストを初期化
        cubeList.Clear();
    }

    //Quadのドラッグ時に面回転を実行
    public void OnDrag()
    {
        //Quad座標系のright(x)軸、up(y)軸取得
        Vector3 right = myTransform.right;
        Vector3 up = myTransform.up;

        //CubeRootの座標軸を取得
        Vector3 roll = rootCubeTransform.forward;
        Vector3 pitch = rootCubeTransform.right;
        Vector3 yaw = rootCubeTransform.up;
        
        //マウス移動量を取得
        Vector3 delta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        //面回転の回転軸および回転対象のCubeリスト取得
        while(axisIdentified == false)
        {
            //rightおよびupとdeltaとの内積を取得
            float rightDotDelta = Mathf.Abs(Vector3.Dot(right,delta));
            float upDotDelta = Mathf.Abs(Vector3.Dot(up,delta));

            //deltaとの内積が小さい方を回転軸とする
            if(rightDotDelta > upDotDelta)
            {
                rotateAxis = up;
                axisIsRight = false;
                axisIdentified = true;
            }
            else if(upDotDelta > rightDotDelta)
            {
                rotateAxis = right;
                axisIsRight = true;
                axisIdentified = true;
            }

            // Debug.Log("rotateAxis:" + rotateAxis);
            // Debug.Log("roll:" + rootCubeTransform.forward);
            // Debug.Log("pitch:" + rootCubeTransform.right);
            // Debug.Log("yaw:" + rootCubeTransform.up);

            //回転軸とCubeRootの各軸との一致性判定のために内積の絶対値を計算
            float axisDotRoll = Mathf.Abs(Vector3.Dot(rotateAxis, roll));
            float axisDotPitch = Mathf.Abs(Vector3.Dot(rotateAxis, pitch));
            float axisDotYaw = Mathf.Abs(Vector3.Dot(rotateAxis, yaw));

            //内積の絶対値が最も大きい軸を一致と判定（==演算子だと判定が不安定）
            if((axisDotRoll > axisDotPitch) && 
                (axisDotRoll > axisDotYaw))
            {
                //回転がroll軸回転ならば回転させるキューブはz座標が同一のもの
                Debug.Log("roll");
                //leafCubeのz座標（ローカル）に応じてCubeリストを取得
                if(Mathf.Abs(leafCubeTransform.localPosition.z + 1.00f) < 0.02f)
                {
                    //座標値が-1.01f（誤差含む）ならばcubeList_Zを取得
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeList_Z);
                }
                else if(Mathf.Abs(leafCubeTransform.localPosition.z) < 0.02f)
                {
                    //座標値が0.00f（誤差含む）ならばcubeListZを取得
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeListZ);
                }
                else if(Mathf.Abs(leafCubeTransform.localPosition.z - 1.00f) < 0.02f)
                {
                    //座標値が1.01f（誤差含む）ならばcubeList_Zを取得
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeListZ_);
                }
            }
            else if((axisDotPitch > axisDotRoll) && 
                (axisDotPitch > axisDotYaw))
            {
                Debug.Log("pitch");
                if(Mathf.Abs(leafCubeTransform.localPosition.x + 1.00f) < 0.02f)
                {
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeList_X);
                }
                else if(Mathf.Abs(leafCubeTransform.localPosition.x) < 0.02f)
                {
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeListX);
                }
                else if(Mathf.Abs(leafCubeTransform.localPosition.x - 1.00f) < 0.02f)
                {
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeListX_);
                }
            }
            else if((axisDotYaw > axisDotRoll) && 
                (axisDotYaw > axisDotPitch))
            {
                Debug.Log("yaw");
                if(Mathf.Abs(leafCubeTransform.localPosition.y + 1.00f) < 0.02f)
                {
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeList_Y);
                }
                else if(Mathf.Abs(leafCubeTransform.localPosition.y) < 0.02f)
                {
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeListY);
                }
                else if(Mathf.Abs(leafCubeTransform.localPosition.y - 1.00f) < 0.02f)
                {
                    cubeList = new List<GameObject>(rootCubeBehaviour.cubeListY_);
                }
            }
        }

        //回転軸に直交する軸を決定
        if(axisIsRight == true)
        {
            orthogonalToAxis = up;
        }
        else
        {
            //軸がupのときは直交軸を-rightにする
            orthogonalToAxis = -right;
        }
        //直交軸とマウス移動量の内積に比例して面回転の回転量を算出
        float degree = Vector3.Dot(orthogonalToAxis,delta) * 30;
        //degreeSumに回転量を積算
        degreeSum += degree;
        //面回転を実行
        leafCubeBehaviour.Rotate(rotateAxis, degree, cubeList);
    }

    public void OnDragEnd()
    {
        //回転量の総和が-360以上360以下になるように正規化
        float degreeSumNormalized = degreeSum % 360;
        //degreeSumNormalizedの値に応じて中途半端な位置にあるキューブリストを整頓する
        if(((degreeSumNormalized < 45) && (degreeSumNormalized >= -45)) ||
            ((degreeSumNormalized < -315) || (degreeSumNormalized >= 315)))
        {
            leafCubeBehaviour.Rotate(rotateAxis, -degreeSumNormalized, cubeList);
        }
        else if(((degreeSumNormalized < 135) && (degreeSumNormalized >= 45)) ||
            ((degreeSumNormalized < -225) && (degreeSumNormalized >= -315)))
        {
            //回転量が45度以上135度未満または-315度以上-225度未満ならば正味90度回転するのと同じとする
            leafCubeBehaviour.Rotate(rotateAxis, 90 - degreeSumNormalized, cubeList);
        }
        else if(((degreeSumNormalized < 225) && (degreeSumNormalized >= 135)) ||
            ((degreeSumNormalized < -135) && (degreeSumNormalized >= -225)))
        {
            leafCubeBehaviour.Rotate(rotateAxis, 180 - degreeSumNormalized, cubeList);
        }
        else if(((degreeSumNormalized < 315) && (degreeSumNormalized >= 225)) ||
            ((degreeSumNormalized < -45) && (degreeSumNormalized >= -135)))
        {
            leafCubeBehaviour.Rotate(rotateAxis, 270 - degreeSumNormalized, cubeList);
        }

        leafCubeBehaviour.Fix(cubeList);
        //回転後に全キューブのリストを更新
        rootCubeBehaviour.UpdateCubelist();
        //CubeRootの回転を許可
        rootCubeBehaviour.rootRotating = true;
        //回転軸の特定を初期値に戻す
        axisIdentified = false;
    }
}
