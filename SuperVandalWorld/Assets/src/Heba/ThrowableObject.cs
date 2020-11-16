using UnityEngine;
class ThrowableObject : MonoBehaviour
{
    public int PoolIndex { get; set; }
    public Enemy Enemy { get;set; }

    void OnBecameInvisible()
    {
        if(Enemy != null)
        {
            print("released: " + PoolIndex);
            Enemy.ReleasePoolingObj(PoolIndex);
            transform.parent.gameObject.SetActive(false);
        }
    }
}