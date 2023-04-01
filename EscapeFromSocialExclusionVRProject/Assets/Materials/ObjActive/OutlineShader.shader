Shader "Custom/OutlineShader"
{
    Properties {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _OutlineColor ("Outline Color", Color) = (1,1,0,1) // yellow outline
        _Outline ("Outline width", Range (.001, 0.1)) = .02 // medium size outline
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
    }
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float4 color : COLOR;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float4 color : COLOR;
		UNITY_FOG_COORDS(1)
	};
	
	uniform float _Outline;
	uniform float4 _OutlineColor;
	
	appdata previous_vertex;

	v2f vert(appdata v) {
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);

    float3 norm = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
    float2 offset = TransformViewToProjection(norm.xy);

    #ifdef UNITY_Z_0_FAR_FROM_CLIPSPACE //to handle recent standard asset package on older version of unity (before 5.5)
        o.pos.xy += offset * UNITY_Z_0_FAR_FROM_CLIPSPACE(o.pos.z) * _Outline;
    #else
        o.pos.xy += offset * o.pos.z * _Outline;
    #endif

    o.color = _OutlineColor;
    UNITY_TRANSFER_FOG(o, o.pos);
    return o;
}

	ENDCG

	SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "Toon/Basic/BASE"
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, i.color);
				return i.color;
			}
			ENDCG
		}
	}
}
