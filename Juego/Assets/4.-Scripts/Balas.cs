using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Balas :MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Velocidad;

   public virtual void Dir(float vel, Vector3 direccion)
    {

        Velocidad = direccion.normalized * vel;
        //Velocidad = new Vector3(direccion.x + vel, 0, direccion.z + vel);
        
    }
    public virtual void Mov()
    {
        Position = Velocidad * Time.deltaTime;
        transform.position += Position;
    }
}
