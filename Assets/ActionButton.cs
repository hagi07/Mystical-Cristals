using UnityEngine;
using System.Collections;

public class ActionButton : MonoBehaviour {
    public bool teletransportacion;
    public bool rotacion;
    public bool tiempo;
    public bool unMovimiento;
    public bool aPunto;
    public bool destruye;

    //Texturas
    public Texture2D[] textura = new Texture2D[10];

	
	void Update () {
        if (teletransportacion)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.teletransportacion];
        }
        if (rotacion)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.rotacion];
        }
        if (tiempo)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.tiempo];
        }
        if (unMovimiento)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.unMovimiento];
        }
        
        if (aPunto)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.aPunto];
        }
        if (destruye)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.destruye];
        }
	}
}
