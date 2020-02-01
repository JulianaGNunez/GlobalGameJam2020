using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShaderonCamera : MonoBehaviour {

	public Material effectMaterial;

	private void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, effectMaterial);	
	}
}