using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private GameObject targetGameObject;

    [SerializeField]
    private string targetMessage;

    public Color HighlightColor = Color.cyan;

    public void OnMouseOver()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = HighlightColor;
        }
    }

    public void OnMouseExit()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
    
    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }
    
    public void OnMouseUp()
    {
        transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        if ( targetGameObject != null)
        {
            targetGameObject.SendMessage(targetMessage);
        }
    }
}