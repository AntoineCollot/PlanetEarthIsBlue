Shader "PostProcess/FadeToBlackDrawColor" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Cutoff("Cutoff", Range(0, 1)) = 0
	_DrawMap("Draw Map",2D)="white"{}
	_PaintColor ("Paint Color", Color) = (0,0,0,0)
	}
		SubShader{
		Pass{
		CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag

#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform float _Cutoff;
		sampler2D _DrawMap;
		float4 _PaintColor;

	float4 frag(v2f_img i) : COLOR{
		float4 c = tex2D(_MainTex, i.uv);
		float4 d = tex2D(_DrawMap,i.uv);
		int paint = step(d.r,_Cutoff * 1.0001);
		return c * (1-paint) + _PaintColor * paint;
	}
		ENDCG
	}
	}
}