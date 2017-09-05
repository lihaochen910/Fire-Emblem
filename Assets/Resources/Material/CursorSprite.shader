Shader "Unlit/CursorSprite"
{
	Properties
	{
		_MainTex ("CursorSprite", 2D) = "white" {}
		_Color ("MixColor",Color) = (1,1,1,1)
	}
	SubShader
	{ 
		pass {
			CGPROGRAM
			#pargma vertex vert
			#pargma fragment frag
			#include "UnityCG.cginc"

			struct v2f {

			};
			void vert(in appdata_base IN,out v2f OUT) {

			}

			ENDCG
		}
		
	}
}
