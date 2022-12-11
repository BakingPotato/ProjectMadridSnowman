using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlphaBuilding : MonoBehaviour
{
	bool _halfAlpha = false;
	Material[] _materials;
	float _alphaTime = 0.3f;

	public bool HalfAlpha { get => _halfAlpha; set => _halfAlpha = value; }

	private void Start()
	{
		_materials = GetComponent<Renderer>().materials;
	}

	void SetMatValues(Material mat)
	{
		mat.SetFloat("_Mode", 3);
		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		mat.SetInt("_ZWrite", 0);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.EnableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = 3000;
	}

	void ResetMatValues(Material mat)
	{
		mat.SetFloat("_Mode", 0);
		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		mat.SetInt("_ZWrite", 1);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.DisableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = -1;
	}
	public void MakeHalfAlpha()
	{
		if (HalfAlpha)
		{
			CancelInvoke("ResetAlpha");
			Invoke("ResetAlpha", _alphaTime);
			return;
		}

		HalfAlpha = true;

		foreach (Material mat in _materials)
		{
			SetMatValues(mat);

			Color alphaColor = mat.color;

			//mat.shader = Shader.Find("Standard");
			alphaColor.a = 0.35f;
			mat.color = alphaColor;
		}

		foreach (Transform child in transform)
		{
			Renderer r = child.GetComponent<Renderer>();
			if (r)
			{
				SetMatValues(r.material);

				Color alphaColor = r.material.color;

				//r.material.shader = Shader.Find("Standard");
				alphaColor.a = 0.35f;
				r.material.color = alphaColor;
			}
		}

		Invoke("ResetAlpha", _alphaTime);
	}

	void ResetAlpha()
	{
		HalfAlpha = false;

		foreach (Material mat in _materials)
		{
			ResetMatValues(mat);

			Color alphaColor = mat.color;

			//En caso de ser material nieve, le devolvemos su shader original
			//if (mat.name.Contains("Snow"))
			//	mat.shader = Shader.Find("BruteForce/Standard/SnowIceNoTessellation");
			alphaColor.a = 1f;
			mat.color = alphaColor;
		}

		foreach (Transform child in transform)
		{
			Renderer r = child.GetComponent<Renderer>();
			if (r)
			{
				ResetMatValues(r.material);

				Color alphaColor = r.material.color;
				//if (r.material.name.Contains("Snow"))
				//	r.material.shader = Shader.Find("BruteForce/Standard/SnowIceNoTessellation");
				alphaColor.a = 1f;
				r.material.color = alphaColor;
			}
		}
	}
}
