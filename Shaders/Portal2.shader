Shader "Custom/Portal2"
{
    Subshader{

        Zwrite Off
        ColorMask 0
    

        Stencil{
            Ref 2
            Comp always
            Pass replace
        }

        Pass{

        }
    }
}
