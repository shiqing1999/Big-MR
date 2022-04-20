Shader "Custom/Portal4"
{
    Subshader{

        Zwrite Off
        ColorMask 0

        Stencil{
            Ref 4
            Comp always
            Pass replace
        }

        Pass{

        }
    }
}