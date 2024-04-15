using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[System.Serializable]
public class ItemObject
{
    public ItemType itemType;
    public int ammount;
}
public class Box : MonoBehaviour
{
    [SerializeField] private List<ItemObject> listItem;
    [SerializeField] private float pushForce;
    public Animator Anim { get; private set; }
    private int pushDirection;
    private float randForce;
    private void Start()
    {
        Anim = GetComponent<Animator>();
        pushDirection = 1;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player)
        {
            IPushable pushable = col.GetComponent<IPushable>();
            pushable?.AddForce(new Vector2(0, col.transform.position.y < transform.position.y ? 0f : 30f));
            Anim.SetTrigger("hit");
        }
    }
    private void DropItem()
    {
        if (listItem.Count <= 0)
            return;
        ItemObject randomItem = listItem[Random.Range(0, listItem.Count)];
        GameObject item = PoolManager.Instance.GetObject((ObjectType)randomItem.itemType);
        if (item != null)
        {
            item.SetActive(true);
            item.transform.position = transform.position;
        }
        randForce = Random.Range(pushForce, pushForce + 10f);
        IPushable pushable = item.GetComponent<IPushable>();
        pushable?.AddForce(new Vector2(randForce * pushDirection, 0f));
        randomItem.ammount--;
        if (randomItem.ammount <= 0)
        {
            listItem.Remove(randomItem);
        }
    }
    public void TakeDamage()
    {
        DropItem();
        pushDirection *= -1;
        if (listItem.Count <= 0)
        {
            GameObject boxParts = PoolManager.Instance.GetObject(ObjectType.BoxParts);
            boxParts.transform.position = transform.position;
            boxParts.SetActive(true);
            Destroy(gameObject);
        }
    }
}
