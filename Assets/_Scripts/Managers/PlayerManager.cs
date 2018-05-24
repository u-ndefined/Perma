using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ISingleton<PlayerManager> {

    public Anim[] anims;

	protected PlayerManager() { }

    public Anim GetAnim(GameData.Animation animation)
    {
        for (int i = 0; i < anims.Length; i++)
        {

            if(anims[i].animation == animation)
            {
                return anims[i];
            }
        }
        return anims[0];
    }
}
