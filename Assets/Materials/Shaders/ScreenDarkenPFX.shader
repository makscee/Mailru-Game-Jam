Shader "ScreenDarkenPFX" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Value ("Value", Range (0, 1)) = 1
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _Value;

			float4 frag(v2f_img i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);
				float4 result = float4(c.r * _Value, c.g * _Value, c.b * _Value, 1);
				return result;
			}
			ENDCG
		}
	}
}