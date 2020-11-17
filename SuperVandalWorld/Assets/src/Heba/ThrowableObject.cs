using UnityEngine;
class ThrowableObject : MonoBehaviour
{
    // Property that indicates the index position of this throwable object in our object pool
    public int PoolIndex { get; set; }
    //public Enemy Enemy { get;set; }

    // called by unity when object goes outside the camera view
    void OnBecameInvisible()
    {
        //if(Enemy != null)
        {
            print("released: " + PoolIndex);
            //Enemy.ReleasePoolingObj(PoolIndex);
            // Once object is not visible we signal to the object pool that the object
            // is available to be reused again and set it to invisible in unity
            ThrowableObjPool.Instance.ReleasePoolingObj(PoolIndex);
            transform.parent.gameObject.SetActive(false);
        }
    }
}