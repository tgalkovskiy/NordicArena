
using System.Collections;
using UnityEngine;

public interface IBlockBuilder 
{
    public void BuilderBlocks(GameObject gameObject, Transform pos);

    public void BuilderEnemy();

    public void BuilderTraps();

    public void BuilderLoot();
}
